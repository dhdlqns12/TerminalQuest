using System.Drawing;
using TerimalQuest.Core;

namespace TerimalQuest.Manager;

public class MonsterManager
{
    private List<Monster> totalMonsterList;
    private const int MaxMonsterCount = 3;
    private const int MinMonsterCount = 0;
    private const int FinalMaxMonsterCount = 7;
    private Random random = new Random();

    public MonsterManager()
    {
        totalMonsterList = new List<Monster>();
        CreateTotalMonsterList();
    }


    /// <summary>
    /// 랜덤 몬스터 리스트 생성
    /// </summary>
    public List<Monster> CreateRandomMonsterList(int playerStage)
    {

        List<Monster> monsterList = new List<Monster>();
        if (playerStage == 5)
        {
            monsterList.Add(CreateBossMonster(false));
            return monsterList;
        }else if (playerStage == 10)
        {
            monsterList.Add(CreateBossMonster(true));
            return monsterList;
        }
        int encounterCount = random.Next(MinMonsterCount + playerStage, MaxMonsterCount + 1 + playerStage > FinalMaxMonsterCount ? FinalMaxMonsterCount : MaxMonsterCount + 1 + playerStage);
        for (int i = 0; i < encounterCount; i++)
        {
            Monster monster = CreateRandomMonster(playerStage);
            monsterList.Add(monster);
        }
        return monsterList;
    }

    /// <summary>
    /// 랜덤 몬스터 생성
    /// </summary>
    public Monster CreateRandomMonster(int playerStage)
    {
        int randomMonsterIndex = random.Next(0, totalMonsterList.Count);
        Monster monster = totalMonsterList[randomMonsterIndex].Clone();
        monster.level += playerStage * 2;
        monster.maxHp += monster.level * 5;
        monster.hp += monster.level * 5;
        monster.atk += monster.level * 2;
        monster.def += monster.level;
        monster.evadeRate = 0.1f * playerStage;
        monster.critRate = 0.1f * playerStage;
        monster.rewardExp = 10 * playerStage;
        monster.rewardGold = 50 * playerStage;
        return monster;
    }

    /// <summary>
    /// 전체 몬스터 목록 초기화
    /// </summary>
    public void CreateTotalMonsterList()
    {
        Monster slime = new Monster("슬라임", 1,10,0,10,1);
        Monster orc = new Monster("오크", 1,10,0,15,2);
        Monster troll = new Monster("트롤", 1,10,0,20,3);
        totalMonsterList.Add(slime);
        totalMonsterList.Add(orc);
        totalMonsterList.Add(troll);
    }

    public Monster CreateBossMonster(bool isLast)
    {
        if (!isLast)
        {
            Monster kefga = new Monster("케프가", 10, 800,0,50,10);
            kefga.rewardExp = 1000;
            kefga.rewardGold = 5000;
            kefga.evadeRate = 0.3f;
            kefga.critRate = 0.5f;
            return kefga;
        }
        else
        {
            Monster labos = new Monster("라보스", 99, 1999,0,99,99);
            labos.rewardExp = 9999;
            labos.rewardGold = 9999;
            labos.evadeRate = 0.3f;
            labos.critRate = 0.5f;
            return labos;
        }
    }

}