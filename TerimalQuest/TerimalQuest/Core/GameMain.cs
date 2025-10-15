using TerimalQuest.Manager;

namespace TerimalQuest.Core
{
    public class GameMain
    {
        static void Main(string[] args)
        {
            UIManager.Instance.ShowTitle("스파르타 마을에 오신것을 환영합니다!");
            int saveDataCount = 0;
            for(int i =1; i<=3;i++)
            {
                saveDataCount++;
            }
            if (saveDataCount >= 1)
            {
                UIManager.Instance.EmptySaveDataScripts();
                GameManager.Instance.Run(0);
            }
            else
            {
                UIManager.Instance.HasSaveDataScripts();
                GameManager.Instance.Run(0);
            }
        }
    }
}
