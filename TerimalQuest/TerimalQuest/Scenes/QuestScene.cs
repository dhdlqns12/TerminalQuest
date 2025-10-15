using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Scenes
{
    public class QuestScene: IScene
    {
        public event Action<IScene> OnSceneChangeRequested;

        QuestManager questManager;
        List<Quest> quests = new List<Quest>();

        bool isSelecting = false;

        bool isRewarding = false;

        public void Enter()
        {
            questManager = QuestManager.Instance;
            quests = QuestManager.Instance.questLists;
            questManager.QuestListShow(quests);
        }

        public void Update()
        {
            string input = Console.ReadLine();
            if (isSelecting && !isRewarding)
            {
                switch (input)
                {
                    case "1":
                        Quest quest = questManager.curQuest;
                        
                        if(GameManager.Instance.player.questList.Contains(quest))
                        {
                            if (quest.isClear)
                            {
                                quest.QuestClear(GameManager.Instance.player);
                                isSelecting = false;
                                isRewarding = true;
                            }
                            else
                                Console.WriteLine("보상을 받을 수 없습니다");
                        }
                        else
                        {
                            questManager.AccepQuest();
                            questManager.QuestListShow(quests);
                            questManager.PlayQuest("미니언", 10);
                            questManager.PlayQuest("슬라임", 5);
                            isSelecting = false;
                        }
                        break;
                    case "2":
                        questManager.QuestListShow(quests);
                        isSelecting = false;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }                  
            }
            else
            {
                if(isRewarding)
                {
                    questManager.QuestListShow(quests);
                    isRewarding = false;
                }
                else
                {
                    if (int.TryParse(input, out int num))
                    {
                        if (num <= quests.Count && num > 0)
                        {
                            QuestManager.Instance.SelectQuest(quests[num - 1]);
                            isSelecting = true;
                        }
                        else
                            Console.WriteLine("잘못된 입력입니다.");
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }
        }

        public void Exit()
        {
            Console.Clear();
        }
    }
}
