using TerimalQuest.Core;
using TerimalQuest.System;

namespace TerimalQuest.Manager
{
    public class BattleResultManager
    {
        private Player player;
        private UIManager uiManager;
        public BattleResultManager()
        {
            player = GameManager.Instance.player;
            uiManager = UIManager.Instance;
        }
        /// <summary>
        /// 전투 결과
        /// </summary>
        public void ProcessResult(BattleResult result)
        {
            uiManager.DisplayBattleResult(result, player);
        }
    }
}