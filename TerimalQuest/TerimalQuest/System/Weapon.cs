using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;

namespace TerimalQuest.System
{
    public class Weapon : Item
    {
        public float atk { get; set; }

        public Weapon(int id, string name, string desc, int price, float atk, ItemType type) : base(id, name, desc, price, type)
        {
            this.atk = atk;
        }

        public override void DisplayInfo()
        {
            GameManager.Instance.player.ToggleEquipItem(this); //아이템 장착
            // 아이템 정보 표시
            // 현재 장착 관리 상태인지 확인 후 아이템 정보 표시
            string isEquippedTxt = (isEquipped) ? "[E]" : "";
            string itemName = $"{isEquippedTxt}{name}";
            string itemEffect = $"공격력 +{atk}";
            string itemCount = $"수량: x{count}";

            Console.WriteLine(
                string.Format("{0} | {1} | {2}",
                ConsoleHelper.PadRightForConsole(itemName, offsetName),
                ConsoleHelper.PadRightForConsole(itemEffect, offsetEffect),
                ConsoleHelper.PadRightForConsole(desc, offsetDesc),
                itemCount));
        }

        public void DisplayInfoProduct()
        {
            // 상품 목록에서 보여줄 아이템 정보 표시
            string itemEffect = $"공격력 +{atk}";
            string itemCount = $"수량: x{count}";
            string itemPurchase = (isPurchase) ? "구매완료" : $"{price}";
            string isGoldIcon = (isPurchase) ? "" : "G";

            Console.WriteLine(
                string.Format("{0} | {1} | {2} | {3} {4}",
                ConsoleHelper.PadRightForConsole(name, offsetName),
                ConsoleHelper.PadRightForConsole(itemEffect, offsetEffect),
                ConsoleHelper.PadRightForConsole(desc, offsetDesc),
                ConsoleHelper.PadRightForConsole(itemCount, offsetCount),
                ConsoleHelper.PadRightForConsole(itemPurchase, offsetPurchase),
                isGoldIcon));
        }

        // 아이템 복제
        public override Weapon Clone()
        {
            this.Id += 1;   // 복제 시 Id 증가
            return (Weapon)this.MemberwiseClone();
        }
    }
}
