using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Scenes
{
    public class EnhancementScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;

        private Action currentEnhancementView;
        private Inventory inventory;
        private EnhancementManager enhancementManager;

        public void Enter()
        {
            enhancementManager = new EnhancementManager();
            inventory = GameManager.Instance.player.inventory;

            currentEnhancementView = EnhanceStartView;
            currentEnhancementView?.Invoke();
        }

        public void Update()
        {
            //Process();
        }

        public void Exit()
        {

        }

        // 강화 시작 화면
        private void EnhanceStartView()
        {
            Console.Clear();
            Console.WriteLine("장비 강화");
            Console.WriteLine("보유 중인 장비를 강화할 수 있습니다.");
            Console.WriteLine();
            inventory.DisplayInfo(true, ItemType.Weapon, ItemType.Armor);
            Console.WriteLine();
            DisplayOption(["1. 무기 강화", "2. 방어구 강화", "0. 나가기"]);

            string choice = ConsoleHelper.GetUserChoice(["0", "1", "2"]);

            switch (choice)
            {
                case "1":
                    enhancementManager.SetEnhancealbeItemList(ItemType.Weapon);
                    currentEnhancementView = EnhanceView;
                    currentEnhancementView?.Invoke();
                    break;
                case "2":
                    enhancementManager.SetEnhancealbeItemList(ItemType.Armor);
                    currentEnhancementView = EnhanceView;
                    currentEnhancementView?.Invoke();
                    break;
                case "0":
                    OnSceneChangeRequested?.Invoke(new StartScene());
                    break;
                default:
                    break;
            }
        }

        // 강화 화면: 무기 or 방어구
        private void EnhanceView()
        {
            // 선택한 화면 보여주기
            Console.Clear();
            Console.WriteLine("장비 강화 - 무기");
            Console.WriteLine("보유 중인 장비를 강화할 수 있습니다.");
            Console.WriteLine();
            enhancementManager.DisplayEnhancealbeItemList();
            Console.WriteLine();
            DisplayOption(["(번호). 해당 장비 강화", "0. 나가기"]);

            // 상품 인덱스 정보 가져오기
            int vaildCount = enhancementManager.enhanceableItems.Count;
            string[] vaildItemOption = Enumerable.Range(0, vaildCount + 1).Select(i => i.ToString()).ToArray();   // LINQ 문법
            var choice = ConsoleHelper.GetUserChoice(vaildItemOption);

            // 아이템 강화
            while (true)
            {
                if (choice == "0") {
                    currentEnhancementView = EnhanceStartView;
                    currentEnhancementView?.Invoke(); 
                    return; 
                }

                // 아이템 강화가 가능한 지 체크
                if (enhancementManager.TryEnhanceItem(int.Parse(choice) - 1) == true)
                {
                    // 아이템 강화
                    enhancementManager.EnhanceItem();

                    // 이후 결과 페이지로 이동
                    currentEnhancementView = EnhanceResultView;
                    currentEnhancementView?.Invoke();
                    break;
                }

                choice = ConsoleHelper.GetUserChoice(vaildItemOption);
            }
        }

        // 강화 결과 화면
        private void EnhanceResultView()
        {
            Console.Clear();
            Console.WriteLine("강화 결과");
            Console.WriteLine();
            enhancementManager.EnhanceResult();
            Console.WriteLine();
            DisplayOption(["0. 나가기"]);

            string choice = ConsoleHelper.GetUserChoice(["0"]);

            currentEnhancementView = EnhanceStartView;
            currentEnhancementView?.Invoke();
        }

        public void DisplayOption(string[] options)
        {
            foreach (var option in options)
            {
                Console.WriteLine(option);
            }
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }
    }
}
