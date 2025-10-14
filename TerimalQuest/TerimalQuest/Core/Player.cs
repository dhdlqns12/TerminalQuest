using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.System;

namespace TerimalQuest.Core
{
    public class Player : Character
    {
        public Job job { get; set; }                    // Job객체
        public string jobName { get; set; }           // 플레이어 직업 이름

        public int gold { get; set; }           // 플레이어 골드
        public int exp { get; set; }            // 플레이어 경험치
        public int maxStamina { get; set; }     // 플레이어 최대 스태미나
        public int stamina;      // 플레이어 스태미나

        public List<int> questList { get; set; }            // 퀘스트 리스트
        public List<Skill> skillList { get; set; }          // 스킬 리스트

        protected Inventory inventory;          // 플레이어 인벤토리

        public int curStage;                 // 현재 스테이지

        public Player() : base()                //기본 생성자
        {
            questList = new List<int>();
            skillList = new List<Skill>();
            level = 1;
            maxStamina = 100;
            gold = 0;
            exp = 0;
            curStage = 1;
        }

        public void Init_Player_Name(string _name)
        {
            name = _name;
        }

        public void Init_Player_job( Job _job)
        {
            job = _job;
            jobName = _job.name;

            ApplyJobStat();
        }

        private void ApplyJobStat()
        {
            maxHp = job.maxHp;
            hp = job.maxHp;
            maxMp = job.maxMp;
            mp = job.maxMp;
            atk = job.atk;
            def = job.def;
            critRate = job.critRate;
            evadeRate = job.evadeRate;
            //크리나 회피율 추가
        }
    }
}
