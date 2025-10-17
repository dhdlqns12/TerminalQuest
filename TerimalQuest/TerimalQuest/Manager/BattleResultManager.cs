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
        private RecodeManager recodeManager;
        private QuestManager questManager;
        public BattleResultManager()
        {
            player = GameManager.Instance.player;
            uiManager = UIManager.Instance;
            recodeManager = RecodeManager.Instance;
            questManager = QuestManager.Instance;
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
                recodeManager.RecordDefeatedMonsters(result.defeatedMonsters);
                if (player.curStage == 10)
                {
                    ProcessEnding();
                    return;
                }
                ProcessFixedReward(result.defeatedMonsters);
                ProcessRandomReward(result.defeatedMonsters);
                BattleQuestResult(result.defeatedMonsters);
                uiManager.DisplayBattleRewardResult(totalReward);
                player.curStage++;
                player.mp += 10;
            }
            uiManager.WaitNextChoice();
        }

        public void ProcessEnding()
        {
            recodeManager.SaveClearPlayer(player);
            recodeManager.ShowEnding();
            Environment.Exit(1);
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
                for (int i = 0; i < itemCount; i++)
                {
                    Item itemToAdd = ItemDatabase.GetItem(itemName);
                    if (itemToAdd != null) player.inventory.Add(itemToAdd);
                }
            }
        }


        private void BattleQuestResult(List<Monster> defeatedMonsters)
        {
            for (int i = 0; i < defeatedMonsters.Count; i++)
            {
                questManager.PlayQuest(defeatedMonsters[i].name);
            }
        }
        /// <summary>
        /// 드랍테이블 생성
        /// </summary>
        private void InitializeDropTable()
        {
            dropTable = new Dictionary<string, List<DropItem>>();

            dropTable.Add("슬라임", new List<DropItem>
            {

                new DropItem { itemName = "강화석", minDropCount = 1, maxDropCount = 1, dropRate = 0.2f},
                new DropItem { itemName = "빨간포션", minDropCount = 1, maxDropCount = 2, dropRate = 0.2f},
                new DropItem { itemName = "파랑포션", minDropCount = 1, maxDropCount = 2, dropRate = 0.2f},
                new DropItem { itemName = "노랑포션", minDropCount = 1, maxDropCount = 2, dropRate = 0.2f},

            });

            dropTable.Add("오크", new List<DropItem>
            {
                new DropItem { itemName = "수련자 갑옷", minDropCount = 0, maxDropCount = 1, dropRate = 0.2f},
                new DropItem { itemName = "무쇠갑옷", minDropCount = 0, maxDropCount = 1, dropRate = 0.1f},
                new DropItem { itemName = "강화석", minDropCount = 1, maxDropCount = 1, dropRate = 0.2f}
            });

            dropTable.Add("트롤", new List<DropItem>
            {
                new DropItem { itemName = "낡은 검", minDropCount = 1, maxDropCount = 1, dropRate = 0.3f},
                new DropItem { itemName = "연습용 창", minDropCount = 0, maxDropCount = 1, dropRate = 0.2f},
                new DropItem { itemName = "청동 도끼", minDropCount = 0, maxDropCount = 1, dropRate = 0.1f},
                new DropItem { itemName = "강화석", minDropCount = 1, maxDropCount = 1, dropRate = 0.2f}
            });
        }
    }
}