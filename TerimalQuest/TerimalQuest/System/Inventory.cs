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
        private List<Item> items;

        // 생성자
        public Inventory(int maxItemCount)
        {
            this.maxItemCount = maxItemCount;
            items = new List<Item>();
        }

        // 아이템 추가
        public void Add(Item item)
        {
            // 최대 아이템 개수 출력
            if(items.Count >= maxItemCount)
            {
                Console.WriteLine("아이템을 최대 소지하였습니다.");
                return;
            }

            items.Add(item);
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

        // 장비 장착/해제 - 인덱스 검색
        public void EquipItemByIdx(int idx)
        {
            if (items.Count <= 0 || idx >= items.Count) return;

            // 이미 장착 중이라면 장착 해제
            if (items[idx].isEquipped)
            {
                items[idx].Equip();
            }

            // 장착 중인 상태가 아니라면 장착
            else
            {
                items[idx].Equip();
            }
        }

        public void DisplayInfo(bool isEquipMode)
        {
            Console.WriteLine("[아이템 목록]\n");

            // 장착 관리 상태인지에 따라 표시 변환
            string equipMode = (isEquipMode) ? ConsoleHelper.PadRightForConsole(" ", 6) : $"  ";

            Console.WriteLine(
                string.Format("{0}{1} | {2} | {3}",
                equipMode,
                ConsoleHelper.PadRightForConsole("[아이템 이름]", 15),
                ConsoleHelper.PadRightForConsole("[아이템 효과]", 15),
                "[아이템 설명]\n"));

            for (int i = 0; i < items.Count; i++)
            {
                string idxTxt = (isEquipMode) ? $"{i + 1} : " : "";
                Console.Write($"- {idxTxt}");
                items[i].DisplayInfo();
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
