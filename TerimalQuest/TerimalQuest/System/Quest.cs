using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;

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
        public void QuestClear(Player player)
        {
            player.exp += rewardExp;
            player.gold += rewardGold;
            //인벤토리 추가
            for (int i = 0; i < rewardItem.Count; i++)
            {
            }
            player.questList.Remove(questNum);
        }
    }
}
