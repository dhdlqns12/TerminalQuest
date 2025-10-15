using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;

namespace TerimalQuest.Scenes
{
    public class SaveDataLoadingScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;
        public void Enter()
        {
            DisplaySlot(0);
            DisplaySlot(1);
            DisplaySlot(2);
        }

        public void Update()
        {
            
        }

        public void Exit()
        {

        }

        static void DisplaySlot(int slotNumber)
        {
            if (SaveManager.HasSaveData(slotNumber))
            {
                SaveData data = SaveManager.GetSlotInfo(slotNumber);
                Console.WriteLine($"슬롯 {slotNumber} : 이름: {data.name} | 직업: {data.job.name} | 레벨: Lv.{data.level} | 골드: {data.gold}G");
            }
            else
            {
                Console.WriteLine("빈 슬롯");
            }
        }
    }
}
