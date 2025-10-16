using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.System
{
    public enum AnimationType
    {
        Idle,
        Attack,
        Hit,
        Death
    }

    public static class MonsterAnimation
    {
        private static Dictionary<string, Dictionary<AnimationType, string[]>> animations = new Dictionary<string, Dictionary<AnimationType, string[]>>();

        static MonsterAnimation()
        {
            InitializeAnimations();
        }

        private static void InitializeAnimations()
        {
            InitSlimeAnimation();
            InitOrcAnimation();
            InitTrollAnimation();
        }

        private static void InitSlimeAnimation()
        {
            animations["슬라임"] = new Dictionary<AnimationType, string[]>
            {
                [AnimationType.Idle] = new string[]
                {
            @"
    ___
   /   \
  | o o |
  |  >  |
   \___/
    | |
"
                },

                [AnimationType.Attack] = new string[]
                {
            @"
     ___
    /   \
   | O O |
   | >>> |
    \___/
     | |
      !
",
            @"
      ___
     /   \
    | >O< |
    | >>> |
     \___/
    __|__
   !!!!!
"
                },

                [AnimationType.Hit] = new string[]
                {
            @"
    ___
   / * \
  | X X |
  |  o  |
   \___/
    |_|
"
                },

                [AnimationType.Death] = new string[]
                {
            @"
    ___
   / X \
  | X_X |
  |  ~  |
   \___/
",
            @"
    ~~~
   ~ X ~
    ~~~
"
                }
            };
        }

        // 오크 애니메이션
        private static void InitOrcAnimation()
        {
            animations["오크"] = new Dictionary<AnimationType, string[]>
            {
                [AnimationType.Idle] = new string[]
                {
            @"
    /\_/\
   | o o |
   |  >  |
  /|     |\
 | |▓▓▓▓▓| |
 |  \___/  |
  \__|_|__/
    || ||
   ||| |||
"
                },

                [AnimationType.Attack] = new string[]
                {
            @"
    /\_/\
   | O O |
   |  V  |
  /|     |\  
 | |▓▓▓▓▓| |
 |  \___/  |
  \__|_|__/
   \|| ||
    || |||
",
            @"
    /\_/\
   | @_@ |
   | VVV |  
  /|     |\
 | |▓▓▓▓▓| |//
 |  \___/  |
  \__|_|__/
    \\||//
     ||||
"
                },

                [AnimationType.Hit] = new string[]
                {
            @"
    /\_/\  *
   | X X |
   |  o  | *
  /|     |\
 | |▓▓▓▓▓| |
 |  \___/  |
  \__|_|__/
    || ||
"
                },

                [AnimationType.Death] = new string[]
                {
            @"
    /\_/\
   | X X |
   |  ~  |
  /|     |\
 | |▓▓▓▓▓| |
 |  \___/  |
  \__|_|__/
",
            @"
     X X
    / ~ \
   |▓▓▓▓▓|
    \___/
   __|_|__
"
                }
            };
        }

        // 트롤 애니메이션
        private static void InitTrollAnimation()
        {
            animations["트롤"] = new Dictionary<AnimationType, string[]>
            {
                [AnimationType.Idle] = new string[]
                {
            @"
      /\___/\
     | O   O |
    /|   ^   |\
   / |       | \
  |  |▓▓▓▓▓▓▓|  |
  |  |▓▓▓▓▓▓▓|  |
   \ |▓▓▓▓▓▓▓| /
    \|_______|/
     |||   |||
    |||| ||||
   ||||| |||||
"
                },

                [AnimationType.Attack] = new string[]
                {
            @"
      /\___/\
     | O   O |  
    /|  VVV  |\
   / |       | \
  |  |▓▓▓▓▓▓▓|  |
  |  |▓▓▓▓▓▓▓|  |
   \ |▓▓▓▓▓▓▓| /
    \|_______|/
     \||   ||/
      ||||||
     |||||||
",
            @"
      /\___/\  
     | @   @ |
    /| VVVVV |\
   / |       | \
  |  |▓▓▓▓▓▓▓|  |//
  |  |▓▓▓▓▓▓▓|  |
   \ |▓▓▓▓▓▓▓| /
    \|_______|/
      \\|||//
       |||||
      ||||||
"
                },

                [AnimationType.Hit] = new string[]
                {
            @"
      /\___/\  **
     | X   X |
    /|   ~   |\
   / |       | \
  |  |▓▓▓▓▓▓▓|  |
  |  |▓▓▓▓▓▓▓|  |
   \ |▓▓▓▓▓▓▓| /
    \|_______|/
     |||   |||
"
                },

                [AnimationType.Death] = new string[]
                {
            @"
      /\___/\
     | X   X |
    /|   ~   |\
   / |       | \
  |  |▓▓▓▓▓▓▓|  |
   \ |▓▓▓▓▓▓▓| /
    \|_______|/
",
            @"
       X   X
      /  ~  \
     |▓▓▓▓▓▓▓|
      \_____/
     ___|___
"
                }
            };
        }

        public static string[] GetAnimation(string monsterName, AnimationType type)
        {
            if (animations.ContainsKey(monsterName) && animations[monsterName].ContainsKey(type))
            {
                return animations[monsterName][type];
            }

            return new string[] { "" };
        }

        public static int GetAnimationHeight(string monsterName)
        {
            var idle = GetAnimation(monsterName, AnimationType.Idle);
            if (idle.Length > 0)
            {
                return idle[0].Split('\n').Length;
            }
            return 10;
        }
    }
}

