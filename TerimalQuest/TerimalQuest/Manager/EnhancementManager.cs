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
    public enum EnhanceState
    {
        Before,         // 강화 전
        InProgress,     // 강화 중
        After           // 강화 후
    }

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

        private List<Item> enhanceableItems;

        private Action currentEnhancementAction;

        public EnhanceState currentState { get; private set; }

        // 강화는 10강까지 있으며 강화에 따라 소진 강화석 개수와 확률이 달라짐
        private int maxEnhancementLevel;
        private int[] stoneRequiredPerLevel = { 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 }; // 0강~10강 필요 강화석
        private float[] successRatePerLevel = { 1f, 0.95f, 0.9f, 0.85f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f, 0.3f, 0.2f }; // 0~10강 확률
        private float[] enhancementRatePerLevel = { 1f, 1f, 2f, 2f, 3f, 3f, 4f, 4f, 5f, 7f }; // 0~10강 증가량

        // 강화 시 사용하는 변수
        Random random;
        int enhancementLevel;
        EnhancementStone enhancementStone;

        public EnhancementManager() 
        {
            player = GameManager.Instance.player;
            inventory = player.inventory;

            uiManager = UIManager.Instance;

            enhanceableItems = new List<Item>();

            random = new Random();
            maxEnhancementLevel = 10;
        }

        public void StartEnhanceProcess()
        {
            DisplayEnhanceableItems();

            var choice = ConsoleHelper.GetUserChoice(["0", "1", "2"]);

            switch (choice)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "0":
                    break;
            }
        }

        // 강화 할 수 있는 아이템(소유하고 있는 무기, 방어구) 보여주기
        public void DisplayEnhanceableItems()
        {
            // 강화 할 수 있는 아이템(소유하고 있는 무기, 방어구) 보여주기
            inventory.DisplayInfo(true, ItemType.Weapon, ItemType.Armor);
        }

        // 강화석 재료가 충분한지 체크
        private bool HasEnoughEnhancementStones(int level)
        {
            // 인벤토리에서 강화석 찾기
            enhancementStone = (EnhancementStone)inventory.FindItemById(4000);

            // 강화석 null 값 체크
            if (enhancementStone == null || enhancementStone.count == 0)
            { 
                return false;
            }

            // 강화 할 때 사용할 강화석 개수가 충분하지 체크
            return (enhancementStone.count >= stoneRequiredPerLevel[level]);
        }

        // 강화 시도
        public void TryEnhanceItem(Item item)
        {
            // 아이템 강화 단계 가져오기
            enhancementLevel = item switch
            {
                //Weapon w => w.enhancementLevel,
                //Armor a => a.enhancementLevel,
                _ => 0
            };

            // 장비 레벨 체크
            if (enhancementLevel >= maxEnhancementLevel)
            {
                Console.WriteLine("더이상 강화를 진행할 수 있습니다.");
                return;
            }

            // 강화석 재료 체크
            if (!HasEnoughEnhancementStones(enhancementLevel))
            {
                Console.WriteLine("강화석 재료가 부족합니다.");
                return;
            }

            EnhanceItem(item);
        }

        // 강화 시작
        private void EnhanceItem(Item item)
        {
            // 강화석 소모
            int requiredStoneCount = stoneRequiredPerLevel[enhancementLevel + 1]; // 다음 단계 필요 강화석
            enhancementStone.count -= requiredStoneCount;

            // 장비 옵션 가져오기

            // 강화 확률 계산
            float successRate = successRatePerLevel[enhancementLevel + 1];
            bool isSuccess = random.NextDouble() < successRate;

            if(isSuccess)
            {

            }
        }
    }
}
