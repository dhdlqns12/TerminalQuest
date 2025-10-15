using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.System;

namespace TerimalQuest.Manager
{
    public class QuestManager
    {
        private static QuestManager instance;
        public static QuestManager Instance => instance ??= new QuestManager();

        public List<Quest> questLists = new List<Quest>();

        public Quest curQuest;

        Player player = new Player();

        public QuestManager()
        {           
            string projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
            string resourcePath = Path.Combine(projectRoot, "Resources", "QuestData.json");
            string json = File.ReadAllText(resourcePath);

            var data = JsonSerializer.Deserialize<List<Quest>>(json);
            questLists = data;
            player = GameManager.Instance.player;
        }

        /// <summary>
        /// 퀘스트 리스트업 함수
        /// </summary>
        /// <param name="quests"></param>
        public void QuestListShow(List<Quest> quests)
        {
            Console.Clear();
            curQuest = null;
            for (int i = 0; i < quests.Count; i++)
            {

                string questRunning = player.questList.Contains(quests[i]) ? "[진행중]" : "";
                Console.WriteLine($"{i+1}. {quests[i].name} {questRunning}");
            }
            Console.Write("\n원하시는 퀘스트를 선택해주세요.\n>>");
        }


        /// <summary>
        /// 퀘스트 정보 출력
        /// </summary>
        /// <param name="quest"></param>
        public void SelectQuest(Quest quest)
        {
            Console.Clear();
            curQuest = quest;
            Console.WriteLine($"{quest.name}\n");
            Console.WriteLine($"\n{quest.description}\n");
            foreach(var questDic in quest.successConditions)
            {
                int curNum = curQuest.currentCounts[questDic.Key];
                Console.WriteLine($"- {questDic.Key} {questDic.Value}마리 처치 ({curNum}/{questDic.Value})");
            }
            Console.WriteLine("\n- 보상 -\n");
            if (quest.rewardItem?.Count != 0)
            {
                for (int i = 0; i < quest.rewardItem?.Count; i++)
                    Console.WriteLine($"  {quest.rewardItem[i].name} x {quest.rewardItem[i].count}");
            }
            Console.WriteLine($"  {quest.rewardGold}G");
            Console.WriteLine($"  경험치 {quest.rewardExp}");

            SelectChoice();
        }
        
        public void SelectChoice()
        {
            if (player.questList.Contains(curQuest))
            {
                Console.WriteLine("\n1. 보상 받기");
                Console.WriteLine("2. 돌아가기");
            }
            else
            {
                Console.WriteLine("\n1. 수락");
                Console.WriteLine("2. 거절");
            }
            Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
        }


        /// <summary>
        /// 퀘스트 수락
        /// </summary>
        public void AccepQuest()
        {
            player.questList.Add(curQuest);
        }

        /// <summary>
        /// 퀘스트 수행 할 시 호출
        /// </summary>
        /// <param name="name"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public void PlayQuest(string name, int num = 1)
        {
            List<Quest> playerQuests = GameManager.Instance.player.questList;
            for (int i = 0; i < playerQuests?.Count; i++)
            {
                Quest quest = playerQuests[i];
                if (quest.currentCounts.ContainsKey(name))
                {
                    quest.currentCounts[name] += num;
                    if (quest.currentCounts[name] >= quest.successConditions[name])
                    {
                        quest.currentCounts[name] = quest.successConditions[name];
                    }
                }
                quest.isClear = CheckQuest(quest);
            }
        }



        /// <summary>
        /// 퀘스트 완료 여부 체크
        /// </summary>
        /// <param name="quest"></param>
        /// <returns></returns>
        public bool CheckQuest(Quest quest)
        {
            if (quest.successConditions.Count != quest.currentCounts.Count) return false;
            foreach(var dic in quest.currentCounts)
            {
                if (!quest.successConditions.TryGetValue(dic.Key, out int value) || value != dic.Value)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 퀘스트를 깼을 때 초기화(만약 퀘스트를 없앨 시 쓰지않을 코드)
        /// </summary>
        /// <param name="quest"></param>
        public void InitializeQuest(Quest quest)
        {
            quest.isClear = false;
            foreach (var key in quest.currentCounts.Keys)
                quest.currentCounts[key] = 0;
        }
    }
}
