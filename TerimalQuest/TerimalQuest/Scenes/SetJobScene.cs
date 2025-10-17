using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Manager;

namespace TerimalQuest.Scenes
{
    public class SetJobScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;

        private JobType selectedJob;
        public void Enter()
        {
            UIManager.Instance.TerminalQuestScripts();
        }

        public void Update()
        {
            string jobName = "";
            UIManager.Instance.SetJobScripts();
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                switch (index)
                {
                    case 1:
                        selectedJob = JobType.전사;
                        jobName = "전사";
                        break;
                    case 2:
                        selectedJob = JobType.궁수;
                        jobName = "궁수";
                        break;
                    case 3:
                        selectedJob = JobType.마법사;
                        jobName = "마법사";
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
            UIManager.Instance.JobConfirmScripts(jobName);
            if (int.TryParse(Console.ReadLine(), out int answer))
            {
                switch (answer)
                {
                    case 1:
                        Job job = new Job(selectedJob);
                        GameManager.Instance.player.Init_Player_job(job);
                        OnSceneChangeRequested?.Invoke(new StartScene());
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }

        }

        public void Exit()
        {

        }
    }
}
