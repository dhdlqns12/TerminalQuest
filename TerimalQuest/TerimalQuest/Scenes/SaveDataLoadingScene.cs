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
            DisplaySlot(1);
            DisplaySlot(2);
            DisplaySlot(3);
            UIManager.Instance.SaveDataLoadingScripts();
        }

        public void Update()
        {
            if (int.TryParse(Console.ReadLine(), out int answer))
            {
                if (answer == 0)
                {
                    OnSceneChangeRequested?.Invoke(new SetNameScene());
                    return;
                }

                if (SaveManager.HasSaveData(answer))
                {
                    switch(answer) 
                    {
                        case 1:
                            //1번슬롯 로드
                            OnSceneChangeRequested?.Invoke(new StartScene());
                            SaveManager.GameLoad(answer);
                            break;
                        case 2:
                            //2번슬롯 로드
                            OnSceneChangeRequested?.Invoke(new StartScene());
                            SaveManager.GameLoad(answer);
                            break;
                        case 3:
                            //3번슬롯 로드
                            OnSceneChangeRequested?.Invoke(new StartScene());
                            SaveManager.GameLoad(answer);
                            break;
                        default:
                            Console.WriteLine("데이터가 존재하지 않습니다.");
                            break;               
                    }
                }
                else
                {
                    Console.WriteLine("데이터가 존재하지 않습니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }


        public void Exit()
        {
            Console.Clear();
        }

        static void DisplaySlot(int slotNumber)
        {
            if (SaveManager.HasSaveData(slotNumber))
            {
                SaveData data = SaveManager.GetSlotInfo(slotNumber);
                Console.WriteLine($"슬롯 {slotNumber} : 이름: {data.name} | 직업: {data.jobType} | 레벨: Lv.{data.level} | 골드: {data.gold}G");
            }
            else
            {
                Console.WriteLine("빈 슬롯");
            }
        }
    }
}
