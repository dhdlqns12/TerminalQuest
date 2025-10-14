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
        static List<Quest> questLists = new List<Quest>();

        public QuestManager()
        {
            
            string projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
            string resourcePath = Path.Combine(projectRoot, "Resources", "QuestData.json");
            string json = File.ReadAllText(resourcePath);

            var data = JsonSerializer.Deserialize<List<Quest>>(json);
            questLists = data;
        }

        static void Main(string[] args)
        {
            QuestManager manager = new QuestManager();
            //ShowQuestInfo(questLists[1]);

            //QuestListShow(questLists);
            Console.Write("원하시는 행동을 입력해주세요.\n>>");
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    
                }
                else
                {
                    
                }
            }
        }


        /// <summary>
        /// 퀘스트 리스트업 함수
        /// </summary>
        /// <param name="quests"></param>
        public static void QuestListShow(List<Quest> quests, Player player)
        {
            for (int i = 0; i < quests.Count; i++)
            {
                //Random rand = new Random();
                //Console.ForegroundColor = (ConsoleColor)rand.Next(0, 16);
                string questRunning = player.questList.Contains(quests[i].questNum) ? "[진행중]" : "";
                Console.WriteLine($"{i}. {quests[i].name}");
                Console.ResetColor();
            }
        }


        /// <summary>
        /// 퀘스트 정보 출력
        /// </summary>
        /// <param name="quest"></param>
        public static void ShowQuestInfo(Quest quest)
        {
            Console.WriteLine($"{quest.name}\n");
            Console.WriteLine($"\n{quest.description}\n");
            foreach(var questDic in quest.successConditions)
            {
                Console.WriteLine($"- {questDic.Key} {questDic.Value}마리 처치");
            }
            Console.WriteLine("\n- 보상 -\n");
            /*if(quest.rewardItem.Count != 0) 아이템 구현시 해제
            {
                for (int i = 0; i < quest.rewardItem.Count; i++)
                    Console.WriteLine($"{quest.rewardItem[i].name} x 1");
            }*/
            Console.WriteLine($"  {quest.rewardGold}G");
            Console.WriteLine($"  경험치 {quest.rewardExp}");

            //보상받기 구현해야 함
        }  
    }
}
