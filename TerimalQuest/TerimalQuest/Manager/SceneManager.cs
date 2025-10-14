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
            if (currentScene != null)
            {
                currentScene.OnSceneChangeRequested -= ChangeScene;
                currentScene.Exit();
            }

            currentScene = newScene;
            currentScene.OnSceneChangeRequested += ChangeScene;
            currentScene.Enter();
        }

        public void Update()
        {
            currentScene?.Update();
        }
    }
}
