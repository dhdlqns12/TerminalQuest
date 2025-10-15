using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Scenes
{
    public class InventoryEquipScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;

        Inventory inventory;

        private int equipIdx;

        public void Enter()
        {
            equipIdx = 0;
            inventory = GameManager.Instance.player.inventory;

            InventoryEquipView();
        }

        public void Update()
        {
            Process();
        }

        public void Exit()
        {

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
        }

        private void Process()
        { 
            // 선택 가능한 장비 배열 만들기
            int vaildCount = inventory.Items.Count;
            string[] vaildItemOption = Enumerable.Range(0, vaildCount + 1).Select(i => i.ToString()).ToArray();   // LINQ 문법
            var choice = GetUserChoice(vaildItemOption);

            // 나가기 설정
            if (choice == "0") { OnSceneChangeRequested?.Invoke(new InventoryScene()); return; }

            // 인벤토리에서 idx로 검색하여 해당 아이템 장착
            equipIdx = int.Parse(choice.ToString());
            inventory.EquipItemByIdx(equipIdx - 1);

            // 업데이트 하여 갱신
            OnSceneChangeRequested?.Invoke(new InventoryEquipScene());
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
    }
}
