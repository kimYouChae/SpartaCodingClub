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

            Console.WriteLine(" 1. 아이템 구매 \n 2. 상점 아이템 보기 \n  0. 나가기 \n");

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
            if (! HasEnoughMoney(selectItem)) 
            {
                Console.WriteLine("골드가 부족합니다!");
                return;
            }

            // 이미 획득한 아이템도 아님 + 골드 충분
            Console.WriteLine("아이템을 구매합니다.!");
            ItemManager.Instance.ChangeStateEquipItem( ItemState.UnObtained , ItemState.InInvetory , selectItem);
        }

        private bool HasEnoughMoney(Item item) 
        {
            // 내 돈 - 아이템 가격이 0이상 -> 구매가능 
            if (0 <= PlayerManager.Instance.UserSelectPlayer.Gold - item.price)
            {
                // 플레이어 골드 차감 
                PlayerManager.Instance.UseGold(item.price);

                return true;
            }
            
            // 구매 불가능 
            else 
                return false;   
            
        }

    }
}
