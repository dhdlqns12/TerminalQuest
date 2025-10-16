using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Manager;

namespace TerimalQuest.System
{
    public class Quest
    {
        public int questNum {  get; set; } //퀘스트 고유 넘버
        public string questType { get; set; } //퀘스트 타입 -> 필요 시
        public string name { get; set; } //퀘스트 이름
        public string description { get; set; } //퀘스트 설명
        
        public string successDes { get; set; } //퀘스트 성공 조건 설명
        //성공조건 <몬스터 이름, 마릿 수> 단 레벨업과 장착 같은 경우는 int = 1로 진행
        public Dictionary<string, int> successConditions { get; set; }


        public int rewardGold { get; set; } //보상 골드
        public int rewardExp { get; set; } //보상 경험치
        public Dictionary<string, int> rewardItem { get; set; } //보상 아이템

        public bool isClear { get; set; } //클리어되었는지

        public Dictionary<string, int> currentCounts;

        public Quest(int questNum, string questType, string name, string description,string successDes, Dictionary<string, int> successConditions, bool isClear, int rewardGold = 0, int rewardExp = 0, Dictionary<string, int> rewardItem = null)
        {
            this.questNum = questNum;
            this.questType = questType;
            this.name = name;
            this.description = description;
            this.successDes = successDes;
            this.successConditions = successConditions;
            this.rewardGold = rewardGold;
            this.rewardExp = rewardExp;
            this.rewardItem = rewardItem;
            this.isClear = isClear;
            currentCounts = new Dictionary<string, int>();
            foreach (var key in successConditions.Keys)
            {
                currentCounts[key] = 0;
            }
        }

        /// <summary>
        /// 퀘스트 완료 함수
        /// </summary>
        /// <param name="player"></param>
        public void QuestClear(Player player)
        {
            UIManager.Instance.RewardMessage();
            QuestManager questManager = QuestManager.Instance;
            player.exp += rewardExp;
            player.gold += rewardGold;
            //인벤토리 추가
            if(rewardItem?.Count > 0)
            {
                foreach (var dic in rewardItem)
                {
                    string itemName = dic.Key;
                    int count = dic.Value;
                    for (int i = 0; i < count; i++)
                    {
                        player.inventory.Add(ItemDatabase.GetItem(dic.Key));
                    }
                }
                /*for (int i = 0; i < rewardItem?.Count; i++)
                {
                    player.inventory.Add(ItemDatabase.GetItem(rewardItem[i]));
                }*/
            }
            if(questManager.subQuests.Contains(this))
                questManager.subQuests.Remove(this);
            else
                questManager.mainQuests.Remove(this);
            questManager.questLists.Remove(this);
            player.clearQuestNums.Add(this.questNum);
            //QuestManager.Instance.InitializeQuest(this);
            player.questList.Remove(this.questNum);
        }
    }
}
