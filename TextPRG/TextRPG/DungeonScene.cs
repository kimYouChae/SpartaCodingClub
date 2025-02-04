using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{

    internal class DungeonScene : Singleton<DungeonScene>, IScene
    {
        private Dungeon[] dungeonLevelList;     // 던전 클래스 리스트
            
        private int _sucsessRate;               // 던전 성공 확률 

        Random random;

        public DungeonScene() 
        {
            random = new Random();

            _sucsessRate = 40;

            dungeonLevelList = new Dungeon[ Enum.GetNames(typeof(DungeonLevel)).Length ];
            dungeonLevelList[(int)DungeonLevel.Easy] = new Dungeon("쉬운 던전", 5 , 1000);
            dungeonLevelList[(int)DungeonLevel.Normal] = new Dungeon("일반 던전", 11 , 1700);
            dungeonLevelList[(int)DungeonLevel.Hard] = new Dungeon("어려운 던전", 17 , 2500);

        }

        public void SceneEntry()
        {
            Console.Clear();
            Console.WriteLine("===던전 입장!===");

            for (int i = 0; i < dungeonLevelList.Length; i++) 
            {
                Console.WriteLine($"{i}. { dungeonLevelList[i].name} \t | 방어력 {dungeonLevelList[i].recommendDefence} 이상 ");
                     
            }
        }

        public void SceneMainFlow()
        {
            int input = int.Parse(Console.ReadLine());

            switch (input) 
            {
                case 0 :
                    Console.WriteLine("쉬운 던전에 입장 합니다.");
                    EnterDungeon(DungeonLevel.Easy);
                    break;
                case 1:
                    Console.WriteLine("일반 던전에 입장 합니다.");
                    EnterDungeon(DungeonLevel.Normal);
                    break;
                case 2:
                    Console.WriteLine("어려운 던전에 입장 합니다.");
                    EnterDungeon(DungeonLevel.Hard);
                    break;
                default:
                    Console.WriteLine("잘못된 접근입니다. 다시 던전으로 돌아갑니다");
                    GameManger.Instance.ChangeScene(SceneType.DungeonScene);
                    break;
            }

            // 끝난 후 
            Console.WriteLine("던전을 빠져나왔습니다. 로비로 돌아가려면 0을 입력하세요");
            input = int.Parse(Console.ReadLine());

            if(input == 0)
                GameManger.Instance.ChangeScene(SceneType.LobbyScene);
        }


        private void EnterDungeon(DungeonLevel level) 
        {
            Dungeon dungeon = dungeonLevelList[(int)level];

            Console.WriteLine( "플레이어 디펜스  : " + PlayerManager.Instance.UserSelectPlayer.DefencePower);
            Console.WriteLine( "던전 디펜스  : " + dungeon.recommendDefence);
            
            // 플레이어 방어력이 권장보다 낮으면 
            if (dungeon.recommendDefence > PlayerManager.Instance.UserSelectPlayer.DefencePower)
            {
                FailDungeon();
                Console.WriteLine("던전실패!! 다음에 다시 도전해보세요! ");
            }
            else 
            {
                Console.WriteLine($"축하합니다!! \n { dungeon.name } 을 클리어 했습니다!!");
                Console.WriteLine("[탐험 결과]");

                int reduce = dungeon.RedeuceHp( PlayerManager.Instance.UserSelectPlayer.DefencePower) * (-1);
                int reward = dungeon.clearGold + dungeon.AddGold( PlayerManager.Instance.UserSelectPlayer.AttackPower );

                Console.WriteLine($"체력 : {PlayerManager.Instance.UserSelectPlayer.HP} -> {PlayerManager.Instance.UserSelectPlayer.HP + reduce}");
                Console.WriteLine($"GOLD : {PlayerManager.Instance.UserSelectPlayer.Gold} -> {PlayerManager.Instance.UserSelectPlayer.Gold + reward}");

                // 플레이어 스탯 업데이트
                PlayerManager.Instance.UpdatePlayerState(HP : reduce , GOLD : reward);
                PlayerManager.Instance.UpdatePlayerState(ATTACK : 2 , DEFENCE : 1 , LEVEL : 1);
            }
        }

        private void FailDungeon() 
        {
            // 40퍼 확률 
            int rate = random.Next(0, 101);

            // 실패
            if (rate < _sucsessRate)
            {
                // 플레이어 체력 감소
                // 현재 체력 1/2
                int reduce = PlayerManager.Instance.UserSelectPlayer.HP / 2;
                PlayerManager.Instance.UpdatePlayerState(HP: reduce);
                Console.WriteLine($"{reduce}만큼 감소했습니다.");
            }
            else 
            {
                Console.WriteLine("운이 좋으시네요 멀쩡하게 돌아오셨네요!");    
            }

        }
    }
}
