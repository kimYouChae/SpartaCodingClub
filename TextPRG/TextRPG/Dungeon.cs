using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Dungeon
    {
        Random random;
        private string _name;                  // 던전 이름 
        private int _recommendedDefence;    // 권장 방어력
        private int _clearGold;           // 클리어 보상 

        private int _minReduceHp;
        private int _maxReduceHp;

        // 프로퍼티
        public string name => _name;
        public int recommendDefence => _recommendedDefence;
        public int clearGold => _clearGold;

        public Dungeon(string n,int reDe, int clearR)
        {
            random = new Random();

            this._name = n;
            this._recommendedDefence = reDe;
            this._clearGold = clearR;

            _minReduceHp = 20;
            _maxReduceHp = 36;
        }

        public int RedeuceHp(int playerDefence)
        {
            // 20 + 플레이어 방어력 ~ 35 + 플레이어 방어력 사이에서 랜덤값
            int gap = _recommendedDefence - playerDefence;
            return random.Next(_minReduceHp + gap, _maxReduceHp + gap);
        }

        public int AddGold(int playerAttack)
        {
            // 플레이어 공격력 ~ 공격력 * 2 사이에서 랜덤값
            return random.Next(playerAttack, playerAttack * 2);
        }
    }
}
