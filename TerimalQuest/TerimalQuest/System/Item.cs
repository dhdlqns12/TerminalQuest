using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerimalQuest.System
{
    public enum ItemType { Weapon, Armor, Potion, EnhancementStone }

    public class Item
    {
        /*
        * Item 스크립트
        * 
        * 이 게임에서 사용하는 아이템 옵션은 다음과 같다.
        * [Armor] 방어구
        * [Weapon] 무기
        * [Potion] 포션
        * [EnhancementStone] 강화석
        * 
        */

        public int Id { get; set; }         // 아이템 Id
        public string name { get; set; }    // 아이템 이름
        public string desc { get; set; }    // 아이템 설명
        public int price { get; set; }      // 아이템 가격
        public int count { get; set; }      // 아이템 수량

        public bool isEquipped { get; set; }    // 아이템 착용 여부
        public bool isPurchase { get; set; }    // 아이템 구매 여부

        public ItemType type { get; private set; } // 아이템 타입

        public Item(int id, string name, string desc, int price, ItemType type)
        {
            Id = id;
            this.name = name;
            this.desc = desc;
            this.price = price;
            this.type = type;

            this.isEquipped = false;
            this.isPurchase = false;

            // 생성자 내 개수 1개로 설정
            this.count = 1;
        }

        public virtual void Equip(bool isEquip)
        {
            // 아이템 착용/해제
            isEquipped = isEquip;
        }

        public virtual string GetEffectText()
        {
            // 아이템 효과 텍스트 반환
            return "";
        }

        public virtual string GetCountText()
        {
            // 아이템 개수 텍스트 반환
            return $"수량: x{count}";
        }

        public virtual void DisplayInfo()
        {
            // 아이템 정보 표시
        }

        public virtual void DisplayInfoProduct()
        {
            // 아이템 정보 표시 : 상점
        }

        // 아이템 복제
        public virtual Item Clone()
        {
            return (Item)this.MemberwiseClone();
        }
    }
}
