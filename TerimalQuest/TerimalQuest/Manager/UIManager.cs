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
            Console.Write("스파르타 던전에 오신 여러분 환영합니다. \n이제 전투를 시작할 수 있습니다. \n\n1.상태 보기 \n2.전투 시작\n0.게임 종료 \n\n원하시는 행동을 입력해주세요.\n>>");
        }

        public void ShowStatusSceneScripts()
        {
            Player p = GameManager.Instance.player;
            Console.Write($"상태 보기\r\n캐릭터의 정보가 표시됩니다.\r\n\r\nLv. {p.level}      \r\n{p.name} ( {p.jobName} )\r\n공격력 : {p.atk}\r\n방어력 : {p.def}\r\n체 력 : {p.hp}\r\nGold : {p.gold} G\r\n\r\n0. 나가기\r\n\r\n원하시는 행동을 입력해주세요.\r\n>> ");
        }

        public void SetNameScripts()
        {
            Console.Clear();
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
                    Console.WriteLine($"{itemPair.Key} - {itemPair.Value}");
                }
            }
        }
        #endregion
    }
}
