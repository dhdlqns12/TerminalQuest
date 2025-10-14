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
        

        private Player player;

        private SceneManager sceneManager;
        private UIManager uiManager;

        public GameManager() 
        {
        }

        private void Init()
        {
            player = new Player("",1,"무직",1,1,1,1,1);
            sceneManager = new SceneManager();
            uiManager = new UIManager();
        }

        public void Run()
        {
            while(true)
            {

            }
        }
    }
}
