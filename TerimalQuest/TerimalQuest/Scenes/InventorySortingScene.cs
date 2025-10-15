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

            UIManager.Instance.InventorySortingScripts(inventory);
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
    }
}
