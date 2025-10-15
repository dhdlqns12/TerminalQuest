using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Scenes;

namespace TerimalQuest.Manager
{
    public class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance => instance ??= new GameManager();

        public Player player;

        private SceneManager sceneManager;
        private UIManager uiManager => UIManager.Instance;

        public GameManager() 
        {
        }

        private void Init()
        {
            player = new Player();
            sceneManager = new SceneManager();
        }

        public void Run(int mode)
        {
            Init();
            switch(mode)
            {
                case 0:
                    sceneManager.ChangeScene(new SetNameScene());
                    break;
                case 1:
                    sceneManager.ChangeScene(new SaveDataLoadingScene());
                    break;
            }
            while (true)
            {
                sceneManager.Update();
            }
        }
    }
}
