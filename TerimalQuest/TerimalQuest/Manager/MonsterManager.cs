using TerimalQuest.Core;

namespace TerimalQuest.Manager;

public class MonsterManager
{
    private List<Monster> totalMonsterList;
    private const int MaxMonsterCount = 4;
    private const int MinMonsterCount = 1;
    private Random random = new Random();

    public MonsterManager()
    {
        totalMonsterList = new List<Monster>();
        CreateTotalMonsterList();
    }


    /// <summary>
    /// 랜덤 몬스터 리스트 생성
    /// </summary>
    public List<Monster> CreateRandomMonsterList()
    {
        List<Monster> monsterList = new List<Monster>();
        int encounterCount = random.Next(MinMonsterCount, MaxMonsterCount+1);
        for (int i = 0; i < encounterCount; i++)
        {
            Monster monster = CreateRandomMonster();
            monsterList.Add(monster);
        }
        return monsterList;
    }
    /// <summary>
    /// 랜덤 몬스터 생성
    /// </summary>
    public Monster CreateRandomMonster()
    {
        int randomMonsterIndex = random.Next(0, totalMonsterList.Count);
        Monster monster = totalMonsterList[randomMonsterIndex].Clone();
        monster.evadeRate = 10;
        monster.critRate = 15;
        monster.rewardExp = 10;
        monster.rewardGold = 10;
        return monster;
    }
    /// <summary>
    /// 전체 몬스터 목록 초기화
    /// </summary>
    public void CreateTotalMonsterList()
    {
        Monster minion = new Monster("미니언", 2,15,0,5,0);
        Monster voidInsect = new Monster("공허충", 2,15,0,5,0);
        Monster cannonMinion = new Monster("대포미니언", 2,15,0,5,0);
        totalMonsterList.Add(minion);
        totalMonsterList.Add(voidInsect);
        totalMonsterList.Add(cannonMinion);
    }

}