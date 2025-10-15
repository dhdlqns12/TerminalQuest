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
            maxHp = 150f;
            maxMp = 50f;
            atk = 10f;
            def = 15f;
            critRate = 0.1f;
            evadeRate = 0.1f;
            //기본 스킬 추가
        }

        private void InitArcher()
        {
            name = "궁수";
            description = "낮은 체방과 높은 공격력을 지닌 원거리 전투 전문가";
            maxHp = 100f;
            maxMp = 100f;
            atk = 20f;
            def = 5f;
            critRate = 0.3f;
            evadeRate = 0.2f;
            //기본 스킬 추가
        }

        private void InitMage()
        {
            name = "마법사";
            description = "매우 낮은 체방과 매우 높은 공격력을 지닌 원거리 전투 전문가";
            maxHp = 75;
            maxMp = 150;
            atk = 25;
            def = 0f;
            critRate = 0.1f;
            evadeRate = 0f;
            //기본 스킬 추가
        }
        #endregion
    }
}
