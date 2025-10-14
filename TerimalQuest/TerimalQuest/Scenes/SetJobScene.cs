using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;

namespace TerimalQuest.Scenes
{
    public class SetJobScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;
        public void Enter()
        {
            UIManager.Instance.ShowTitle("스파르타 마을에 오신 여러분들을 환영합니다.");
        }

        public void Update()
        {
            string job = "";
            UIManager.Instance.SetNameScripts();
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                switch (index)
                {
                    case 1:
                        job = "전사";
                        break;
                    case 2:
                        job = "궁수";
                        break;
                    case 3:
                        job = "마법사";
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
            UIManager.Instance.JobConfirmScripts(job);
            if (int.TryParse(Console.ReadLine(), out int answer))
            {
                switch (answer)
                {
                    case 1:
                        OnSceneChangeRequested?.Invoke(new StartScene());
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }

        }

        public void Exit()
        {

        }
    }
}
