using TerimalQuest.Core;
using TerimalQuest.System;

namespace TerimalQuest.Manager;

public class RecodeManager
{
    private static RecodeManager instance;
    public static RecodeManager Instance => instance ??= new RecodeManager();
    private UIManager uiManager;
    public int totalDamage { get; set; }
    public int totalDamageTaken { get; set; }

    public List<UsedSkillRecord> usedSkillRecords { get; set; }

    public Dictionary<string ,int> defeatedMonsterList  { get; set; }
    public Player clearPlayer  { get; set; }

    public RecodeManager()
    {
        clearPlayer = new Player();
        uiManager = UIManager.Instance;
        defeatedMonsterList = new Dictionary<string ,int>();
        usedSkillRecords = new List<UsedSkillRecord>();
    }

    public void RecordTotalDamage(int atkDamage)
    {
        this.totalDamage += atkDamage;
    }


    public void RecordTotalTakenDamage(int totalDamageTaken)
    {
        this.totalDamageTaken += totalDamageTaken;
    }

    public void SaveClearPlayer(Player player)
    {
        clearPlayer.jobName = player.jobName;
        clearPlayer.name = player.name;
        clearPlayer.level = player.level;
        clearPlayer.exp = player.exp;
    }

    public void RecordDefeatedMonsters(List<Monster> monsterList)
    {
        foreach (Monster monster in monsterList)
        {
            if (defeatedMonsterList.ContainsKey(monster.name))
            {
                defeatedMonsterList[monster.name]++;
            }
            else
            {
                defeatedMonsterList.Add(monster.name, 1);
            }
        }
    }

    public void RecordSkillUse(Skill skill, int finalDamage)
    {
        RecordTotalDamage(finalDamage);
        foreach (UsedSkillRecord record in usedSkillRecords)
        {
            if (record.skillName == skill.name)
            {
                record.skillUseCount++;
                record.totalDamage += finalDamage;
                return;
            }
        }
        UsedSkillRecord newRecord = new UsedSkillRecord
        {
            skillName = skill.name,
            skillUseCount = 1,
            totalDamage = finalDamage
        };
        usedSkillRecords.Add(newRecord);

    }


    public void ShowEnding()
    {
        uiManager.DisplayShowEnding();
    }

    public class UsedSkillRecord()
    {
        public string skillName { get; set; }
        public int skillUseCount { get; set; }
        public int totalDamage { get; set; }
    }
}