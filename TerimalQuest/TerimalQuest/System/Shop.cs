using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Manager;

namespace TerimalQuest.System
{
    public class Shop
    {
        /*
         * Shop: 상점 클래스
         * 
         * [상점 기능]
         * - 필요한 아이템을 얻을 수 있습니다.
         * - Player와 마찬가지로 GameManager에서 공유
         * - 상품 리스트를 가지고 있음
         * 
         */

        private List<Item> productList = new List<Item>();
        private float saleRate = 0.85f;     // 판매 가격 환율

        public Shop()
        {
            Init();
        }

        private void Init()
        {
            // 상품 리스트 초기화
            productList.Clear();

            // 상품 리스트 채우기
            productList.Add(ItemDatabase.GetArmor("수련자 갑옷"));
            productList.Add(ItemDatabase.GetArmor("무쇠갑옷"));
            productList.Add(ItemDatabase.GetArmor("스파르타의 갑옷"));
            productList.Add(ItemDatabase.GetWeapon("낡은 검"));
            productList.Add(ItemDatabase.GetWeapon("청동 도끼"));
            productList.Add(ItemDatabase.GetWeapon("스파르타의 창"));
        }

        // 상품 구매 시도 - 인덱스 검색
        public bool TryPurchaseItem(int idx)
        {
            if (productList.Count <= 0 || idx >= productList.Count) return false;

            Item product = productList[idx];

            // 이미 구매한 상품이라면 구매한 아이템입니다 출력
            if (product.isPurchase)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                return false;
            }

            // 플레이어의 골드가 충분한지 체크
            Player player = GameManager.Instance.player;
            if (player.gold < product.price)
            {
                Console.WriteLine("골드가 충분하지 않습니다.");
                return false;
            }

            // 아이템 구매
            PurchaseItem(product.Clone());
            return true;
        }

        // 상품 구매
        public void PurchaseItem(Item item)
        {
            item.isPurchase = true;

            Player player = GameManager.Instance.player;

            // Player 골드 차감 및 인벤토리 추가
            player.gold -= item.price;
            player.inventory.Add(item);

            Console.WriteLine($"{item.name} 아이템을 구매했습니다.");
        }

        // 상품 판매 시도 - 인덱스 검색
        public bool TrySaleItem(int idx)
        {
            Player player = GameManager.Instance.player;

            // 선택한 번호가 인벤토리 내 있는지 체크
            if (player.inventory.Items.Count <= 0 || idx >= player.inventory.Items.Count) return false;

            // 아이템 판매
            SailItem(idx);
            return true;
        }

        // 상품 구매
        public void SailItem(int idx)
        {
            Player player = GameManager.Instance.player;
            Inventory inventory = player.inventory;
            Item item = inventory.Items[idx];

            // 이미 장착 중인 아이템이라면 장착 해제
            if (item.isEquipped)
            {
                item.Equip();
            }

            // 판매 골드 흭득 : 85%
            player.gold += (int)(item.price * saleRate);

            // 판매 및 골드 흭득 메세지 출력
            Console.WriteLine($"{item.name} 아이템을 판매하였습니다.");

            // 플레이어 인벤토리에서 삭제
            inventory.Items.RemoveAt(idx);
        }

        // 상품 목록 보여주기
        public void DisplayInfo(bool isPurchase)
        {
            Console.WriteLine("[아이템 목록]\n");
            string purchase = (isPurchase) ? ConsoleHelper.PadRightForConsole(" ", 6) : $"  ";

            Console.WriteLine(
                string.Format("{0}{1} | {2} | {3} | {4}",
                purchase,
                ConsoleHelper.PadRightForConsole("[아이템 이름]", 20),
                ConsoleHelper.PadRightForConsole("[아이템 효과]", 15),
                ConsoleHelper.PadRightForConsole("[아이템 설명]", 50),
                "[아이템 가격]\n"));

            for (int i = 0; i < productList.Count; i++)
            {
                string idxTxt = (isPurchase) ? $"{i + 1} : " : "";
                Console.Write($"- {idxTxt}");
                productList[i].DisplayInfoProduct();
            }
        }

        // 프로퍼티 변수
        public List<Item> ProductList { get { return productList; } set { productList = value; } }
    }
}
