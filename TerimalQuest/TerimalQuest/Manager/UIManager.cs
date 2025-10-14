using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
