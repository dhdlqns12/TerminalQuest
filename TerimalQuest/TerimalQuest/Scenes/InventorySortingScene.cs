using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Scenes
{
    public class InventorySortingScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;

        private Inventory inventory;

        public void Enter()
        {
            inventory = GameManager.Instance.player.inventory;

            InventorySortingView();
        }

        public void Update()
        {
            Process();
        }

        public void Exit()
        {

        }

        // 플레이어 인벤토리 정렬 창 : 인벤토리의 아이템들을 옵션에 따라 정렬할 수 있다.
        private void InventorySortingView()
        {
            Console.Clear();
            Console.WriteLine("[인벤토리 - 아이템 정렬]");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            inventory.DisplayInfo(false);
            Console.WriteLine();
            DisplayOption(["1. 이름", "2. 장착순", "3. 공격력", "4. 방어력", "0. 나가기"]);
        }

        private void Process()
        { 
            var choice = GetUserChoice(["0", "1", "2", "3", "4"]);

            // 나가기 설정
            if (choice == "0") { OnSceneChangeRequested?.Invoke(new InventoryScene()); return; }

            // 옵션에 따라 아이템 정렬
            inventory.SortItemByOption(int.Parse(choice.ToString()));

            // 업데이트 하여 갱신
            OnSceneChangeRequested?.Invoke(new InventorySortingScene());
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
