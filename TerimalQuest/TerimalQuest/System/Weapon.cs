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
        public int enhancementLevel { get; set; }

        public Weapon(int id, string name, string desc, int price, float atk, ItemType type) : base(id, name, desc, price, type)
        {
            this.atk = atk;
            this.enhancementLevel = 0;
        }

        public override void Equip(bool isEquip)
        {
            //아이템 장착

            base.Equip(isEquip);

            // 플레이어 무기 장착
            GameManager.Instance.player.ToggleEquipItem(this);
        }

        public override string GetEffectText()
        {
            return $"공격력 +{atk}";
        }

        public override void Enhance(float enhanceValue)
        {
            // 강화 레벨 증가 후 방어력 증가
            enhancementLevel++;
            atk += enhanceValue;
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

        public override int GetLevel() => enhancementLevel;

        // 아이템 복제
        public override Item Clone()
        {
            return new Weapon(Id, name, desc, price, atk, type);
        }
    }
}
