using TerimalQuest.Core;

namespace TerimalQuest.Manager;

public class BattleManager
{
    private bool isPlayerTurn;
    private bool isBattleProgress;
    private MonsterManager monsterManager;
    private Player player;
    private List<Monster> encounterMonsterList;

    public BattleManager()
    {
        monsterManager = new MonsterManager();

        player = new Player("우탄이",JobType.전사);
        player.atk = 10;
        player.def = 99;
        player.hp = 1;
        player.maxHp = 9999;
    }

    public async Task StartBattle()
    {
        isPlayerTurn = true;
        isBattleProgress = true;
        encounterMonsterList = monsterManager.CreateRandomMonsterList();
        while (isBattleProgress)
        {
            CheckBattleProgress();
            if(isPlayerTurn) await PlayerTurn();
            else await MonsterTurn();
        }
        EndBattle();
    }

    public void EndBattle()
    {
        if(player.hp > 0) Console.Write("전투종료!!! \n플레이어승리!");
        else Console.Write("전투종료!!! \n플레이어패배!");
    }

    public async Task PlayerTurn()
    {
        if (!isPlayerTurn) return;
        TempDisplayBattleUI();
        Console.WriteLine("대상을 선택해주세요.\n>>");

        int choice;
        while (true)
        {
            string input = Console.ReadLine();
            if (CheckPlayerInputInvalid(input, out choice))
            {
                Console.WriteLine("잘못된 입력입니다.");
                await Task.Delay(1000);
                TempDisplayBattleUI();
                Console.WriteLine("대상을 선택해주세요.\n>>");
            }
            else
            {
                break;
            }
        }

        Monster targetMonster = encounterMonsterList[choice - 1];

        float deviation = (float)Math.Ceiling(player.atk * 0.1);
        int minDamage = (int)Math.Ceiling(player.atk - deviation);
        int maxDamage = (int)Math.Ceiling(player.atk + deviation);
        int finalDamage = new Random().Next(minDamage, maxDamage + 1);


        Console.Clear();
        Console.WriteLine($"{player.name}의 공격!");
        Console.WriteLine($"Lv.{targetMonster.level} {targetMonster.name} 을(를) 맞췄습니다. [데미지 :{finalDamage}]");
        Console.WriteLine($"Lv.{targetMonster.level} {targetMonster.name}");
        Console.WriteLine($"HP {targetMonster.hp} -> {(targetMonster.hp - finalDamage > 0 ? targetMonster.hp - finalDamage : "Dead")}");
        Console.WriteLine();
        Console.WriteLine("0. 다음");
        targetMonster.hp -= finalDamage;

        while (true)
        {
            string turnEndInput = Console.ReadLine();
            if (turnEndInput == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.Write(">> ");
            }
        }

        isPlayerTurn = false;
    }

    public async Task MonsterTurn()
    {
        if (isPlayerTurn) return;
        for (int i = 0; i < encounterMonsterList.Count; i++)
        {
            Monster monster = encounterMonsterList[i];
            if (monster.hp < 0) continue;
            Console.Clear();
            Console.WriteLine("Battle!");
            Console.WriteLine($"Lv.{monster.level} {monster.name} 의 공격!");
            Console.WriteLine($"{player.name} 을(를) 맞췄습니다. [데미지 : {monster.atk}]");
            Console.WriteLine($"Lv. {player.level} {player.name}");
            Console.WriteLine($"HP {player.hp} -> {(player.hp - monster.atk > 0 ? player.hp - monster.hp : 0)}");
            await Task.Delay(1000);
            player.hp -= monster.atk;
            if (player.hp <= 0)
            {
                isBattleProgress = false;
                break;
            }
        }

        isPlayerTurn = true;
    }

    public void CheckBattleProgress()
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

    public void TempDisplayBattleUI()
    {
        Console.Clear();
        Console.WriteLine("Battle!!\n");
        for (int i = 0; i < encounterMonsterList.Count; i++)
        {
            if(encounterMonsterList[i].hp > 0)
            {
                Console.Write($"{i + 1}. ");
            }
            Console.Write($"Lv.{encounterMonsterList[i].level} {encounterMonsterList[i].name}  HP {(encounterMonsterList[i].hp > 0 ? encounterMonsterList[i].hp : "Dead")}\n");
        }

        Console.WriteLine();

        Console.WriteLine("[내정보]");
        Console.WriteLine($"Lv.{player.level} {player.name} ({player.jobName})");
        Console.WriteLine($"HP {player.hp}/{player.maxHp}");
    }

    public bool CheckPlayerInputInvalid(string input, out int choice)
    {
        if (!int.TryParse(input, out choice))
        {
            return true;
        }

        if (choice < 1 || choice > encounterMonsterList.Count)
        {
            return true;
        }

        if (encounterMonsterList[choice - 1].hp <= 0)
        {
            return true;
        }

        return false;
    }
}