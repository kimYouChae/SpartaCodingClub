using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class PlayerManager : Singleton<PlayerManager> , IScene 
    {
        // Player클래스 
        private Player[] playerContainer;
        private Player _userSelectPlayer;

        // 추가 스탯
        private int[] addState;

        // 클래스 배열 
        private PlayerClass[] playerClassArray;

        // 프로퍼티
        public Player UserSelectPlayer => _userSelectPlayer;

        public PlayerManager()
        {
            // 클래스 초기화 
            ClassInit();

            // 추가스탯 초기화
            AddStateInit();
        }



        private void ClassInit() 
        {
            // player 데이터 저장 
            playerClassArray = (PlayerClass[])Enum.GetValues(typeof(PlayerClass));

            playerContainer = new Player[playerClassArray.Length];

            playerContainer[(int)PlayerClass.Gunsliger] 
                = new Player(0, PlayerClass.Gunsliger, "", 10, 5, 100, 1500 , "건슬링어는 3가지 총을 사용하여 빠르게 움직이며 스타일리쉬한 전투를 펼칩니다.");
            playerContainer[(int)PlayerClass.Sorceress] 
                = new Player(0, PlayerClass.Sorceress, "", 10, 5, 100, 1500, "원소를 기본으로 한 강력한 마법을 다루며 캐스팅 마법으로 강력한 피해를 선사합니다.");
            playerContainer[(int)PlayerClass.Blade] 
                = new Player(0, PlayerClass.Blade, "", 10, 5, 100, 1500, "쌍검과 장검을 사용하는 암살자이며 빠르고 절도있게 적들을 공격합니다.");
            playerContainer[(int)PlayerClass.Aeromancer] 
                = new Player(0, PlayerClass.Aeromancer, "", 10, 5, 100, 1500, "신비로운 환영의 힘으로 기상을 다루고 우산으로 적을 제압합니다.");
        }

        private void AddStateInit() 
        {
            int temp = Enum.GetNames(typeof(AddState)).Length;
            addState = new int[temp];

            addState[(int)AddState.Attack] = 0;
            addState[(int)AddState.Defence] = 0;
        }

        public void printPlayer() 
        {
            _userSelectPlayer.Print();
        }

        public void SelectPlayer() 
        {
            for(int i = 0; i < playerClassArray.Length; i++)
            {
                string str = string.Empty;

                PlayerClass type = playerClassArray[i];
                str += "[" + i.ToString() + " ] ";
                str += "클래스 | " + playerContainer[(int)type].Playerclass + "\t";
                str += "설명 | " + playerContainer[(int)type].ToolTip + "\n";

                Console.WriteLine(str);
            }

            Console.WriteLine($"직업을 선택하세요 [{0} 부터 {playerClassArray.Length - 1} 사이 숫자 입력 ]");
            int playerInput = int.Parse(Console.ReadLine());

            // 인덱스에 해당하는 player로 할당 
            _userSelectPlayer = playerContainer[playerInput];
            // 이름 할당 
            _userSelectPlayer.PlayerName = GameManger.Instance.playerEnterName;

            Console.WriteLine($" 모험가님은 {_userSelectPlayer.Playerclass}를 선택했습니다 ! ");
        }

        public void SceneEntry()
        {
            Console.WriteLine();
            //Console.WriteLine("Player : SceneEntry");

            Console.WriteLine("===캐릭터 선택창 입니다===");
        }

        public void SceneMainFlow()
        {
            //Console.WriteLine("Player : SceneMainFlow");

            SelectPlayer();

            // Lobby로 돌아가기
            GameManger.Instance.ChangeScene(SceneType.LobbyScene);
        }

        public void SceneExit()
        {
            Console.WriteLine("루테란 성으로 돌아갑니다! ");

            //Console.WriteLine("Player : SceneExit");
        }


    }
}
