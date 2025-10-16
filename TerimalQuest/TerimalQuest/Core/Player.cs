using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Core
{
    public class Player : Character
    {
        public Job job { get; set; }                    // Job객체
        public string jobName { get; set; }           // 플레이어 직업 이름
        public int gold { get; set; }           // 플레이어 골드
        public int maxStamina { get; set; }     // 플레이어 최대 스태미나
        public int stamina;      // 플레이어 스태미나
        public int curStage { get; set; }                 // 현재 스테이지

        public List<Quest> questList { get; set; }            // 퀘스트 리스트
        public List<Skill> skillList { get; set; }          // 스킬 리스트

        public Inventory inventory { get; set; }          // 플레이어 인벤토리

        public Weapon equippedWeapon { get; private set; }
        public Armor equippedArmor { get; private set; }

        public float baseAtk { get; set; } //아이템 장착하지  않았을 때의 플레이어 공격력
        public float baseDef { get; set; } //아이템 장착하지 않았을 때의 플레이어 방어력
        public float baseCritRate { get; set; } //아이템 장착하지 않았을 때의 플레이어 치명타 확률
        public float baseEvadeRate { get; set; } //아이템 장착하지 않았을 때의 플레이어 회피 확률

        public int[] requiredExp= { 10, 35, 65, 100 };

        private int _exp { get; set; }            // 플레이어 경험치
        public int exp
        {
            get => _exp;
            set
            {
                _exp = value;
                Check_LevelUp();
            }
        }

        public Player() : base()                //기본 생성자
        {
            questList = new List<Quest>();
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

        public void Init_Player_job(Job _job)
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

        #region 레벨업
        public void Check_LevelUp()
        {
            if (level == 0 || job == null) //player초기화 안됬을때(로드에서 오류 생김)
                return;

            while (level < requiredExp.Length + 1 && exp >= requiredExp[level-1])
            {
                _exp -= requiredExp[level - 1];

                LevelUp();

                Console.WriteLine($"레벨업! Lv.{level}");

                if(level>=requiredExp.Length+1)
                {
                    Console.WriteLine("최대 레벨");
                    break;
                }
            }

            UpdateStats();
        }

        private void LevelUp()
        {
            level++;
            baseAtk += 0.5f;
            baseDef += 1f;
            QuestManager.Instance.PlayQuest("레벨", 1);
        }

        public void SetExpWithoutLevelUp(int value) //로드 전용
        {
            _exp = value;
        }
        #endregion

        #region 스탯
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
            skillList = job.DefaultSkills;

            UpdateStats();
        }

        public void UpdateStats()
        {
            atk = baseAtk;
            def = baseDef;
            critRate = baseCritRate; //나중에 아이템에 치명타 또는 회피 확률 생기면 삽입
            evadeRate = baseEvadeRate;

            if (equippedWeapon != null)
            {
                atk += equippedWeapon.atk;
            }

            if (equippedArmor != null)
            {
                def += equippedArmor.def;
            }
        }
        #endregion

        #region 아이템 장착 관리
        public void ToggleEquipItem(Item item)
        {
            if (item is Weapon weapon)
            {
                ToggleEquipWeapon(weapon);
            }
            else if(item is Armor armor)
            {
                ToogleEquipArmor(armor);
            }
        }

        private void ToggleEquipWeapon(Weapon  weapon)
        {
            if(weapon.isEquipped)
            {
                if(equippedWeapon!=null&&equippedWeapon!=weapon)
                {
                    equippedWeapon.isEquipped = false;
                }

                equippedWeapon = weapon;
                UpdateStats();
            }
            else
            {
                equippedWeapon = null;
                UpdateStats();
            }
        }

        private void ToogleEquipArmor(Armor armor)
        {
            if (armor.isEquipped)
            {
                if (equippedArmor != null && equippedArmor != armor)
                {
                    equippedWeapon.isEquipped = false;
                }

                equippedArmor = armor;
                UpdateStats();
            }
            else
            {
                equippedArmor = null;
                UpdateStats();
            }
        }

        #endregion

        #region 소비 아이템 사용
        public void UsePotion(Potion potion)
        {
            if (!IsUsePotion(potion)) // 체크
            {
                return;
            }

            Heal_Potion(potion); // 회복

            potion.count--;
            if (potion.count <= 0)
            {
                inventory.Remove(potion); // 제거
            }
        }

        private bool IsUsePotion(Potion potion)
        {
            switch (potion.potiontype)
            {
                case PotionType.HP:
                    if (hp >= maxHp)
                    {
                        Console.WriteLine("HP최대");
                        return false;
                    }
                    break;
                case PotionType.MP:
                    if (mp >= maxMp)
                    {
                        Console.WriteLine("MP최대");
                        return false;
                    }
                    break;
                case PotionType.Stamina:
                    if (stamina >= maxStamina)
                    {
                        Console.WriteLine("스태미나최대");
                        return false;
                    }
                    break;
            }
            return true;
        }

        private void Heal_Potion(Potion potion)
        {
            switch (potion.potiontype)
            {
                case PotionType.HP:
                    hp = Math.Min(hp + potion.healAmount, maxHp);
                    break;

                case PotionType.MP:
                    mp = Math.Min(mp + potion.healAmount, maxMp);
                    break;

                case PotionType.Stamina:
                    stamina = (int)Math.Min(stamina + potion.healAmount, maxStamina);
                    break;
            }
        }
        #endregion
    }
}
