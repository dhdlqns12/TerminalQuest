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
    public class ShopScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;

        private Shop shop;
        private Player player;

        public void Enter()
        {
            shop = GameManager.Instance.shop;
            player = GameManager.Instance.player;

            UIManager.Instance.ShopScripts(player, shop);
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
            var choice = ConsoleHelper.GetUserChoice(["0", "1", "2"]);

            switch (choice)
            {
                case "1":
                    OnSceneChangeRequested?.Invoke(new ShopPurchaseScene());
                    break;
                case "2":
                    OnSceneChangeRequested?.Invoke(new ShopSaleScene());
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
