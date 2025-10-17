using TerimalQuest.Core;
using TerimalQuest.Manager;
using TerimalQuest.Scenes;

public class StartScene : IScene
{
    public event Action<IScene> OnSceneChangeRequested;
    Player player;
    QuestManager questManager;
    UIManager uiManager;
    public void Enter()
    {
        player = GameManager.Instance.player;
        questManager = QuestManager.Instance;
        uiManager = UIManager.Instance;
    }

    public void Update()
    {
        Console.Clear();
        uiManager.ShowStartSceneScripts();
        if (int.TryParse(Console.ReadLine(), out int answer))
        {
            switch (answer)
            {
                case 1:
                    OnSceneChangeRequested?.Invoke(new ShowStatusScene());
                    break;
                case 2:
                    OnSceneChangeRequested?.Invoke(new InventoryScene());
                    break;
                case 3:
                    int mainQuestNum = questManager.mainQuests[0].questNum;
                    if ((player.curStage == 5 || player.curStage == 10) && !player.questList.ContainsKey(mainQuestNum) && player.questList[mainQuestNum].isClear)
                    {
                        uiManager.ColorText("메인 퀘스트를 수락 후 입장 가능합니다!!", ConsoleColor.DarkRed);
                        Console.ReadKey();
                    }
                    else
                    {
                        OnSceneChangeRequested?.Invoke(new BattleScene());
                    }
                    break;
                case 4:
                    OnSceneChangeRequested?.Invoke(new QuestScene());
                    break;
                case 5:
                    OnSceneChangeRequested?.Invoke(new ShopScene());
                    break;
                case 6:
                    OnSceneChangeRequested?.Invoke(new TownActivityScene());
                    break;
                case 7:
                    OnSceneChangeRequested?.Invoke(new EnhancementScene());
                    break;
                case 0:
                    OnSceneChangeRequested?.Invoke(new DataSaveScene());
                    break;
                default:
                    uiManager.ShowInvalidInput();
                    Console.ReadKey();
                    break;
            }
        }
        else
        {
            uiManager.ShowInvalidInput();
            Console.ReadKey();
        }
    }

    public void Exit()
    {
        Console.Clear();
    }
}
