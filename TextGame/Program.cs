using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection.PortableExecutable;

namespace TextGame
{
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public float Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Equipment
    {
        public int SellGold { get; }
        public int EquipPart { get; }
        public bool Isequip { get; set; }
        public bool Ishave { get; set; }
        public string Description { get; }
        public string Name { get; }

        public Equipment(int sellGold, int equipPart, bool isequip, bool ishave, string description, string name)
        {
            Ishave = ishave;
            SellGold = sellGold;
            EquipPart = equipPart;
            Isequip = isequip;
            Description = description;
            Name = name;
        }

        public virtual void DisplayEquipment(bool shop, bool isSell)
        {
        }

        public virtual int GetNumber()
        {
            return 0;
        }
        public void setLayout(int length, int remain, string str)
        {
            for (int i = 0; i < length; i++)
            {
                if (i == remain)
                {
                    Console.Write($"{str}");
                }
                else
                {
                    Console.Write(" ");
                }
            }
        }
    }

    public class Weapon : Equipment
    {
        public int Attack { get; }
        public Weapon(int sellGold, int equipPart, bool isequip, bool ishave, string description, string name, int attack) : base(sellGold, equipPart, isequip, ishave, description, name)
        {
            Attack = attack;
        }
        
        public override void DisplayEquipment(bool shop, bool isSell)
        {
            string Equip = (Isequip) ? "[E]" : "[X]";
            Equip = shop ? "" : Equip;
            int NameLengthRemain = (10 - Name.Length) / 2;
            int AttackLengthRemain = (4 - Attack.ToString().Length) / 2;
            int DescriptionRemain = (30 - Description.ToString().Length) / 2;
            string Money = (Ishave && !isSell) ? "구매완료" : SellGold.ToString() + " G";
            int MoneyRemain = (8 - Money.Length) / 2;

            setLayout(10 - Name.Length, NameLengthRemain, Equip + Name);
            Console.Write("|");
            setLayout(4 - Attack.ToString().Length, AttackLengthRemain, $"공격력 : +{Attack}");
            Console.Write("|");
            setLayout(30 - Description.ToString().Length, DescriptionRemain, Description);
            if (shop)
            {
                Console.Write("|");
                setLayout(8 - Money.Length, MoneyRemain, Money);
            }
            Console.WriteLine();
        }

        public override int GetNumber()
        {
            return Attack;
        }
    }

    public class Armor : Equipment
    {
        public int Protect { get; }
        public Armor(int sellGold, int equipPart, bool isequip, bool ishave, string description, string name, int protect) : base(sellGold, equipPart, isequip, ishave, description, name)
        {
            Protect = protect;
        }

        public override void DisplayEquipment(bool shop, bool isSell)
        {
            string Equip = (Isequip) ? "[E]" : "[X]";
            Equip = shop ? "" : Equip;
            int NameLengthRemain = (10 - Name.Length) / 2;
            int ProtectLengthRemain = (4 - Protect.ToString().Length) / 2;
            int DescriptionRemain = (30 - Description.ToString().Length) / 2;
            string Money = (Ishave && !isSell) ? "구매완료" : SellGold.ToString() + " G";
            int MoneyRemain = (8 - Money.Length) / 2;

            setLayout(10 - Name.Length, NameLengthRemain, Equip + Name);
            Console.Write("|");
            setLayout(4 - Protect.ToString().Length, ProtectLengthRemain, $"방어력 : +{Protect}");
            Console.Write("|");
            setLayout(30 - Description.ToString().Length, DescriptionRemain, Description);
            if (shop)
            {
                Console.Write("|");
                setLayout(8 - SellGold.ToString().Length, MoneyRemain, Money);
            }
            Console.WriteLine();
        }

        public override int GetNumber()
        {
            return Protect;
        }
    }

    internal class Program
    {
        private static Character player;
        private static List<Equipment> equipments = new List<Equipment>();
        static void EquipmentDataSetting()
        {
            Armor ironArmor = new Armor(500, 1, true, true, "무쇠로 만들어져 튼튼한 갑옷입니다.", "무쇠갑옷", 5);
            Armor noviceArmor = new Armor(2000, 1, false, false, "수련에 도움을 주는 갑옷입니다.", "수련자 갑옷", 9);
            Armor spartanArmor = new Armor(3500, 1, false, false, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", "스파르타의 갑옷", 15);

            Weapon oldSword = new Weapon(600, 0, false, true, "쉽게 볼 수 있는 낡은 검입니다.", "낡은 검", 2);
            Weapon bronzeAx = new Weapon(500, 0, false, false, "어디선가 사용됐던거 같은 도끼입니다.", "청동 도끼", 5);
            Weapon spearOfSparta = new Weapon(500, 0, false, false, "스파르타의 전사들이 사용했다는 전설의 창입니다.", "스파르타의 창", 7);

            equipments.Add(oldSword);
            equipments.Add(noviceArmor);
            equipments.Add(spartanArmor);
            equipments.Add(ironArmor);
            equipments.Add(bronzeAx);
            equipments.Add(spearOfSparta);
        }

        static int Checknum(int equippart)
        {
            int num = 0;
            for (int i = 0; i < equipments.Count; i++)
            {
                if (equipments[i].Isequip && equipments[i].EquipPart == equippart)
                {
                    num += equipments[i].GetNumber();
                }
            }
            return num;
        }

        static void GameDataSetting()
        {
            player = new Character("Chad", "전사", 1, 10 + Checknum(0), 5 + Checknum(1), 100, 1500);
        }
        static void EquipmentPlayerDataSetting(int num, int equippart, bool istake)
        {
            //공격력
            if (equippart == 0)
            {
                if(istake)
                    player.Atk += num;
                else
                    player.Atk -= num;
            }
            //방어력
            else
            {
                if(istake)
                    player.Def += num;
                else
                    player.Def -= num;
            }
        }

        static void Main(string[] args)
        {
            EquipmentDataSetting();
            GameDataSetting();
            DisplayGameIntro();
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4 : 던전입장");
            Console.WriteLine("5 : 휴식하기");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(1, 5);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop(); 
                    break;
                case 4:
                    DisplayDungeon(); 
                    break;
                case 5:
                    DisplayRest();
                    break;
            }
        }


        //-----------------------------------------상태보기-------------------------------------------------//
        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            int plusAtk = Checknum(0);
            int plusDef = Checknum(1);
            Console.Write($"공격력 :{player.Atk} ");
            if(plusAtk == 0)
            {
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine($"({Checknum(0)})");
            }
            Console.Write($"방어력 : {player.Def} ");

            if (plusDef == 0)
            {
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine($"({Checknum(1)})");
            }
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }


        //--------------------------------------------상점-------------------------------------------------//

        static void DisplayShop()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            for (int i = 0; i < equipments.Count; i++)
            {
                Console.Write($"- ");
                equipments[i].DisplayEquipment(true, false);
            }
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    ShopPurchase();
                    break;
                case 2:
                    ShopSell();
                    break;
            }
        }


        //상점 판매
        static void ShopSell()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            int num = 1;
            Dictionary<int,int> dic = new Dictionary<int,int>();
            for (int i = 0; i < equipments.Count; i++)
            {
                if (equipments[i].Ishave)
                {
                    Console.Write($"- {num}. ");
                    equipments[i].DisplayEquipment(true, true);
                    dic.Add(num,i);
                    num++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = CheckValidInput(0, num);
            if(input == 0)
            {
                DisplayShop();
            }
            else
            {
                player.Gold += equipments[dic[input]].SellGold;
                equipments[dic[input]].Ishave = false;
                if (equipments[dic[input]].Isequip)
                {
                    EquipmentPlayerDataSetting(equipments[dic[input]].GetNumber(), equipments[dic[input]].EquipPart, !equipments[dic[input]].Isequip);
                }
                equipments[dic[input]].Isequip = false;
                ShopSell();
            }
        }

        //상점 구매
        static void ShopPurchase()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            for (int i = 0; i < equipments.Count; i++)
            {
                Console.Write($"- {i + 1}. ");
                equipments[i].DisplayEquipment(true, false);
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                int input = CheckValidInput(0, equipments.Count);
                if (input == 0)
                {
                    DisplayGameIntro();
                    break;
                }
                else
                {
                    if (equipments[input - 1].Ishave == true)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else
                    {
                        if (player.Gold < equipments[input - 1].SellGold)
                        {
                            Console.WriteLine("Gold 가 부족합니다.");
                        }
                        else
                        {
                            player.Gold -= equipments[input - 1].SellGold;
                            Console.WriteLine("구매를 완료했습니다.");
                            equipments[input - 1].Ishave = true;
                        }
                    }
                }
            }
        }

        //--------------------------------------------인벤토리-------------------------------------------------//
        static void DisplayInventory()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < equipments.Count; i++)
            {
                if (equipments[i].Ishave)
                {
                    Console.Write($"- ");
                    equipments[i].DisplayEquipment(false, false);
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 아이템 정렬");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    InvenEquipManagement();
                    break;
                case 2:
                    InvenSort();
                    break;
            }
        }

        static void InvenSort()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 아이템 정렬");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for(int i = 0; i < equipments.Count; i++)
            {
                if (equipments[i].Ishave)
                {
                    Console.Write($"- ");
                    equipments[i].DisplayEquipment(false, false);
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 이름");
            Console.WriteLine("2. 장착순"); 
            Console.WriteLine("3. 공격력/방어력 수치");  
            Console.WriteLine("0. 나가기"); 
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;
                case 1:
                case 2:
                case 3:
                    Sort(input);
                    InvenSort();
                    break;
            }
        }

        static void Sort(int num)
        {
            for (int i = 0; i < equipments.Count - 1; i++)
            {
                int minIndex = i;
                if(num == 2 && equipments[i].Isequip)
                    continue;

                for (int j = i + 1; j < equipments.Count; j++)
                {
                    if(num == 1)
                    {
                        if (equipments[j].Name.Length > equipments[minIndex].Name.Length)
                        {
                            minIndex = j;
                        }
                    }else if(num == 2)
                    {
                        if (equipments[j].Isequip && !equipments[minIndex].Isequip)
                        {
                            minIndex = j;
                        }
                    }
                    else if(num == 3)
                    {
                        if (equipments[j].GetNumber() > equipments[minIndex].GetNumber())
                        {
                            minIndex = j;
                        }
                    }
                }

                Equipment temp = equipments[i];
                equipments[i] = equipments[minIndex];
                equipments[minIndex] = temp;
            }
        }

        static void InvenEquipManagement()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Dictionary<int, int> dic = new Dictionary<int, int>();
            int num = 1;
            for (int i = 0; i < equipments.Count; i++)
            {
                if (equipments[i].Ishave)
                {
                    Console.Write($"- {num}. ");
                    equipments[i].DisplayEquipment(false, false);
                    dic.Add(num, i);
                    num++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            
            int input = CheckValidInput(0, num);

            if(input == 0)
            {
                DisplayInventory();
            }
            else
            {
                if(!equipments[dic[input]].Isequip)
                {
                    for(int i = 0; i < equipments.Count ; i++)
                    {
                        if(equipments[dic[input]].EquipPart == equipments[i].EquipPart && equipments[i].Isequip)
                        {
                            EquipmentPlayerDataSetting(equipments[i].GetNumber(), equipments[i].EquipPart, !equipments[i].Isequip);
                            equipments[i].Isequip = false;
                        }
                    }
                }
                equipments[dic[input]].Isequip = equipments[dic[input]].Isequip ? false : true;
                EquipmentPlayerDataSetting(equipments[dic[input]].GetNumber(), equipments[dic[input]].EquipPart, equipments[dic[input]].Isequip);
                InvenEquipManagement();
            }
        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }


        //--------------------------------------------던전입장-------------------------------------------------//
        static void DisplayDungeon()
        {
            Console.Clear();
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 쉬운 던전        | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전        | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전      | 방어력 17 이상 권장");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    if(player.Def >= 5)
                    {
                        DungeonClearFail(true,1);
                    }
                    else
                    {
                        bool clear = GetRandomNum(1, 10) > 5 ? true : false;
                        DungeonClearFail(clear, 1);
                    }
                    break;
                case 2:
                    if (player.Def >= 11)
                    {
                        DungeonClearFail(true, 2);
                    }
                    else
                    {
                        bool clear = GetRandomNum(1, 10) > 5 ? true : false;
                        DungeonClearFail(clear, 2);
                    }
                    break;
                case 3:
                    if (player.Def >= 5)
                    {
                        DungeonClearFail(true, 3);
                    }
                    else
                    {
                        bool clear = GetRandomNum(1, 10) > 5 ? true : false;
                        DungeonClearFail(clear, 3);
                    }
                    break;
            }
        }

        static int GetRandomNum(int min, int max)
        {
            var rand = new Random();
            return rand.Next(min, max + 1);
        }

        static void DungeonClearFail(bool clear, int dungeonNum)
        {
            Console.Clear();
            string dungeonName = "";
            int rewardGold = 0;
            int recommendedDefenses = 0;
            if (dungeonNum == 0)
            {
                rewardGold = 1000 + 1000 * GetRandomNum((int)player.Atk, (int)player.Atk * 2) / 100;
                dungeonName = "쉬운 던전";
                recommendedDefenses = 5;
            }
            else if(dungeonNum == 1)
            {
                rewardGold = 1700 + 1700 * GetRandomNum((int)player.Atk, (int)player.Atk * 2) / 100;
                dungeonName = "일반 던전";
                recommendedDefenses = 11;
            }
            else if(dungeonNum == 2)
            {
                rewardGold = 2500 + 2500 * GetRandomNum((int)player.Atk, (int)player.Atk * 2) / 100;
                dungeonName = "어려운 던전";
                recommendedDefenses = 17;
            }
            if (clear)
            {
                Console.WriteLine("던전 클리어");
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{dungeonName}을 클리어 하셨습니다.");
                Console.WriteLine();
                Console.WriteLine("[탐험 결과]");
                int rand = GetRandomNum(20 + (player.Def - recommendedDefenses), 35 + (player.Def - recommendedDefenses));
                Console.WriteLine($"체력 {player.Hp} -> {player.Hp - rand}");
                player.Hp -= rand;
                Console.WriteLine($"Gold {player.Gold} G -> {player.Gold + rewardGold}");
                player.Gold += rewardGold;

                player.Level += 1;
                player.Def += 1;
                player.Atk += 0.5f;
            }
            else
            {
                Console.WriteLine("던전 클리어 실패");
                Console.WriteLine("아쉽습니다..");
                Console.WriteLine($"{dungeonName}을 클리어하지 못하셨습니다.");
                Console.WriteLine();
                Console.WriteLine("[탐험 결과]");
                
                Console.WriteLine($"체력 {player.Hp} -> {player.Hp / 2}");
                player.Hp /= 2;
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = CheckValidInput(0, 0);
            if(input == 0)
            {
                DisplayGameIntro();
            }
        }

        //--------------------------------------------휴식하기-------------------------------------------------//
        static void DisplayRest()
        {
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G)");
            Console.WriteLine();
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            while (true)
            {
                int input = CheckValidInput(0, 1);
                switch (input)
                {
                    case 0:
                        DisplayGameIntro();
                        break;
                    case 1:
                        if (player.Gold < 500)
                        {
                            Console.WriteLine("Gold 가 부족합니다.");
                        }
                        else
                        {
                            Console.WriteLine("휴식을 완료했습니다.");
                            player.Hp = 100;
                        }
                        break;
                }
            }
        }


    }


}