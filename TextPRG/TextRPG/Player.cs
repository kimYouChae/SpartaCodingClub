using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Player
    {
        private int _LV;
        private PlayerClass _playerClass;
        private string _name;
        private float _attackPower;
        private float _defencPower;
        private int _HP;
        private int _MAXHP;
        private int _gold;
        private string _tooltip;

        // 프로퍼티
        public PlayerClass Playerclass => _playerClass;
        public string ToolTip => _tooltip;
        public string PlayerName { get { return _name; } set { _name = value; } }
        public int Gold { get => _gold; set { _gold = value; } }
        public int HP { get => _HP; set { _HP = value; } }
        public int MAXHP { get => _MAXHP; }

        public Player(int lv, PlayerClass pClass, string name, float attak, float defence, int hp, int gold, string tool)
        {
            this._LV = lv;
            this._playerClass = pClass;
            this._name = name;
            this._attackPower = attak;
            this._defencPower = defence;
            this._HP = hp;
            this._MAXHP = 100;          // ##TODO : 임시 max 설정 
            this._gold = gold;
            this._tooltip = tool;
        }

        public void Print()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("======플레이어정보출력====== \n");
            stringBuilder.Append("|| LV." + _LV.ToString() + " || " +  '\n' );
            stringBuilder.Append("|| PlayerName" + _name + "(" + _playerClass.ToString() + " ) || \n");
            stringBuilder.Append("|| 공격력 : " + _attackPower.ToString() +" || " + '\n' );
            stringBuilder.Append("|| 방어력 : " + _attackPower.ToString() + " || " + '\n');
            stringBuilder.Append("|| 체력 : " + _HP.ToString() + " || " + '\n');
            stringBuilder.Append("|| 방어력 : " + _gold.ToString() + " || " + '\n' );
            stringBuilder.Append("==================== \n");

            Console.WriteLine(stringBuilder);

            
        }
    }
}
