using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TerimalQuest.Core;
using TerimalQuest.System;

namespace TerimalQuest.Manager
{
    public class UIManager
    {
        private static UIManager instance;
        public static UIManager Instance => instance ??= new UIManager();

        public void ShowTitle(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("========================================");
            Console.WriteLine($"   {text}");
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

        public void ShowSection(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n--- {title} ---");
            Console.ResetColor();
        }

        public void PrintOption(string number, string desc)
        {
            Console.WriteLine($"[{number}] {desc}");
        }

        public void ShowStartSceneScripts()
        {
            Player p = GameManager.Instance.player;
            Console.Clear();

            // 헤더 꾸미기
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          ⚔ 터미널 퀘스트에 오신 걸 환영합니다 ⚔          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("─────────────────────────────────────────");
            Console.WriteLine($"  이름: {p.name}   HP: {p.hp}/{p.maxHp}   GOLD: {p.gold}");
            Console.WriteLine("─────────────────────────────────────────");
            Console.ResetColor();

            Console.WriteLine();
            TypeWrite("이제 전투를 시작할 수 있습니다.\n", 30);

            // 메뉴 항목
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌────────── [메뉴 선택] ──────────┐");
            Console.ResetColor();

            PrintMenuOption(1, "상태 보기");
            PrintMenuOption(2, "인벤토리");
            PrintMenuOption(3, $"전투 시작 (현재 진행: {GameManager.Instance.player.curStage}층)");
            PrintMenuOption(4, "퀘스트");
            PrintMenuOption(5, "상점");
            PrintMenuOption(6, "마을 활동");
            PrintMenuOption(7, "장비 강화");
            PrintMenuOption(8, "게임 저장");
            PrintMenuOption(0, "게임 종료");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("└─────────────────────────────────┘");
            Console.ResetColor();

            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요 \n>> ");
        }

        private void PrintMenuOption(int number, string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"│ ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{number,2}. ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(ConsoleHelper.PadRightForConsole(text, 28) + "│");
        }

        void TypeWrite(string text, int delay = 20)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }

        public void ShowInvalidInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            TypeWrite("\n잘못된 입력입니다! 다시 선택해주세요.\n", 25);
            Console.ResetColor();
            Thread.Sleep(1000);
        }


        public void ShowStatusSceneScripts()
        {
            Player p = GameManager.Instance.player;
            Console.Write($"상태 보기\r\n캐릭터의 정보가 표시됩니다.\r\n\r\nLv. {p.level}       경험치: {p.exp}\r\n{p.name} ( {p.jobName} )\r\n공격력 : {p.atk}\r\n방어력 : {p.def}\r\n체 력 : {p.hp}\r\n마나: {p.mp}\r\n스태미나 : {p.stamina}\r\nGold : {p.gold} G\r\n\r\n0. 나가기\r\n\r\n원하시는 행동을 입력해주세요.\r\n>> ");
        }

        public void TerminalQuestScripts()
        {
            Console.OutputEncoding = global::System.Text.Encoding.UTF8;
            Console.Clear();
            Console.CursorVisible = false;

            string[] terminal = new string[]
            {
        " ▀█▀ █▀▀ █▀█ █▀▄▀█ █ █▄ █ ▄▀█ █   ",
        "  █  ██▄ █▀▄ █ ▀ █ █ █ ▀█ █▀█ █▄▄ "
            };

            string[] quest = new string[]
            {
        "     █▀█ █ █ █▀▀ █▀ ▀█▀ ",
        "     ▀▀█ █▄█ ██▄ ▄█  █  "
            };

            for (int step = 0; step <= 10; step++)
            {
                Console.Clear();
                double scale = step / 10.0;

                for (int i = 0; i < terminal.Length; i++)
                {
                    int lineLength = (int)(terminal[i].Length * scale);
                    int startPos = (terminal[i].Length - lineLength) / 2;

                    if (lineLength > 0)
                    {
                        string displayText = terminal[i].Substring(startPos, lineLength);

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.SetCursorPosition(startPos, i);
                        Console.Write(displayText);
                    }
                }

                global::System.Threading.Thread.Sleep(50);
            }

            global::System.Threading.Thread.Sleep(200);

            for (int step = 0; step <= 10; step++)
            {
                double scale = step / 10.0;

                for (int i = 0; i < quest.Length; i++)
                {
                    int lineLength = (int)(quest[i].Length * scale);
                    int startPos = (quest[i].Length - lineLength) / 2;

                    if (lineLength > 0)
                    {
                        string displayText = quest[i].Substring(startPos, lineLength);

                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.SetCursorPosition(startPos + 3, i + 3);
                        Console.Write(displayText);
                    }
                }

                global::System.Threading.Thread.Sleep(50);
            }

            Console.ResetColor();
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, 7);
        }

        public void SetNameScripts()
        {
            Console.Write("원하시는 이름을 설정해주세요. \n>>");
        }

        public void NameConfirmScripts(string name)
        {
            Console.Write($"{name}이 맞습니까? \n\n1. 저장 \n2. 취소 \n\n원하시는 행동을 입력해주세요. \n>>");
        }

        public void SetJobScripts()
        {
            Console.Clear();
            Console.Write("원하시는 직업을 설정해주세요. \n\n1. 전사 \n2. 궁수 \n3. 마법사 \n\n원하시는 행동을 입력해주세요. \n>>");
        }

        public void JobConfirmScripts(string job)
        {
            Console.Write($"{job}가 되시겠습니까? \n\n1. 저장 \n2. 취소 \n\n원하시는 행동을 입력해주세요. \n>>");
        }

        public void HasSaveDataScripts()
        {
            Console.WriteLine("\n[기존 세이브 데이터를 발견했습니다.]");
            Console.WriteLine("불러오기 화면으로 이동합니다...");
            Console.ReadKey();
        }

        public void EmptySaveDataScripts()
        {
            Console.WriteLine("\n[저장 데이터가 없습니다.]");
            Console.WriteLine("새 캐릭터를 생성합니다...");
            Console.ReadKey();
        }

        public void SaveDataLoadingScripts()
        {
            Console.WriteLine("0: 새게임");
            Console.WriteLine("\n[불러올 세이브파일 숫자를 입력해주세요.]");
            Console.Write(">>");
        }

        public bool YesOrNo()
        {
            while(true)
            {

                string answer = Console.ReadLine();
                if (answer != null)
                {
                    if(answer == "y")
                    {
                        return true;
                    }
                    else if(answer == "n")
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
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

        // 문자 사이 정렬 offset 값
        private int offsetName = 20;
        private int offsetEffect = 13;
        private int offsetDesc = 50;
        private int offsetCount = 10;
        private int offsetPurchase = 5;

        // 아이템 정보 표시 헤더
        public void DisplayItemInfoHeader(bool isEquipMode = false)
        {
            string equipMode = isEquipMode ? ConsoleHelper.PadRightForConsole(" ", 6) : "  ";

            Console.WriteLine(
                string.Format("{0}{1} | {2} | {3} | {4}",
                equipMode,
                ConsoleHelper.PadRightForConsole("[아이템 이름]", offsetName),
                ConsoleHelper.PadRightForConsole("[아이템 효과]", offsetEffect),
                ConsoleHelper.PadRightForConsole("[아이템 설명]", offsetDesc),
                "[수량]\n")
            );
        }

        // 인벤토리 아이템 정보 표시
        public void DisplayItemInfo(Item item, string idxTxt, bool isEquipMode = false)
        {
            string itemName = $"{idxTxt}{item.name}";

            if (item is Weapon || item is Armor)
            {
                itemName = $"{idxTxt}{GetEquipItemName(item)}";
            }

            string name = ConsoleHelper.PadRightForConsole(itemName, offsetName);
            string effect = ConsoleHelper.PadRightForConsole(item.GetEffectText(), offsetEffect);
            string desc = ConsoleHelper.PadRightForConsole(item.desc, offsetDesc);
            string count = ConsoleHelper.PadRightForConsole(item.GetCountText(), offsetCount);

            // 번호 모드
            string numberMode = (isEquipMode) ? ConsoleHelper.PadRightForConsole(" ", 6) : $"  ";

            string line = $"{name} | {effect} | {desc} | {count}";
            int totalWidth = ConsoleHelper.GetDisplayWidth(line) + 4;

            // 테두리 색상
            Console.ForegroundColor = GetColorByItemType(item.type);
            Console.WriteLine("╔" + new string('═', totalWidth - 2) + "╗");
            Console.ResetColor();

            // 내용 출력
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("║ ");
            Console.Write(line);
            Console.ResetColor();
            Console.WriteLine(" ║");

            // 하단 테두리
            Console.ForegroundColor = GetColorByItemType(item.type);
            Console.WriteLine("╚" + new string('═', totalWidth - 2) + "╝");
            Console.ResetColor();
        }

        // 상점 아이템 정보 표시 헤더
        public void DisplayItemProductHeader(bool isPurchase = true)
        {
            Console.WriteLine("[아이템 목록]\n");
            string purchase = (isPurchase) ? ConsoleHelper.PadRightForConsole(" ", 6) : $"  ";

            Console.WriteLine(
                string.Format("{0}{1} | {2} | {3} | {4} | {5}",
                purchase,
                ConsoleHelper.PadRightForConsole("[아이템 이름]", offsetName),
                ConsoleHelper.PadRightForConsole("[아이템 효과]", offsetEffect),
                ConsoleHelper.PadRightForConsole("[아이템 설명]", offsetDesc),
                ConsoleHelper.PadRightForConsole("[수량]", offsetCount),
                "[아이템 가격]\n"));
        }

        // 상점 아이템 정보 표시
        public void DisplayItemProduct(Item item, string idxTxt)
        {
            string itemPurchase = (item.isPurchase) ? "구매완료" : $"{item.price}";
            string isGoldIcon = (item.isPurchase) ? "" : "G";

            string itemName = $"{idxTxt}{item.name}";
            string name = ConsoleHelper.PadRightForConsole(itemName, offsetName);
            string effect = ConsoleHelper.PadRightForConsole(item.GetEffectText(), offsetEffect);
            string desc = ConsoleHelper.PadRightForConsole(item.desc, offsetDesc);
            string count = ConsoleHelper.PadRightForConsole(item.GetCountText(), offsetCount);

            string line = $"{name} | {effect} | {desc} | {count} | {itemPurchase} {isGoldIcon}";
            int totalWidth = ConsoleHelper.GetDisplayWidth(line) + 4;

            // 테두리 색상
            Console.ForegroundColor = GetColorByItemType(item.type);
            Console.WriteLine("╔" + new string('═', totalWidth - 2) + "╗");
            Console.ResetColor();

            // 내용 출력
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("║ ");
            Console.Write(line);
            Console.ResetColor();
            Console.WriteLine(" ║");

            // 하단 테두리
            Console.ForegroundColor = GetColorByItemType(item.type);
            Console.WriteLine("╚" + new string('═', totalWidth - 2) + "╝");
            Console.ResetColor();
        }

        private static ConsoleColor GetColorByItemType(ItemType type)
        {
            return type switch
            {
                ItemType.Weapon => ConsoleColor.Cyan,
                ItemType.Armor => ConsoleColor.Green,
                ItemType.Potion => ConsoleColor.Red,
                ItemType.EnhancementStone => ConsoleColor.Yellow,
                _ => ConsoleColor.Gray
            };
        }

        private string GetEquipItemName(Item item)
        {
            // 강화 레벨
            string enhanceLevelTxt = $"(+{item.GetLevel()}강)";

            // 착용 여부
            string equipTxt = (item.isEquipped) ? "[E]" : "";

            return $"{equipTxt}{enhanceLevelTxt}{item.name}";
        }

        #region InventoryUI

        // 플레이어 인벤토리 창 : 플레이어의 인벤토리를 볼 수 있는 창. 아이템을 확인 할 수 있다.
        public void InventoryScripts(Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            inventory.DisplayInfo(false);
            Console.WriteLine();
            DisplayOption(["1. 장착 관리", "2. 아이템 사용", "3. 아이템 정렬", "0. 나가기"]);
        }

        // 플레이어 인벤토리 장착 관리 창 : 플레이어의 아이템을 장착/해제 할 수 있다.
        public void InventoryEquipScripts(Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            inventory.DisplayInfo(true, ItemType.Weapon, ItemType.Armor);
            Console.WriteLine();
            DisplayOption(["(번호). 해당 장비 장착", "0. 나가기"]);
        }

        // 플레이어 인벤토리 사용 창 : 포션 등 아이템을 사용할 수 있다.
        public void InventoryUseScripts(Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 아이템 사용");
            Console.WriteLine("보유 중인 아이템을 사용 할 수 있습니다.");
            Console.WriteLine();
            inventory.DisplayInfo(true, ItemType.Potion);
            Console.WriteLine();
            DisplayOption(["(번호). 해당 아이템 사용", "0. 나가기"]);
        }

        // 플레이어 인벤토리 정렬 창 : 인벤토리의 아이템들을 옵션에 따라 정렬할 수 있다.
        public void InventorySortingScripts(Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("[인벤토리 - 아이템 정렬]");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            inventory.DisplayInfo(false);
            Console.WriteLine();
            DisplayOption(["1. 이름", "2. 장착순", "3. 공격력", "4. 방어력", "0. 나가기"]);
        }

        // 메세지 : 인벤토리 공간 부족
        public void MessageNotEnoughInventorySpace() { Console.WriteLine("소지 할 수 있는 인벤토리 공간이 없습니다!"); }

        #endregion

        #region ShopUI

        // 상점
        public void ShopScripts(Player player, Shop shop)
        {
            Console.Clear();
            ConsoleHelper.PrintColored("╔══════════════════════════════════════════════════════════╗", ConsoleColor.DarkCyan);
            ConsoleHelper.PrintColoredWithBackground("║                     상   점                             ║", ConsoleColor.Yellow, ConsoleColor.DarkBlue);
            ConsoleHelper.PrintColored("╚══════════════════════════════════════════════════════════╝", ConsoleColor.DarkCyan);
            Console.WriteLine();
            ConsoleHelper.PrintColored("필요한 아이템을 얻을 수 있는 상점입니다.", ConsoleColor.DarkCyan);
            ConsoleHelper.PrintDivider(55, '─', ConsoleColor.DarkCyan);
            Console.WriteLine();
            ConsoleHelper.PrintColored("[보유 골드]", ConsoleColor.Yellow);
            ConsoleHelper.PrintColored($"{player.gold} G", ConsoleColor.Green);
            Console.WriteLine();
            shop.DisplayInfo(false);
            Console.WriteLine();
            DisplayOption(["1. 아이템 구매", "2. 아이템 판매", "0. 나가기"]);
        }

        // 상점 : 상품 구매
        public void ShopPurchaseScripts(Player player, Shop shop)
        {
            Console.Clear();
            ConsoleHelper.PrintColored("╔══════════════════════════════════════════════════════════╗", ConsoleColor.DarkCyan);
            ConsoleHelper.PrintColoredWithBackground("║                     상   점                             ║", ConsoleColor.Yellow, ConsoleColor.DarkBlue);
            ConsoleHelper.PrintColored("╚══════════════════════════════════════════════════════════╝", ConsoleColor.DarkCyan);
            Console.WriteLine();
            ConsoleHelper.PrintColored("구매 페이지", ConsoleColor.DarkCyan);
            ConsoleHelper.PrintDivider(55, '─', ConsoleColor.DarkCyan);
            Console.WriteLine();
            ConsoleHelper.PrintColored("[보유 골드]", ConsoleColor.Yellow);
            ConsoleHelper.PrintColored($"{player.gold} G", ConsoleColor.Green);
            Console.WriteLine();
            shop.DisplayInfo(true);
            Console.WriteLine();
            DisplayOption(["(번호). 해당 아이템 구매", "0. 나가기"]);
        }

        // 상점 : 아이템 판매
        public void ShopSaleScripts(Player player)
        {
            Console.Clear();
            ConsoleHelper.PrintColored("╔══════════════════════════════════════════════════════════╗", ConsoleColor.DarkCyan);
            ConsoleHelper.PrintColoredWithBackground("║                     상   점                             ║", ConsoleColor.Yellow, ConsoleColor.DarkBlue);
            ConsoleHelper.PrintColored("╚══════════════════════════════════════════════════════════╝", ConsoleColor.DarkCyan);
            Console.WriteLine();
            ConsoleHelper.PrintColored("필요한 아이템을 얻을 수 있는 상점입니다.", ConsoleColor.DarkCyan);
            ConsoleHelper.PrintDivider(55, '─', ConsoleColor.DarkCyan);
            Console.WriteLine();
            ConsoleHelper.PrintColored("[보유 골드]", ConsoleColor.Yellow);
            ConsoleHelper.PrintColored($"{player.gold} G", ConsoleColor.Green);
            Console.WriteLine();
            player.inventory.DisplayInfoWithGold();
            Console.WriteLine();
            DisplayOption(["(번호). 해당 아이템 판매", "0. 나가기"]);
        }

        // 메세지 : 상품 품절
        public void MessageSoldOut() { Console.WriteLine("이미 품절된 아이템입니다."); }

        // 메세지 : 돈 부족
        public void MessageNotEnoughGold() { Console.WriteLine("골드가 충분하지 않습니다."); }

        // 메세지 : 아이템 구매
        public void MessagePurchaseItem(Item item) { Console.WriteLine($"{item.name} 아이템을 구매했습니다."); }

        // 메세지 : 아이템 판매
        public void MessageSaleItem(Item item) { Console.WriteLine($"{item.name} 아이템을 판매했습니다."); }

        #endregion

        #region EnhancementUI

        public void DisplayEnhancementStartScripts(Inventory inventory, EnhancementManager enhancementManager)
        {
            Console.Clear();
            Console.WriteLine("장비 강화");
            Console.WriteLine("보유 중인 장비를 강화할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 강화석]");
            Console.WriteLine($"강화석: {enhancementManager.GetPlayerEnhancementStoneCount()}개");
            Console.WriteLine();
            inventory.DisplayInfo(true, ItemType.Weapon, ItemType.Armor);
            Console.WriteLine();
            DisplayOption(["1. 무기 강화", "2. 방어구 강화", "0. 나가기"]);
        }

        public void DisplayEnhancementScripts(EnhancementManager enhancementManager, ItemType type)
        {
            string typeTxt = (type == ItemType.Weapon) ? "무기" : "방어구";

            Console.Clear();
            Console.WriteLine($"장비 강화 - {typeTxt}");
            Console.WriteLine("보유 중인 장비를 강화할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 강화석]");
            Console.WriteLine($"강화석: {enhancementManager.GetPlayerEnhancementStoneCount()}개");
            Console.WriteLine();
            enhancementManager.DisplayEnhancealbeItemList();
            Console.WriteLine();
            DisplayOption(["(번호). 해당 장비 강화", "0. 나가기"]);
        }

        public void DisplayEnhancementResultScripts(bool success, int prevLevel, Item item)
        {
            Console.Clear();
            Console.WriteLine("강화 결과");
            Console.WriteLine();
            if (success)
            {
                // 강화 성공 텍스트 출력
                Console.WriteLine("강화에 성공했습니다!");
                Console.WriteLine($"[{prevLevel}강] -> [{item.GetLevel()}강]");
            }
            else
            {
                // 강화 실패 텍스트 출력
                Console.WriteLine("강화에 실패하였습니다..");
            }
            Console.WriteLine();
            DisplayOption(["0. 나가기"]);
        }

        // 메세지 : 최대 강화 레벨일 시 출력
        public void MessageNoMoreEnhancement() { Console.WriteLine("더이상 강화를 진행할 수 있습니다."); }

        // 메세지 : 강화석 부족
        public void MessageNotEnoughEnhancementStone() { Console.WriteLine("강화석 재료가 부족합니다."); }


        #endregion

        #region BattleUI

        private bool kefgaEntranceShown = false;
        // 전투 화면 진입 - 몬스터 애니메이션 포함
        public void BattleEntrance(List<Monster> monsters, Player player)
        {
            Monster boss = monsters.FirstOrDefault(m => m.name == "케프가" || m.name == "라보스");
            if (boss != null)
            {
                // 보스별 등장 연출
                if (boss.name == "케프가"&& !kefgaEntranceShown)
                {
                    DisplayKefgaBossEntrance();
                    kefgaEntranceShown = true;
                }
                else if (boss.name == "라보스")
                {

                }
            }
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            int screenWidth = Console.WindowWidth;

            ConsoleHelper.PrintColored(new string('═', screenWidth), ConsoleColor.Yellow);
            string battleText = "  B A T T L E  ";
            int padding = (screenWidth - battleText.Length) / 2;
            ConsoleHelper.PrintColored(new string(' ', padding) + battleText, ConsoleColor.Yellow);
            ConsoleHelper.PrintColored(new string('═', screenWidth), ConsoleColor.Yellow);

            Console.WriteLine();

            BattleDisplay.DisplayMonsters(monsters, AnimationType.Idle);

            ConsoleHelper.PrintColored("────────────────── [내 정보] ──────────────────", ConsoleColor.Cyan);
            Console.WriteLine($"Lv.{player.level} {player.name} ({player.jobName})");
            Console.Write("HP [");
            PlayerHpBar(player);
            Console.WriteLine($"] {player.hp:F0}/{player.maxHp:F0}");
            Console.Write("MP [");
            PlayerMpBar(player);
            Console.WriteLine($"] {player.mp:F0}/{player.maxMp:F0}");
            Console.WriteLine($"공격력: {player.atk:F0}  방어력: {player.def:F0}");
            ConsoleHelper.PrintColored("───────────────────────────────────────────────", ConsoleColor.Cyan);
        }

        private void PlayerHpBar(Player player)
        {
            int barLength = 20;
            float hpPercent = player.hp / player.maxHp;
            int filled = (int)(barLength * hpPercent);

            if (filled < 0) filled = 0;

            if (hpPercent > 0.5f)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (hpPercent > 0.2f)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(new string('█', filled));
            Console.ResetColor();
            Console.Write(new string('▒', barLength - filled));
        }

        private void PlayerMpBar(Player player)
        {
            int barLength = 20;
            float mpPercent = player.mp / player.maxMp;
            int filled = (int)(barLength * mpPercent);

            if (filled < 0) filled = 0;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(new string('█', filled));
            Console.ResetColor();
            Console.Write(new string('▒', barLength - filled));
        }

        public void AttackTarget(Character attacker, Character target, bool isEvade)
        {
            Console.Clear();

            if (attacker is Player)
            {
                ConsoleHelper.PrintColored("\n 플레이어의 공격! \n", ConsoleColor.Yellow);
                Thread.Sleep(500);

                if (target is Monster monster)
                {
                    BattleDisplay.PlayMonsterAnimation(monster,
                        isEvade ? AnimationType.Idle : AnimationType.Hit, 200);
                }
            }
            else if (attacker is Monster attackMonster)
            {
                ConsoleHelper.PrintColored($"\n {attacker.name}의 공격! \n", ConsoleColor.Red);
                Thread.Sleep(500);
                BattleDisplay.PlayMonsterAnimation(attackMonster, AnimationType.Attack, 200);
            }

            if (isEvade)
            {
                ConsoleHelper.PrintColored($"\n {target.name}이(가) 회피했습니다! ", ConsoleColor.Cyan);
            }
            else
            {
                int finalDamage = attacker.GetFinalDamage(out bool isCritical, (int)target.def);

                if (isCritical)
                {
                    ConsoleHelper.PrintColored("\n 크리티컬 히트! ", ConsoleColor.Magenta);
                }

                ConsoleHelper.PrintColored($"\n 명중! [{finalDamage} 데미지]", ConsoleColor.Yellow);

                string deadResult = target is Player ? "0" : "Dead";
                float newHp = target.hp - finalDamage;
                Console.WriteLine($"Lv.{target.level} {target.name}");
                Console.WriteLine($"HP {target.hp:F0} → {(newHp > 0 ? newHp.ToString("F0") : deadResult)}");

                if (newHp <= 0 && target is Monster deadMonster)
                {
                    Thread.Sleep(800);
                    ConsoleHelper.PrintColored($"\n {deadMonster.name}을(를) 처치했습니다! ", ConsoleColor.Green);
                    BattleDisplay.PlayMonsterAnimation(deadMonster, AnimationType.Death, 300);
                }
            }

            Thread.Sleep(1000);
        }

        public void AttackTargetWithSkill(Character attacker, Character target, Skill skill)
        {
            Console.Clear();
            ConsoleHelper.PrintColored($"\n {attacker.name}이(가) [{skill.name}]을(를) 사용했습니다! \n", ConsoleColor.Magenta);
            Console.WriteLine($" ※{skill.description}");
            Thread.Sleep(800);

            if (target is Monster monster)
            {
                BattleDisplay.PlayMonsterAnimation(monster, AnimationType.Hit, 200);
            }

            string deadResult = target is Player ? "0" : "Dead";
            float damage = skill.damageType == SkillDamageType.FixedDamage
                ? skill.damage
                : skill.damage * attacker.atk;

            float newHp = target.hp - damage;

            ConsoleHelper.PrintColored($"\n {target.name}에게 {damage:F0} 데미지!", ConsoleColor.Yellow);
            Console.WriteLine($"Lv.{target.level} {target.name}");
            Console.WriteLine($"HP {target.hp:F0} → {(newHp > 0 ? newHp.ToString("F0") : deadResult)}");

            if (newHp <= 0 && target is Monster deadMonster)
            {
                Thread.Sleep(800);
                ConsoleHelper.PrintColored($"\n {deadMonster.name}을(를) 처치했습니다! ", ConsoleColor.Green);
                BattleDisplay.PlayMonsterAnimation(deadMonster, AnimationType.Death, 300);
            }

            Thread.Sleep(1000);
        }

        public void FullRangeAttackSkill(List<Monster> monsterList, Skill skill, float finalSkillDamage)
        {
            Console.Clear();
            ConsoleHelper.PrintColored($"\n {skill.name}! \n", ConsoleColor.Magenta);
            Console.WriteLine($"  ▶ {skill.description}");
            Thread.Sleep(800);

            ConsoleHelper.PrintColored($"\n 모든 적에게 {finalSkillDamage:F0} 데미지! \n", ConsoleColor.Yellow);

            List<Monster> aliveMonsters = monsterList.Where(m => m.hp > 0).ToList();
            if (aliveMonsters.Count > 0)
            {
                BattleDisplay.DisplayMonsters(aliveMonsters, AnimationType.Hit);
            }

            Thread.Sleep(500);

            foreach (var monster in monsterList)
            {
                if (monster.hp <= 0) continue;

                float newHp = monster.hp - finalSkillDamage;
                Console.WriteLine($"{monster.name} - HP {monster.hp:F0} → {(newHp >= 0 ? newHp.ToString("F0") : "Dead")}");

                if (newHp <= 0)
                {
                    ConsoleHelper.PrintColored($"   {monster.name} 처치!", ConsoleColor.Green);
                }
            }

            Thread.Sleep(1000);
        }

        public void UseSupportSkill(Player player, Skill skill)
        {
            Console.Clear();
            ConsoleHelper.PrintColored($"\n {player.name}이(가) [{skill.name}]을(를) 사용했습니다! ", ConsoleColor.Green);
            Console.WriteLine($"\n   {skill.description}");
            Console.WriteLine($"\n HP가 {skill.damage:F0}만큼 회복되었습니다!");
            Console.WriteLine($"HP {player.hp:F0} → {player.hp + skill.damage:F0}");
            Thread.Sleep(1500);
        }

        public void SelectTarget()
        {
            Console.WriteLine("\n");
            ConsoleHelper.PrintColored("─────── [타겟 선택] ───────", ConsoleColor.Cyan);
            Console.WriteLine("공격할 대상의 번호를 입력하세요.");
            Console.WriteLine("0. 돌아가기");
            Console.Write(">> ");
        }

        public void DisplayBattleChoice()
        {
            Console.WriteLine("\n");
            ConsoleHelper.PrintColored("─────── [행동 선택] ───────", ConsoleColor.Cyan);
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("0. 도망");
            Console.Write(">> ");
        }

        public void SelectingSkill(List<Skill> skillList)
        {
            Console.WriteLine("\n");
            ConsoleHelper.PrintColored("─────── [스킬 선택] ───────", ConsoleColor.Cyan);

            for (int i = 0; i < skillList.Count; i++)
            {
                Skill skill = skillList[i];
                Console.Write($"{i + 1}. {skill.name}");
                ConsoleHelper.PrintColored($" (MP: {skill.cost})", ConsoleColor.Blue);
                Console.WriteLine($"   ▶ {skill.description}");
            }

            Console.WriteLine("0. 취소");
            Console.Write(">> ");
        }

        public void SelectWrongSelection()
        {
            ConsoleHelper.PrintColored("\n 잘못된 입력입니다!", ConsoleColor.Red);
        }

        public void DisplayNotEnoughMagicCost()
        {
            ConsoleHelper.PrintColored("\n MP가 부족합니다!", ConsoleColor.Red);
        }

        public void DisplayNotEnoughHp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            ConsoleHelper.PrintColored("\n HP가 부족해서 전투가 불가능합니다!", ConsoleColor.Red);
            Console.ResetColor();
        }

        public void DisplayPressAnyKeyToNext()
        {
            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey(true);
        }


        public void StageClearStatus(int lastClearStage) //스테이지 선택?
        {
            int totalStage = 10;

            Console.Clear();
            ConsoleHelper.PrintColored("\n══════════════════════════════════════════", ConsoleColor.Yellow);
            ConsoleHelper.PrintColored("           스테이지 현황", ConsoleColor.Yellow);
            ConsoleHelper.PrintColored("══════════════════════════════════════════", ConsoleColor.Yellow);

            for (int i = 0; i < totalStage; i++)
            {
                int currentStage = i + 1;
                string statusText;
                ConsoleColor statusColor;

                if (currentStage <= lastClearStage - 1)
                {
                    statusText = " Clear!";
                    statusColor = ConsoleColor.Green;
                }
                else if (currentStage == lastClearStage)
                {
                    statusText = " Now";
                    statusColor = ConsoleColor.Yellow;
                }
                else
                {
                    statusText = " Locked";
                    statusColor = ConsoleColor.DarkGray;
                }

                Console.Write($"Stage {currentStage,-2} ");
                ConsoleHelper.PrintColored($"{statusText,-10}", statusColor);
            }

            ConsoleHelper.PrintColored("══════════════════════════════════════════\n", ConsoleColor.Yellow);
            Console.WriteLine("적들이 나타났습니다!\n");
            Thread.Sleep(1500);
        }

        public void WaitNextChoice()
        {
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine(">>");
        }

        public void DisplayBattleResult(BattleResult result, Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine($"{(result.isPlayerWin ? "Victory" : "You Lose")}");
            if (result.isPlayerWin)
            {
                Console.WriteLine($"던전에서 몬스터 {result.defeatedMonsters.Count}마리를 잡았습니다.");
            }
            Console.WriteLine($"Lv.{player.level} {player.name}");
            Console.WriteLine($"HP {player.hp + result.hpReduction} -> {player.hp}");
            Console.WriteLine();


        }

        public void DisplayBattleRewardResult(TotalReward totalReward)
        {
            Console.WriteLine("[획득 아이템]");
            if (totalReward.totalRewardExp > 0)  Console.WriteLine($"{totalReward.totalRewardExp} Exp");
            if (totalReward.totalRewardGold > 0)  Console.WriteLine($"{totalReward.totalRewardGold} Gold");
            if (totalReward.totalRewardItems != null && totalReward.totalRewardItems.Count > 0)
            {
                foreach (var itemPair in totalReward.totalRewardItems)
                {
                    Console.WriteLine($"{itemPair.Value} - {itemPair.Key}");
                }
            }
        }

        #endregion

        #region QuestUI
        /// <summary>
        /// 퀘스트 리스트업 함수
        /// </summary>
        /// <param name="quests"></param>
        public void QuestListShow(List<Quest> quests)
        {
            Player player = GameManager.Instance.player;
            Console.Clear();
            ColorText("---------------------------", ConsoleColor.DarkCyan);
            Console.WriteLine("        퀘스트 목록");
            ColorText("---------------------------", ConsoleColor.DarkCyan);
            Console.WriteLine();
            QuestManager.Instance.curQuest = null;
            ShowMainQuest(player);
            Console.WriteLine();
            ShowSubQuests(player);
            Console.WriteLine("\n0. 돌아가기");
            Console.Write("\n원하시는 퀘스트를 선택해주세요.\n>>");
        }

        public void ShowSubQuests(Player player)
        {
            List<Quest> subQuests = QuestManager.Instance.subQuests;
            ColorText("\n서브 퀘스트\n", ConsoleColor.DarkBlue);
            for (int i = 0; i < subQuests.Count; i++)
            {
                string questRunning = "";
                int num = subQuests[i].questNum;
                ConsoleColor textColor = ConsoleColor.White;
                if (player.questList != null && subQuests.Count > 0)
                {
                    questRunning = QuestStatusText(player.questList, num);
                    textColor = QuestStatusColor(player.questList, num);
                }
                string questName = ($"{i + 2}. {subQuests[i].name} {questRunning}");
                ColorText(questName, textColor);
            }
        }

        public void ShowMainQuest(Player player)
        {
            List<Quest> mainQuests = QuestManager.Instance.mainQuests;
            ColorText("메인 퀘스트 (필수!!)\n", ConsoleColor.DarkRed);
            if (player.questList != null && mainQuests.Count > 0)
            {
                int num = mainQuests[0].questNum;
                string questRunning = QuestStatusText(player.questList, num);
                ConsoleColor textColor = QuestStatusColor(player.questList, num);
                string questName = ($"1. {mainQuests[0].name} {questRunning}");
                ColorText(questName, textColor);
            }
        }

        public string QuestStatusText(Dictionary<int, Quest> quests, int num)
        {
            if (quests.ContainsKey(num) && quests[num].isClear)
            {
                return "[완료]";
            }
            else if (quests.ContainsKey(num) && !quests[num].isClear)
            {
                return "[진행중]";
            }
            else
            {
                return "";
            }
        }

        public ConsoleColor QuestStatusColor(Dictionary<int, Quest> quests, int num)
        {
            if (quests.ContainsKey(num) && quests[num].isClear)
            {
                return ConsoleColor.DarkGreen;
            }
            else if (quests.ContainsKey(num) && !quests[num].isClear)
            {
                return ConsoleColor.DarkYellow;
            }
            else
            {
                return ConsoleColor.White;
            }
        }


        /// <summary>
        /// 퀘스트 정보 출력
        /// </summary>
        /// <param name="quest"></param>
        public void SelectQuest(Quest quest)
        {
            Console.Clear();
            Player player = GameManager.Instance.player;
            QuestManager.Instance.curQuest = quest;
            ColorText("---------------------------------------------", ConsoleColor.DarkCyan);
            Console.WriteLine($"퀘스트 : {quest.name}");
            ColorText("---------------------------------------------", ConsoleColor.DarkCyan);
            Console.WriteLine();
            Console.WriteLine($"{quest.description}\n");
            if (player.questList.ContainsKey(quest.questNum))
                QuestInfo(player.questList[quest.questNum]);
            else
                QuestInfo(quest);
            Console.WriteLine("\n- 보상 -\n");
            if (quest.rewardItem != null && quest.rewardItem.Count != 0)
            {
                foreach (var dic in quest.rewardItem)
                {
                    Item item = ItemDatabase.GetItem(dic.Key);
                    Console.WriteLine($"  {item.name} x {dic.Value}");
                }
            }
            if(quest.rewardGold > 0)
                Console.WriteLine($"  {quest.rewardGold}G");
            if(quest.rewardExp > 0)
                Console.WriteLine($"  경험치 {quest.rewardExp}");

            SelectChoice();
        }

        public void QuestInfo(Quest quest)
        {
            Player player = GameManager.Instance.player;
            switch (quest.questType)
            {
                case "사냥":
                    foreach (var questDic in quest.successConditions)
                    {
                        int curNum = quest.currentCounts[questDic.Key];
                        Console.WriteLine($"- {questDic.Key}을(를) {questDic.Value}마리 처치하세요 ({curNum}/{questDic.Value})");
                    }
                    break;
                case "레벨":
                    int presentLevel = quest.currentCounts[quest.questType];
                    int successLevel = quest.successConditions[quest.questType];
                    Console.WriteLine($"- 레벨을 {quest.successConditions["레벨"]}올리세요 ({presentLevel}/{successLevel})");
                    break;
                case "강화":
                    int presentEnhance = quest.currentCounts[quest.questType];
                    int successEnhance = quest.successConditions[quest.questType];
                    Console.WriteLine($"- 강화를 {successEnhance}회 성공하세요 ({presentEnhance}/{successEnhance})");
                    break;
                default:
                    Console.WriteLine($"- {quest.successDes}");
                    break;
            }
        }

        /// <summary>
        /// 퀘스트 수락, 보상 버튼 조건부 표출
        /// </summary>
        public void SelectChoice()
        {
            Player player = GameManager.Instance.player;
            if (player.questList.ContainsKey(QuestManager.Instance.curQuest.questNum))
            {
                Console.WriteLine("\n1. 보상 받기");
                Console.WriteLine("2. 돌아가기");
            }
            else
            {
                Console.WriteLine("\n1. 수락");
                Console.WriteLine("2. 거절");
            }
            Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
        }

        /// <summary>
        /// 보상메시지
        /// </summary>
        public void RewardMessage()
        {
            Player player = GameManager.Instance.player;
            Quest quest = QuestManager.Instance.curQuest;
            Console.WriteLine("보상을 획득하였습니다!");
            Console.WriteLine($"경험치 : {player.exp} -> {player.exp + quest.rewardExp}");
            Console.WriteLine($"골드 : {player.gold}G -> {player.gold + quest.rewardGold}G");
            if (quest.rewardItem != null && quest.rewardItem.Count > 0)
            {
                Console.WriteLine("획득 아이템");
                foreach (var dic in quest.rewardItem)
                {
                    Item item = ItemDatabase.GetItem(dic.Key);
                    Console.WriteLine($"{item.name} x {dic.Value}");
                }
            }
        }

        public void ColorText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        #endregion

        #region EndingUI

        public void DisplayShowEnding()
        {
            RecodeManager recode = RecodeManager.Instance;
            Console.Clear();

            int screenWidth = Console.WindowWidth;

            Action<string> writeCentered = (text) =>
            {
                if (string.IsNullOrEmpty(text))
                {
                    Console.WriteLine();
                    return;
                }
                int leftPadding = (screenWidth - text.Length) / 2;
                Console.CursorLeft = Math.Max(0, leftPadding);
                Console.WriteLine(text);
            };

            Action<string> writeSectionHeader = (text) =>
            {
                string headerText = $" {text} ";
                int remainingWidth = screenWidth - headerText.Length;
                int leftPadding = remainingWidth / 2;
                int rightPadding = remainingWidth - leftPadding;
                string header = new string('━', leftPadding) + headerText + new string('━', rightPadding);
                Console.WriteLine(header);
            };

            string border = new string('*', screenWidth);
            string title = "G A M E   C L E A R";
            string emptyBorderLine = "*" + new string(' ', screenWidth - 2) + "*";
            int titlePadding = (screenWidth - 2 - title.Length) / 2;
            string titleLine = "*" + new string(' ', titlePadding) + title + new string(' ', screenWidth - 2 - title.Length - titlePadding) + "*";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(border);
            Console.WriteLine(emptyBorderLine);
            Console.WriteLine(titleLine);
            Console.WriteLine(emptyBorderLine);
            Console.WriteLine(border);
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            writeCentered($" >> Lv.{recode.clearPlayer.level} {recode.clearPlayer.name} ({recode.clearPlayer.jobName}) << ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            writeSectionHeader("최종 기록");
            Console.ResetColor();
            Console.WriteLine();
            writeCentered($"가한 총 데미지  : {recode.totalDamage:N0}");
            writeCentered($"받은 총 데미지  : {recode.totalDamageTaken:N0}");
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            writeSectionHeader("처치한 몬스터");
            Console.ResetColor();
            Console.WriteLine();

            if (recode.defeatedMonsterList.Count == 0)
            {
                writeCentered("처치한 몬스터가 없습니다.");
            }
            else
            {
                foreach (var monster in recode.defeatedMonsterList)
                {
                    writeCentered($"- {monster.Key,-20} : {monster.Value,3}마리");
                }
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            writeSectionHeader("사용한 스킬 및 데미지");
            Console.ResetColor();
            Console.WriteLine();

            if (recode.usedSkillRecords.Count == 0)
            {
                writeCentered("사용한 스킬이 없습니다.");
            }
            else
            {
                foreach (var skill in recode.usedSkillRecords)
                {
                    writeCentered($"- {skill.skillName,-20} ({skill.skillUseCount,2}회) | 총 데미지: {skill.totalDamage:N0}");
                }
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(border);
            Console.WriteLine();
            writeCentered("Thank you for playing!");
            Console.WriteLine();
            Console.WriteLine(border);
            Console.ResetColor();

            Console.ReadKey(true);
        }
        #endregion

        #region TownActivityUI
        public void TownActivityScripts()
        {
            Console.WriteLine("마을에서 수행 할 활동을 선택해 주세요.\n\n1.순찰\n2.훈련\n\n0.나가기\n");
        }
        #endregion

        #region UIAnimation
        private void TunnelAnimation()
        {
            int centerX = 40;
            int centerY = 12;

            for (int frame = 0; frame < 25; frame++)
            {
                Console.Clear();

                for (int layer = 5; layer >= 0; layer--)
                {
                    int size = (layer * 8 + frame * 2) % 50;

                    if (size < 2) continue;

                    ConsoleColor color;
                    if (layer == 0) color = ConsoleColor.DarkGray;
                    else if (layer == 1) color = ConsoleColor.DarkGray;
                    else if (layer == 2) color = ConsoleColor.DarkGray;
                    else if (layer == 3) color = ConsoleColor.DarkBlue;
                    else if (layer == 4) color = ConsoleColor.DarkBlue;
                    else color = ConsoleColor.Black;

                    Console.ForegroundColor = color;

                    DrawRectangle(centerX - size / 2, centerY - size / 4, size, size / 2);
                }

                global::System.Threading.Thread.Sleep(150);
            }
            Console.Clear();
        }

        private void DrawRectangle(int x, int y, int width, int height)
        {
            if (width <= 0 || height <= 0) return;

            if (y >= 0 && y < Console.WindowHeight)
            {
                Console.SetCursorPosition(Math.Max(0, x), y);
                Console.Write(new string('█', Math.Min(width, Console.WindowWidth - Math.Max(0, x))));
            }

            if (y + height >= 0 && y + height < Console.WindowHeight)
            {
                Console.SetCursorPosition(Math.Max(0, x), y + height);
                Console.Write(new string('█', Math.Min(width, Console.WindowWidth - Math.Max(0, x))));
            }

            for (int i = 1; i < height; i++)
            {
                if (y + i >= 0 && y + i < Console.WindowHeight)
                {
                    if (x >= 0 && x < Console.WindowWidth)
                    {
                        Console.SetCursorPosition(x, y + i);
                        Console.Write('█');
                    }
                    if (x + width >= 0 && x + width < Console.WindowWidth)
                    {
                        Console.SetCursorPosition(x + width, y + i);
                        Console.Write('█');
                    }
                }
            }
        }

        public void DisplayKefgaBossEntrance()
        {
            Console.Clear();
            Console.CursorVisible = false;

            string[] kefgaArt = new string[]
            {
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠲⣶⠤⣤⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠓⣄⠉⠙⠛⠽⣶⣤⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣖⠦⣄⡀⠈⠙⠻⢽⣲⢤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢦⠀⠈⠓⠢⣄⡀⠀⠙⠺⣗⡢⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢳⡒⠤⢤⣀⣈⠓⠦⣄⠀⠉⠲⣝⠢⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢣⡀⠀⠀⠈⠉⠓⠦⣝⡢⣄⠀⠙⠮⡑⢦⣀⣀⡐⢻⠢⣌⡷⡄⠀⢿⢷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢧⠤⣄⡀⠀⠀⠀⠀⠉⠙⠿⣦⡀⠈⠣⡍⠿⣭⡙⢷⠸⠃⡇⠀⠈⣇⠙⢿⣤⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡆⠀⠉⠙⠲⠤⣄⡀⠀⠀⠈⠙⠂⠀⠈⠣⡌⠻⣆⠀⢰⠃⠀⠀⢹⠳⣀⠙⢿⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡏⠉⠉⠑⠒⠒⠤⣍⣓⠦⣀⠀⠀⠀⠀⠀⠈⢦⠈⠀⢸⠀⠀⠀⠘⡆⠘⢦⡀⠙⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢷⠀⠀⠀⠀⠀⠀⠀⠀⠉⠒⠻⣦⣄⠀⠀⠀⠀⢳⡀⠘⡆⠀⠀⠀⡗⠢⣀⠙⣄⠈⢫⣦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠲⠤⣤⣀⡀⠀⠀⠀⠀⠀⠀⠀⠈⠓⠀⠀⠀⠀⢣⠀⡇⠀⠀⠀⡇⠀⠈⠳⡌⢦⡀⠱⡷⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⣀⣀⣈⡙⠒⠢⠤⣀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠀⢳⠀⠀⢰⠛⢤⠀⠀⠈⠣⡳⡀⠸⡽⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⠀⠈⠉⠉⠑⠲⠭⢷⣤⣄⠀⠀⠀⠀⢨⠃⠸⡄⠀⡼⠤⡀⠙⢄⠀⠀⠙⢿⡄⠸⡜⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⠂⠀⠀⣇⠀⠀⡇⢠⠃⠀⠈⠱⢄⡳⡄⠀⠈⢻⡀⠸⡜⣶⣄⢲⣄⢀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⠧⠤⠤⠤⢀⣀⣀⣀⣀⣀⠀⠀⠀⠀⠀⠀⠀⢀⡔⠁⣠⠊⢀⠗⢄⡀⠀⠀⠀⠑⢎⣦⠀⠀⠁⠀⢳⠹⣎⢿⢸⢞⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣇⣀⡠⠤⠄⠒⠒⠒⠒⠒⠒⠚⠛⠒⠒⠄⠀⣠⠊⢀⡔⠁⢀⡟⠦⣀⠙⢢⡀⠀⠀⠀⠑⣷⡀⠀⠀⠈⡇⢻⠀⢀⠞⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡞⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡞⠁⣰⠊⠀⣠⠞⠀⠀⠀⠙⠢⣜⢦⡀⠀⠀⠈⢳⡀⠀⠀⢳⠀⢠⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡰⠃⢀⠞⠁⢠⠜⠥⣄⠀⠀⠀⠀⠀⠈⠑⢽⣦⡀⠀⠀⠁⠀⠀⣸⠀⡎⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡯⠤⠤⠒⠒⠒⣈⡩⠭⠭⠭⠭⠒⠒⢠⠞⠀⡴⠃⢠⠜⠓⠒⠤⠤⣍⡓⢤⡀⠀⠀⠀⠀⠙⢷⡄⠀⠀⠀⠀⡇⣰⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣠⠤⠖⠊⠉⠁⠀⠀⠀⠀⠀⠀⢀⠼⠁⢠⠞⢀⡔⠁⠀⠀⠀⠀⠀⠀⠉⠙⠺⢵⣦⡀⠀⠀⠀⠙⠆⠀⣠⠜⠀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡏⠀⠀⣹⣴⣋⣉⣉⡒⠦⢄⣀⠀⠀⠀⠀⠀⠀⠈⠙⠦⠀⠀⢀⣀⠴⠃⢀⡠⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣳⠀⠀⡏⠀⠀⠀⠀⠈⠉⠑⠒⠻⢖⣤⣀⠀⠀⠀⢀⣀⠴⠒⢉⣠⠔⠚⣉⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡞⠒⠊⢉⣩⠤⠔⠒⠒⠊⠉⠉⠀⠀⠈⢇⠀⠸⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢉⣣⠴⠒⢉⡠⠔⠚⠁⠀⠀⢰⠃⢀⣈⣙⣒⠢⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⠤⠒⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣸⠀⠀⠙⠮⡭⣗⣦⢤⡀⠀⠀⡴⠋⠁⡤⠔⠊⠁⠀⠀⠀⠀⠀⠀⡼⠊⠁⡴⣴⡾⣿⣌⠳⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡜⢁⡤⠔⢒⣒⡒⠒⠒⠒⠢⠔⠚⠉⠉⠁⠀⡎⠀⠀⠀⠀⠈⠏⠒⠭⣙⠓⠤⡇⠀⡴⠃⠀⠀⠀⠀⠀⠀⠀⣠⠜⠁⠀⠀⠓⠻⠷⠿⠟⠣⣌⠉⣲⡤⢄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣎⣴⡯⠊⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠀⠀⠀⠀⠀⠀⠀⠀⠈⠑⠴⠧⢤⣇⣀⣀⣀⣀⡤⠤⢖⡉⠀⠀⠀⠀⠀⠀⠀⠀⢠⢀⠤⠬⣍⣛⣧⣄⠈⠉⠒⠦⢤⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⣀⠀⠀⠀⠀⠀⢀⡤⠖⠉⠉⠉⠉⠉⠉⠩⠭⠵⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⠴⠊⠁⣠⣶⣤⣀⡀⠀⠀⣱⣄⠘⠸⢄⡐⠲⠶⣤⣌⣀⣀⠀⠀⠀⠀⠙⠛⢶⣤⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⡰⣾⡻⣅⡀⣀⡴⠚⠁⢀⡠⠖⠒⠒⠒⢶⠤⠤⠤⠤⠤⠤⢴⡶⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⣉⣁⡤⠴⠒⠊⠁⠀⠀⠉⠉⠉⠉⠁⠀⠉⠒⠒⠚⠷⠦⣄⣉⠒⠪⢍⣑⠒⠒⠤⢄⣀⣈⠓⣄⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠰⣟⠒⠁⠀⠉⠁⣠⠴⠚⠁⠀⠀⠀⢴⣞⣁⣀⣀⡤⠴⠒⠊⠁⠀⠀⠀⠀⠀⣀⣠⣄⣀⣠⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠴⠚⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠒⠦⣤⣉⣓⠲⠤⣄⡀⠉⠙⠁⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠉⠙⠛⠛⠉⠀⠀⠀⠀⠀⠀⣠⠔⠚⠉⠉⠀⠀⠀⠀⢀⣀⣠⠤⠤⠒⠋⠁⠀⠀⠀⠀⠀⠉⠙⠲⠤⣄⣀⠀⠀⠀⠀⠀⢀⣠⠤⠚⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠛⠓⠛⠛⠛⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠞⠁⢀⠤⠤⠴⠒⠚⠉⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠉⠉⠉⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣤⣶⠀⠀⣠⠞⠁⣠⠞⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⣏⣳⠈⠉⠚⠁⣠⠞⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠳⠤⠤⠤⠖⠚⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀"
            };

            int screenWidth = Console.WindowWidth/2;
            int artHeight = kefgaArt.Length;
            int artWidth = 150; 

            for (int x = -artWidth; x < screenWidth; x += 5)  
            {
                Console.Clear();

                Console.SetCursorPosition(screenWidth, 5);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" WARNING ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Gray;
                int startY = 10; 

                for (int i = 0; i < artHeight; i++)
                {
                    if (x >= 0 && x < screenWidth && startY + i < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(x, startY + i);
                        Console.Write(kefgaArt[i]);
                    }
                    else if (x < 0)  
                    {
                        int visibleStart = Math.Abs(x);
                        if (visibleStart < kefgaArt[i].Length)
                        {
                            string visiblePart = kefgaArt[i].Substring(visibleStart);
                            Console.SetCursorPosition(0, startY + i);
                            Console.Write(visiblePart);
                        }
                    }
                }
                Console.ResetColor();

                Thread.Sleep(150);
            }

            Console.Clear();
            Console.SetCursorPosition(screenWidth, Console.WindowHeight / 2);
            ConsoleHelper.PrintColored("『 케프가 』", ConsoleColor.White);
            Thread.Sleep(1000);
        }
        #endregion
    }
}
