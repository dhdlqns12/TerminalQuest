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
        public EnhancementStone(int id, string name, string desc, int price, ItemType type, int lastId) : base(id, name, desc, price, type, lastId)
        {
            
        }

        public override void Equip(bool isEquip)
        {
            // 강화석 소모

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
        public override Item Clone()
        {
            return new EnhancementStone(Id, name, desc, price, type);
        }
    }
}
