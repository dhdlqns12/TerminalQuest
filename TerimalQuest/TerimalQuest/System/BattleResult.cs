using TerimalQuest.Core;

namespace TerimalQuest.System;

public class BattleResult
{
    public bool isPlayerWin { get; set; }
    public List<Monster> defeatedMonsters { get; set; }

    public float hpReduction { get; set; }
}
