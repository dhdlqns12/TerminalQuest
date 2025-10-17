using TerimalQuest.Core;
using TerimalQuest.Manager;
using TerimalQuest.Scenes;

namespace TerimalQuest.Scenes
{
    public class ShowStatusScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;
        public void Enter()
        {
            Player player = GameManager.Instance.player;
            player.RefreshStat();

            UIManager.Instance.ShowSection("상태창");
            UIManager.Instance.ShowStatusSceneScripts();
        }

        public void Update()
        {
            if (int.TryParse(Console.ReadLine(), out int answer))
            {
                switch (answer)
                {
                    case 0:
                        OnSceneChangeRequested?.Invoke(new StartScene());
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

        }
    }
}
