using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class InventoryManger : Singleton<InventoryManger>, IScene
    {       

        public InventoryManger() 
        {
            
        }

        public void SceneEntry()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 | 보유중인 아이템을 확인할 수 있습니다. \n [보유중인 아이템 목록] \n ");

            ItemManager.Instance.PrintItemByItemState( ItemState.InInvetory );
        }

        public void SceneMainFlow()
        {
            Console.WriteLine("1. 장착관리 2. 보유중인 아이템 보기 3. 착용중인 아이템 보기 \n 0. 나가기");

            int input = int.Parse(Console.ReadLine());

            switch (input) 
            {
                case 1:
                    Console.WriteLine("장착관리를 선택하셨습니다.");
                    // 장착 시스템 출력 
                    EquitOnOFf();
                    // 인벤토리 씬으로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.InventoryScene);
                    break;
                case 2:
                    Console.WriteLine("보유중인 아이템 보기를 선택하셨습니다.");
                    // 인벤토리 출력 
                    ItemManager.Instance.PrintItemByItemState(ItemState.InInvetory);
                    // 인번토리 씬으로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.InventoryScene);
                    break;
                case 3:
                    Console.WriteLine("착용중인 아이템 보기를 선택하셨습니다.");
                    // 착용아이템 출력 
                    ItemManager.Instance.PrintItemByItemState(ItemState.Equipped);
                    // 인벤토리 씬으로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.InventoryScene);
                    break;
                case 0:
                    Console.WriteLine("\"나가기\"를 선택하셨습니다. 로비로 돌아갑니다.");
                    // 로비로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.LobbyScene);
                    break;
                default:
                    Console.WriteLine("잘못된 접근입니다. 다시 입력해주세요");
                    GameManger.Instance.ChangeScene(SceneType.InventoryScene);
                    break;
            }
        }

        public void SceneExit()
        {

        }

        private void EquitOnOFf() 
        {
            // Console.WriteLine("**미장착 아이템 **");
            // 아직 미장착한 아이템 출력 ( 인벤토리에 있는 )          
            // ItemManager.Instance.PrintItemByItemState(ItemState.InInvetory);

            // Console.WriteLine("**장착 아이템 **");
            // 장착한 아이템 출력 
            // ItemManager.Instance.PrintItemByItemState(ItemState.Equipped);

            // 획득 아이템 출력 ( InInvetory내의 아이템 )
            ItemManager.Instance.PrintItemByItemState(ItemState.InInvetory , _isEquitManage : true);

            // 입력
            Console.WriteLine("장착하실 아이템을 고르세요");
            int input = int.Parse(Console.ReadLine());

            // 아이템 획득 타입 변환 ( 인벤토리 -> 착용 )
            Item selectItem = ItemManager.Instance.GetEquitInDictionray(ItemState.InInvetory ,input);

            ItemManager.Instance.ChangeStateEquipItem(ItemState.InInvetory, ItemState.Equipped, selectItem);
        }

    }
}
