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
        public PotionType potionType { get; set; }  // 포션 타입

        public Potion() { }

        public Potion(int id, string name, string desc, int price, float healAmount, ItemType type, PotionType _potionType) : base(id, name, desc, price, type)
        {
            this.healAmount = healAmount;
            this.potionType = _potionType;
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

        public override string GetEffectText()
        {
            string potionTypeTxt = "";

            switch (potionType)
            {
                case PotionType.HP:
                    potionTypeTxt = "HP";
                    break;
                case PotionType.MP:
                    potionTypeTxt = "MP";
                    break;
                case PotionType.Stamina:
                    potionTypeTxt = "Stamina";
                    break;
                default:
                    break;
            }

            return $"{potionTypeTxt} + {healAmount}"; ;
        }

        public override void DisplayInfo()
        {
            // 아이템 정보 표시
            UIManager.Instance.DisplayItemInfo(this);
        }

        public override void DisplayInfoProduct()
        {
            // 상품 목록에서 보여줄 아이템 정보 표시
            UIManager.Instance.DisplayItemProduct(this);
        }

        // 아이템 복제
        public override Potion Clone()
        {
            return new Potion(Id, name, desc, price, healAmount, type, potionType);
        }
    }
}
