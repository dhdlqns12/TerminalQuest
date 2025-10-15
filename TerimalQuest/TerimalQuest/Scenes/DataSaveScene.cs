using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;

namespace TerimalQuest.Scenes
{
    public class DataSaveScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;
        public void Enter()
        {
            Console.WriteLine("데이터를 저장하시겠습니까?");
            bool answer = UIManager.Instance.YesOrNo();
            if (answer)
            {
                Console.WriteLine("데이터를 저장할 슬롯을 골라주세요.");
                DisplaySlot(0);
                DisplaySlot(1);
                DisplaySlot(2);
                Console.WriteLine("0.돌아가기");
            }
            else
            {
                Console.WriteLine("시작화면으로 돌아갑니다...");
                Console.ReadKey();
                OnSceneChangeRequested?.Invoke(new StartScene());
            }


        }

        public void Update()
        {
            if (int.TryParse(Console.ReadLine(), out int answer))
            {
                if(SaveManager.HasSaveData(answer))
                {
                    switch(answer) 
                    {
                        case 1:
                            //1번슬롯 저장
                            ConfirmQuit();
                            break;
                        case 2:
                            //2번슬롯 저장
                            ConfirmQuit();
                            break;
                        case 3:
                            //3번슬롯 저장
                            ConfirmQuit();
                            break;
                        case 0:
                            OnSceneChangeRequested?.Invoke(new StartScene());
                            break;
                        default:
                            Console.WriteLine("데이터가 존재하지 않습니다.");
                            Console.ReadKey();
                            break;
                    
                    }

                }
                else
                {
                    Console.WriteLine("데이터가 존재하지 않습니다.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
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
                Console.WriteLine($"슬롯 {slotNumber} : 이름: {data.name} | 직업: {data.job.name} | 레벨: Lv.{data.level} | 골드: {data.gold}G");
            }
            else
            {
                Console.WriteLine("빈 슬롯");
            }
        }

        static void ConfirmQuit()
        {
            while(true)
            {
                Console.Write("정말 종료하시겠습니까? \n\n1.종료한다. \n2.종료하지않는다. \n>>");
                if (int.TryParse(Console.ReadLine(), out int answer))
                {
                    if (SaveManager.HasSaveData(answer))
                    {
                        switch (answer)
                        {
                            case 1:
                                Environment.Exit(0);
                                break;
                            case 2:
                                break;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                Console.ReadKey();
                                continue;

                        }

                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        continue;
                    }
                }
                break;
            }
        }
    }
}
