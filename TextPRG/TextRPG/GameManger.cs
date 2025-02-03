using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    enum PlayerClass
    {
        Gunsliger,
        Sorceress,
        Blade,
        Aeromancer
    }

    enum AddState
    {
        Attack,
        Defence
    }

    enum SceneType 
    {
        LobbyScene,
        PlayerScene,
        InventoryScene,
        StoreScene
    }

    class GameManger : Singleton<GameManger>    
    {
        // 씬 저장 리스트 
        private IScene[] gameScene;
        private IScene nextScene;
        private IScene preScene;

        // 캐릭터를 만든적이 있는지 
        private bool _flag = false;

        private string _playerEnterName = "/테스트닉네임입니다/";

        // 프로퍼티
        public string playerEnterName => _playerEnterName;

        public GameManger() 
        {
            int temp = Enum.GetNames(typeof(SceneType)).Length;
            gameScene = new IScene[temp];

            // 싱글톤
            gameScene[(int)SceneType.LobbyScene]        = LobbyScene.Instance;
            gameScene[(int)SceneType.PlayerScene]       = PlayerManager.Instance;
            gameScene[(int)SceneType.InventoryScene]    = InventoryManger.Instance;
            gameScene[(int)SceneType.StoreScene]        = InventoryManger.Instance;
        }


        public void EnterGame()
        {
            Console.WriteLine("===LOST ARK===");

            // 접속 이력이 있으면 
            // ##TODO : 저장 기능 넣으면 저장이력이있는지로 검사 
            if (!_flag)
            {
                Console.WriteLine("===LOST ARK===");
                Console.WriteLine("환영합니다 모험가님 \n 모험가님의 닉네임을 입력해주세요 ");

                _playerEnterName = Console.ReadLine();

                Console.WriteLine($" {_playerEnterName} 님 7개의 아크를 되찾아서 아크라시아를 지켜주세요 ");

                // 최초진입 -> 캐릭터 선택창으로 이동 
                ChangeScene(SceneType.PlayerScene);
            }
            else 
            {
                Console.WriteLine("다시 아크라시아에 돌아온 모험가님을 환영합니다.");
            }
        }


        // 씬 변경 ( ##TODO : 씬매니져 하나 만들어도될듯 )
        public void ChangeScene(SceneType type) 
        {
            // 지금 씬 = 예전 씬으로
            if(nextScene != null) 
            {
                preScene = nextScene;

                // Lobby->Lobby일 때 exit와 enter은 실행안해도됨
                if (preScene != nextScene)
                    preScene.SceneExit();
            }

            // 현재 씬 지정 
            nextScene = gameScene[(int)type];

            // 현재씬 실행 
            if (preScene != nextScene)
                nextScene.SceneEntry();

            nextScene.SceneMainFlow();
        }

    }
}
