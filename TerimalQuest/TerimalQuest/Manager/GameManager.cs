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

        private Player player;

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

        public void Run()
        {
            Init();

            sceneManager.ChangeScene(new StartScene());
            while (true)
            {
                sceneManager.Update();
            }
        }
    }
}
