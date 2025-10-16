using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.Core
{
    public class Character
    {
        public string name { get; set; }      // 캐릭터 이름
        public int level { get; set; }          // 캐릭터 레벨

        public float maxHp { get; set; }        // 캐릭터 최대 체력
        private float _hp;

        public float hp
        {
            get { return _hp; }
            set { _hp = Math.Max(0, value); }
        }

        public float maxMp { get; set; }        // 캐릭터 최대 마나
        private float _mp;

        public float mp // 캐릭터 마나
        {
            get { return _mp; }
            set { _mp = Math.Max(0, value); }
        }

        public float atk { get; set; }          // 캐릭터 공격력
        public float def { get; set; }          // 캐릭터 방어력

        public float critRate { get; set; }     // 캐릭터 크리율 
        public float evadeRate { get; set; }    // 캐릭터 회피율

        public Character() { }

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

        public int GetFinalDamage(out bool isCritical, int targetDef)
        {
            Random random = new Random();
            isCritical = random.NextDouble() < this.critRate;
            float deviation = (float)Math.Ceiling(atk * 0.1);
            int minDamage = (int)Math.Ceiling(atk - deviation);
            int maxDamage = (int)Math.Ceiling(atk + deviation);
            int finalDamage = random.Next(minDamage, maxDamage + 1) - targetDef;
            if(finalDamage <= 0) finalDamage = 1;
            if (isCritical)
            {
                finalDamage = (int)(finalDamage * 1.6);
            }
            return  finalDamage;
        }
    }
}
