using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.System
{
    public static class ItemSorter
    {
        // 아이템 정렬 - Id
        public static List<Item> SortById(List<Item> list)
        {
            return list.OrderBy(item => item.Id).ToList();
        }

        // 아이템 정렬 - 이름
        public static List<Item> SortByName(List<Item> list)
        {
            return list.OrderBy(item => item.name).ToList();
        }

        // 아이템 정렬 - 장착
        public static List<Item> SortByEquipped(List<Item> list)
        {
            return list.OrderBy(item => !item.isEquipped).ToList();
        }

        // 아이템 정렬 - 공격력
        public static List<Item> SortByAtk(List<Item> list)
        {
            return list.OrderByDescending(item => item is Weapon)
                        .ThenByDescending(Item => (Item as Weapon)?.atk ?? 0).ToList();
        }

        // 아이템 정렬 - 방어력
        public static List<Item> SortByDef(List<Item> list)
        {
            return list.OrderByDescending(item => item is Armor)
                        .ThenByDescending(Item => (Item as Armor)?.def ?? 0).ToList();
        }

        // 아이템 정렬 - 타입 별
        public static List<Item> SortByItemType(List<Item> list)
        {
            Dictionary<ItemType, int> typeOrder = new Dictionary<ItemType, int>
    {
        { ItemType.Weapon, 0 },
        { ItemType.Armor, 1 },
        { ItemType.Potion, 2 },
        { ItemType.EnhancementStone, 3 }
    };

            return list
                .OrderBy(item => typeOrder.ContainsKey(item.type) ? typeOrder[item.type] : int.MaxValue)
                .ToList();
        }
    }
}
