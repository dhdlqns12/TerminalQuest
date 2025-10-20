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
                (30,"아이들에게 삥을 뜯겻다.",-1500),
                (60, "길 읽은 아이를 안내해주었다.",0),
                (99, "마을 주민에게 선물을  받았다.",1000),
                (100, "아무 일도 일어나지 않았다.",10000)
            };

            int ranNum = ran.Next(1, 101);

            if (player.stamina >= 10)
            {
                foreach (var (percent, message, gold) in events)
                {
                    if (ranNum <= percent)
                    {
                        questManager.PlayQuest("마을순찰");
                        if (ranNum > 60 && ranNum <= 99)
                            questManager.PlayQuest("선물");
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
                (10,"아무것도 안했습니다.",0),
                (20, "열심히 바닥을 굴렀습니다!",30),
                (75,"오늘 훈련 완료",20),
                (100, "하기 싫다... 훈련이...",15)
            };

            int ranNum = ran.Next(1, 101);

            if (player.stamina >= 20 && player.gold >= 250)
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
                player.stamina -= 20;
            }
            else
            {
                Console.WriteLine("스태미나 또는 골드가 부족합니다.");
            }
        }
    }
}

