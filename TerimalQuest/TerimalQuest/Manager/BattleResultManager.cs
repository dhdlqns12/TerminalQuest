using TerimalQuest.Core;
using TerimalQuest.System;

namespace TerimalQuest.Manager
{
    public class BattleResultManager
    {
        private Player player;
        private UIManager uiManager;
        private TotalReward totalReward;
        private Dictionary<string, List<DropItem>> dropTable;
        private Random random = new Random();
        public BattleResultManager()
        {
            player = GameManager.Instance.player;
            uiManager = UIManager.Instance;
            InitializeDropTable();
        }
        /// <summary>
        /// 전투 결과
        /// </summary>
        public void ProcessResult(BattleResult result)
        {
            totalReward = new TotalReward();
            uiManager.DisplayBattleResult(result, player);
            if (result.isPlayerWin)
            {
                ProcessFixedReward(result.defeatedMonsters);
                ProcessRandomReward(result.defeatedMonsters);
                uiManager.DisplayBattleRewardResult(totalReward);
            }
            uiManager.WaitNextChoice();

        }

        /// <summary>
        /// 고정 보상 획득
        /// </summary>
        private void ProcessFixedReward(List<Monster> defeatedMonsters)
        {
            int totalRewardExp = 0;
            int totalRewardGold = 0;
            for (int i = 0; i < defeatedMonsters.Count; i++)
            {
                totalRewardExp += defeatedMonsters[i].rewardExp;
                totalRewardGold += defeatedMonsters[i].rewardGold;
            }
            totalReward.totalRewardExp = totalRewardExp;
            totalReward.totalRewardGold = totalRewardGold;
            player.exp += totalReward.totalRewardExp;
            player.gold += totalReward.totalRewardGold;
        }
        /// <summary>
        /// 랜덤 보상 획득
        /// </summary>
        private void ProcessRandomReward(List<Monster> defeatedMonsters)
        {
            totalReward.totalRewardItems = new Dictionary<string, int>();

            for (int i = 0; i < defeatedMonsters.Count; i++)
            {
                string monsterName = defeatedMonsters[i].name;
                if (dropTable.ContainsKey(monsterName))
                {
                    List<DropItem> drops = dropTable[monsterName];
                    for (int j = 0; j < drops.Count; j++)
                    {
                        float roll = (float)random.NextDouble();
                        if (roll <= drops[j].dropRate)
                        {
                            int dropCount = random.Next(drops[j].minDropCount, drops[j].maxDropCount + 1);

                            if (dropCount > 0)
                            {
                                string itemName = drops[j].itemName;

                                if (totalReward.totalRewardItems.ContainsKey(itemName))
                                {
                                    totalReward.totalRewardItems[itemName] += dropCount;
                                }
                                else
                                {
                                    totalReward.totalRewardItems[itemName] = dropCount;
                                }
                            }
                        }
                    }
                }
            }

            foreach (var rewardItem in totalReward.totalRewardItems)
            {
                string itemName = rewardItem.Key;
                int itemCount = rewardItem.Value;
                Item itemToAdd = ItemDatabase.GetItem(itemName);
                for (int i = 0; i < itemCount; i++)
                {
                    player.inventory.Add(itemToAdd);
                }
            }
        }

        /// <summary>
        /// 드랍테이블 생성
        /// </summary>
        private void InitializeDropTable()
        {
            dropTable = new Dictionary<string, List<DropItem>>();

            dropTable.Add("미니언", new List<DropItem>
            {
                new DropItem { itemName = "낡은 검", minDropCount = 1, maxDropCount = 1, dropRate = 0.5f},

            });

            dropTable.Add("대포미니언", new List<DropItem>
            {
                new DropItem { itemName = "연습용 창", minDropCount = 0, maxDropCount = 1, dropRate = 0.1f }
            });

            dropTable.Add("공허충", new List<DropItem>
            {
                new DropItem { itemName = "스파르타의 창", minDropCount = 0, maxDropCount = 1, dropRate = 0.01f  }
            });
        }
    }
}