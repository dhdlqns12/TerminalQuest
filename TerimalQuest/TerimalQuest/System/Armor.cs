using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;

namespace TerimalQuest.System
{
    public class Armor : Item
    {
        public float def;

        public Armor(int id, string name, string desc, int price, float def, ItemType type) : base(id, name, desc, price, type)
        {
            this.def = def;
        }

        public override void Equip()
        {
            base.Equip();

            // 플레이어 방어구 장착
            
        }

        public override void DisplayInfo()
        {
            // 아이템 정보 표시
            //Console.WriteLine($"{name}")
        }
    }
}
