using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.Scenes
{
    public interface IScene
    {
        void Enter();
        void Update();
        void Exit();
    }
}
