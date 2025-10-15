using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Scenes
{
    public class InventoryScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;

        private Inventory inventory;

        public void Enter()
        {
            inventory = GameManager.Instance.player.inventory;

            UIManager.Instance.InventoryScripts(inventory);
        }

        public void Update()
        {
            Process();
        }

        public void Exit()
        {
             
        }

        // 플레이어 인벤토리 창 : 플레이어의 인벤토리를 볼 수 있는 창. 아이템을 확인 할 수 있다.
        private void InventoryView()
        {
            Inventory inventory = GameManager.Instance.player.inventory;

            Console.WriteLine("[인벤토리]");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            inventory.DisplayInfo(false);
            Console.WriteLine();
            DisplayOption(["1. 장착 관리", "2. 아이템 정렬", "0. 나가기"]);

        }

        private void Process()
        {
            string choice = GetUserChoice(["1", "2", "3"]);

            switch(choice)
            {
                case "1":
                    OnSceneChangeRequested?.Invoke(new InventoryEquipScene());
                    break;
                case "2":
                    OnSceneChangeRequested?.Invoke(new InventorySortingScene());
                    break;
                case "0":
                    OnSceneChangeRequested?.Invoke(new StartScene());
                    break;
                default:
                    break;
            }
        }

        // 사용자 입력 체크
        protected string GetUserChoice(string[] vaildOptions)
        {
            string choice;
            while (true)
            {
                Console.Write(">> ");
                choice = Console.ReadLine();
                Console.WriteLine();

                foreach (var option in vaildOptions) if (choice == option) return choice;

                UIManager.Instance.SelectWrongSelection();
            }
        }

        public void DisplayOption(string[] options)
        {
            foreach (var option in options)
            {
                Console.WriteLine(option);
            }
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }
    }
}
