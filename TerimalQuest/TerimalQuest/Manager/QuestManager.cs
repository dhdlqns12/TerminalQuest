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

        public List<Quest> mainQuests = new List<Quest>();
        public List<Quest> subQuests = new List<Quest>();
        public QuestManager()
        {           
            string projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
            string resourcePath = Path.Combine(projectRoot, "Resources", "QuestData.json");
            string json = File.ReadAllText(resourcePath);

            var data = JsonSerializer.Deserialize<List<Quest>>(json);
            questLists = data;
            player = GameManager.Instance.player;
            InitializeQuests(questLists);
            QuestClassify(questLists);
        }

        public void InitializeQuests(List<Quest> quests)
        {
            List<int> clearNums = player.clearQuestNums;
            if (clearNums != null)
            {
                for(int i = 0; i < quests.Count; i++)
                {
                    if (clearNums.Contains(quests[i].questNum))
                        quests.Remove(quests[i]);
                }
            }
        }



        public void QuestClassify(List<Quest> quests)
        {
            for (int i = 0; i < quests.Count; i++)
            {
                switch(quests[i].questType)
                {
                    case "보스":
                        mainQuests.Add(quests[i]);
                        break;
                    default:
                        subQuests.Add(quests[i]);
                        break;
                }
            }
        }


        /// <summary>
        /// 퀘스트 수락
        /// </summary>
        public void AccepQuest()
        {
            player.questList.Add(curQuest.questNum, curQuest);
        }

        /// <summary>
        /// 퀘스트 수행 할 시 호출
        /// </summary>
        /// <param name="name"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public void PlayQuest(string name, int num = 1)
        {
            Dictionary<int, Quest> playerQuests = GameManager.Instance.player.questList;
            foreach(var quest in playerQuests)
            {
                Quest curQuest = quest.Value;
                if (curQuest.currentCounts.ContainsKey(name))
                {
                    curQuest.currentCounts[name] += num;
                    if (curQuest.currentCounts[name] >= curQuest.successConditions[name])
                    {
                        curQuest.currentCounts[name] = curQuest.successConditions[name];
                    }
                }
                curQuest.isClear = CheckQuest(curQuest);
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
