using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class PlayerManager
    {

        private Player[] playerContainer;
        private Player userSelectPlayer;
        private int[] addState;

        private PlayerClass[] playerClassArray; 

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

            playerContainer[(int)PlayerClass.Gunsliger] = new Player(0, PlayerClass.Gunsliger, "", 10, 5, 100, 1500);
            playerContainer[(int)PlayerClass.Sorceress] = new Player(0, PlayerClass.Sorceress, "", 10, 5, 100, 1500);
            playerContainer[(int)PlayerClass.Blade] = new Player(0, PlayerClass.Blade, "", 10, 5, 100, 1500);
            playerContainer[(int)PlayerClass.Aeromancer] = new Player(0, PlayerClass.Aeromancer, "", 10, 5, 100, 1500);
        }

        private void AddStateInit() 
        {
            int temp = Enum.GetNames(typeof(AddState)).Length;
            addState = new int[temp];

            addState[(int)AddState.Attack] = 0;
            addState[(int)AddState.Defence] = 0;
        }

        public void SetUserSelectPlayer(int idx) 
        {
            userSelectPlayer = playerContainer[idx];
        }

        public void printPlayer() 
        {
            userSelectPlayer.Print();
        }

        public void SelectPlayer() 
        {
            Console.WriteLine("--- 직업을 선택 하세요 ---");
            for (int i = 0; i < playerClassArray.Length; i++) 
            {
                Console.WriteLine( "[ " + i + " ]" + playerClassArray[i].ToString());
            }

            int idx = int.Parse(Console.ReadLine());

            if (idx >= playerClassArray.Length) 
            {
                        
            }

        }
    }
}
