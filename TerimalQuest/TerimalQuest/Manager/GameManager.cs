using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.Scenes;
using TerimalQuest.System;

namespace TerimalQuest.Manager
{
    public class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance => instance ??= new GameManager();

        public Player player;
        public Shop shop;

        private SceneManager sceneManager;
        private UIManager uiManager => UIManager.Instance;

        public GameManager() 
        {
        }

        private void Init()
        {
            player = new Player();
            shop = new Shop();
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

        public void SaveGame(int slot)
        {
            try
            {
                SaveManager.GameSave(slot);

                Console.WriteLine($"\n슬롯 {slot}에 게임이 저장되었습니다!");
                Console.ResetColor();
                global::System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n게임 저장 실패: {ex.Message}");
                Console.ResetColor();
                global::System.Threading.Thread.Sleep(1500);
            }
        }

        public void LoadGameFromSlot(int slot)
        {
            try
            {
                SaveManager.GameLoad(slot);

                Console.WriteLine($"\n게임을 불러왔습니다!");
                Console.WriteLine($"플레이어: {player.name} | 직업: {player.jobName} | 레벨: {player.level}");
                global::System.Threading.Thread.Sleep(1500);

                sceneManager.ChangeScene(new StartScene());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n게임 로드 실패: {ex.Message}");
                Console.WriteLine("\n아무 키나 눌러 돌아가기...");
                Console.ReadKey();

                sceneManager.ChangeScene(new SaveDataLoadingScene());
            }
        }
    }
}
