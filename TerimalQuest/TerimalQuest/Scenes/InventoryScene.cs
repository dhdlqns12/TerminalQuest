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

        private void Process()
        {
            string choice =  ConsoleHelper.GetUserChoice(["0", "1", "2", "3"]);

            switch(choice)
            {
                case "1":
                    OnSceneChangeRequested?.Invoke(new InventoryEquipScene());
                    break;
                case "2":
                    OnSceneChangeRequested?.Invoke(new InventoryItemUseScene());
                    break;
                case "3":
                    OnSceneChangeRequested?.Invoke(new InventorySortingScene());
                    break;
                case "0":
                    OnSceneChangeRequested?.Invoke(new StartScene());
                    break;
                default:
                    break;
            }
        }
    }
}
