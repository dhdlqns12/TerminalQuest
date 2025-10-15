using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;

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

    }
}
