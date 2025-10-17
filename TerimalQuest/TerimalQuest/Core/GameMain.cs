using System.Runtime.InteropServices;
using TerimalQuest.Manager;

namespace TerimalQuest.Core
{
    public class GameMain
    {
        static void Main(string[] args)
        {
            SafeResize(200, 50);

            UIManager.Instance.ShowTitle("스파르타 마을에 오신것을 환영합니다!");
            Console.OutputEncoding = global::System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            int saveDataCount = 0;
            for(int i =1; i<=3;i++)
            {
                if (SaveManager.HasSaveData(i))
                {
                    saveDataCount++;
                }
            }
            if (saveDataCount >= 1)
            {
                UIManager.Instance.HasSaveDataScripts();
                GameManager.Instance.Run(1);
            }
            else
            {
                UIManager.Instance.EmptySaveDataScripts();
                GameManager.Instance.Run(0);
            }
        }

        static void SafeResize(int cols, int rows)
        {
            try
            {
                cols = Math.Max(1, cols);
                rows = Math.Max(1, rows);

                if (Console.LargestWindowWidth > 0) cols = Math.Min(cols, Console.LargestWindowWidth);
                if (Console.LargestWindowHeight > 0) rows = Math.Min(rows, Console.LargestWindowHeight);
                int bufW = Math.Max(Console.BufferWidth, cols);
                int bufH = Math.Max(Console.BufferHeight, rows);
                if (bufW != Console.BufferWidth || bufH != Console.BufferHeight)
                    Console.SetBufferSize(bufW, bufH);
                Console.SetWindowSize(cols, rows);
            }
            catch (Exception ex) when (
                ex is IOException ||
                ex is PlatformNotSupportedException ||
                ex is ArgumentOutOfRangeException)
            {
                Console.WriteLine($"콘솔 크기 변경 실패 {ex.GetType().Name} 발생");
            }
        }
    }
}
