﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class LobbyScene : Singleton<LobbyScene>, IScene
    {
        StringBuilder stringBuilder;

        public LobbyScene() 
        {
            stringBuilder = new StringBuilder();
        }

        public void SceneEntry()
        {
            stringBuilder.Clear();

            stringBuilder.Append('\n');
            stringBuilder.Append("=== 루테란성입니다. === \n ");
            stringBuilder.Append("루테란의 주민들은 용기와 기사의 긍지를 중요시 합니다. 아직도 건국왕 루테란의 이념인 \"용기\" 를 가슴에 품고 살아갑니다. \n");

            Console.WriteLine(stringBuilder.ToString());
        }

        public void SceneMainFlow()
        {
            stringBuilder.Clear();

            stringBuilder.Append($"{PlayerManager.Instance.UserSelectPlayer.PlayerName} 모험가님 , 어디로 이동하시겠습니까 ? \n ");
            stringBuilder.Append("[0] 플레이어 상태 보기 \n");
            stringBuilder.Append("[1] 인벤토리 \n");
            stringBuilder.Append("[2] 상점 \n");
            stringBuilder.Append("[3] 던전 입장 \n");
            stringBuilder.Append("[4] 휴식 \n");
            stringBuilder.Append("\n [5] 저장 \n");

            Console.WriteLine(stringBuilder.ToString());

            int input;
            while (true)
            {
                Console.Write("숫자를 입력하세요 >>> ");
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    break;  // 성공적으로 숫자를 입력받으면 반복문 종료
                }
                Console.WriteLine("올바른 숫자를 입력해주세요.");
            }

            // input에 맞게 변경 
            ChangeScene(input);
        }

        private void ChangeScene(int input) 
        {
            switch (input) 
            {
                case 0:
                    // 플레이어 출력 
                    PlayerManager.Instance.printPlayer();
                    // 로비로 씬 전환 
                    GameManger.Instance.ChangeScene(SceneType.LobbyScene);
                    break;
                case 1:
                    // 인벤토리로 씬 전환 
                    GameManger.Instance.ChangeScene(SceneType.InventoryScene);
                    break;
                case 2:
                    // 상점으로 씬 전환 
                    GameManger.Instance.ChangeScene(SceneType.StoreScene);
                    break;
                case 3:
                    // 던전 씬 전환
                    GameManger.Instance.ChangeScene(SceneType.DungeonScene);
                    break;
                case 4:
                    // 휴식
                    GameManger.Instance.ChangeScene(SceneType.RestScene);
                    break;
                case 5:
                    // 저장 
                    GameManger.Instance.SaveData();
                    break;
                default:
                    Console.WriteLine("잘못된 접근입니다. 다시 로비로 돌아갑니다");

                    // Lobby로 돌아가기
                    GameManger.Instance.ChangeScene(SceneType.LobbyScene);
                    break;

            }
        }

    }
}
