using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.System
{
    public class Weapon : Item
    {
        public float atk;

        public Weapon(int id, string name, string desc, int price, float atk, ItemType type) : base(id, name, desc, price, type)
        {
            this.atk = atk;
        }

        public override void Equip()
        {
            // 아이템 정보 표시

            // 현재 장착 관리 상태인지 확인 후 아이템 정보 표시
            string isEquippedTxt = (isEquipped) ? "[E]" : "";
            string itemName = $"{isEquippedTxt}{name}";

            Console.WriteLine(
                string.Format("{0} | {1} | {2}",
                ConsoleHelper.PadRightForConsole(itemName, 20),
                ConsoleHelper.PadRightForConsole($"공격력 +{atk}", 15),
                desc));
        }

        public void DisplayInfoProduct()
        {
            // 상품 목록에서 보여줄 아이템 정보 표시
            string itemPurchase = (isPurchase) ? "구매완료" : $"{price}";
            string isGoldIcon = (isPurchase) ? "" : "G";

            Console.WriteLine(
                string.Format("{0} | {1} | {2} | {3} {4}",
                ConsoleHelper.PadRightForConsole(name, 20),
                ConsoleHelper.PadRightForConsole($"방어력 +{atk}", 15),
                ConsoleHelper.PadRightForConsole(desc, 50),
                ConsoleHelper.PadRightForConsole(itemPurchase, 6),
                isGoldIcon));
        }
    }
}
