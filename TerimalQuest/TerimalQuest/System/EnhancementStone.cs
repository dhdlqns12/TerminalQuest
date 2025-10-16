using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TerimalQuest.Manager;
using TerimalQuest.Scenes;

namespace TerimalQuest.System
{
    public class EnhancementStone : Item
    {
        public EnhancementStone(int id, string name, string desc, int price, ItemType type) : base(id, name, desc, price, type)
        {
            
        }

        public override void Equip(bool isEquip)
        {
            // 강화석 소모

        }

        public override void DisplayInfo()
        {
            // 아이템 정보 표시

            string itemName = $"{name}";
            string itemEffect = $"-";
            string itemCount = $"수량: x{count}";

            Console.WriteLine(
                string.Format("{0} | {1} | {2} | {3}",
                ConsoleHelper.PadRightForConsole(itemName, offsetName),
                ConsoleHelper.PadRightForConsole(itemEffect, offsetEffect),
                ConsoleHelper.PadRightForConsole(desc, offsetDesc),
                itemCount));
        }

        public override void DisplayInfoProduct()
        {
            // 상품 목록에서 보여줄 아이템 정보 표시
            string itemEffect = $"-";
            string itemCount = $"수량: x{count}";
            string itemPurchase = (isPurchase) ? "구매완료" : $"{price}";
            string isGoldIcon = (isPurchase) ? "" : "G";

            Console.WriteLine(
                string.Format("{0} | {1} | {2} | {3} | {4} {5}",
                ConsoleHelper.PadRightForConsole(name, offsetName),
                ConsoleHelper.PadRightForConsole(itemEffect, offsetEffect),
                ConsoleHelper.PadRightForConsole(desc, offsetDesc),
                ConsoleHelper.PadRightForConsole(itemCount, offsetCount),
                ConsoleHelper.PadRightForConsole(itemPurchase, offsetPurchase),
                isGoldIcon));
        }

        // 아이템 복제
        public override Item Clone()
        {
            this.Id += 1;   // 복제 시 Id 증가
            return new EnhancementStone(Id, name, desc, price, type);
        }
    }
}
