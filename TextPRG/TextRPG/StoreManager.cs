using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class StoreManager : Singleton<StoreManager>, IScene 
    {
        public StoreManager() 
        {
                                      
        }

        public void SceneEntry()
        {
            Console.Clear();
            Console.WriteLine("===상점에 접속하였습니다===");

            Console.WriteLine("[보유골드]");
            Console.WriteLine(PlayerManager.Instance.UserSelectPlayer.Gold + "   GOLD");

            Console.WriteLine("\n [아이템 목록] ");
            ItemManager.Instance.PrintEquipItem(_isStore : true);

        }

        public void SceneMainFlow()
        {
            Console.WriteLine("[보유골드]");
            Console.WriteLine(PlayerManager.Instance.UserSelectPlayer.Gold + "   GOLD");

            Console.WriteLine(" 1. 아이템 구매 \n 2. 상점 아이템 보기 \n 3.아이템 판매 \n 0. 나가기 \n");

            int input = int.Parse(Console.ReadLine());

            switch (input) 
            {
                case 1:
                    Console.WriteLine("아이템 구매 목록 입니다.");
                    // 구매 로직
                    BuyItem();
                    // 인벤토리 씬으로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.StoreScene);
                    break;
                case 2:
                    Console.WriteLine("상점 아이템 목록 입니다.");
                    // 장비 아이템 출력 
                    ItemManager.Instance.PrintEquipItem(_isStore : true);
                    // 인벤토리 씬으로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.StoreScene);
                    break;
                case 3:
                    Console.WriteLine("아이템 판매 목록입니다.");
                    // 판매로직
                    SellItem();
                    // 인벤토리 씬으로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.StoreScene);
                    break;
                case 0 :
                    Console.WriteLine("나가기를 선택하셨습니다.");
                    // 인벤토리 씬으로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.LobbyScene);
                    break;

                default:
                    Console.WriteLine("Store : 잘못된 접근 입니다");
                    // 인벤토리 씬으로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.StoreScene);
                    break;
            }


        }

        public void SceneExit()
        {
                              
        }

        private void BuyItem() 
        {
            Console.WriteLine($"구매할 아이템 번호를 입력해 주세요 {0} ~ {ItemManager.Instance.EquipListCount - 1}");

            int input = int.Parse(Console.ReadLine());

            // ##TODO : 골드 검사 해야함 
            Item selectItem = ItemManager.Instance.GetEquipByIndex(input);

            // 만약 이미 획득한 아이템이면 ? state가 착용 or 인벤토리
            if (selectItem.state == ItemState.Equipped || selectItem.state == ItemState.InInvetory)
            {
                Console.WriteLine("이미 획득한 아이템입니다.");
                return;
            }
            // 골드 체크 -> 부족하면 
            if (! PlayerManager.Instance.HasEnoughMoney(selectItem.price)) 
            {
                Console.WriteLine("골드가 부족합니다!");
                return;
            }

            // 이미 획득한 아이템도 아님 + 골드 충분
            Console.WriteLine("아이템을 구매합니다.!");
            // 돈 차감 
            PlayerManager.Instance.UpdatePlayerState(GOLD: (-1) * selectItem.price);
            // 아이템 변경 
            ItemManager.Instance.ChangeStateEquipItem( ItemState.UnObtained , ItemState.InInvetory , selectItem);
        }

        private void SellItem() 
        {
            // 판매 가능한 아이템 목록 출력 ( 장착한 아이템은 판매 불가능 )
            ItemManager.Instance.PrintItemByItemState(ItemState.InInvetory);

            Console.WriteLine("판매할 품목을 선택하세요");
            int input = int.Parse(Console.ReadLine());

            // 아이템 변경 
            Item selectItem = ItemManager.Instance.GetEquitInDictionray( ItemState.InInvetory , input);
            ItemManager.Instance.ChangeStateEquipItem(ItemState.InInvetory, ItemState.UnObtained, selectItem);

            // 돈 추가 
            PlayerManager.Instance.UpdatePlayerState( GOLD : selectItem.price / 2 );
        }


       
    }
}
