using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using TerimalQuest.Core;
using TerimalQuest.System;

namespace TerimalQuest.Manager
{
    public class SaveData
    {
        public string name { get; set; }
        public int level { get; set; }
        public float hp { get; set; }
        public float atk { get; set; }
        public float def { get; set; }
        public JobType jobType { get; set; }
        public int gold { get; set; }
        public int stamina { get; set; }
        public int exp { get; set; }
        public int curStage { get; set; }

        //아이템 장착 안한 기본 스탯들
        public float baseAtk { get; set; }
        public float baseDef { get; set; }
        public float baseCritRate { get; set; }
        public float baseEvadeRate { get; set; }

        public List<Item> playerInventory { get; set; } //배열로 구현하신다 하셧으니 배열로 변경
        public List<Item> equipItem { get; set; }

        public SaveData() { }
        public SaveData(Player player/*, List<Item> _playerInventory, List<Item> _equipItem*/) //아이템 저장 및 장착 부분은 추후에 수정 예정
        {
            name = player.name;
            level = player.level;
            hp = player.hp;
            atk = player.atk;
            def = player.def;
            jobType = player.job.jobType;
            gold = player.gold;
            stamina = player.stamina;
            exp = player.exp;
            curStage = player.curStage;

            baseAtk = player.baseAtk;
            baseDef = player.baseDef;
            baseCritRate = player.baseCritRate;
            baseEvadeRate = player.baseEvadeRate;

            //playerInventory = new List<Item>(_playerInventory);
            //equipItem = new List<Item>(_equipItem);
        }
    }

    public static class SaveManager
    {
        static string SavePath(int slot)
        {
            return $"SaveGame{slot}.json";
        }

        public static void GameSave(/*, List<Item> _playerInventory, List<Item> _equipItem,*/int _slot) //추후 아이템 관련 및 추가 저장 수정
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            Player player = GameManager.Instance.player;
            SaveData data = new SaveData(player/*, _playerInventory, _equipItem*/);
            string json_Serialize = JsonSerializer.Serialize(data, options);
            File.WriteAllText(SavePath(_slot), json_Serialize);
        }

        public static SaveData GameLoad(int _slot)
        {
            string path = SavePath(_slot);

            string json_Deserialize = File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };

            SaveData data = JsonSerializer.Deserialize<SaveData>(json_Deserialize, options);

            Player loadedPlayer = new Player();

            loadedPlayer.Init_Player_Name(data.name);
            Job job = new Job(data.jobType);
            loadedPlayer.Init_Player_job(job);

            loadedPlayer.level = data.level;
            loadedPlayer.hp = data.hp;
            loadedPlayer.gold = data.gold;
            loadedPlayer.stamina = data.stamina;
            loadedPlayer.curStage = data.curStage;
            loadedPlayer.baseAtk = data.baseAtk;
            loadedPlayer.baseDef = data.baseDef;
            loadedPlayer.baseCritRate = data.baseCritRate;
            loadedPlayer.baseEvadeRate = data.baseEvadeRate;

            loadedPlayer.SetExpWithoutLevelUp(data.exp);

            loadedPlayer.UpdateStats();

            GameManager.Instance.player = loadedPlayer;
            return data;
        }

        public static bool HasSaveData(int _slot)
        {
            return File.Exists(SavePath(_slot));
        }

        public static SaveData GetSlotInfo(int _slot)
        {
            if (!HasSaveData(_slot))
            {
                return null;
            }

            string path = SavePath(_slot);
            string json_Deserialize = File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };

            SaveData data = JsonSerializer.Deserialize<SaveData>(json_Deserialize, options);
            return data;
        }
    }
}



