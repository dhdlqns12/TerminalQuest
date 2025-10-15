using TerimalQuest.Core;
using TerimalQuest.System;

namespace TerimalQuest.Manager;

public class BattleManager
{
    private bool isPlayerTurn;
    private bool isBattleProgress;
    private MonsterManager monsterManager;
    private Player player;
    private List<Monster> encounterMonsterList;
    private UIManager uiManager;
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
        // player.critRate = 100;
        // player.evadeRate = 100;
    }

    /// <summary>
    /// 배틀시작
    /// </summary>
    public async Task<BattleResult> StartBattle()
    {
        isPlayerTurn = true;
        isBattleProgress = true;
        encounterMonsterList = monsterManager.CreateRandomMonsterList();
        float oriHpPlayer = player.hp;
        while (isBattleProgress)
        {
            CheckBattleProgressByIsMonsterAlive();
            if(isPlayerTurn) await PlayerTurn();
            else await MonsterTurn();
        }
        return new BattleResult
        {
            isPlayerWin = (player.hp > 0),
            defeatedMonsters = encounterMonsterList,
            hpReduction = oriHpPlayer - player.hp
        };
    }

    /// <summary>
    /// 플레이어 턴 진행
    /// </summary>
    private async Task PlayerTurn()
    {
        if (!isPlayerTurn) return;
        uiManager.BattleEntrance(encounterMonsterList, player);
        uiManager.SelectTarget();

        int choice;
        while (true)
        {
            string input = Console.ReadLine();
            if (CheckPlayerInputInvalid(input, out choice))
            {
                uiManager.SelectWrongSelection();
                await Task.Delay(1000);
                uiManager.BattleEntrance(encounterMonsterList, player);
                uiManager.SelectTarget();
            }
            else
            {
                break;
            }
        }

        Monster targetMonster = encounterMonsterList[choice - 1];
        this.AttackTarget(player, targetMonster);
        uiManager.WaitNextChoice();

        while (true)
        {
            string turnEndInput = Console.ReadLine();
            if (turnEndInput == "0")
            {
                break;
            }
            uiManager.SelectWrongSelection();
        }

        isPlayerTurn = false;
    }
    /// <summary>
    /// 몬스터 턴진쟇ㅇ
    /// </summary>
    private async Task MonsterTurn()
    {
        if (isPlayerTurn) return;
        for (int i = 0; i < encounterMonsterList.Count; i++)
        {
            Monster monster = encounterMonsterList[i];
            if (monster.hp < 0) continue;
            this.AttackTarget(monster, player);
            await Task.Delay(1000);

            if (player.hp <= 0)
            {
                isBattleProgress = false;
                break;
            }
        }

        isPlayerTurn = true;
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
    private bool CheckPlayerInputInvalid(string input, out int choice)
    {
        if (!int.TryParse(input, out choice))
        {
            return true;
        }

        if (choice < 1 || choice > encounterMonsterList.Count)
        {
            return true;
        }
        return encounterMonsterList[choice - 1].hp <= 0;
    }

    /// <summary>
    /// 타겟과 대상 정보를 받아 공격 실행
    /// </summary>
    private void AttackTarget(Character attacker, Character target)
    {
        Random random = new Random();
        bool isEvade = random.NextDouble() < target.evadeRate;
        uiManager.AttackTarget(attacker, target, isEvade);
        if (isEvade) return;
        if(attacker is Player) target.hp -= player.GetFinalDamage(out bool isCritical);
        else target.hp -= attacker.atk;
    }
}