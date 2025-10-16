using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Console.WriteLine($"           {text}");
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
            Console.Write($"스파르타 던전에 오신 여러분 환영합니다. \n이제 전투를 시작할 수 있습니다. \n\n1.상태 보기 \n2.인벤토리\n3.전투 시작(현재 진행 : {GameManager.Instance.player.curStage}층)\n4.퀘스트\n5.상점\n6.마을활동\n0.게임 종료 \n\n원하시는 행동을 입력해주세요.\n>>");
        }

        public void ShowStatusSceneScripts()
        {
            Player p = GameManager.Instance.player;
            Console.Write($"상태 보기\r\n캐릭터의 정보가 표시됩니다.\r\n\r\nLv. {p.level}      \r\n{p.name} ( {p.jobName} )\r\n공격력 : {p.atk}\r\n방어력 : {p.def}\r\n체 력 : {p.hp}\r\nGold : {p.gold} G\r\n\r\n0. 나가기\r\n\r\n원하시는 행동을 입력해주세요.\r\n>> ");
        }

        public void SetNameScripts()
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
                        Console.SetCursorPosition(startPos, i );
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
        public void DisplayItemInfo(Item item, bool isEquipMode = false)
        {
            string itemName = item.name;

            if (item is Weapon || item is Armor)
            {
                itemName = GetEquipItemName(item);
            }

            // 번호 모드
            string numberMode = (isEquipMode) ? ConsoleHelper.PadRightForConsole(" ", 6) : $"  ";

            Console.WriteLine(string.Format("{0} | {1} | {2} | {3}",
                ConsoleHelper.PadRightForConsole(itemName, offsetName),
                ConsoleHelper.PadRightForConsole(item.GetEffectText(), offsetEffect),
                ConsoleHelper.PadRightForConsole(item.desc, offsetDesc),
                item.GetCountText()
            ));
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
        public void DisplayItemProduct(Item item)
        {
            string itemPurchase = (item.isPurchase) ? "구매완료" : $"{item.price}";
            string isGoldIcon = (item.isPurchase) ? "" : "G";

            Console.WriteLine(string.Format("{0} | {1} | {2} | {3} | {4} {5}",
                ConsoleHelper.PadRightForConsole(item.name, offsetName),
                ConsoleHelper.PadRightForConsole(item.GetEffectText(), offsetEffect),
                ConsoleHelper.PadRightForConsole(item.desc, offsetDesc),
                ConsoleHelper.PadRightForConsole(item.GetCountText(), offsetCount),
                ConsoleHelper.PadRightForConsole(itemPurchase, offsetPurchase),
                isGoldIcon));
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
            DisplayOption(["1. 장착 관리", "2. 아이템 정렬", "0. 나가기"]);
        }

        // 플레이어 인벤토리 장착 관리 창 : 플레이어의 아이템을 장착/해제 할 수 있다.
        public void InventoryEquipScripts(Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            inventory.DisplayInfo(true);
            Console.WriteLine();
            DisplayOption(["(번호). 해당 장비 장착", "0. 나가기"]);
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

        #endregion

        #region ShopUI

        // 상점
        public void ShopScripts(Player player, Shop shop)
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G");
            Console.WriteLine();
            shop.DisplayInfo(false);
            Console.WriteLine();
            DisplayOption(["1. 아이템 구매", "2. 아이템 판매", "0. 나가기"]);
        }

        // 상점 : 상품 구매
        public void ShopPurchaseScripts(Player player, Shop shop)
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G");
            Console.WriteLine();
            shop.DisplayInfo(true);
            Console.WriteLine();
            DisplayOption(["(번호). 해당 아이템 구매", "0. 나가기"]);
        }

        // 상점 : 아이템 판매
        public void ShopSaleScripts(Player player)
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G");
            Console.WriteLine();
            player.inventory.DisplayInfoWithGold();
            Console.WriteLine();
            DisplayOption(["(번호). 해당 아이템 판매", "0. 나가기"]);
        }

        #endregion

        #region EnhancementUI

        public void DisplayEnhancementStartScripts(Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("장비 강화");
            Console.WriteLine("보유 중인 장비를 강화할 수 있습니다.");
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

        #endregion

        #region BattleUI

        public void AttackTarget(Character attacker, Character target, bool isEvade)
        {
            Console.Clear();
            Console.WriteLine("Battle!");
            Console.WriteLine($"Lv.{attacker.level} {attacker.name} 의 공격!");

            if (isEvade)
            {
                Console.WriteLine($"Lv.{target.level} {target.name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                return;
            }

            int finalDamage = attacker.GetFinalDamage(out bool isCritical, (int)target.def);
            string attackStr = $"{target.name} 을(를) 맞췄습니다. [데미지 : {finalDamage}]";

            if (isCritical) attackStr += " - 치명타 공격!!";

            Console.WriteLine(attackStr);
            Console.WriteLine($"Lv. {target.level} {target.name}");

            string deadResult = target is Player ? "0" : "Dead";
            string attackResult =
                $"HP {target.hp} -> {(target.hp - finalDamage > 0 ? target.hp - finalDamage : deadResult)}";

            Console.WriteLine(attackResult);
        }

        public void AttackTargetWithSkill(Character attacker, Character target, Skill skill)
        {
            Console.Clear();
            Console.WriteLine("Battle!\n");
            Console.WriteLine($"{target.name}에게 {attacker.name}의 {skill.name} 공격!");
            string deadResult = target is Player ? "0" : "Dead";
            string attackResult = "";
            if (skill.damageType == SkillDamageType.FixedDamage)
            {
                attackResult =
                    $"Lv.{target.level} {target.name} HP {target.hp} -> {(target.hp - skill.damage > 0 ? target.hp - skill.damage : deadResult)} [데미지 : {skill.damage}]";
            }
            else
            {
                attackResult =
                    $"Lv.{target.level} {target.name} HP {target.hp} -> {(target.hp - skill.damage * attacker.atk > 0 ? target.hp - skill.damage * attacker.atk : deadResult)} [데미지 : {skill.damage * attacker.atk}]";
            }

            Console.WriteLine(attackResult);
        }

        public void BattleEntrance(List<Monster> monsters, Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            for (int i = 0; i < monsters.Count; i++)
            {
                if(monsters[i].hp > 0)
                {
                    Console.Write($"{i + 1}. ");
                }
                Console.Write($"Lv.{monsters[i].level} {monsters[i].name}  HP {(monsters[i].hp > 0 ? monsters[i].hp : "Dead")}\n");
            }

            Console.WriteLine();

            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.level} {player.name} ({player.jobName})");
            Console.WriteLine($"HP {player.hp}/{player.maxHp}");
            Console.WriteLine($"MP {player.mp}/{player.maxMp}");
        }

        public void SelectTarget()
        {
            Console.WriteLine();
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("대상을 선택해주세요.\n>>");

        }

        public void SelectWrongSelection()
        {
            Console.WriteLine("잘못된 입력입니다.");
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

        public void DisplayBattleChoice()
        {
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("0. 후퇴");
        }

        public void DisplaySelectingSkill(List<Skill> skillList)
        {
            Console.WriteLine();
            for (int i = 0; i < skillList.Count; i++)
            {
                Skill skill = skillList[i];
                Console.WriteLine($"{i + 1}. {skill.name} - MP {skill.cost}");
                Console.WriteLine($"{skill.description}");
            }
            Console.WriteLine("0. 취소");
        }

        public void DisplayNotEnoughMagicCost()
        {
            Console.WriteLine("MP가 부족합니다.");
        }

        public void DisplayNotEnoughHp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("HP가 부족해서 전투가 불가능합니다.");
            Console.ResetColor();
        }

        public void DisplayUseSupportSkill(Player player ,Skill skill)
        {
            Console.WriteLine();
            Console.WriteLine($"{skill.name} 사용!");
            Console.WriteLine($"{player.name}의 체력이 회복되었다.");
            Console.WriteLine($"{player.hp} -> {(player.hp + skill.damage)}");
        }

        public void DisplayFullRangeAttackSkillResult(List<Monster> monsterList, Skill skill, float finalSkillDamage)
        {
            Console.WriteLine();
            Console.WriteLine($"모든 적에게 {skill.name} 시전! {finalSkillDamage}의 데미지!");
            for (int i = 0; i < monsterList.Count; i++)
            {
                Monster monster = monsterList[i];
                if (monster.hp < 0) continue;
                Console.WriteLine($"{monster.name} - HP {monster.hp} -> {(monster.hp - finalSkillDamage >= 0 ? monster.hp -finalSkillDamage : "Dead")}");
            }
        }

        public void DisplayStageClearStatus(int lastClearStage)
        {
            const int TOTAL_STAGES = 10;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=============== 스테이지 현황 ===============");
            Console.ResetColor();

            for (int i = 0; i < TOTAL_STAGES; i++)
            {
                int currentStage = i + 1;
                string statusText;
                ConsoleColor statusColor;

                if (currentStage <= lastClearStage-1)
                {
                    statusText = "Clear!";
                    statusColor = ConsoleColor.Green;
                }
                else if (currentStage == lastClearStage)
                {
                    statusText = "(Now)";
                    statusColor = ConsoleColor.Yellow;
                }
                else
                {
                    statusText = "(Locked)";
                    statusColor = ConsoleColor.DarkGray;
                }

                Console.Write($"Stage{currentStage, -2} ");
                Console.ForegroundColor = statusColor;
                Console.Write($"{statusText,-8}\n");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===========================================");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        public void DisplayPressAnyKeyToNext()
        {
            Console.WriteLine("진행하려면 아무키나 입력해주세요.");
            Console.ReadKey(true);
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
            Console.WriteLine("퀘스트 목록\n");
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
            Console.WriteLine("서브 퀘스트\n");
            for (int i = 0; i < subQuests.Count; i++)
            {
                string questRunning = "";
                if (player.questList != null)
                {
                    questRunning = player.questList.ContainsKey(subQuests[i].questNum) ? "[진행중]" : "";
                }
                Console.WriteLine($"{i + 2}. {subQuests[i].name} {questRunning}");
            }
        }

        public void ShowMainQuest(Player player)
        {
            List<Quest> mainQuests = QuestManager.Instance.mainQuests;
            Console.WriteLine("메인 퀘스트\n");
            if (player.questList != null && mainQuests.Count > 0)
            {
                string questRunning = player.questList.ContainsKey(mainQuests[0].questNum) ? "[진행중]" : "";
                Console.WriteLine($"1. {mainQuests[0].name} {questRunning}");
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
            Console.WriteLine($"퀘스트 : {quest.name}\n");
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
            Console.WriteLine($"  {quest.rewardGold}G");
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
                    Console.WriteLine($"- 레벨을 {quest.successConditions["레벨"]}올리세요");
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
        #endregion

        #region EndingUI

        public void DisplayShowEnding()
        {
            RecodeManager recode = RecodeManager.Instance;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**************************************************");
            Console.WriteLine("*                                                *");
            Console.WriteLine("*             G A M E   C L E A R                *");
            Console.WriteLine("*                                                *");
            Console.WriteLine("**************************************************");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" >>Lv.{recode.clearPlayer.level} {recode.clearPlayer.name} ({recode.clearPlayer.jobName}) <<");
            Console.ResetColor();
            Console.WriteLine();

            // 4. 종합 기록 출력
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("━━━━━━━━━━━━━━ 최종 기록 ━━━━━━━━━━━━━━");
            Console.ResetColor();
            Console.WriteLine($"  가한 총 데미지  : {recode.totalDamage:N0}");
            Console.WriteLine($"  받은 총 데미지  : {recode.totalDamageTaken:N0}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("━━━━━━━━━━━━ 처치한 몬스터 ━━━━━━━━━━━━");
            Console.ResetColor();

            if (recode.defeatedMonsterList.Count == 0)
            {
                Console.WriteLine("  처치한 몬스터가 없습니다.");
            }
            else
            {
                foreach (var monster in recode.defeatedMonsterList)
                {
                    Console.WriteLine($"  - {monster.Key,-15} : {monster.Value,3}마리");
                }
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("━━━━━━━━━━ 사용한 스킬 및 데미지 ━━━━━━━━━━");
            Console.ResetColor();

            if (recode.usedSkillRecords.Count == 0)
            {
                Console.WriteLine("  사용한 스킬이 없습니다.");
            }
            else
            {
                foreach (var skill in recode.usedSkillRecords)
                {
                    Console.WriteLine($"  - {skill.skillName,-15} ({skill.skillUseCount,2}회) | 총 데미지: {skill.totalDamage:N0}");
                }
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**************************************************");
            Console.WriteLine("\n           Thank you for playing!\n");
            Console.WriteLine("**************************************************");
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
    }
}
