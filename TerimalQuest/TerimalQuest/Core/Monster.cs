using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.System;

namespace TerimalQuest.Core
{
    public class Monster : Character
    {
        
        public int rewardExp { get; set; }  //드랍 경험치
        public int rewardGold { get; set; } //드랍 골드
        public Monster(string name, int level, float maxHp, float maxMp, float atk, float def) : base(name, level, maxHp, maxMp, atk, def)
        {
        }

        public string[] GetIdleAnimation() => MonsterAnimation.GetAnimation(name, AnimationType.Idle);
        public string[] GetAttackAnimation() => MonsterAnimation.GetAnimation(name, AnimationType.Attack);
        public string[] GetHitAnimation() => MonsterAnimation.GetAnimation(name, AnimationType.Hit);
        public string[] GetDeathAnimation() => MonsterAnimation.GetAnimation(name, AnimationType.Death);

        public Monster Clone()
        {
            return new Monster(this.name, this.level, this.maxHp, this.maxMp, this.atk, this.def);
        }
    }
}
