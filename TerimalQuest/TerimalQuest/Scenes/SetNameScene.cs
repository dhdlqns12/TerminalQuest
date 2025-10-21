using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;

namespace TerimalQuest.Scenes
{
    public class SetNameScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;
        public void Enter()
        {
            UIManager.Instance.TerminalQuestScripts();
        }

        public void Update()
        {           
            UIManager.Instance.SetNameScripts();
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("공백또는 잘못된 입력입니다.");
            }
            else
            {
                UIManager.Instance.NameConfirmScripts(name);
                if(int.TryParse(Console.ReadLine(), out int answer))
                {
                    switch(answer)
                    {
                        case 1:
                            GameManager.Instance.player.Init_Player_Name(name);
                            OnSceneChangeRequested?.Invoke(new SetJobScene());
                            break;
                        case 2:
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }
        }

        public void Exit()
        {

        }
    }
}
