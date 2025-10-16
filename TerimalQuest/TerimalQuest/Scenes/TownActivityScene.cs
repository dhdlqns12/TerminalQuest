using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Scenes
{
    public class TownActivityScene : IScene // 마을  활동 추가(순찰,훈련)
    {
        Player player = GameManager.Instance.player;

        public event Action<IScene> OnSceneChangeRequested;

        QuestManager questManager;
        public void Enter()
        {
            questManager = QuestManager.Instance;
            UIManager.Instance.ShowSection("마을 활동");
            Console.WriteLine("\n마을에서 할 활동을 선택해 주세요.\n");
            UIManager.Instance.TownActivityScripts();
        }

        public void Update()
        {
            Process();
        }

        public void Process()
        {
            string input = ConsoleHelper.GetUserChoice(["1", "2", "0"]);

            switch (input)
            {
                case "1":
                    TownPatrol();
                    break;
                case "2":
                    Training();
                    break;
                case "0":
                    OnSceneChangeRequested?.Invoke(new StartScene());
                    break;
                default:
                    break;
            }
        }

        public void Exit()
        {

        }

        private void TownPatrol()
        {
            Random ran = new Random();
            
            var events = new[]
            {
                (20,"마을 주민과 만나서 심부름을 했다.",1000),
                (40, "길 읽은 아이를 안내해주고 보수를 받았다.",500),
                (70, "마을 주민에게 선물을  받았다.",250),
                (100, "아무 일도 일어나지 않았다.",0)
            };

            int ranNum = ran.Next(1, 101);

            if (player.stamina >= 10)
            {
                foreach (var (percent, message, gold) in events)
                {
                    if (ranNum <= percent)
                    {
                        if (gold > 0)
                            questManager.PlayQuest("마을순찰");
                        Console.WriteLine($"{message} {gold}골드 흭득");
                        player.gold += gold;
                        break;
                    }
                }
                player.stamina -= 10;
            }
            else
            {
                Console.WriteLine("스태미나 부족!");
            }
        }

        private void Training()
        {
            Random ran = new Random();

            var events = new[]
            {
                (15, "열심히 바닥을 굴렀습니다!",30),
                (75,"오늘 훈련 완료",20),
                (100, "하기 싫다... 훈련이...",15)
            };

            int ranNum = ran.Next(1, 101);

            if (player.stamina >= 20)
            {
                foreach (var (percent, message, exp) in events)
                {
                    if (ranNum <= percent)
                    {
                        Console.WriteLine($"{message} {exp}경험치 획득");
                        player.exp += exp;
                        questManager.PlayQuest("훈련");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("스태미나 부족!");
            }
                player.stamina -= 20;
        }
    }
}

