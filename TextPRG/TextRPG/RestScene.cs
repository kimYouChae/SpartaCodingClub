using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class RestScene : Singleton<RestScene>, IScene
    {
        private int restCost;   // 회복비용
        private int restAmout;  // 회복량

        public RestScene() 
        {
            restCost = 500;
            restAmout = 100;
        }

        public void SceneEntry()
        {
            Console.Clear();
            Console.WriteLine("휴식하기");   
        }

        public void SceneExit()
        {


        }

        public void SceneMainFlow()
        {
            Console.WriteLine($"{restCost} G 를 내면 체력을 회복할 수 있습니다. ");
            Console.WriteLine($"보유골드 {PlayerManager.Instance.UserSelectPlayer.Gold} ");
            Console.WriteLine("1. 휴식하기 \n 0. 나가기");

            int input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    Console.WriteLine("휴식하기를 선택 하셨습니다.");
                    GetRest();
                    GameManger.Instance.ChangeScene(SceneType.RestScene);
                    break;
                case 0:
                    Console.WriteLine("나가기를 선택하셨습니다.");
                    // 인벤토리 씬으로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.LobbyScene);
                    break;
                default:
                    Console.WriteLine("휴식 : 잘못된 접근 입니다");
                    // 인벤토리 씬으로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.StoreScene);
                    break;
            }

        }

        private void GetRest() 
        {
            // 돈부족하면 return
            if (!PlayerManager.Instance.HasEnoughMoney(restCost)) 
            {
                Console.WriteLine("골드가 부족합니다.");
                return;
            }

            // 체력 회복
            PlayerManager.Instance.UpdateHP(restAmout);

            // 돈 사용
            PlayerManager.Instance.UseGold(restCost);
        }
    }
}
