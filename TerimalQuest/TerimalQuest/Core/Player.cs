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
        public int curStage;                 // 현재 스테이지

        public List<int> questList { get; set; }            // 퀘스트 리스트
        public List<Skill> skillList { get; set; }          // 스킬 리스트

        public Inventory inventory;          // 플레이어 인벤토리

        public Weapon equippedWeapon { get; private set; }
        public Armor equippedArmor { get; private set; }

        public float baseAtk{ get; set; } //아이템 장착하지  않았을 때의 플레이어 공격력
        public float baseDef { get; set; } //아이템 장착하지 않았을 때의 플레이어 방어력
        public float baseCritRate{ get; set; } //아이템 장착하지 않았을 때의 플레이어 치명타 확률
        public float baseEvadeRate { get; set; } //아이템 장착하지 않았을 때의 플레이어 회피 확률

        public Player() : base()                //기본 생성자
        {
            questList = new List<int>();
            skillList = new List<Skill>();
            inventory = new Inventory(50);
            level = 1;
            maxStamina = 100;
            gold = 0;
            exp = 0;
            curStage = 1;

            Set_Item_Inveontory();
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

        public void Set_Item_Inveontory()
        {
            inventory.Add(ItemDatabase.GetArmor("무쇠갑옷"));
            inventory.Add(ItemDatabase.GetWeapon("낡은 검"));
            inventory.Add(ItemDatabase.GetWeapon("연습용 창"));
        }

        private void ApplyJobStat()
        {
            maxHp = job.maxHp;
            hp = job.maxHp;
            maxMp = job.maxMp;
            mp = job.maxMp;
            baseAtk = job.atk;
            baseDef = job.def;
            critRate = job.critRate;
            evadeRate = job.evadeRate;

            UpdateStats();
        }

        public void UpdateStats()
        {
            atk = baseAtk;
            def = baseDef;

            if(equippedWeapon!=null)
            {
                atk += equippedWeapon.atk;
            }

            if (equippedArmor != null)
            {
                def += equippedArmor.def;
            }
        }
    }
}
