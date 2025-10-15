using System;
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
                //Random rand = new Random();
                //Console.ForegroundColor = (ConsoleColor)rand.Next(0, 16);
                string questRunning = player.questList.Contains(quests[i]) ? "[진행중]" : "";
                Console.WriteLine($"{i+1}. {quests[i].name} {questRunning}");
                //Console.ResetColor();
            }
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
                Console.WriteLine($"- {questDic.Key} {questDic.Value}마리 처치");
            }
            Console.WriteLine("\n- 보상 -\n");
            if (quest.rewardItem?.Count != 0)
            {
                for (int i = 0; i < quest.rewardItem?.Count; i++)
                    Console.WriteLine($"  {quest.rewardItem[i].name} x {quest.rewardItem[i].count}");
            }
            Console.WriteLine($"  {quest.rewardGold}G");
            Console.WriteLine($"  경험치 {quest.rewardExp}");

            Console.WriteLine("\n1. 수락");
            Console.WriteLine("2. 거절");
            if (player.questList.Contains(quest))
                Console.WriteLine("3. 보상 받기");
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
        public bool CheckQuest(string name, int num = 1)
        {
            List<Quest> playerQuests = GameManager.Instance.player.questList;
            for (int i = 0; i < playerQuests?.Count; i++)
            {
                if (playerQuests[i].currentCounts.ContainsKey(name))
                {
                    playerQuests[i].currentCounts[name] += num;
                    if (playerQuests[i].currentCounts[name] >= playerQuests[i].successConditions[name])
                    {
                        playerQuests[i].isClear = true;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
