using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerimalQuest.Manager;
using TerimalQuest.System;

namespace TerimalQuest.Scenes
{
    public class InventoryScene : IScene
    {
        public event Action<IScene> OnSceneChangeRequested;
        public void Enter()
        {

        }

        public void Update()
        {

        }

        public void Exit()
        {
             
        }

        // 인벤토리 뷰
        private void InventoryView()
        {
            Inventory inventory = GameManager.Instance.player.inventory;

            Console.Write("인벤토리 \n보유 중인 아이템을 관리할 수 있습니다. \n\n");
            inventory.DisplayInfo(false);
            Console.Write("\n\n1. 장착 관리 \n2. 아이템 정렬\n0. 나가기 \n\n원하시는 행동을 입력해주세요.\n>>");

            
        }

        // 인벤토리 장착 관리 뷰
        private void InventoryEquipView()
        {
            Inventory inventory = GameManager.Instance.player.inventory;

            Console.Write("인벤토리 \n보유 중인 아이템을 관리할 수 있습니다. \n\n");
            inventory.DisplayInfo(false);
            Console.Write("\n\n1. 장착 관리 \n2. 아이템 정렬\n0. 나가기 \n\n원하시는 행동을 입력해주세요.\n>>");
        }
    }
}
