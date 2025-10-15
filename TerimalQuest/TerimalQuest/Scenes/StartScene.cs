using TerimalQuest.Manager;
using TerimalQuest.Scenes;

public class StartScene : IScene
{
    public event Action<IScene> OnSceneChangeRequested;

    public void Enter()
    {
        UIManager.Instance.ShowStartSceneScripts();
    }

    public void Update()
    {
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
                    OnSceneChangeRequested?.Invoke(new BattleScene());
                    break;
                case 4:
                    OnSceneChangeRequested?.Invoke(new QuestScene());
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    break;
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadKey();
        }
    }

    public void Exit()
    {
        // 나중에 UI 클리어, 리소스 해제 등 넣을 수 있음
    }
}
