using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.System;

namespace TerimalQuest.Core
{
    public enum JobType
    {
        전사,
        궁수,
        마법사
    }

    public class Job
    {
        public JobType jobType { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public float maxHp { get; set; }
        public float maxMp { get; set; }

        public float atk { get; set; }
        public float def { get; set; }

        public float critRate { get; set; }
        public float evadeRate { get; set; }

        public List<Skill> DefaultSkills { get; set; }          //기본 스킬(나중에 추가 위해 작성)

        protected Job()                                            // 역직렬화용 생성자
        {
            DefaultSkills = new List<Skill>();
        }

        public Job(JobType _jobType)
        {
            jobType = _jobType;
            DefaultSkills = new List<Skill>();
            Init(jobType);
        }

        protected void Init(JobType _jobType)
        {
            switch(_jobType)
            {
                case JobType.전사:
                    InitWarrior();
                    break;
                case JobType.궁수:
                    InitArcher();
                    break;
                case JobType.마법사:
                    InitMage();
                    break;
            }
        }

        #region 직업별초기화
        private void InitWarrior()
        {
            name = "전사";
            description = "높은 체력과 높은 방어력을 가진 근접 전투  전문가";
            maxHp = 200f;
            maxMp = 80f;
            atk = 15f;
            def = 15f;
            critRate = 0.15f;
            evadeRate = 0.08f;
            //기본 스킬 추가
            Skill eatRice = new Skill("밥먹기", "밥을 먹어 100의 체력을 회복",  100, SkillType.Support, SkillDamageType.FixedDamage, SkillRangeType.One, 10);
            Skill bash = new Skill("배쉬", "힘을 세게주어 적에게 공격력 *1.5의 데미지",  1.5f, SkillType.Attack, SkillDamageType.BaseAttack, SkillRangeType.One, 10);
            Skill powerBash = new Skill("파워배쉬", "온 힘을 다해 공격하여 적에게 공격력 *2.5의 데미지",  2.5f, SkillType.Attack, SkillDamageType.BaseAttack, SkillRangeType.One,  30);
            DefaultSkills.Add(eatRice);
            DefaultSkills.Add(bash);
            DefaultSkills.Add(powerBash);
        }

        private void InitArcher()
        {
            name = "궁수";
            description = "낮은 체방과 높은 공격력을 지닌 원거리 전투 전문가";
            maxHp = 130f;
            maxMp = 120f;
            atk = 22f;
            def = 8f;
            critRate = 0.3f;
            evadeRate = 0.2f;
            //기본 스킬 추가
            Skill rapidArrow = new Skill("빨리쏘기", "화살을 강하게 발사해 50의 고정데미지",  50, SkillType.Attack, SkillDamageType.FixedDamage, SkillRangeType.One,  10);
            Skill arrowRain = new Skill("화살비", "화살비를 내려 모든 적에게 공격력 *0.9의 데미지",  0.9f, SkillType.Attack,  SkillDamageType.BaseAttack, SkillRangeType.All, 20);
            Skill arrowBomb = new Skill("폭발화살", "화살에 폭탄을 장착하고 발사해 적에게 공격력 *2의 데미지",  2f, SkillType.Attack,  SkillDamageType.BaseAttack, SkillRangeType.One, 50);
            DefaultSkills.Add(rapidArrow);
            DefaultSkills.Add(arrowRain);
            DefaultSkills.Add(arrowBomb);
        }

        private void InitMage()
        {
            name = "마법사";
            description = "매우 낮은 체방과 매우 높은 공격력을 지닌 원거리 전투 전문가";
            maxHp = 100;
            maxMp = 200;
            atk = 28;
            def = 3;
            critRate = 0.12f;
            evadeRate = 0.08f;
            //기본 스킬 추가
            Skill blizzard = new Skill("블리자드", "모든 적에게 냉기를 발산해 100의 고정데미지",  100, SkillType.Attack, SkillDamageType.FixedDamage, SkillRangeType.All, 40);
            Skill meteor = new Skill("메테오", "모든 적에게 운석을 떨어뜨려 180의 고정데미지",  300, SkillType.Attack, SkillDamageType.FixedDamage, SkillRangeType.All, 200);
            Skill ragnarok = new Skill("라그나로크", "암흑 에너지를 발산시켜 적에게 공격력 *1.5의 데미지", 1.5f, SkillType.Attack, SkillDamageType.BaseAttack, SkillRangeType.One, 80);
            DefaultSkills.Add(blizzard);
            DefaultSkills.Add(meteor);
            DefaultSkills.Add(ragnarok);
        }
        #endregion
    }
}
