using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{

    class PlayerSaveData
    {
        [JsonProperty] public int _level;
        [JsonProperty] public PlayerClass _playerClass;
        [JsonProperty] public string _name;
        [JsonProperty] public int _attackPower;
        [JsonProperty] public int _defencPower;
        [JsonProperty] public int _HP;
        [JsonProperty] public int _MAXHP;
        [JsonProperty] public int _gold;
        [JsonProperty] public string _tooltip;

        // 추가스탯
        [JsonProperty] public int _addAttack;
        [JsonProperty] public int _addDefenc;

        // 아이템 정보
        [JsonProperty] public List<EquitItem> _equipItem;       // 장비아이템
        

        public void FillPlayerData(Player PLAYER , List<EquitItem> EQUIPT ) // 인벤토리 아이템, 장착 아이템 
        {
            // 저장할 때 플레이어 데이터를 가져와야함
            this._level         = PLAYER.LV;
            this._playerClass   = PLAYER.Playerclass;
            this._name          = PLAYER.PlayerName;
            this._attackPower   = PLAYER.AttackPower;
            this._defencPower   = PLAYER.DefencePower; 
            this._HP            = PLAYER.HP;
            this._MAXHP         = PLAYER.MAXHP;
            this._gold          = PLAYER.Gold;
            this._tooltip       = PLAYER.ToolTip;
            this._addAttack     = PLAYER.AddAttack;
            this._addDefenc     = PLAYER.AddDefence;

            // 저장할 때 아이템 데이터를 가져와야함
            _equipItem = new List<EquitItem>();

            for (int i = 0; i < EQUIPT.Count; i++) 
            {
                // 현재 equipt는 Euipt타입
                // 리스트는 Item 타입
                _equipItem.Add( EQUIPT[i] );
            }

        }
    }
}
