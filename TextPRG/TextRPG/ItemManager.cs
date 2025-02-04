using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class ItemManager : Singleton<ItemManager>
    {
        // 장비 타입별 Item (획득전, 인벤토리 , 착용후 )
        private Dictionary<ItemState, List<Item>> DICT_stateToItem;

        // 장비 착용 부위별 item (상의, 하의, 무기, 반지, 목걸이)
        // item은 null 허용!
        private Dictionary<ItemSlot , Item?> DICT_slotToItem;

        private List<EquitItem> equipItem;
        private List<PortionItem> portionItem;

        public int StateToListCount(ItemState state) 
        {
            try
            {
                return DICT_stateToItem[state].Count;
            }
            catch 
            {
                Console.WriteLine("딕셔너리에 해당하는 list가 없습니다.");
                return -1;
            }
        }

        public ItemManager() 
        {
            InitContainer();
        }

        private void InitContainer() 
        {
            // 1. 딕셔너리 
            // 1-1 .장비 타입별 item
            DICT_stateToItem = new Dictionary<ItemState, List<Item>>();
            Array stateArray = Enum.GetValues(typeof(ItemState));
            foreach (ItemState temp in stateArray) 
            {
                if( !DICT_stateToItem.ContainsKey(temp))
                    DICT_stateToItem[temp] = new List<Item>();   
            }

            // 1-2. 장비 부위별 item
            DICT_slotToItem = new Dictionary<ItemSlot , Item?>();
            Array slotArray = Enum.GetValues(typeof(ItemSlot));
            foreach (ItemSlot temp in slotArray)
            {
                if (!DICT_slotToItem.ContainsKey(temp))
                    DICT_slotToItem[temp] = null;
            }


            // 2. 장비 아이템
            equipItem = new List<EquitItem>()
            {
                // 장비
                // 갑옷 (상의)
                new EquitItem(name : "수련자 갑옷" ,  itemType : ItemType.Armor ,tooltip : "수련에 도움을 주는 갑옷입니다." ,
                    price : 1000 , stateType : ItemState.UnObtained , add : 5 , qulity : 70 , slot : ItemSlot.Armor , addType : AddState.Defence) ,
                new EquitItem(name : "무쇠갑옷" ,  itemType : ItemType.Armor ,tooltip : "무쇠로 만들어져 튼튼한 갑옷입니다." ,
                    price : 1500 , stateType : ItemState.UnObtained , add : 9 , qulity : 80 , slot : ItemSlot.Armor , addType : AddState.Defence) , 
                new EquitItem(name : "스파르타의 갑옷" ,  itemType : ItemType.Armor ,tooltip : "스파르타의 전사들이 사용했다는 전설의 갑옷입니다." ,
                        price : 3500 , stateType : ItemState.UnObtained , add : 15 , qulity : 90 , slot : ItemSlot.Armor , addType : AddState.Defence) ,
                
                // 하의
                new EquitItem(name : "운명의 업화 투구" ,  itemType : ItemType.Armor ,tooltip : "카제로스 1막 : 대지를 부수는 업화의 궤적/을 클리어시 획득 가능한 투구 입니다." ,
                        price : 5000 , stateType : ItemState.UnObtained , add : 30 , qulity : 99 , slot : ItemSlot.Bottoms , addType : AddState.Defence) ,
                new EquitItem(name : "알키오네의 투구" ,  itemType : ItemType.Armor ,tooltip : "서막 : 붉어진 백야의 나선/을 클리어시 획득 가능한 투구 입니다." ,
                        price : 6000 , stateType : ItemState.UnObtained , add : 45 , qulity : 99 , slot : ItemSlot.Bottoms , addType : AddState.Defence) ,
                
                // 무기
                new EquitItem(name : "낡은 검" ,  itemType : ItemType.Weapon ,tooltip : "쉽게 볼 수 있는 낡은 검 입니다" ,
                        price : 600 , stateType : ItemState.UnObtained , add : 2 , qulity : 50 , slot : ItemSlot.Weapon , addType : AddState.Attack) ,
                new EquitItem(name : "청동 도끼" ,  itemType : ItemType.Weapon ,tooltip : "어디선가 사용됐던거 같은 도끼입니다" ,
                        price : 1500 , stateType : ItemState.UnObtained , add : 5 , qulity : 60 , slot : ItemSlot.Weapon , addType : AddState.Attack) ,
                new EquitItem(name : "스파르타의 창" ,  itemType : ItemType.Weapon ,tooltip : "스파르타의 전사들이 사용했다는 전설의 창입니다." ,
                        price : 2300 , stateType : ItemState.UnObtained , add : 7 , qulity : 70 , slot : ItemSlot.Weapon , addType : AddState.Attack) ,

                // 장신구 
                // 반지
                new EquitItem(name : "마주한 종언의 반지" ,  itemType : ItemType.Armor ,tooltip : "쿠르잔 전선에서 획득 가능한 고대반지 입니다." ,
                        price : 2000 , stateType : ItemState.UnObtained , add : 20 , qulity : 99 , slot : ItemSlot.Ring , addType : AddState.Attack) ,
                new EquitItem(name : "도래한 결전의 반지" ,  itemType : ItemType.Armor ,tooltip : "카제로스 레이드에서 획득 가능한 고대반지 입니다." ,
                        price : 2300 , stateType : ItemState.UnObtained , add : 20 , qulity : 99 , slot : ItemSlot.Ring , addType : AddState.Attack) ,
                // 목걸이
                new EquitItem(name : "마주한 종언의 목걸이" ,  itemType : ItemType.Armor ,tooltip : "쿠르잔 전선에서 획득 가능한 고대 목걸이 입니다." ,
                        price : 2500 , stateType : ItemState.UnObtained , add : 30 , qulity : 99 , slot : ItemSlot.Necklaces , addType : AddState.Attack) ,
                new EquitItem(name : "도래한 결전의 목걸이" ,  itemType : ItemType.Armor ,tooltip : "카제로스 레이드에서 획득 가능한 고대 목걸이 입니다." ,
                        price : 2800 , stateType : ItemState.UnObtained , add : 30 , qulity : 99 , slot : ItemSlot.Necklaces , addType : AddState.Attack)
            };

            // 3. 물약 아이템
            portionItem = new List<PortionItem>()
            {
                new PortionItem( name : "회복약" ,  itemType : ItemType.Portion , tooltip : "체력을 30 회복해줍니다." ,
                    price : 100 , stateType : ItemState.UnObtained , acquireCount : 0 , recoverAmount : 30),
                new PortionItem( name : "고급 회복약" ,  itemType : ItemType.Portion , tooltip : "체력을 50 회복해줍니다." ,
                    price : 150 , stateType : ItemState.UnObtained , acquireCount : 0 , recoverAmount : 50),
                new PortionItem( name : "정령의 회복약" ,  itemType : ItemType.Portion , tooltip : "체력을 70 회복해줍니다.." ,
                    price : 170 , stateType : ItemState.UnObtained , acquireCount : 0 , recoverAmount : 70),
            };
        }

        // 획득 타입별 출력 
        public void PrintItemByItemState( ItemState state , bool isEquitManage = false ) // flag : 장착관리인지 ( 앞에 [E]붙일지 말지 )
        {
            if ( ! DICT_stateToItem.ContainsKey(state)) 
            {
                Console.WriteLine($"{state}에 해당하는 타입의 아이템이 딕셔너리에 없습니다");
                return;
            }

            // state에 해당하는 list
            List<Item> list = DICT_stateToItem[state];
            if (list.Count <= 0)
            {
                Console.WriteLine($"{state}에 해당하는 인벤토리는 비어있습니다! ");
                return;
            }

            // 출력 
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < list.Count; i++) 
            {
                // ##TODO : StringBuilder 관련 모듈 만들기! 
                stringBuilder.Clear();

                stringBuilder.Append("-" + i.ToString());
                // 장착관리중이면 [E] 같이 출력 
                if (isEquitManage)
                    stringBuilder.Append("[ E ]");

                stringBuilder.Append("|" + list[i].name);

                // item이 equit이면=> 방어력 + n
                // protion이면=> 타입 "Portion"출력

                if (list[i] is EquitItem)
                    stringBuilder.Append("|" + ((EquitItem)list[i]).addState + " + " +((EquitItem)list[i]).addStateAmount);
                // EquitItem으로 형변환을 먼저 해준 후 프로퍼티에 접근해야함! 
                else if (list[i] is PortionItem)
                    stringBuilder.Append("| 갯수" +((PortionItem)list[i]).acquireCount);

                stringBuilder.Append("|" + list[i].toolTip +'\n');
            }
        }

        private Item StateToItem(ItemState state , int idx) 
        {
            try
            {
                return DICT_stateToItem[state][idx];
            }
            catch 
            {
                return null;
            }
        }

        // 착용 관리 딕셔너리의 부위에 해당하는 item 검사
        private bool isEquitSlotIsEmpty(Item item) 
        {
            // 착용가능한 장비이면
            if (item is EquitItem)
            {
                // null 이면 true(착용가능), 아니면 false
                return DICT_slotToItem[((EquitItem)item).slot] == null ? true : false;
            }
            else 
            {
                Console.WriteLine("왜 착용 불가능한 장비가 들어왔지 ?");
                return false;
            }
        }

        // 아이템의 장착 타입 변환 
        public void ChangeItenEquitType(ItemState preState, ItemState nextState, int idx) 
        {
            Item item = StateToItem( preState , idx );

            // 예외 : 인덱스 오류
            if (idx < 0 || idx >= DICT_stateToItem[preState].Count)
            {
                Console.WriteLine("인벤토리 | 장착 | 잘못된 인덱스 접근 입니다.");
                return;
            }
            // Item의 null 오류
            if (item == null) 
            {
                Console.WriteLine("ItemManager | 아이템 타입 변환 null 오류 ");
                return;
            }
            // 딕셔너리[state]의 리스트에서 item 검사
            if (!DICT_stateToItem.ContainsKey(preState))
            {
                Console.WriteLine($"{preState}에 해당하는 dictionary 오류 ");
                return;
            }
            // 만약 아이템 타입이 Armor + 착용 딕셔너리에 이미 착용하고있으면 ?
            if (item.type == ItemType.Armor && !isEquitSlotIsEmpty(item))
            {
                Console.WriteLine($"{item.name} 은/는 이미 착용하고 있습니다");
                return;
            }
            // 만약 아이템이 물약이면 ?
            if (item.type == ItemType.Portion) 
            {
                Console.WriteLine($"{item.name} 물약은 착용할 수 없습니다");
                return;
            }

            // 리스트에서 지우기 
            DICT_stateToItem[preState].Remove(item);

            // 리스트에 추가하기
            DICT_stateToItem[nextState].Add(item);
        }

    }
}
