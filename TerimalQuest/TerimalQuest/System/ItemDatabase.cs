using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.System
{
    public static class ItemDatabase
    {
        /*
         * ItemDatabase: 아이템 데이터베이스 클래스
         * 
         * 게임에서 사용하는 모든 아이템 정보를 보관한 Database 클래스입니다.
         * Database 내 있는 모든 데이터는 원본 데이터이다.
         * 
         */

        private static Dictionary<string, Item> itemDatabase = new Dictionary<string, Item>();

        static ItemDatabase()
        {
            InitDatabase();
        }

        // 아이템 데이터베이스 : 아이템 생성
        private static void InitDatabase()
        {
            // 무기
            itemDatabase["낡은 검"] = new Weapon(1000, "낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 600, 2, ItemType.Weapon);
            itemDatabase["연습용 창"] = new Weapon(1100, "연습용 창", "검보다는 그래도 창이 다루기 쉽죠.", 700, 3, ItemType.Weapon);
            itemDatabase["청동 도끼"] = new Weapon(1200, "청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 1500, 5, ItemType.Weapon);
            itemDatabase["스파르타의 창"] = new Weapon(1300, "스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 4000, 7, ItemType.Weapon);

            // 방어구
            itemDatabase["무쇠갑옷"] = new Armor(2000, "무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 800, 9, ItemType.Armor);
            itemDatabase["수련자 갑옷"] = new Armor(2100, "수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 1000, 5, ItemType.Armor);
            itemDatabase["스파르타의 갑옷"] = new Armor(2200, "스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, 15, ItemType.Armor);

            // 포션
            itemDatabase["빨간포션"] = new Potion(3000, "빨간포션", "마시면 생기가 돌며 체력이 회복됩니다.", 400, 30, ItemType.Potion, PotionType.HP);
            itemDatabase["파랑포션"] = new Potion(3100, "파랑포션", "마시면 마력이 되살아나며 정신이 맑아집니다.", 400, 30, ItemType.Potion, PotionType.MP);
            itemDatabase["노랑포션"] = new Potion(3200, "노랑포션", "마시면 온몸에 힘이 솟아오릅니다.", 400, 30, ItemType.Potion, PotionType.Stamina);
        }

        // 아이템 반환 : itemDatabase는 원본 데이털를 가지고 있으므로 복제본을 반환한다.
        public static Weapon GetWeapon(string name) { if (itemDatabase.TryGetValue(name, out var item) && item is Weapon weapon) return weapon.Clone(); else return null; }
        public static Armor GetArmor(string name) { if (itemDatabase.TryGetValue(name, out var item) && item is Armor armor) return armor.Clone(); else return null; }
        public static Potion GetPotion(string name) { if (itemDatabase.TryGetValue(name, out var item) && item is Potion potion) return potion.Clone(); else return null; }
        public static Item GetItem(string name) { return itemDatabase.TryGetValue(name, out var item) ? item.Clone() : null; }
    }
}
