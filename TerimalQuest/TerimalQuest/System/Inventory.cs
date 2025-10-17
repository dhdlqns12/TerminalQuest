using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;

namespace TerimalQuest.System
{
    public class Inventory
    {
        /*
         * 인벤토리는 캐릭터에 소속된다.
         * 아이템 리스트를 가지고 있으며 최대 아이템 소지 개수가 정해져 있다.
         * 각 아이템을 관리할 수 있는 기능을 가지고 있다.
         * 
         * [인벤토리 기능]
         *  - 아이템 추기
         *  - 아이템 삭제
         *  - 아이템 찾기(이름/인덱스)
         *  - 아이템 장착/해제
         *  - 아이템 정보 표시
         * 
         */

        private int maxItemCount;    // 최대 아이템 개수

        // 아이템 리스트
        public List<Item> items;

        // 생성자
        public Inventory(int maxItemCount)
        {
            this.maxItemCount = maxItemCount;
            items = new List<Item>();
        }

        // 아이템 추가 
        public void Add(Item item)
        {
            // 최대 소지 개수를 넘어가면 취소
            if (items.Count >= maxItemCount)
            {
                Console.WriteLine("소지 할 수 있는 인벤토리 공간이 없습니다!");
                return;
            }

            AddByItemType(item);
        }

        // 아이템 타입에 따라 추가
        private void AddByItemType(Item item)
        {
            if(item is Weapon weapon || item is Armor armor)
            {
                // 장비면 새로 추가
                items.Add(item);
            }
            else
            {
                // 장비가 아니면 수량을 올려서 겹쳐서 사용
                Item originItem = FindItemByName(item.name);
                if(originItem == null)
                {
                    items.Add(item);
                }
                else
                {
                    originItem.count++;
                }
            }
        }

        // 아이템 삭제
        public void Remove(Item item)
        {
            if (!items.Contains(item)) return;

            items.Remove(item);
        }

        // 인벤토리 초기화
        public void Clear()
        {
            items.Clear();
        }

        //  아이템 검색 : 아이디로 검색
        public Item FindItemById(int id)
        {
            foreach (var item in items)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        //  아이템 검색 : 이름으로 검색
        public Item FindItemByName(string name)
        {
            foreach (var item in items)
            {
                if (item.name == name)
                {
                    return item;
                }
            }

            return null;
        }

        // 장비 장착/해제 - 인덱스 검색
        public void EquipItemByIdx(int idx)
        {
            if (items.Count <= 0 || idx > items.Count)
            {
                return;
            }

            Item item = items[idx];

            // 이미 장착되어 있다면 장착 해제
            if(item.isEquipped)
            {
                item.Equip(false);
            }

            // 장착 중인 상태가 아니라면 장착
            else
            {
                item.Equip(true);
                QuestManager.Instance.PlayQuest("장착");
            }
        }
        
        // 인벤토리 보여주기
        public void DisplayInfo(bool isEquipMode, params ItemType[] filterTypes)
        {
            UIManager.Instance.DisplayItemInfoHeader(isEquipMode);

            IEnumerable<Item> displayList;

            // filterType에 따라 보여줄 ItemList 갱신
            if (filterTypes == null || filterTypes.Length == 0)
                displayList = items;
            else
                displayList = items.Where(item => filterTypes.Contains(item.type));

            foreach (var item in displayList)
            {
                string idxTxt = (isEquipMode) ? $"{items.IndexOf(item) + 1} : " : "";
                Console.Write($"- {idxTxt}");
                item.DisplayInfo();
            }
        }

        // 인벤토리 보여주기 - 아이템 판매
        public void DisplayInfoWithGold()
        {
            UIManager.Instance.DisplayItemProductHeader();

            for (int i = 0; i < items.Count; i++)
            {
                string idxTxt = $"{i + 1} : ";
                Console.Write($"- {idxTxt}");
                items[i].DisplayInfoProduct();
            }
        }

        // 아이템 정렬
        public void SortItemByOption(int option)
        {
            switch (option)
            {
                case 1:     // 이름 정렬
                    items = ItemSorter.SortByName(items);
                    break;
                case 2:     // 장착 순 정렬
                    items = ItemSorter.SortByEquipped(items);
                    break;
                case 3:     // 공격력 정렬
                    items = ItemSorter.SortByAtk(items);
                    break;
                case 4:     // 방어력 정렬
                    items = ItemSorter.SortByDef(items);
                    break;
                default:
                    break;
            }
        }

        // 프로퍼티 변수
        public List<Item> Items { get { return items; } set { items = value; } }
    }
}
