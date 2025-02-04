using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    abstract class Item
    {
        protected string _name;      // 이름
        protected ItemType _type;    // 장비 , 방어구, 포션 타입
        protected string _toolTip;   // 설명
        protected int _price;        // 가격
        protected ItemState _state;  // 아이템이 획득전 or 인벤토리내 or 착용중인지 

        // 프로퍼티
        public string name => _name;
        public ItemState state { get => _state; set { _state = value; } }
        public string toolTip => _toolTip;
        public int price => _price;
        public ItemType type => _type;


        protected Item(string name, ItemType type, string tooltip, int price, ItemState state) 
        {
            this._name = name;
            this._type = type;
            this._toolTip = tooltip;
            this._price = price;
            this._state = state;
        }

        public virtual void Use() { }
        public virtual void Use(int count) { }

    }

    class EquitItem : Item 
    {
        private int _addStateAmount;   // 추가스탯 량 ex)공격력 15증가
        private int _quality;    //  품질
        private ItemSlot _slot;  // 아이템이 어느부위에 장착되는지
        private AddState _stateType; // 추가스탯 타입 ex) 공격력/방어력

        // 프로퍼티
        public int addStateAmount => _addStateAmount;
        public int quality => _quality;
        public ItemSlot slot => _slot;
        public AddState addState => _stateType;

        public EquitItem(string name, ItemType itemType, string tooltip , int price , ItemState stateType
            , int add , int qulity, ItemSlot slot, AddState addType) : base(name, itemType , tooltip , price, stateType)
        {
            this._addStateAmount = add;
            this._quality = qulity;
            this._slot = slot;
            this._stateType = addType;
        }
        
        public override void Use() 
        {
               
        }
    }

    class PortionItem : Item
    {
        private int _acquireCount;       // 획득 수량 
        private float _recoverAmount;    // 회복수량 

        // 프로퍼티
        public int acquireCount => _acquireCount;

        public PortionItem(string name, ItemType itemType, string tooltip, int price, ItemState stateType, int acquireCount, float recoverAmount) 
            : base(name, itemType, tooltip, price, stateType)
        {
            this._acquireCount = acquireCount;
            this._recoverAmount = recoverAmount;
        }
    }

}
