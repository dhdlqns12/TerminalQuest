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
            Console.Write("스파르타 던전에 오신 여러분 환영합니다. \n이제 전투를 시작할 수 있습니다. \n\n1.상태 보기 \n2.인벤토리\n3.전투 시작\n4.퀘스트\n5.상점\n0.게임 종료 \n\n원하시는 행동을 입력해주세요.\n>>");
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

            int finalDamage = attacker.GetFinalDamage(out bool isCritical);
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

        public void DisplayUseSupportSkill(Player player ,Skill skill)
        {
            Console.WriteLine();
            Console.WriteLine($"{skill.name} 사용!");
            Console.WriteLine($"{player.name}의 체력이 회복되었다.");
            Console.WriteLine($"{player.hp} -> {(player.hp + skill.damage)}");
        }

        public void DisplayFullRangeAttackSkillResult(List<Monster> monsterList, Skill skill)
        {
            Console.WriteLine();
            Console.WriteLine($"모든 적에게 {skill.name} 시전!");
            for (int i = 0; i < monsterList.Count; i++)
            {
                Monster monster = monsterList[i];
                if (monster.hp < 0) continue;
                Console.WriteLine($"{monster.name} - HP {monster.hp + skill.damage} -> {monster.hp}");
            }
        }

        #endregion
    }
}
