using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Player
    {
        private int _level;
        private PlayerClass _playerClass;
        private string _name;
        private int _attackPower;
        private int _defencPower;
        private int _HP;
        private int _MAXHP;
        private int _gold;
        private string _tooltip;

        // 추가스탯
        private int _addAttack;
        private int _addDefenc; 

        // 프로퍼티
        public int LV { get => _level; set { _level = value; } }
        public PlayerClass Playerclass { get => _playerClass; set { _playerClass = value; } }
        public string ToolTip { get => _tooltip; set { _tooltip = value; } }
        public string PlayerName { get  => _name; set { _name = value; } }
        public int Gold { get => _gold; set { _gold = value; } }
        public int HP { get => _HP; set { _HP = value; } }
        public int MAXHP { get => _MAXHP;  set { _MAXHP = value;} }

        // defence - attack
        public int AttackPower { get => _attackPower; set { _attackPower = value; } }
        public int DefencePower { get => _defencPower; set { _defencPower = value; } }

        // 추가스탯 프로퍼티
        public int AddDefence { get => _addDefenc; set { _addDefenc = value; } }
        public int AddAttack { get => _addAttack; set { _addAttack = value; } }

        public Player(int lv, PlayerClass pClass, string name, int attak, int defence, int hp, int gold, string tool)
        {
            this._level = lv;
            this._playerClass = pClass;
            this._name = name;
            this._attackPower = attak;
            this._defencPower = defence;
            this._HP = hp;
            this._MAXHP = 100;          // ##TODO : 임시 max 설정 
            this._gold = gold;
            this._tooltip = tool;

            _addAttack = 0;
            _addDefenc = 0;
        }

        public void Print()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("======플레이어정보출력====== \n");
            stringBuilder.Append( $"|| LV. {_level} " );
            stringBuilder.Append($"|| PlayerName {_name} ( {_playerClass} ) \n ");
            stringBuilder.Append($"|| 공격력 : { _attackPower} ( + { _addAttack }) \n " );
            stringBuilder.Append($"|| 방어력 : { _defencPower} ( + { _addDefenc}) \n " );
            stringBuilder.Append($"|| 체력 : { _HP} \n " );
            stringBuilder.Append($"|| 골드 : {_gold} \n" );
            stringBuilder.Append("==================== \n");

            Console.WriteLine(stringBuilder);
        }

    }
}
