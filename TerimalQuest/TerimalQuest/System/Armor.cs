using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Manager;

namespace TerimalQuest.System
{
    public class Armor : Item
    {
        public float def { get; set; }
        public int enhancementLevel { get; set; }

        public Armor(int id, string name, string desc, int price, float def, ItemType type) : base(id, name, desc, price, type)
        {
            this.def = def;
            this.enhancementLevel = 0;
        }

        public override void Equip(bool isEquip)
        {
            //아이템 장착
            base.Equip(isEquip);

            // 플레이어 방어구 장착
            GameManager.Instance.player.ToggleEquipItem(this);
        }

        // 아이템 효과 가져오기
        public override string GetEffectText()
        {
            return $"방어력 +{def}";
        }

        public override void Enhance(float enhanceValue)
        {
            // 강화 레벨 증가 후 방어력 증가
            enhancementLevel++;
            def += enhanceValue;
            QuestManager.Instance.PlayQuest("강화");
        }

        public override void Degrade(float degradeValue)
        {
            // 강화 레벨 하락 후 방어력 감소
            enhancementLevel--;
            def -= degradeValue;
        }

        public override void DisplayInfo(string idxTxt)
        {
            // 아이템 정보 표시
            UIManager.Instance.DisplayItemInfo(this, idxTxt);
        }

        public override void DisplayInfoProduct(string idxTxt)
        {
            // 상품 목록에서 보여줄 아이템 정보 표시
            UIManager.Instance.DisplayItemProduct(this, idxTxt);
        }

        // 강화 레벨 가져오기
        public override int GetLevel() => enhancementLevel;

        // 아이템 복제
        public override Item Clone()
        {
            Armor armor = new Armor(ItemDatabase.GetLastId(Id), name, desc, price, def, type);

            // 강화 레벨 포함
            armor.enhancementLevel = enhancementLevel;

            return armor;
        }
    }
}
