using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Core;

namespace TerimalQuest.System
{
    public static class BattleDisplay
    {
        private const int MonsterSpacing = 30;

        public static void DisplayMonsters(List<Monster> monsters, AnimationType animationType = AnimationType.Idle)
        {
            if (monsters == null || monsters.Count == 0) return;

            int startTop = Console.CursorTop;

            var aliveMonsters = monsters.Where(m => m.hp > 0).ToList();
            if (aliveMonsters.Count == 0) return;

            for (int i = 0; i < 20; i++)
            {
                Console.SetCursorPosition(0, startTop + i);
                Console.Write(new string(' ', Console.WindowWidth - 1));
            }

            for (int i = 0; i < aliveMonsters.Count; i++)
            {
                int xPos = i * MonsterSpacing;
                Console.SetCursorPosition(xPos+5, startTop);

                int originalIndex = monsters.IndexOf(aliveMonsters[i]);
                string nameWithLv = $"[{originalIndex + 1}] {aliveMonsters[i].name} Lv.{aliveMonsters[i].level}";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(nameWithLv);
                Console.ResetColor();
            }

            for (int i = 0; i < aliveMonsters.Count; i++)
            {
                int xPos = i * MonsterSpacing;
                Console.SetCursorPosition(xPos, startTop + 1);
                DrawHpBar(aliveMonsters[i]);
            }

            if (animationType == AnimationType.Idle)
            {
                DisplayStaticMonsters(aliveMonsters, startTop + 3);
            }
            else
            {
                DisplayAnimatedMonsters(aliveMonsters, animationType, startTop + 3);
            }

            Console.SetCursorPosition(0, startTop + 20);
        }

        private static void DisplayStaticMonsters(List<Monster> monsters, int startLine)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                int xPos = i * MonsterSpacing;
                var animation = MonsterAnimation.GetAnimation(monsters[i].name, AnimationType.Idle);
                string[] lines = animation[0].Split('\n');

                for (int lineIdx = 0; lineIdx < lines.Length; lineIdx++)
                {
                    Console.SetCursorPosition(xPos, startLine + lineIdx);
                    Console.Write(lines[lineIdx]);
                }
            }
        }

        private static void DisplayAnimatedMonsters(List<Monster> monsters, AnimationType type, int startLine)
        {
            string[][] animations = new string[monsters.Count][];
            int maxFrames = 0;

            for (int i = 0; i < monsters.Count; i++)
            {
                animations[i] = MonsterAnimation.GetAnimation(monsters[i].name, type);
                if (animations[i].Length > maxFrames)
                {
                    maxFrames = animations[i].Length;
                }
            }

            for (int frame = 0; frame < maxFrames; frame++)
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    int xPos = i * MonsterSpacing;
                    int frameIndex = Math.Min(frame, animations[i].Length - 1);
                    string[] lines = animations[i][frameIndex].Split('\n');

                    for (int lineIdx = 0; lineIdx < lines.Length; lineIdx++)
                    {
                        Console.SetCursorPosition(xPos, startLine + lineIdx);
                        Console.Write(lines[lineIdx]);
                    }
                }

                if (frame < maxFrames - 1)
                {
                    Thread.Sleep(300);
                }
            }
        }

        private static void DrawHpBar(Monster monster)
        {
            int barLength = 15;
            float hpPercent = monster.hp / monster.maxHp;
            int filled = (int)(barLength * hpPercent);

            if (filled < 0) filled = 0;
            if (filled > barLength) filled = barLength;

            Console.Write("HP [");

            if (hpPercent > 0.5f)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (hpPercent > 0.2f)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(new string('█', filled));
            Console.ResetColor();
            Console.Write(new string('▒', barLength - filled));
            Console.Write("]");
        }

        public static void PlayMonsterAnimation(Monster monster, AnimationType type, int delayMs = 300)
        {
            var animation = MonsterAnimation.GetAnimation(monster.name, type);
            int startLine = Console.CursorTop;

            Console.WriteLine($"\n{monster.name}");
            startLine = Console.CursorTop;

            foreach (var frame in animation)
            {
                Console.SetCursorPosition(0, startLine);
                string[] lines = frame.Split('\n');
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }

                Thread.Sleep(delayMs);
            }

            Console.SetCursorPosition(0, startLine + 15);
        }
    }
}
