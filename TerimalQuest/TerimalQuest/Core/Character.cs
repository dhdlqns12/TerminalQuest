using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.Core
{
    public class Character
    {
        private const int DefenseConst = 30;
        public string name { get; set; }      // 캐릭터 이름
        public int level { get; set; }          // 캐릭터 레벨

        public float maxHp { get; set; }        // 캐릭터 최대 체력
        private float _hp;

        public float hp
        {
            get { return _hp; }
            set { _hp = Math.Clamp(value, 0, maxHp); }
        }

        public float maxMp { get; set; }        // 캐릭터 최대 마나
        private float _mp;

        public float mp // 캐릭터 마나
        {
            get { return _mp; }
            set { _mp = Math.Clamp(value, 0, maxMp); }
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
            float deviation = atk * 0.1f;
            float randomDeviation = (float)(random.NextDouble() * (deviation * 2) - deviation);
            float baseDamage = atk + randomDeviation;
            isCritical = random.NextDouble() < this.critRate;
            if (isCritical)
            {
                baseDamage *= 1.6f;
            }
            float damageReduction = (float)targetDef / (targetDef + DefenseConst);
            int finalDamage = (int)Math.Ceiling(baseDamage * (1 - damageReduction));
            if (finalDamage <= 0)
            {
                finalDamage = 1;
            }
            if (this is Monster) finalDamage /= 2;
            return finalDamage;
        }
    }
}
