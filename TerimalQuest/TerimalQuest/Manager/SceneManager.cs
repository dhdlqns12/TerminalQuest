using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Scenes;

namespace TerimalQuest.Manager
{
    public class SceneManager
    {
        private IScene currentScene;

        public void ChangeScene(IScene newScene)
        {
            currentScene?.Exit();
            currentScene = newScene;
            Console.Clear();
            currentScene.Enter();
        }

        public void Update()
        {
            currentScene?.Update();
        }
    }
}
