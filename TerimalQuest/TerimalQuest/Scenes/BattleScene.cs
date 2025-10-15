using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Scenes
{
    public class BattleScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;
        private BattleManager battleManager;
        private BattleResultManager resultManager;
        public async void Enter()
        {
            battleManager = new BattleManager();
            BattleResult battleResult = await battleManager.StartBattle();
            resultManager = new BattleResultManager();
            resultManager.ProcessResult(battleResult);
        }

        public void Update()
        {

        }

        public void Exit()
        {

        }
    }
}
