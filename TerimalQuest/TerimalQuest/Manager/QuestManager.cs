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
