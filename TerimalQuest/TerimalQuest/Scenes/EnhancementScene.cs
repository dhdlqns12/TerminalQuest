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
        private UIManager uiManager;

        private ItemType type;

        public void Enter()
        {
            enhancementManager = new EnhancementManager();
            inventory = GameManager.Instance.player.inventory;
            uiManager = UIManager.Instance;

            // 강화 시작 화면부터 시작
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
            uiManager.DisplayEnhancementStartScripts(inventory);

            string choice = ConsoleHelper.GetUserChoice(["0", "1", "2"]);

            switch (choice)
            {
                case "1":
                    type = ItemType.Weapon;
                    enhancementManager.SetEnhancealbeItemList(type);
                    ChangeView(EnhanceView);
                    break;
                case "2":
                    type = ItemType.Armor;
                    enhancementManager.SetEnhancealbeItemList(type);
                    ChangeView(EnhanceView);
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
            uiManager.DisplayEnhancementScripts(enhancementManager, type);

            // 강화 가능한 아이템 리스트 정보 가져오기
            int vaildCount = enhancementManager.enhanceableItems.Count;
            string[] vaildItemOption = Enumerable.Range(0, vaildCount + 1).Select(i => i.ToString()).ToArray();   // LINQ 문법
            var choice = ConsoleHelper.GetUserChoice(vaildItemOption);

            // 아이템 강화
            while (true)
            {
                if (choice == "0") {
                    ChangeView(EnhanceStartView);
                    return; 
                }

                // 아이템 강화가 가능한 지 체크
                if (enhancementManager.TryEnhanceItem(int.Parse(choice) - 1) == true)
                {
                    // 아이템 강화
                    enhancementManager.EnhanceItem();

                    // 이후 결과 페이지로 이동
                    ChangeView(EnhanceResultView);
                    break;
                }

                choice = ConsoleHelper.GetUserChoice(vaildItemOption);
            }
        }

        // 강화 결과 화면
        private void EnhanceResultView()
        {
            enhancementManager.EnhanceResult();

            string choice = ConsoleHelper.GetUserChoice(["0"]);

            ChangeView(EnhanceStartView);
        }

        // ViewChange
        private void ChangeView(Action view)
        {
            currentEnhancementView = view;
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
