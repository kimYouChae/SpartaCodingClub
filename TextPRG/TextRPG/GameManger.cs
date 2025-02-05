using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TextRPG
{
    #region
    
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
        StoreScene,
        RestScene,
        DungeonScene
    }

    enum ItemType
    {
        Armor,
        Weapon,
        Portion
    }

    enum ItemState 
    {
        UnObtained,     // 얻기전 (상점)
        InInvetory,     // 얻은후(인벤토리)
        Equipped        // 장착 (장착)
    }

    enum ItemSlot 
    {
        Armor,      // 상의
        Bottoms,    // 하의
        Weapon,     // 무기
        Ring,       // 반지
        Necklaces   // 목걸이
    }

    enum DungeonLevel 
    { 
        Easy,
        Normal,
        Hard
    }

    #endregion

    class GameManger : Singleton<GameManger>    
    {
        // 씬 저장 리스트 
        private IScene[] gameScene;
        private IScene nextScene;
        private IScene preScene;

        // 캐릭터를 만든적이 있는지 
        private bool _isCharacterCreated = false;

        // 임시 닉네임 
        private string _playerEnterName = "/테스트닉네임입니다/";

        // 세이프파일 저장위치
        string savePath = @"D:\SpartaCodingClub\TextRPGSaveFile\";
        string saveFileName = "SaveFile";

        // 프로퍼티
        public string playerEnterName => _playerEnterName;
        public bool isCharacterCreated => _isCharacterCreated;
        // ##TODO : 캐릭터를 만든적이 없을 때 초기화할 (아이템, 플레이어) 데이터들을 저장해놓은 
        // 델리게이트가 있었어도 좋았을것같음

        public GameManger() 
        {
            int temp = Enum.GetNames(typeof(SceneType)).Length;
            gameScene = new IScene[temp];

            // 싱글톤
            gameScene[(int)SceneType.LobbyScene]        = LobbyScene.Instance;
            gameScene[(int)SceneType.PlayerScene]       = PlayerManager.Instance;
            gameScene[(int)SceneType.InventoryScene]    = InventoryManger.Instance;
            gameScene[(int)SceneType.StoreScene]        = StoreManager.Instance;
            gameScene[(int)SceneType.RestScene]         = RestScene.Instance;
            gameScene[(int)SceneType.DungeonScene]      = DungeonScene.Instance;

            nextScene = gameScene[0];
        }


        public void EnterGame()
        {
            Console.WriteLine("===LOST ARK===");

            // 텍스트파일 로드 
            LoadData();

            // 접속 이력이 없으면
            if (!_isCharacterCreated)
            {
                Console.WriteLine("===LOST ARK===");
                Console.WriteLine("환영합니다 모험가님 \n 모험가님의 닉네임을 입력해주세요 ");

                _playerEnterName = Console.ReadLine();

                // 최초진입 -> 캐릭터 선택창으로 이동 
                ChangeScene(SceneType.PlayerScene);
            }
            else 
            {
                ChangeScene(SceneType.LobbyScene);
            }


        }


        // 씬 변경 ( ##TODO : 씬매니져 하나 만들어도될듯 )
        public void ChangeScene(SceneType type) 
        {
            // 지금 씬 = 예전 씬으로
            preScene = nextScene;

            // 현재 씬 지정 
            nextScene = gameScene[(int)type];

            // 현재씬 실행 
            if (preScene != nextScene)
                nextScene.SceneEntry();

            nextScene.SceneMainFlow();
        }

        // 직렬화
        private string SerializedObject() 
        {
            try 
            {
                PlayerSaveData data = new PlayerSaveData();
                data.FillPlayerData(PlayerManager.Instance.UserSelectPlayer,
                    ItemManager.Instance.EquipItem );
                 
                string json = JsonConvert.SerializeObject(data);

                //Console.WriteLine(json);
                return json;
            }
            catch (Exception e) 
            {
                Console.WriteLine($"직렬화 중 오류 발생 : {e.Message}");
                return string.Empty;
            }
          
        } 

        public void LoadData() 
        {
            // 경로 + 파일명에 파일이 있으면 ?
            if (File.Exists(savePath + saveFileName))
            {
                Console.WriteLine(" 저장파일이 있습니다.");

                _isCharacterCreated = true;

                // 불러오기 
                string desString = File.ReadAllText(savePath + saveFileName);

                // 역 직렬화
                try
                {
                    PlayerSaveData data = JsonConvert.DeserializeObject<PlayerSaveData>(desString);

                    // 플레이어 세팅 
                    PlayerManager.Instance.LoadPlayerData(data);

                    // 아이템 세팅 
                    ItemManager.Instance.SettingSavedEquipItem(data._equipItem);
                }

                catch (Exception e) { Console.WriteLine(e); }
            }
            else 
            {
                _isCharacterCreated = false;
            }
        }

        // ##TODO : 임시 , 로비에서 저장할 수 있게 
        public void SaveData() 
        {
            Console.WriteLine("데이터를 저장합니다.");

            // 직렬화
            string jsonString = SerializedObject();

            // 해당 디렉토리 (폴더) 가 없으면 생성
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            // string을 textFile로 저장
            // 파일이 이미 있으면 덮어씀
            using (StreamWriter sw = File.CreateText(savePath + saveFileName))
            {
                sw.WriteLine(jsonString);
            }
;        }
    }
}
