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

        public List<Weapon> weapons { get; set; }
        public List<Armor> armors { get; set; }
        public List<Potion> potions { get; set; }
        public List<EnhancementStone> enhanceStone { get; set; }

        public string equippedWeapon { get; set; }
        public string equippedArmor { get; set; }

        public Dictionary<int, Quest> questList { get; set; }

        public List<int> clearQuestNums { get; set; }

        // 상점 아이템 목록
        public List<Weapon> shopWeapons { get; set; }
        public List<Armor> shopArmors { get; set; }
        public List<Potion> shopPotions { get; set; }
        public List<EnhancementStone> shopStones { get; set; }

        public SaveData() { }

        public SaveData(Player player)
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

            //아이템 타입별 리스트 분리!
            weapons = player.inventory.Items.OfType<Weapon>().ToList();
            armors = player.inventory.Items.OfType<Armor>().ToList();
            potions = player.inventory.Items.OfType<Potion>().ToList();
            enhanceStone = player.inventory.Items.OfType<EnhancementStone>().ToList();

            equippedWeapon = player.equippedWeapon?.name;
            equippedArmor = player.equippedArmor?.name;

            questList = player.questList;
            clearQuestNums = player.clearQuestNums;

            // 상점 아이템 타입별 리스트 분리!
            Shop shop = GameManager.Instance.shop;
            shopWeapons = shop.ProductList.OfType<Weapon>().ToList();
            shopArmors = shop.ProductList.OfType<Armor>().ToList();
            shopPotions = shop.ProductList.OfType<Potion>().ToList();
            shopStones = shop.ProductList.OfType<EnhancementStone>().ToList();
        }
    }

    public static class SaveManager
    {
        static string SavePath(int slot)
        {
            return $"SaveGame{slot}.json";
        }

        public static void GameSave(int _slot)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            Player player = GameManager.Instance.player;
            SaveData data = new SaveData(player);
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

            loadedPlayer.questList = (data.questList != null) ? data.questList : new Dictionary<int, Quest>();
            loadedPlayer.clearQuestNums = (data.clearQuestNums != null) ? data.clearQuestNums : new List<int> ();

            loadedPlayer.SetExpWithoutLevelUp(data.exp);

            LoadInventory(loadedPlayer, data);

            EquippedItems(loadedPlayer, data);

            loadedPlayer.UpdateStats();

            GameManager.Instance.player = loadedPlayer;

            GameManager.Instance.shop = new Shop();

            LoadShopState(data);

            return data;
        }

        private static void LoadInventory(Player player, SaveData data)
        {
            player.inventory.Clear();

            if (data.weapons != null)
            {
                foreach (var weapon in data.weapons)
                {
                    player.inventory.Items.Add(weapon);
                }
            }

            if (data.armors != null)
            {
                foreach (var armor in data.armors)
                {
                    player.inventory.Items.Add(armor);
                }
            }

            if (data.potions != null)
            {
                foreach (var potion in data.potions)
                {
                    player.inventory.Items.Add(potion);
                }
            }

            if(data.enhanceStone!=null)
            {
                foreach(var enhancementStone in data.enhanceStone)
                {
                    player.inventory.Items.Add(enhancementStone);
                }
            }
        }

        private static void EquippedItems(Player player, SaveData data)
        {
            if (!string.IsNullOrEmpty(data.equippedWeapon))
            {
                var weapon = player.inventory.Items.OfType<Weapon>().FirstOrDefault(w => w.name == data.equippedWeapon);

                if (weapon != null)
                {
                    weapon.isEquipped = true;
                    player.equippedWeapon = weapon;
                }
            }

            if (!string.IsNullOrEmpty(data.equippedArmor))
            {
                var armor = player.inventory.Items.OfType<Armor>().FirstOrDefault(a => a.name == data.equippedArmor);

                if (armor != null)
                {
                    armor.isEquipped = true;
                    player.equippedArmor = armor;
                }
            }
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

        private static void LoadShopState(SaveData data)
        {
            Shop shop = GameManager.Instance.shop;

            shop.ProductList.Clear();

            if (data.shopWeapons != null)
            {
                foreach (var weapon in data.shopWeapons)
                {
                    shop.ProductList.Add(weapon);
                }
            }

            if (data.shopArmors != null)
            {
                foreach (var armor in data.shopArmors)
                {
                    shop.ProductList.Add(armor);
                }
            }

            if (data.shopPotions != null)
            {
                foreach (var potion in data.shopPotions)
                {
                    shop.ProductList.Add(potion);
                }
            }

            if (data.shopStones != null)
            {
                foreach (var stone in data.shopStones)
                {
                    shop.ProductList.Add(stone);
                }
            }
        }
    }
}



