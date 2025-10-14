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
        protected string job;       // 플레이어 직업

        protected int gold;         // 플레이어 골드
        protected int exp;          // 플레이어 경험치

        protected int maxStamina;   // 플레이어 최대 스태미나
        protected int stamina;      // 플레이어 스태미나

        protected List<int> questList;          // 퀘스트 리스트
        protected List<Skill> skillList;        // 스킬 리스트

        protected Inventory inventory;          // 플레이어 인벤토리

        protected int curStage;                 // 현재 스테이지

        public Player(string name, int level, string job, float maxHp, float maxMp, int maxStamina, float atk, float def) : base(name, level, maxHp, maxMp, atk, def)
        {
            this.job = job;
            this.maxStamina = maxStamina;
        }
    }
}
