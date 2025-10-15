using TerimalQuest.Core;

namespace TerimalQuest.Manager;

public class MonsterManager
{
    private List<Monster> totalMonsterList;
    private const int MaxMonsterCount = 3;
    private const int MinMonsterCount = 0;
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
        int encounterCount = random.Next(MinMonsterCount + playerStage, MaxMonsterCount + 1 + playerStage);
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
        monster.rewardGold = 10 * playerStage;
        return monster;
    }
    /// <summary>
    /// 전체 몬스터 목록 초기화
    /// </summary>
    public void CreateTotalMonsterList()
    {
        Monster minion = new Monster("미니언", 1,10,0,10,1);
        Monster voidInsect = new Monster("공허충", 1,10,0,15,2);
        Monster cannonMinion = new Monster("대포미니언", 1,10,0,20,3);
        totalMonsterList.Add(minion);
        totalMonsterList.Add(voidInsect);
        totalMonsterList.Add(cannonMinion);
    }

}