using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Scenes
{
    public class EnhancementScene
    {
        public event Action<IScene> OnSceneChangeRequested;

        private Inventory inventory;

        public void Enter()
        {
            inventory = GameManager.Instance.player.inventory;

            Console.Clear();
            Console.WriteLine("장비 강화");
            Console.WriteLine("보유 중인 장비를 강화할 수 있습니다.");
            Console.WriteLine();
            inventory.DisplayInfo(true, ItemType.Weapon, ItemType.Armor);
            Console.WriteLine();
            DisplayOption(["1. 무기 강화", "2. 방어구 강화", "0. 나가기"]);
        }

        public void Update()
        {
            //Process();
        }

        public void Exit()
        {

        }

        private void Process()
        {
            string choice = ConsoleHelper.GetUserChoice(["0", "1", "2"]);

            switch (choice)
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
