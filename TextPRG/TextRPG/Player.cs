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
        private float _HP;
        private float _gold;

        public Player(int lv, PlayerClass pClass, string name, float attak, float defence, float hp, float gold )
        { 
            this._LV = lv;
            this._playerClass = pClass;
            this._name = name;
            this._attackPower = attak;
            this._defencPower = defence;
            this._HP = hp;
            this._gold = gold;
        }

        public void Print() 
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("LV." + _LV.ToString() + '\n');
            stringBuilder.Append("PlayerName" + _name + "(" + _playerClass.ToString() + " )" + '\n');
            stringBuilder.Append("공격력 : " + _attackPower.ToString() + '\n');
            stringBuilder.Append("방어력 : " + _attackPower.ToString() + '\n');
            stringBuilder.Append("체력 : " + _HP.ToString() + '\n');
            stringBuilder.Append("방어력 : " + _gold.ToString() + '\n');

            Console.WriteLine(stringBuilder);

            
        }
    }
}
