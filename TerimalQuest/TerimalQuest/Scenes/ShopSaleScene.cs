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
    public class ShopSaleScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;

        private Shop shop;
        private Player player;

        public void Enter()
        {
            shop = GameManager.Instance.shop;
            player = GameManager.Instance.player;

            UIManager.Instance.ShopSaleScripts(player);
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
            // 인벤토리 인덱스 정보 가져오기
            int vaildCount = player.inventory.Items.Count;
            string[] vaildItemOption = Enumerable.Range(0, vaildCount + 1).Select(i => i.ToString()).ToArray();   // LINQ 문법
            var choice = GetUserChoice(vaildItemOption);

            // 아이템 판매
            while (true)
            {
                if (choice == "0") { OnSceneChangeRequested?.Invoke(new ShopScene()); return; }

                // 판매 가능한 아이템이면 판매 
                if (shop.TrySaleItem(int.Parse(choice) - 1) == true) break;

                choice = GetUserChoice(vaildItemOption);
            }

            // 판매가 완료 되었으면 1초 후 갱신
            Thread.Sleep(1000);
            OnSceneChangeRequested?.Invoke(new ShopSaleScene());
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
