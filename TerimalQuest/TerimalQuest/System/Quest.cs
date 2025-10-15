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
        public Dictionary<string, int> successConditions { get; set; } //성공조건 <몬스터 이름, 마릿 수>
        public int rewardGold { get; set; } //보상 골드
        public int rewardExp { get; set; } //보상 경험치
        public List<Item> rewardItem { get; set; } //보상 아이템

        public bool isClear; //클리어되었는지

        public Quest(int questNum, string questType, string name, string description, Dictionary<string, int> successConditions, int rewardGold = 0, int rewardExp = 0, List<Item> rewardItem = null)
        {
            this.questNum = questNum;
            this.questType = questType;
            this.name = name;
            this.description = description;
            this.successConditions = successConditions;
            this.rewardGold = rewardGold;
            this.rewardExp = rewardExp;
            this.rewardItem = rewardItem;
        }

        /// <summary>
        /// 퀘스트 완료 함수
        /// </summary>
        /// <param name="player"></param>
        public void QuestClear(Player player, Quest quest)
        {
            RewardMessage();
            player.exp += rewardExp;
            player.gold += rewardGold;
            //인벤토리 추가
            if(rewardItem?.Count > 0)
            {
                for (int i = 0; i < rewardItem?.Count; i++)
                {
                    player.inventory.Add(rewardItem[i]);
                }
            }
            player.questList.Remove(questNum);
        }

        public void RewardMessage()
        {
            Player player = GameManager.Instance.player;
            Quest quest = QuestManager.Instance.curQuest;
            Console.WriteLine("보상을 획득하였습니다!");
            Console.WriteLine($"경험치 : {player.exp} -> {player.exp + quest.rewardExp}");
            Console.WriteLine($"골드 : {player.gold} -> {player.gold + quest.rewardGold}");
            if(quest.rewardItem?.Count > 0)
            {
                Console.WriteLine("획득 아이템");
                for (int i = 0; i < quest.rewardItem?.Count; i++)
                {
                    Console.WriteLine($"{quest.rewardItem[i].name} x {quest.rewardItem[i].count}");
                }
            }
        }
    }
}
