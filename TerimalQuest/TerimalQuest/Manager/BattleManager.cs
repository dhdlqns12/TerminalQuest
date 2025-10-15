using System.Threading;
using TerimalQuest.Core;
using TerimalQuest.System;

namespace TerimalQuest.Manager
{
    public enum BattleState
    {
        PlayerActionSelect,
        PlayerTargetSelect,
        PlayerSelectingSkill,
        PlayerTargetSelectWithSkill,
        MonsterTurn,
        BattleOver
    }

    public class BattleManager
    {
        private bool isPlayerTurn;
        public bool isBattleProgress;
        private MonsterManager monsterManager;
        private Player player;
        private List<Monster> encounterMonsterList;
        private UIManager uiManager;
        private float oriHpPlayer;
        private Skill selectedSkill;
        public bool isTryingToEscape { get; private set; }
        public BattleState currentState { get; private set; }

        public BattleManager()
        {
            monsterManager = new MonsterManager();
            uiManager = UIManager.Instance;
            player = GameManager.Instance.player;
            player.name = "민지";
            player.jobName = "전사";
            player.atk = 10;
            player.def = 99;
            player.hp = 9999;
            player.maxHp = 9999;
            player.maxMp = 9999;
            player.mp = 9999;
            // player.critRate = 100;
            // player.evadeRate = 100;
            isTryingToEscape = false;
            List<Skill> skills = new List<Skill>();
            Skill rapidArrow = new Skill("빨리쏘기", "화살을 강하게 발사해 100의 고정데미지", 100, SkillType.Attack, SkillDamageType.FixedDamage, SkillRangeType.One,  10);
            Skill arrowRain = new Skill("화살비", "모든 적에게 공격력 *0.5의 데미지",  0.5f, SkillType.Attack,  SkillDamageType.BaseAttack, SkillRangeType.All, 15);
            Skill arrowBomb = new Skill("폭발화살", "화살에 폭탄을 장착하고 발사해 모든 적에게 공격력 * 1.5의 데미지",  1.5f, SkillType.Attack,  SkillDamageType.BaseAttack, SkillRangeType.All, 20);
            Skill eatRice = new Skill("밥먹기", "밥을 먹어 500의 체력을 회복",  500, SkillType.Support, SkillDamageType.FixedDamage, SkillRangeType.One, 10);
            Skill bash = new Skill("배쉬", "힘을 세게주어 1.5배의 데미지",  1.5f, SkillType.Attack, SkillDamageType.BaseAttack, SkillRangeType.One, 10);
            Skill powerBash = new Skill("파워배쉬", "온 힘을 다해 공격하여 공격력 5배의 데미지",  5, SkillType.Attack, SkillDamageType.BaseAttack, SkillRangeType.One,  30);
            skills.Add(rapidArrow);
            skills.Add(arrowRain);
            skills.Add(arrowBomb);
            skills.Add(eatRice);
            skills.Add(bash);
            skills.Add(powerBash);
            player.skillList = skills;
        }

        /// <summary>
        /// 배틀시작
        /// </summary>
        public void StartBattle()
        {
            isPlayerTurn = true;
            isBattleProgress = true;
            encounterMonsterList = monsterManager.CreateRandomMonsterList();
            oriHpPlayer = player.hp;
            currentState = BattleState.PlayerActionSelect;
            uiManager.BattleEntrance(encounterMonsterList, player);
            uiManager.DisplayBattleChoice();
        }

        public void BattleProcess()
        {
            if (!isBattleProgress || currentState == BattleState.BattleOver) return;

            if (currentState == BattleState.MonsterTurn)
            {
                MonsterTurn();
            }
        }

        public bool IsWaitingForInput()
        {
            return currentState == BattleState.PlayerActionSelect || currentState == BattleState.PlayerTargetSelect || currentState == BattleState.PlayerSelectingSkill || currentState == BattleState.PlayerTargetSelectWithSkill;
        }

        public bool IsBattleOver()
        {
            return !isBattleProgress;
        }

        public void ProcessPlayerInput(string input)
        {
            switch (currentState)
            {
                case BattleState.PlayerActionSelect:
                    ProcessActionSelection(input);
                    break;
                case BattleState.PlayerTargetSelect :
                    ProcessTargetSelection(input);
                    break;
                case BattleState.PlayerTargetSelectWithSkill:
                    ProcessTargetSelection(input);
                    break;
                case BattleState.PlayerSelectingSkill:
                    ProcessSkillSelection(input);
                    break;
            }
        }

        private void ProcessActionSelection(string input)
        {
            switch (input)
            {
                case "1": // Attack
                    currentState = BattleState.PlayerTargetSelect;
                    uiManager.BattleEntrance(encounterMonsterList, player);
                    uiManager.SelectTarget();
                    break;

                case "2": // Skill
                    currentState = BattleState.PlayerSelectingSkill;
                    uiManager.BattleEntrance(encounterMonsterList, player);
                    uiManager.DisplaySelectingSkill(player.skillList);
                    break;

                case "0": // Escape
                    isTryingToEscape = true;
                    isBattleProgress = false;
                    currentState = BattleState.BattleOver;
                    break;

                default:
                    uiManager.SelectWrongSelection();
                    Thread.Sleep(1000);
                    uiManager.BattleEntrance(encounterMonsterList, player);
                    uiManager.DisplayBattleChoice();
                    break;
            }
        }

        private void ProcessTargetSelection(string input)
        {
            int choice;
            if (!CheckPlayerInputInvalidTargetSelection(input, out choice))
            {
                uiManager.SelectWrongSelection();
                Thread.Sleep(1000);
                uiManager.BattleEntrance(encounterMonsterList, player);
                uiManager.SelectTarget();
            }
            else
            {
                if (choice == 0)
                {
                    currentState = BattleState.PlayerActionSelect;
                    uiManager.BattleEntrance(encounterMonsterList, player);
                    uiManager.DisplayBattleChoice();
                    return;
                }
                Monster targetMonster = encounterMonsterList[choice - 1];

                if (selectedSkill != null && currentState == BattleState.PlayerTargetSelectWithSkill)
                {
                    AttackSkill(selectedSkill, targetMonster);
                    selectedSkill = null;
                }
                else
                {
                    this.AttackTarget(player, targetMonster);
                }

                Thread.Sleep(1000);
                CheckBattleProgressByIsMonsterAlive();
                if (!isBattleProgress)
                {
                    currentState = BattleState.BattleOver;
                    return;
                }
                if (currentState != BattleState.BattleOver)
                {
                    currentState = BattleState.MonsterTurn;
                }
            }
        }

        private void ProcessSkillSelection(string input)
        {
            int choice;
            if (CheckPlayerInputInvalidSkillSelection(input, out choice))
            {
                if(choice == 0) {
                    currentState = BattleState.PlayerActionSelect;
                    uiManager.BattleEntrance(encounterMonsterList, player);
                    uiManager.DisplayBattleChoice();
                    return;
                }
                selectedSkill = player.skillList[choice - 1];
                Console.WriteLine($"선택된 스킬{selectedSkill.name}");
                UseSkill();
            }
            else
            {
                uiManager.SelectWrongSelection();
                Thread.Sleep(1000);
                uiManager.BattleEntrance(encounterMonsterList, player);
                uiManager.DisplaySelectingSkill(player.skillList);
            }
        }
        public BattleResult GetBattleResult()
        {
            return new BattleResult
            {
                isPlayerWin = (player.hp > 0),
                defeatedMonsters = encounterMonsterList,
                hpReduction = oriHpPlayer - player.hp
            };
        }

        /// <summary>
        /// 몬스터 턴진쟇ㅇ
        /// </summary>
        private void MonsterTurn()
        {
            for (int i = 0; i < encounterMonsterList.Count; i++)
            {
                Monster monster = encounterMonsterList[i];
                if (monster.hp <= 0) continue;
                this.AttackTarget(monster, player);
                Thread.Sleep(1000);

                if (player.hp <= 0)
                {
                    isBattleProgress = false;
                    currentState = BattleState.BattleOver;
                    break;
                }
            }

            if (isBattleProgress)
            {
                currentState = BattleState.PlayerActionSelect;
                uiManager.BattleEntrance(encounterMonsterList, player);
                uiManager.DisplayBattleChoice();
            }
        }
        /// <summary>
        /// 몬스터 생존 여부로 배틀 진행여부 확인
        /// </summary>
        private void CheckBattleProgressByIsMonsterAlive()
        {
            bool isMonsterAlive = false;
            for (int i = 0; i < encounterMonsterList.Count; i++)
            {
                if (encounterMonsterList[i].hp > 0)
                {
                    isMonsterAlive = true;
                    break;
                }
            }
            isBattleProgress = isMonsterAlive;
        }

        /// <summary>
        /// 플레이어 입력 유효성 검사
        /// </summary>
        private bool CheckPlayerInputInvalidTargetSelection(string input, out int choice)
        {
            if (!int.TryParse(input, out choice))
            {
                return false;
            }
            if (choice == 0) return true;
            if (choice > encounterMonsterList.Count)
            {
                return false;
            }
            return encounterMonsterList[choice - 1].hp >= 0;
        }

        private bool CheckPlayerInputInvalidSkillSelection(string input, out int choice)
        {
            if (!int.TryParse(input, out choice))
            {
                return false;
            }

            if (choice == 0) return true;
            if (choice > player.skillList.Count) return false;
            return true;
        }

        /// <summary>
        /// 타겟과 대상 정보를 받아 공격 실행
        /// </summary>
        private void AttackTarget(Character attacker, Character target) // skill 파라미터 제거
        {
            Random random = new Random();
            bool isEvade = random.NextDouble() < target.evadeRate;
            uiManager.AttackTarget(attacker, target, isEvade);
            if (isEvade) return;
            target.hp -= attacker.GetFinalDamage(out bool ignore);
        }


        /// <summary>
        /// 스킬공격
        /// </summary>
        private void AttackSkill(Skill skill, Character target = null)
        {
            if (skill.rangeType == SkillRangeType.All)
            {
                for (int i = 0; i < encounterMonsterList.Count; i++)
                {
                    Monster monster = encounterMonsterList[i];
                    if(monster.hp <= 0) continue;
            
                    if (selectedSkill.damageType == SkillDamageType.FixedDamage)
                    {
                        monster.hp -= skill.damage;
                    }
                    else if (selectedSkill.damageType == SkillDamageType.BaseAttack)
                    {
                        monster.hp -= (skill.damage * player.atk);
                    }
                }
                uiManager.DisplayFullRangeAttackSkillResult(encounterMonsterList, skill);

            }
            else if (skill.rangeType == SkillRangeType.One)
            {
                uiManager.AttackTargetWithSkill(player, target, skill);
                if (selectedSkill.damageType == SkillDamageType.FixedDamage)
                {
                    target.hp -= skill.damage;
                }
                else if (selectedSkill.damageType == SkillDamageType.BaseAttack)
                {
                    target.hp -= (skill.damage * player.atk);
                }
            }
            CheckBattleProgressByIsMonsterAlive();
            if (!isBattleProgress)
            {
                currentState = BattleState.BattleOver;
                return;
            }
            currentState = BattleState.MonsterTurn;
        }
        /// <summary>
        /// 스킬사용
        /// </summary>
        private void UseSkill()
        {
            if (selectedSkill == null)
            {
                Console.Error.WriteLine("Please select a skill.");
                return;
            }

            if (player.mp < selectedSkill.cost)
            {
                uiManager.DisplayNotEnoughMagicCost();
                Thread.Sleep(1000);
                uiManager.BattleEntrance(encounterMonsterList, player);
                uiManager.DisplaySelectingSkill(player.skillList);
                return;
            }
            player.mp -= selectedSkill.cost;
            if (selectedSkill.skillType == SkillType.Support)
            {
                uiManager.DisplayUseSupportSkill(player, selectedSkill);
                player.hp += selectedSkill.damage;
                Thread.Sleep(1000);
                currentState = BattleState.MonsterTurn;

            }else if (selectedSkill.skillType == SkillType.Attack)
            {
                if (selectedSkill.rangeType == SkillRangeType.All)
                {
                    AttackSkill(selectedSkill);
                }
                else if (selectedSkill.rangeType == SkillRangeType.One)
                {
                    currentState = BattleState.PlayerTargetSelectWithSkill;
                    uiManager.BattleEntrance(encounterMonsterList, player);
                    uiManager.SelectTarget();
                }
            }
        }
    }
}