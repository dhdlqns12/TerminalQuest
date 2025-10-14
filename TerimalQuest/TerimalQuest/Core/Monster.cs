using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.Core
{
    public class Monster : Character
    {
        public Monster(string name, int level, float maxHp, float maxMp, float atk, float def) : base(name, level, maxHp, maxMp, atk, def)
        {
        }
    }
}
