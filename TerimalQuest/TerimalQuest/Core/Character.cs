using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.Core
{
    public class Character
    {
        protected string name;      // 캐릭터 이름
        protected int level;        // 캐릭터 레벨

        protected float maxHp;      // 캐릭터 최대 체력
        protected float hp;         // 캐릭터 체력

        protected float maxMp;      // 캐릭터 최대 마나
        protected float mp;         // 캐릭터 마나

        protected float atk;        // 캐릭터 공격력
        protected float def;        // 캐릭터 방어력

        protected float critRate;   // 캐릭터 크리율 
        protected float evadeRate;  // 캐릭터 회피율

        public Character(string name, int level, float maxHp, float maxMp, float atk, float def) 
        {
            this.name = name;
            this.level = level;

            this.maxHp = maxHp;
            this.hp = maxHp; 
            
            this.maxMp = maxMp;
            this.mp = maxMp;

            this.def = def;
            this.atk = atk;
        }
    }
}
