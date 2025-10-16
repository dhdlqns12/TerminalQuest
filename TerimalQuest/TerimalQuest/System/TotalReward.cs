namespace TerimalQuest.System;

public class TotalReward
{
    public int totalRewardExp { get; set; } // 총 보상 경험치
    public int totalRewardGold { get; set; } //총 보상 골드
    public Dictionary<string, int> totalRewardItems { get; set; } // 총 보상 아이템 갯수, 아이템이름
}