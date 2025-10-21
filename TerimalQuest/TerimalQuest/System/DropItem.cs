namespace TerimalQuest.System;

public class DropItem
{
    public string dropMonsterName { get; set; } //드랍 몬스터명
    public string itemName { get; set; } //드랍아이템
    public int maxDropCount { get; set; } // 최대 드랍갯수
    public int minDropCount { get; set; } // 최소 드랍갯수
    public float dropRate { get; set; } //드랍확률

    public DropItem()
    {

    }

    public DropItem(string dropMonsterName, string itemName, int maxDropCount, int minDropCount, float dropRate)
    {
        this.dropMonsterName = dropMonsterName;
        this.itemName = itemName;
        this.maxDropCount = maxDropCount;
        this.minDropCount = minDropCount;
        this.dropRate = dropRate;
    }
}