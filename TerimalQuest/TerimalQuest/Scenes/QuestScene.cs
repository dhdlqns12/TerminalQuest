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


        public void Enter()
        {
            questManager = QuestManager.Instance;
            quests = QuestManager.Instance.questLists;
            questManager.QuestListShow(quests);
        }

        public void Update()
        {
            string input = Console.ReadLine();
            if (isSelecting)
            {
                switch (input)
                {
                    case "1":
                        questManager.SelectQuest(questManager.curQuest);
                        questManager.QuestListShow(quests);
                        isSelecting = false;
                        break;
                    case "2":
                        questManager.QuestListShow(quests);
                        isSelecting = false;
                        break;
                    case "3":
                        if (questManager.curQuest.isClear)
                        {
                            questManager.curQuest.QuestClear(GameManager.Instance.player);

                        }
                        else
                            Console.WriteLine("잘못된 입력입니다.");
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }                  
            }
            else
            {
                if (int.TryParse(input, out int num))
                {
                    if (num <= quests.Count && num > 0)
                    {
                        QuestManager.Instance.SelectQuest(quests[num-1]);
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

        public void Exit()
        {
            Console.Clear();
        }
    }
}
