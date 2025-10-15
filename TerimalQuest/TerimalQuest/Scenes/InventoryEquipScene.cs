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

            UIManager.Instance.InventoryEquipScripts(inventory);
        }

        public void Update()
        {
            Process();
        }

        public void Exit()
        {

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
