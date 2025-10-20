using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Scenes;
using TerimalQuest.System;

namespace TerimalQuest.Manager
{
    public class EnhancementManager
    {
        /*
        * EnhancementManager 스크립트
        * 
        * 장비를 강화 기능을 도와주는 Manager 클래스이다.
        * 
        * 
        */
        private Player player;
        private Inventory inventory;
        private UIManager uiManager;

        public List<Item> enhanceableItems { get; set; }

        // 강화는 10강까지 있으며 강화에 따라 소진 강화석 개수와 확률이 달라짐
        private int maxEnhancementLevel = 10;
        public int[] stoneRequiredPerLevel = { 0, 1, 1, 2, 2, 3, 4, 5, 6, 8, 12 };
        public float[] successRatePerLevel = { 1f, 0.9f, 0.8f, 0.7f, 0.6f, 0.5f, 0.3f, 0.2f, 0.1f, 0.05f, 0.03f };
        public float[] degradeRatePerLevel = { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };
        public float[] enhancementRatePerLevel = { 0, 3, 3, 5, 5, 7, 9, 11, 18, 28, 40 };

        // 강화 시 사용하는 변수
        private Random random;                  // 강화 확률에 사용할 랜덤 객체
        public Item prevItem { get; set; }      // 강화 이전 상태를 담은 아이템
        public Item enhanceItem { get; set; }   // 강화 할 대상 아이템
        private bool enhanceSuccess;            // 강화 성공 여부
        private int enhancementLevel;           // 강화 레벨
        private int enhancementStoneId;         // 강화석 Id
        private EnhancementStone enhancementStone { get; set; } // 강화석
        public bool isDegrade { get; set; }

        public EnhancementManager() 
        {
            player = GameManager.Instance.player;
            inventory = player.inventory;
            uiManager = UIManager.Instance;
            enhanceableItems = new List<Item>();

            // 변수 초기화
            random = new Random();
            maxEnhancementLevel = 10;
            enhancementStoneId = 4000;
            enhanceSuccess = false;

            // 인벤토리에서 강화석 찾기
            enhancementStone = (EnhancementStone)inventory.FindItemById(enhancementStoneId);
        }

        // 강화 할 수 있는 아이템 리스트에 저장
        public void SetEnhancealbeItemList(params ItemType[] filterTypes)
        {
            enhanceableItems.Clear();

            List<Item> items = inventory.items;

            if (filterTypes == null || filterTypes.Length == 0) return;

            // 필터링된 아이템만 추가
            enhanceableItems.AddRange(items.Where(item => filterTypes.Contains(item.type)));
        }

        // 강화 가능한 아이템 리스트 보여주기 : 장비 아이템
        public void DisplayEnhancealbeItemList()
        {
            uiManager.DisplayItemInfoHeader(true);

            foreach (var item in enhanceableItems)
            {
                string idxTxt = $"{enhanceableItems.IndexOf(item) + 1} : ";
                item.DisplayInfo(idxTxt);
            }
        }

        // 강화석 재료가 충분한지 체크
        private bool HasEnoughEnhancementStones(int level)
        {
            // 강화석 null 값 체크
            if (enhancementStone == null || enhancementStone.count == 0)
            { 
                return false;
            }

            // 강화 할 때 사용할 강화석 개수가 충분하지 체크
            return (enhancementStone.count >= stoneRequiredPerLevel[level]);
        }

        // 강화석 개수 얻기
        public int GetPlayerEnhancementStoneCount()
        {
            return (enhancementStone == null) ? 0 : enhancementStone.count;
        }

        // 강화할 장비 선택
        public void ChoseEnhanceItem(int idx)
        {
            // 아이템 정보 가져오기
            enhanceItem = enhanceableItems[idx];

            if (enhanceItem == null) return;

            // 아이템 저장
            prevItem = enhanceItem.Clone(); 
        }

        // 강화 시도
        public bool TryEnhanceItem()
        {
            if(enhanceItem == null) return false;

            // 아이템 강화 단계 가져오기
            enhancementLevel = enhanceItem.GetLevel();

            // 장비 레벨 체크
            if (enhancementLevel >= maxEnhancementLevel)
            {
                uiManager.MessageNoMoreEnhancement();
                return false;
            }

            // 강화석 재료 체크
            if (!HasEnoughEnhancementStones(enhancementLevel))
            {
                uiManager.MessageNotEnoughEnhancementStone();
                return false;
            }

            return true;
        }

        // 강화 시작
        public void EnhanceItem()
        {
            // 강화석 소모
            int requiredStoneCount = stoneRequiredPerLevel[enhancementLevel+1]; // 다음 단계 필요 강화석
            enhancementStone.count -= requiredStoneCount;

            // 강화석 0이하로 내려갈시 개수 조정하고 인벤토리에서 삭제
            if (enhancementStone.count <= 0)
            {
                enhancementStone.count = 0;
                inventory.Remove(enhancementStone);
            }

            // 강화 확률 계산
            float successRate = successRatePerLevel[enhancementLevel+1];
            enhanceSuccess = random.NextDouble() < successRate;

            // 성공 시 강화 레벨 증가
            if(enhanceSuccess)
            {
                enhanceItem.Enhance(enhancementRatePerLevel[enhancementLevel + 1]);
            }
            else
            {
                // 하락 확률 계산
                float degardRate = degradeRatePerLevel[enhancementLevel];
                isDegrade = random.NextDouble() < degardRate;

                if(isDegrade)
                {
                    enhanceItem.Degrade(enhancementRatePerLevel[enhancementLevel]);
                }
            }
        }

        // 강화 결과
        public void EnhanceResult()
        {
            // 플레이어 스탯 업데이트
            player.UpdateStats();

            uiManager.DisplayEnhancementResultScripts(enhanceSuccess, isDegrade, prevItem, enhanceItem);
        }
    }
}
