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

    public class Player : Character
    {
        public JobType jobType { get; set; }         // 플레이어 직업
        public string jobName { get; set; }

        public int gold { get; set; }           // 플레이어 골드
        public int exp { get; set; }            // 플레이어 경험치

        public int maxStamina { get; set; }     // 플레이어 최대 스태미나
        protected int stamina;      // 플레이어 스태미나

        public List<int> questList { get; set; }            // 퀘스트 리스트
        public List<Skill> skillList { get; set; }          // 스킬 리스트

        protected Inventory inventory;          // 플레이어 인벤토리

        protected int curStage;                 // 현재 스테이지

        public Player() : base()
        {
            questList = new List<int>();
            skillList = new List<Skill>();
        }

        public Player(string name, JobType jobType): base(name, 1, 0, 0, 0, 0)
        {
            jobType = jobType;
            jobName = jobType.ToString();
            gold = 1000;
            exp = 0;
            questList = new List<int>();
            skillList = new List<Skill>();
            curStage = 0;
        }
    }
}
