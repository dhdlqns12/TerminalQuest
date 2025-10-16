using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Scenes
{
    internal class TownActivityScene : IScene //순찰,훈련
    {
        public event Action<IScene> OnSceneChangeRequested;

        public void Enter()
        {
            UIManager.Instance.ShowSection("마을 활동");
        }

        public void Update()
        {

        }

        public void Exit()
        {

        }
    }
}
