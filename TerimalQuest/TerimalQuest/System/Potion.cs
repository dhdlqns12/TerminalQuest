using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Manager;

namespace TerimalQuest.System
{
    public enum PotionType
    {
        HP,        // 체력 회복
        MP,        // 마나 회복
        Stamina    // 스태미나 회복
    }

    public class Potion : Item
    {
        public float healAmount { get; set; }       // 회복량
        public PotionType potiontype { get; set; }  // 포션 타입

        public Potion(int id, string name, string desc, int price, float healAmount, ItemType type, PotionType _potionType) : base(id, name, desc, price, type)
        {
            this.healAmount = healAmount;
            this.potiontype = _potionType;
        }

        public void AddPotion()
        {
            // 같은 종류의 포션 들어오면 겹치기
            count++;
        }

        public override void Equip(bool isEquip)
        {
            Player player = GameManager.Instance.player;
            player.UsePotion(this);
        }

        public override void DisplayInfo()
        {
            // 아이템 정보 표시

            string potionTypeTxt = GetPotionEffectTxt();

            Console.WriteLine(
                string.Format("{0} | {1} | {2}",
                ConsoleHelper.PadRightForConsole(name, offsetName),
                ConsoleHelper.PadRightForConsole($"{potionTypeTxt} +{healAmount}", offsetEffect),
                desc));
        }

        public void DisplayInfoProduct()
        {
            // 상품 목록에서 보여줄 아이템 정보 표시
            string itemCount = $"수량: x{count}";
            string itemPurchase = (isPurchase) ? "구매완료" : $"{price}";
            string isGoldIcon = (isPurchase) ? "" : "G";

            string potionTypeTxt = GetPotionEffectTxt();

            Console.WriteLine(
                string.Format("{0} | {1} | {2} | {3} {4}",
                ConsoleHelper.PadRightForConsole(name, offsetName),
                ConsoleHelper.PadRightForConsole($"{potionTypeTxt} +{healAmount}", offsetEffect),
                ConsoleHelper.PadRightForConsole(desc, offsetDesc),
                ConsoleHelper.PadRightForConsole(itemCount, offsetCount),
                ConsoleHelper.PadRightForConsole(itemPurchase, offsetPurchase),
                isGoldIcon));
        }

        private string GetPotionEffectTxt()
        {
            string potionTypeTxt = "";

            switch (potiontype)
            {
                case PotionType.HP:
                    potionTypeTxt = "HP 회복";
                    break;
                case PotionType.MP:
                    potionTypeTxt = "MP 회복";
                    break;
                case PotionType.Stamina:
                    potionTypeTxt = "Stamina 회복";
                    break;
                default:
                    break;
            }

            return potionTypeTxt;
        }

        // 아이템 복제
        public override Potion Clone()
        {
            this.Id += 1;   // 복제 시 Id 증가
            return new Potion(Id, name, desc, price, healAmount, type, potiontype);
        }
    }
}
