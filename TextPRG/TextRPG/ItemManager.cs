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
        private Dictionary<ItemSlot, Item?> DICT_slotToItem;

        private List<EquitItem> equipItem;
        private List<PortionItem> portionItem;

        // 아이템이 리스트안에 있는지 확인, 인텍스에 해당하는 값 리턴
        public Item GetEquipByIndex(int idx)
        {
            if (idx < 0 || idx >= equipItem.Count)
                return null;

            return equipItem[idx];
        }

        // 타입별 인덱스에 해당하는 아이템 리턴
        public Item GetEquitInDictionray(ItemState state, int idx)
        {
            try
            {
                return DICT_stateToItem[state][idx];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        // 타입별 리스트 리턴
        public List<Item> GetListByState(ItemState state) 
        {
            try
            {
                return DICT_stateToItem[state];
            }
            catch 
            {
                return null;
            }
        }

        public int EquipListCount => equipItem.Count;
        public List<EquitItem> EquipItem => equipItem;

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
                if (!DICT_stateToItem.ContainsKey(temp))
                    DICT_stateToItem[temp] = new List<Item>();
            }

            // 1-2. 장비 부위별 item
            DICT_slotToItem = new Dictionary<ItemSlot, Item?>();
            Array slotArray = Enum.GetValues(typeof(ItemSlot));
            foreach (ItemSlot temp in slotArray)
            {
                if (!DICT_slotToItem.ContainsKey(temp))
                    DICT_slotToItem[temp] = null;
            }

            // 4. 장비아이템은 딕셔너리에 넣어놓기 
            // 캐릭터를 만든적 없을때만 -> 
            if (! GameManger.Instance.isCharacterCreated)
            {
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

                // STATE 딕셔너리에 ITEM 넣기 
                AddToDictByState();
            }


        }

        // 장비 아이템 돌면서 STATE따라서 DICT 에 넣기
        private void AddToDictByState() 
        {
            for (int i = 0; i < equipItem.Count; i++)
            {
                // 초기에는 UnObtained로 들어가있음 
                DICT_stateToItem[equipItem[i].state].Add(equipItem[i]);
            }
        }

        // 획득 타입별 출력 ( Dictionary 출력 )
        public void PrintItemByItemState(ItemState itemState , bool _isEquitManage = false)
        {
            // isEquitManage : 장착관리인지 ( 앞에 [E]붙일지 말지 )
            // isStore : 상점인지 ( 맨 뒤에 골드 or 구매완료 붙일지 말지)

            if (!DICT_stateToItem.ContainsKey(itemState))
            {
                Console.WriteLine($"{itemState}에 해당하는 타입의 아이템이 딕셔너리에 없습니다");
                return;
            }

            // state에 해당하는 list
            List<Item> list = DICT_stateToItem[itemState];
            if (list.Count <= 0)
            {
                Console.WriteLine($"{itemState}에 해당하는 인벤토리는 비어있습니다! ");
                return;
            }

            // 출력 
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                Item currItem = list[i];

                stringBuilder.Clear();
                StringBuilderAppender(stringBuilder , i , currItem , isEquitManage : _isEquitManage);

                Console.WriteLine(stringBuilder);
            }
        }

        // 스트링 빌더에 Item 붙이기
        private void StringBuilderAppender(StringBuilder stringBuilder, int idx , Item currItem , bool isEquitManage = false, bool isStore = false ) 
        {
            stringBuilder.Append("-" + idx.ToString());

            if (isEquitManage)
                stringBuilder.Append("[ E ]");

            stringBuilder.Append("|" + currItem.name);

            // item이 equit이면=> 방어력 + n
            // protion이면=> 타입 "Portion"출력
            if (currItem is EquitItem)
                stringBuilder.Append("|" + ((EquitItem)currItem).addState + " + " + ((EquitItem)currItem).addStateAmount);
            // EquitItem으로 형변환을 먼저 해준 후 프로퍼티에 접근해야함! 
            else if (currItem is PortionItem)
                stringBuilder.Append("| 갯수" + ((PortionItem)currItem).acquireCount);

            stringBuilder.Append("|" + currItem.toolTip);

            if (isStore)
                stringBuilder.Append(PrintItemPrice(currItem));
        }

        // 장비 출력 
        public void PrintEquipItem( bool _isStore = false) 
        {
            StringBuilder stringBuilder = new StringBuilder();
            // 착용관리일때 "[E]"붙이기
            // 상점일때 구매했으면 "구매완료" 아니면 {골드}
            for (int i = 0; i < equipItem.Count; i++) 
            {
                stringBuilder.Clear();
                StringBuilderAppender( stringBuilder , i , equipItem[i]  , isStore : _isStore);
                Console.WriteLine(stringBuilder);
            }
        }

        // return 아이템 가격 or 판매완료
        private string PrintItemPrice(Item item) 
        {
            // 획득했으면 
            if (item.state != ItemState.UnObtained)
                return "{ 구매완료 }";
            else
                return "{ " + item.price.ToString() + " } ";
        }

        // 착용 관리 딕셔너리의 부위에 해당하는 item 검사
        private void WearEquipItem(Item item) 
        {
            // 장비 착용 관리
            // 1. 착용 딕셔너리에 있는지 검사해야함 
            // 1-2. 없으면 -> 착용 (딕셔너리 추가)

            ItemSlot mySlot = ((EquitItem)item).slot;

            if (DICT_slotToItem[mySlot] == null)
            {
                DICT_slotToItem[mySlot] = item;
                return;
            }

            // 1-3. null이 아니면 (착용하고 있는게 있으면 )
            // -> 원래 착용하고 있던 장비를 상태변화 (Equipped -> inventory)
            Item oriItem = DICT_slotToItem[mySlot];

            ChangeStateEquipItem(ItemState.Equipped , ItemState.InInvetory, oriItem);
        }

        // 장비 아이템의 장착 타입 변환 
        public void ChangeStateEquipItem(ItemState preState, ItemState nextState, Item item ) 
        {
            // Item의 null 오류
            if (item == null)
            {
                Console.WriteLine("ItemManager | 아이템 타입 변환 null 오류 ");
                return;
            }

            // nextState가 Equipped일때 (착용해야할 장비일때)
            // 1. 착용해야할 장비일때
            if (nextState == ItemState.Equipped)
            { 
                WearEquipItem(item);

                // 아이템 추가 스탯 설정
                PlayerManager.Instance.UpdateAddState(((EquitItem)item).addState, ((EquitItem)item).addStateAmount);
            }

            // 2. 그냥 인벤토리에 획득만 하면 될 때
            // (착용해야할 장비도 포함됨)
            // 리스트에서 지우기 
            DICT_stateToItem[preState].Remove(item);

            // 리스트에 추가하기
            DICT_stateToItem[nextState].Add(item);

            // 아이템의 상태 바꿔주기
            item.state = nextState;

          
        }

        // 저장된 Equit 아이템 불러와서 딕셔너리 와 equip리스트에 넣기
        public void SettingSavedEquipItem(List<EquitItem> list) 
        {
            equipItem = new List<EquitItem>();

            for (int i = 0; i < list.Count; i++) 
            {
                EquitItem my = list[i];

                // 1. 장비리스트에 추가
                equipItem.Add( my );

                // 3. 착용한흔적이있으면 
                if (my.state == ItemState.Equipped) 
                {
                    // 착용 딕셔너리에 넣기
                    DICT_slotToItem[ my.slot ] = my;
                }
            }
            // STATE 딕셔너리에 ITEM 넣기 
            AddToDictByState();
        }
    }
}
