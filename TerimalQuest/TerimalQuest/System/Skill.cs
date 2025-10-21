using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.System
{
    public class Skill
    {
        public string name {get; set;} //스킬명
        public string description {get; set;} //스킬 설명
        public float damage {get; set;} // 데미지
        public SkillType skillType {get; set;} //스킬타입
        public SkillDamageType damageType {get; set;} //스킬 데미지타입
        public SkillRangeType rangeType {get; set;} //스킬범위
        public int cost {get; set;} //소비엠피

        public Skill(string name, string description, float damage, SkillType skillType, SkillDamageType damageType, SkillRangeType rangeType, int cost)
        {
            this.name = name;
            this.description = description;
            this.damage = damage;
            this.skillType = skillType;
            this.damageType = damageType;
            this.rangeType = rangeType;
            this.cost = cost;
        }
    }
    public enum SkillType
    {
        Attack,
        Support
    }

    public enum SkillDamageType
    {
        BaseAttack,
        FixedDamage,
    }

    public enum SkillRangeType
    {
        One,
        All
    }
}
