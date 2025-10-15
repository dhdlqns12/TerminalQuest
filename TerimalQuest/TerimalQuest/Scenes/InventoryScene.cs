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

        private int equipIdx;

        public void Enter()
        {
            equipIdx = 0;

            InventoryView();
        }

        public void Update()
        {

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

            string choice = GetUserChoice(["1", "2", "3"]);

            switch(choice)
            {
                case "1":
                    InventoryEquipView();
                    break;
                case "2":
                    InventorySortingView();
                    break;
                case "0":
                    OnSceneChangeRequested?.Invoke(new StartScene());
                    break;
                default:
                    break;
            }
        }

        // 플레이어 인벤토리 장착 관리 창 : 플레이어의 아이템을 장착/해제 할 수 있다.
        private void InventoryEquipView()
        {
            Inventory inventory = GameManager.Instance.player.inventory;

            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            inventory.DisplayInfo(true);
            Console.WriteLine();
            DisplayOption(["(번호). 해당 장비 장착", "0. 나가기"]);

            // 선택 가능한 장비 배열 만들기
            int vaildCount = inventory.Items.Count;
            string[] vaildItemOption = Enumerable.Range(0, vaildCount + 1).Select(i => i.ToString()).ToArray();   // LINQ 문법
            var choice = GetUserChoice(vaildItemOption);

            // 나가기 설정
            if (choice == "0") { OnSceneChangeRequested?.Invoke(new InventoryScene()); return; }

            // 인벤토리에서 idx로 검색하여 해당 아이템 장착
            equipIdx = int.Parse(choice.ToString());
            inventory.EquipItemByIdx(equipIdx - 1);
        }

        // 플레이어 인벤토리 정렬 창 : 인벤토리의 아이템들을 옵션에 따라 정렬할 수 있다.
        private void InventorySortingView()
        {
            Inventory inventory = GameManager.Instance.player.inventory;

            Console.Clear();
            Console.WriteLine("[인벤토리 - 아이템 정렬]");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            inventory.DisplayInfo(false);
            Console.WriteLine();
            DisplayOption(["1. 이름", "2. 장착순", "3. 공격력", "4. 방어력", "0. 나가기"]);

            var choice = GetUserChoice(["0", "1", "2", "3", "4"]);

            // 나가기 설정
            if (choice == "0") { OnSceneChangeRequested?.Invoke(new InventoryScene()); return; }

            // 옵션에 따라 아이템 정렬
            inventory.SortItemByOption(int.Parse(choice.ToString()));
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
