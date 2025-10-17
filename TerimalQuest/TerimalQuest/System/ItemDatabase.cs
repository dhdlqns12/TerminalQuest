using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TerimalQuest.System
{
    public static class ItemDatabase
    {
        class ItemDatabaseAIConfig
        {
            public int lastId { get; set; }
            public int id  { get; set; }
        }
        /*
         * ItemDatabase: 아이템 데이터베이스 클래스
         * 
         * 게임에서 사용하는 모든 아이템 정보를 보관한 Database 클래스입니다.
         * Database 내 있는 모든 데이터는 원본 데이터이다.
         * 
         */

        private static Dictionary<string, Item> itemDatabase = new Dictionary<string, Item>();
        private static List<ItemDatabaseAIConfig> aiConfigs = new List<ItemDatabaseAIConfig>();

        static ItemDatabase()
        {
            LoadItems();
            LoadAIConfigs();
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        }
        private static void OnProcessExit(object sender, EventArgs e)
        {
            SaveAIConfigs();
        }
        // Item JSON 파일 로드
        private static void LoadItems()
        {
            List<ItemData> itemList;
            try
            {
                string projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
                string path = Path.Combine(projectRoot, "Resources", "ItemData.json");

                // JSON 파일 읽기
                string json = File.ReadAllText(path);

                // 직렬화 옵션
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                itemList = JsonSerializer.Deserialize<List<ItemData>>(json, options);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            // itemDatabase에 아이템 등록
            foreach (var item in itemList)
            {
                switch (item.type)
                {
                    case "Weapon":
                        itemDatabase[item.name] = new Weapon(item.id, item.name, item.desc, item.price, item.atk, ItemType.Weapon);
                        break;

                    case "Armor":
                        itemDatabase[item.name] = new Armor(item.id, item.name, item.desc, item.price, item.def, ItemType.Armor);
                        break;

                    case "Potion":
                        PotionType pType = Enum.Parse<PotionType>(item.potionType, true);
                        itemDatabase[item.name] = new Potion(item.id, item.name, item.desc, item.price, item.healAmount, ItemType.Potion, pType);
                        break;

                    case "EnhancementStone":
                        itemDatabase[item.name] = new EnhancementStone(item.id, item.name, item.desc, item.price, ItemType.EnhancementStone);
                        break;
                }
            }
        }

        private static void LoadAIConfigs()
        {
            try
            {
                string projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
                string path = Path.Combine(projectRoot, "Resources", "AutoIncrementConfig.json");

                // JSON 파일 읽기
                string json = File.ReadAllText(path);
                aiConfigs = JsonSerializer.Deserialize<List<ItemDatabaseAIConfig>>(json);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        private static void SaveAIConfigs()
        {
            try
            {
                string projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
                string path = Path.Combine(projectRoot, "Resources", "AutoIncrementConfig.json");
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(aiConfigs, options);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static int GetLastId(int baseItemId)
        {
            var config = aiConfigs.FirstOrDefault(c => c.id == baseItemId);

            if (config == null)
            {
                config = new ItemDatabaseAIConfig { id = baseItemId, lastId = 0 };
                aiConfigs.Add(config);
            }

            config.lastId++;
            return config.lastId;
        }
        // 아이템 반환 : itemDatabase는 원본 데이털를 가지고 있으므로 복제본을 반환한다.
        public static Weapon GetWeapon(string name) { if (itemDatabase.TryGetValue(name, out var item) && item is Weapon weapon) return (Weapon)weapon.Clone(); else return null; }
        public static Armor GetArmor(string name) { if (itemDatabase.TryGetValue(name, out var item) && item is Armor armor) return (Armor)armor.Clone(); else return null; }
        public static Potion GetPotion(string name) { if (itemDatabase.TryGetValue(name, out var item) && item is Potion potion) return potion.Clone(); else return null; }
        public static EnhancementStone GetEnhancementStone(string name) { if (itemDatabase.TryGetValue(name, out var item) && item is EnhancementStone enhancementStone) return (EnhancementStone)enhancementStone.Clone(); else return null; }
        public static Item GetItem(string name) { return itemDatabase.TryGetValue(name, out var item) ? item.Clone() : null; }
    }
}
