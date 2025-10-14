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
            base.Equip();

            // 플레이어 방어구 장착

        }
    }
}
