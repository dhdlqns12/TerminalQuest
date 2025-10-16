using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;

namespace TerimalQuest.System
{
    public static class ConsoleHelper
    {
        /*
          * ConsoleHelper 스크립트
          * 
          * 콘솔 앱 터미널에 출력에 있어서 도움이 되는 기능들을 제공하는 스크립트
          * 
          */

        // 사용자 입력 체크 (옵션 여러가지 가져와서 예외처리)
        public static string GetUserChoice(string[] vaildOptions)
        {
            string choice;
            while (true)
            {
                Console.Write(">> ");
                choice = Console.ReadLine();
                Console.WriteLine();

                foreach (var option in vaildOptions) if (choice == option) return choice;

                UIManager.Instance.SelectWrongSelection();
            }
        }

        // 문자열 실제 표시 폭 계산 (한글 2칸, 알파벳 1칸 가정)
        public static int GetDisplayWidth(string text)
        {
            int width = 0;
            foreach (char c in text)
            {
                if (char.GetUnicodeCategory(c) == global::System.Globalization.UnicodeCategory.OtherLetter) width += 2; // 한글 등 넓은 글자
                else width += 1; // 알파벳, 숫자 등
            }
            return width;
        }

        // 문자열을 특정 칸에 맞춰 정렬
        public static string PadRightForConsole(string text, int totalWidth)
        {
            int pad = totalWidth - GetDisplayWidth(text);
            if (pad > 0) return text + new string(' ', pad);
            else return text;
        }
    }
}
