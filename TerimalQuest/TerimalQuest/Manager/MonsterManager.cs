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

    public Monster CreateRandomMonster()
    {
        int randomMonsterIndex = random.Next(0, totalMonsterList.Count);
        return totalMonsterList[randomMonsterIndex].Clone();
    }

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