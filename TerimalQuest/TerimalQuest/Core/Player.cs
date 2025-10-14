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
        public string jobName { get; set; }           // 플레이어 직업

        public int gold { get; set; }           // 플레이어 골드
        public int exp { get; set; }            // 플레이어 경험치
        public int maxStamina { get; set; }     // 플레이어 최대 스태미나
        protected int stamina;      // 플레이어 스태미나

        public List<int> questList { get; set; }            // 퀘스트 리스트
        public List<Skill> skillList { get; set; }          // 스킬 리스트

        protected Inventory inventory;          // 플레이어 인벤토리

        protected int curStage;                 // 현재 스테이지

        public Player() : base()                // 역직렬화용 생성자
        {
            questList = new List<int>();
            skillList = new List<Skill>();
        }

        public Player(string name, Job _job): base(name, 1, 0, 0, 0, 0)      // 일반  생성자
        {
            job = _job;
            gold = 0;
            curStage = 0;
        }
    }
}
