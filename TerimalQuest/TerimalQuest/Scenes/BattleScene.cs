using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Scenes
{
    public class BattleScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;
        private BattleManager battleManager;
        private BattleResultManager resultManager;
        Player player = GameManager.Instance.player;

        public void Enter()
        {
            battleManager = new BattleManager();
            resultManager = new BattleResultManager();
            player.RefreshStat();
            battleManager.StartBattle();
        }

        public void Update()
        {
            if (battleManager == null) return;

            battleManager.BattleProcess();

            if (battleManager.isTryingToEscape)
            {
                OnSceneChangeRequested?.Invoke(new StartScene());
                battleManager = null;
                return;
            }

            if (battleManager.IsBattleOver())
            {
                if (!battleManager.isTryingToEscape)
                {
                    BattleResult battleResult = battleManager.GetBattleResult();
                    resultManager.ProcessResult(battleResult);
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (input == "0")
                        {
                          OnSceneChangeRequested?.Invoke(new StartScene());
                          break;
                        }
                    }

                }
                battleManager = null;
                return;
            }

            if (battleManager.IsWaitingForInput())
            {
                string input = Console.ReadLine();
                battleManager.ProcessPlayerInput(input);
            }
        }

        public void Exit()
        {

        }
    }
}