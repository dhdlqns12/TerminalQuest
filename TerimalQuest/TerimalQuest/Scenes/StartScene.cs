using TerimalQuest.Manager;
using TerimalQuest.Scenes;

public class StartScene : IScene
{
    public event Action<IScene> OnSceneChangeRequested;

    public void Enter()
    {
    }

    public void Update()
    {
        Console.Clear();
        UIManager.Instance.ShowStartSceneScripts();
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
                    UIManager.Instance.ShowInvalidInput();
                    Console.ReadKey();
                    break;
            }
        }
        else
        {
            UIManager.Instance.ShowInvalidInput();
            Console.ReadKey();
        }
    }

    public void Exit()
    {
        Console.Clear();
    }
}
