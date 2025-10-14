using TerimalQuest.Core;
using TerimalQuest.Manager;
using TerimalQuest.Scenes;

public class StartScene : IScene
{
    public event Action<IScene> OnSceneChangeRequested;

    public void Enter()
    {
        for (int i = 1; i <= 3; i++)
        {
            if (!SaveManager.HasSaveData(i))
            {
                TempCreatePlayer();
            }
            else
            {
                break; // 추후 작성 예정
            }
        }

        ShowMainMenu();
    }

    public void Update()
    {
        if (int.TryParse(Console.ReadLine(), out int answer))
        {
            switch (answer)
            {
                case 1:
                    OnSceneChangeRequested?.Invoke(new ShowStatusScene());
                    break;
                case 2:
                    OnSceneChangeRequested?.Invoke(new BattleScene());
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    break;
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadKey();
        }
    }

    public void Exit()
    {
        // 나중에 UI 클리어, 리소스 해제 등 넣을 수 있음
    }

    private void TempCreatePlayer()
    {
        Console.Clear();
        UIManager.Instance.ShowTitle("캐릭터 생성");

        string name = TempInputName();

        Job job = TempSelectJob();

        GameManager.Instance.player.Init_Player(name, job);
    }

    private string TempInputName() // 이름 입력
    {
        UIManager.Instance.ShowTitle("캐릭터 생성");

        while (true)
        {
            Console.Write("캐릭터의 이름을 입력하세요: ");
            string name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("이름을 입력해주세요!");
            }

            return name;
        }
    }

    private Job TempSelectJob()  //직업 선택
    {
        JobType[] jobType = (JobType[])Enum.GetValues(typeof(JobType));
        UIManager.Instance.ShowTitle("캐릭터 생성");

        while (true)
        {
            Console.Clear();
            UIManager.Instance.ShowTitle("직업 선택\n");

            for (int i = 0; i < jobType.Length; i++)
            {
                Job job = new Job(jobType[i]);
                Console.WriteLine($"[{i + 1}] {job.name}");
                Console.WriteLine($"    {job.description}");
                Console.WriteLine($"    HP: {job.maxHp} | MP: {job.maxMp}");
                Console.WriteLine($"    공격력: {job.atk} | 방어력: {job.def}");
                Console.WriteLine("-------------------------------------------------------------\n");
            }

            Console.Write("직업을 선택하세요: ");

            if (int.TryParse(Console.ReadLine(), out int choice)) // 숫자인지 문자인지
            {
                if (choice >= 1 && choice <= jobType.Length)
                {
                    return new Job(jobType[choice - 1]);
                }
            }
        }
    }

    private void ShowMainMenu()
    {
        Console.Clear();
        UIManager.Instance.ShowStartSceneScripts();
    }
}
