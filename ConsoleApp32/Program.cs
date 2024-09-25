using ConsoleApp32;

namespace ConsoleApp32
{
    struct Player
    {
        public int Level { get; set; }
        public double Atk { get; set; }
        public double Def { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }
        public Job Job { get; set; }
        public string Name { get; set; }
        public Item EquippedWeapon { get; set; }
        public Item EquippedArmor { get; set; }
    }

    struct Monster
    {
        public string Name;
        public int Hp;
        public int Atk;

        public Monster(string name, int hp, int atk)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
        }
    }

    class Sparta
    {
        static Player player;
        static void Main()
        {
            player = new Player { Level = 1, Atk = 10, Def = 5, Hp = 100, MaxHp = 100, Gold = 0, MaxExp = 100, Exp = 0, Job = Job.전사 };
            Console.WriteLine("스파르타 던전 RPG");
            Console.WriteLine("모험을 떠나고 싶으면 아무키나 눌러주세요!");
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine("모험을 시작합니다!");
            Naming.Naminged(ref player);
        }
    }

    public enum Job { 전사, 궁수, 마법사 }

    class Naming
    {
        public static void Naminged(ref Player player)
        {
            Console.WriteLine("용사여 당신의 이름은 무엇인가?");
            string heroname = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"당신의 이름은 {heroname}가 확실한가?");
            Console.WriteLine("맞으면 '1' 틀리면 '2'를 눌러주시게!");
            string yesorno = Console.ReadLine();

            switch (yesorno)
            {
                case "1":
                    player.Name = heroname;
                    Console.Clear();
                    break;

                case "2":
                    Console.Clear();
                    Naminged(ref player);
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Naminged(ref player);
                    break;
            }

            while (true)
            {
                Console.WriteLine("훌륭한 이름이군 {0}이여, 직업을 골라보게나!", heroname);
                Console.WriteLine();
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 궁수");
                Console.WriteLine("3. 마법사");

                string jobname = Console.ReadLine();
                if (SetPlayerJob(ref player, jobname))
                {
                    break;
                }

                Console.Clear();
                Console.WriteLine("당신 백수는 아니겠지? 다시 직업을 선택해주게.");
                Console.WriteLine();
            }

            Console.WriteLine("다음으로 이동하고 싶으시면 아무키나 눌러주세요!");
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine("마을에 도착했습니다!");
            Starter.Startered(ref player);
        }

        private static bool SetPlayerJob(ref Player player, string jobname)
        {
            switch (jobname)
            {
                case "1":
                    player.Job = Job.전사;
                    player.Atk = 10;
                    player.Def = 10;
                    player.Hp = 150;
                    player.MaxHp = 150;
                    Console.Clear();
                    Console.WriteLine("전사를 선택했구만. 좋은 직업이야. 스파르타 마을로 이동하지.");
                    return true;

                case "2":
                    player.Job = Job.궁수;
                    player.Atk = 13;
                    player.Def = 7;
                    player.Hp = 130;
                    player.MaxHp = 130;
                    Console.Clear();
                    Console.WriteLine("궁수를 선택했구만. 좋은 직업이야. 스파르타 마을로 이동하지.");
                    return true;

                case "3":
                    player.Job = Job.마법사;
                    player.Atk = 15;
                    player.Def = 5;
                    player.Hp = 100;
                    player.MaxHp = 100;
                    Console.Clear();
                    Console.WriteLine("마법사를 선택했구만. 좋은 직업이야. 스파르타 마을로 이동하지.");
                    return true;

                default:
                    return false;
            }
        }
    }

    class Starter
    {
        public static Inventory inventory = new Inventory();

        public static void Startered(ref Player player)
        {
            string[] menuOptions =
            {
                "상태창",
                "인벤토리",
                "상점",
                "던전",
                "휴식"
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine("");

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                }

                Console.WriteLine("");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                string action = Console.ReadLine();

                if (int.TryParse(action, out int choice) && choice >= 1 && choice <= menuOptions.Length)
                {
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("상태창으로 이동합니다.");
                            Statue.Statueed(ref player);
                            break;

                        case 2:
                            Console.WriteLine("인벤토리창으로 이동합니다.");
                            inventory.Inventoryed(ref player);
                            break;

                        case 3:
                            Console.WriteLine("상점에 들어갑니다.");
                            Store.Storeed(ref player, ref inventory);
                            break;

                        case 4:
                            Console.WriteLine("던전에 들어갑니다.");
                            Dungeon.Dungeoned(ref player, ref inventory);
                            break;

                        case 5:
                            Console.WriteLine("여관에 방문합니다.");
                            Rest.Rested(ref player);
                            break;
                    }
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 정확하게 눌러주세요.");
                    Console.WriteLine("계속하려면 아무 키나 누르세요.");
                    Console.ReadKey(true);
                }
            }
        }
    }

    class Statue
    {
        public static void Statueed(ref Player player)
        {
            while (true)
            {
                DisplayPlayerStatus(player);
                Console.WriteLine("메인 메뉴로 돌아가려면 아무 키나 누르세요.");
                Console.ReadKey();
                Console.Clear();
                Starter.Startered(ref player);
            }
        }

        private static void DisplayPlayerStatus(Player player)
        {
            double weaponValue = player.EquippedWeapon != null ? player.EquippedWeapon.Value : 0;
            double armorValue = player.EquippedArmor != null ? player.EquippedArmor.Value : 0;
            Console.WriteLine($"LV: {player.Level}");
            Console.WriteLine($"이름: {player.Name}");
            Console.WriteLine($"직업: {player.Job}");
            Console.WriteLine($"경험치: {player.Exp} / {player.MaxExp}");
            Console.WriteLine($"공격력: {player.Atk} + ({weaponValue})");
            Console.WriteLine($"방어력: {player.Def} + ({armorValue})");
            Console.WriteLine($"체력: {player.Hp} / {player.MaxHp}");
            Console.WriteLine($"GOLD: {player.Gold} G");
            Console.WriteLine();
        }
    }

    class LevelSystem
    {
        public static void GainExperience(ref Player player)
        {
            if (player.Level < 5)
            {
                player.Exp += 100;
                Console.WriteLine($"경험치가 100 증가했습니다! 현재 경험치: {player.Exp} / {player.MaxExp}");
            }
            else
            {
                Console.WriteLine("이미 최고 레벨에 도달하였습니다. 경험치를 더 이상 획득할 수 없습니다.");
                return;
            }

            CheckForLevelUp(ref player);
        }

        public static void CheckForLevelUp(ref Player player)
        {
            while (player.Exp >= player.MaxExp)
            {
                player.Level++;
                player.MaxHp += 20;
                player.Hp = player.MaxHp;
                player.Atk += 0.5;
                player.Def += 1;
                player.Exp -= player.MaxExp;

                if (player.Level < 5)
                {
                    player.MaxExp += 100;
                    Console.WriteLine($"레벨 업! 현재 레벨: {player.Level}");
                    Console.WriteLine("능력치가 상승했습니다!");
                }
            }

            if (player.Level >= 5)
            {
                player.Exp = Math.Min(player.Exp, player.MaxExp);
                Console.WriteLine("축하합니다 최고 레벨에 도달하였습니다.");
            }
        }
    }

    public enum ItemType { 무기, 방어구 }

    class Item
    {
        public string Name { get; }
        public ItemType Type { get; }
        public double Cost { get; }
        public double Value { get; }
        public string Description { get; }

        public bool IsPurchased { get; set; }

        public Item(string name, ItemType type, int cost, int value, string description)
        {
            Name = name;
            Type = type;
            Cost = cost;
            Value = value;
            Description = description;
            IsPurchased = false;
        }
    }

    class Inventory
    {
        private List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            items.Add(item);
            item.IsPurchased = true;
        }

        public bool IsItemPurchased(string itemName)
        {
            foreach (var item in items)
            {
                if (item.Name == itemName)
                {
                    return item.IsPurchased;
                }
            }
            return false;
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }

        public void Inventoryed(ref Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리:");
                Console.WriteLine("");

                if (IsEmpty())
                {
                    Console.WriteLine("인벤토리가 비어있습니다.");
                }
                else
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {(player.EquippedWeapon == items[i] || player.EquippedArmor == items[i] ? "[E] " : "")}{items[i].Name} - {items[i].Type} | {items[i].Description} ");
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("메뉴:");
                Console.WriteLine("");
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("2. 나가기");
                Console.WriteLine("");
                Console.Write("원하는 행동을 선택하세요: ");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    ManageEquip(ref player);
                }

                else if (action == "2")
                {
                    Console.Clear();
                    Starter.Startered(ref player);
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                }
            }
        }

        private void EquipItem(ref Player player, Item item)
        {
            if (item.Type == ItemType.무기)
            {
                if (player.EquippedWeapon != null)
                {
                    player.Atk -= player.EquippedWeapon.Value;
                }

                if (player.EquippedWeapon == item)
                {
                    player.EquippedWeapon = null;
                    Console.WriteLine($"{item.Name}을(를) 벗었습니다.");
                    return;
                }

                player.EquippedWeapon = item;
                player.Atk += item.Value;
                Console.WriteLine($"{item.Name}을(를) 장착했습니다.");
            }
            else if (item.Type == ItemType.방어구)
            {
                if (player.EquippedArmor != null)
                {
                    player.Def -= (int)player.EquippedArmor.Value;
                }

                if (player.EquippedArmor == item)
                {
                    player.EquippedArmor = null;
                    Console.WriteLine($"{item.Name}을(를) 벗었습니다.");
                    return;
                }

                player.EquippedArmor = item;
                player.Def += (int)item.Value;
                Console.WriteLine($"{item.Name}을(를) 장착했습니다.");
            }
        }

        public void ManageEquip(ref Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("장착 관리:");
                Console.WriteLine("");
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {(player.EquippedWeapon == items[i] || player.EquippedArmor == items[i] ? "[E] " : "")}{items[i].Name} - {items[i].Type} | {items[i].Description}");
                }
                Console.WriteLine("");
                Console.WriteLine("장착할 아이템 번호를 선택하세요 (취소하려면 0 입력):");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        Console.WriteLine("장착 관리에서 나갑니다.");
                        return;
                    }

                    if (choice > 0 && choice <= items.Count)
                    {
                        Item selectedItem = items[choice - 1];
                        EquipItem(ref player, selectedItem);
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자만 입력 가능합니다.");
                }
                Console.WriteLine("장착 관리로 돌아가려면 아무 키나 누르세요.");
                Console.ReadKey();
            }
        }

        public void SellItem(ref Player player)
        {
            while (true)
            {
                if (items.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("인벤토리에 판매할 아이템이 없습니다.");
                    Console.WriteLine("상점으로 돌아갑니다.");
                    Console.ReadKey();
                    return;
                }

                Console.Clear();
                Console.WriteLine("판매할 아이템을 선택하세요.");
                Console.WriteLine();
                for (int i = 0; i < items.Count; i++)
                {
                    string equippedStatus = (player.EquippedWeapon == items[i] || player.EquippedArmor == items[i]) ? "[E] " : "";
                    Console.WriteLine($"{i + 1}. {equippedStatus}{items[i].Name} - {items[i].Type} (판매가: {items[i].Cost * 0.8} G)");
                }

                Console.WriteLine();
                Console.WriteLine("판매할 아이템 번호를 선택하세요 (취소하려면 0 입력):");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        Console.WriteLine("판매가 취소되었습니다.");
                        return;
                    }
                    else if (choice > 0 && choice <= items.Count)
                    {
                        Item itemToSell = items[choice - 1];

                        if (itemToSell.Type == ItemType.무기 && player.EquippedWeapon == itemToSell)
                        {
                            player.Atk -= player.EquippedWeapon.Value;
                            player.EquippedWeapon = null;
                        }
                        else if (itemToSell.Type == ItemType.방어구 && player.EquippedArmor == itemToSell)
                        {
                            player.Def -= (int)player.EquippedArmor.Value;
                            player.EquippedArmor = null;
                        }

                        int salePrice = (int)(itemToSell.Cost * 0.8);
                        player.Gold += salePrice;
                        items.Remove(itemToSell);
                        Console.WriteLine($"{itemToSell.Name}을(를) 판매하여 {salePrice} G를 얻었습니다.");

                        itemToSell.IsPurchased = false;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 선택입니다. 다시 선택해주세요.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자만 입력 가능합니다. 다시 선택해주세요.");
                }

                Console.WriteLine("판매 화면으로 돌아가려면 아무 키나 누르세요.");
                Console.ReadKey();
            }
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
    }

    class Store
    {
        public static void Storeed(ref Player player, ref Inventory inventory)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점에 오신 것을 환영합니다!");
                Console.WriteLine($"현재 골드: {player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("1. 구매하기");
                Console.WriteLine("2. 판매하기");
                Console.WriteLine("3. 나가기");
                Console.WriteLine();
                Console.Write("원하는 행동을 선택하세요: ");
                string action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        Console.Clear();
                        BuyItems(ref player, ref inventory);
                        break;

                    case "2":
                        Console.Clear();
                        inventory.SellItem(ref player);
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("상점을 나갑니다.");
                        Console.WriteLine("");
                        Starter.Startered(ref player);
                        return;

                    default:
                        Console.WriteLine("올바른 선택이 아닙니다. 다시 선택해주세요.");
                        break;
                }
            }
        }

        private static void BuyItems(ref Player player, ref Inventory inventory)
        {
            List<Item> Items = new List<Item>();

            if (player.Job == Job.전사)
            {
                Items.Add(new Item("낡은 검", ItemType.무기, 600, 2, "쉽게 볼 수 있는 낡은 검입니다. | 공격력 +2"));
                Items.Add(new Item("강철 검", ItemType.무기, 1000, 5, "강철로 만든 검입니다. | 공격력 +5"));
                Items.Add(new Item("청동 도끼", ItemType.무기, 1500, 10, "어디선가 사용됐던 도끼입니다. | 공격력 +10"));
                Items.Add(new Item("엑스칼리버", ItemType.무기, 4000, 20, "전설적인 영웅 아서가 썼다고 전해지는 검입니다. | 공격력 +20"));
                Items.Add(new Item("수련자 갑옷", ItemType.방어구, 1000, 5, "수련에 도움을 주는 갑옷입니다. | 방어력 +5"));
                Items.Add(new Item("무쇠 갑옷", ItemType.방어구, 2000, 9, "무쇠로 만들어져 튼튼한 갑옷입니다. | 방어력 +9"));
                Items.Add(new Item("스파르타 갑옷", ItemType.방어구, 3500, 15, "스파르타의 전사들이 사용한 전설의 갑옷입니다. | 방어력 +15"));
            }
            else if (player.Job == Job.궁수)
            {
                Items.Add(new Item("낡은 석궁", ItemType.무기, 600, 2, "쉽게 볼 수 있는 낡은 석궁입니다. | 공격력 +2"));
                Items.Add(new Item("강철 활", ItemType.무기, 1000, 5, "강철로 만든 활입니다. | 공격력 +5"));
                Items.Add(new Item("다이아몬드 활", ItemType.무기, 1500, 10, "강력한 다이아몬드 활입니다. | 공격력 +10"));
                Items.Add(new Item("아스트라", ItemType.무기, 4000, 20, "고대 엘프 족장 아스트라가 썼다고 전해지는 활입니다. | 공격력 +20"));
                Items.Add(new Item("수련자 가죽갑옷", ItemType.방어구, 1000, 5, "수련에 도움을 주는 가죽갑옷입니다. | 방어력 +5"));
                Items.Add(new Item("무쇠 가죽갑옷", ItemType.방어구, 2000, 9, "무쇠로 만들어져 튼튼한 가죽갑옷입니다. | 방어력 +9"));
                Items.Add(new Item("스파르타 가죽갑옷", ItemType.방어구, 3500, 15, "스파르타의 전사들이 사용한 전설의 가죽갑옷입니다. | 방어력 +15"));
            }
            else if (player.Job == Job.마법사)
            {
                Items.Add(new Item("낡은 지팡이", ItemType.무기, 600, 2, "쉽게 볼 수 있는 낡은 지팡이입니다. | 공격력 +2"));
                Items.Add(new Item("강철 지팡이", ItemType.무기, 1000, 5, "강철로 만든 지팡이입니다. | 공격력 +5"));
                Items.Add(new Item("마법 지팡이", ItemType.무기, 1500, 10, "어디선가 사용됐던 마법 지팡이입니다. | 공격력 +10"));
                Items.Add(new Item("아티쉬", ItemType.무기, 4000, 20, "9서클 대마법사 랑그란트가 사용한 지팡이입니다. | 공격력 +20"));
                Items.Add(new Item("수련자 로브", ItemType.방어구, 1000, 5, "수련에 도움을 주는 로브입니다. | 방어력 +5"));
                Items.Add(new Item("무쇠 로브", ItemType.방어구, 2000, 9, "무쇠로 만들어져 튼튼한 로브입니다. | 방어력 +9"));
                Items.Add(new Item("스파르타 로브", ItemType.방어구, 3500, 15, "스파르타의 전사들이 사용한 전설의 로브입니다. | 방어력 +15"));
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{player.Job} 아이템 목록:");
                Console.WriteLine($"현재 보유하고 있는 골드: {player.Gold} G");
                Console.WriteLine();

                Console.WriteLine("무기 목록:");
                for (int i = 0; i < Items.Count; i++)
                {
                    Item item = Items[i];
                    if (item.Type == ItemType.무기)
                    {
                        string purchasedText = inventory.IsItemPurchased(item.Name) ? " (구매 완료)" : "";
                        Console.WriteLine($"{i + 1}. {item.Name}{purchasedText} | {item.Description} | 가격: {item.Cost} G");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("방어구 목록:");
                for (int i = 0; i < Items.Count; i++)
                {
                    Item item = Items[i];
                    if (item.Type == ItemType.방어구)
                    {
                        string purchasedText = inventory.IsItemPurchased(item.Name) ? " (구매 완료)" : "";
                        Console.WriteLine($"{i + 1}. {item.Name}{purchasedText} | {item.Description} | 가격: {item.Cost} G");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("구매할 아이템 번호를 선택하세요. (나가시려면 '0'을 입력하세요.)");

                int choice = 0;

                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("숫자만 입력 가능합니다.");
                    Console.WriteLine("계속 진행하려면 아무 키나 누르세요.");
                    Console.ReadKey();
                    continue;
                }

                if (choice == 0)
                {
                    Console.WriteLine("상점을 나갑니다.");
                    break;
                }

                if (choice > 0 && choice <= Items.Count)
                {
                    Item selectedItem = Items[choice - 1];

                    if (inventory.IsItemPurchased(selectedItem.Name))
                    {
                        Console.WriteLine("이미 구매한 아이템입니다. 다른 아이템을 선택하세요.");
                    }
                    else if (player.Gold >= selectedItem.Cost)
                    {
                        player.Gold -= (int)selectedItem.Cost;
                        inventory.AddItem(selectedItem);
                        Console.WriteLine($"{selectedItem.Name}을(를) {selectedItem.Cost} G에 구매했습니다.");
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 선택입니다.");
                }

                Console.WriteLine("계속 구매를 원하시면 아무 키나 누르세요. 나가시려면 '0'을 입력하세요.");
                string input = Console.ReadLine();
                if (input.Equals("0", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("상점을 나갑니다.");
                    break;
                }
                Console.Clear();
            }
        }
    }

    class Dungeon
    {
        private enum Difficulty { 쉬운, 일반, 어려운 }

        private static Random random = new Random();

        public static void Dungeoned(ref Player player, ref Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("던전에 입장하였습니다.");
            Console.WriteLine();

            while (true)
            {
                int previousHp = player.Hp;
                int previousGold = player.Gold;

                Difficulty? difficulty = SelectDifficulty(ref player);
                if (difficulty == null) return;

                if (player.Hp <= 20)
                {
                    Console.WriteLine("체력이 너무 적습니다. 던전에 진입할 수 없습니다.");
                    Console.WriteLine("여관으로 이동하겠습니다.");
                    Console.ReadKey();
                    Rest.Rested(ref player);
                    return;
                }

                int recDefense = RecDefense(difficulty.Value);

                if (player.Def < recDefense)
                {
                    int failChance = random.Next(1, 101);
                    if (failChance <= 40)
                    {
                        player.Hp /= 2;
                        Console.WriteLine("방어력이 부족하여 던전 도전이 실패했습니다.");
                        Console.WriteLine($"체력이 절반으로 깎입니다. 현재 체력: {player.Hp} HP");
                        Console.WriteLine("보상을 얻지 못하고 마을로 돌아갑니다.");
                        Console.ReadKey();
                        Starter.Startered(ref player);
                        return;
                    }
                }

                int damageTaken = DamageTaken((int)player.Def, recDefense);
                player.Hp -= damageTaken;
                player.Hp = Math.Max(player.Hp, 0);

                if (player.Hp <= 0)
                {
                    Console.WriteLine("체력이 바닥났습니다. 마을로 돌아갑니다.");
                    Console.ReadKey();
                    Starter.Startered(ref player);
                    return;
                }

                int baseReward = BaseReward(difficulty.Value);
                int extraReward = ExtraReward(player.Atk, baseReward);
                int totalReward = baseReward + extraReward;

                player.Gold += totalReward;

                Console.WriteLine();
                Console.WriteLine($"축하드립니다!! {difficulty}던전을 클리어 하였습니다.");
                Console.WriteLine($"획득한 총 보상: {totalReward} G | 기본 보상: {baseReward} G + 추가 보상: {extraReward} G");
                Console.WriteLine($"골드 {previousGold} G =>> {player.Gold} G");
                Console.WriteLine($"체력 {previousHp} hp =>> {player.Hp} hp | 총 {previousHp - player.Hp} hp만큼 피해를 입었습니다.");
                Console.WriteLine();

                LevelSystem.GainExperience(ref player);
                Console.WriteLine($"현재 경험치: {player.Exp} / {player.MaxExp}");

                Console.WriteLine("계속 던전을 진행하시려면 아무키나 눌러주세요.");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        private static int RecDefense(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.쉬운:
                    return 5;
                case Difficulty.일반:
                    return 11;
                case Difficulty.어려운:
                    return 17;
                default:
                    return 0;
            }
        }

        private static int BaseReward(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.쉬운:
                    return 1000;
                case Difficulty.일반:
                    return 1700;
                case Difficulty.어려운:
                    return 2500;
                default:
                    return 0;
            }
        }

        private static int DamageTaken(int playerDefense, int recommendedDefense)
        {
            int baseDamage = random.Next(20, 36);
            int adjustedDamage = baseDamage + (recommendedDefense - playerDefense);
            return Math.Max(adjustedDamage, 0);
        }

        private static int ExtraReward(double playerAtk, int baseReward)
        {
            int minPercentage = (int)(playerAtk);
            int maxPercentage = (int)(playerAtk * 2);
            int percentageIncrease = random.Next(minPercentage, maxPercentage + 1);
            return (baseReward * percentageIncrease) / 100;
        }

        private static Difficulty? SelectDifficulty(ref Player player)
        {
            Console.WriteLine("던전 난이도를 선택하세요:");
            Console.WriteLine();
            Console.WriteLine("1. 쉬운 | 방어력 '5'이상 권장");
            Console.WriteLine("2. 일반 | 방어력 '11'이상 권장");
            Console.WriteLine("3. 어려운 | 방어력 '17'이상 권장");
            Console.WriteLine("4. 나가기");
            Console.WriteLine();

            Console.Write("겁쟁이라 나가기를 선택하진 않겠지?: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1": return Difficulty.쉬운;
                case "2": return Difficulty.일반;
                case "3": return Difficulty.어려운;
                case "4":
                    Console.Clear();
                    Starter.Startered(ref player);
                    return null;
                default:
                    Console.WriteLine("올바른 선택이 아닙니다. 다시 선택해주세요.");
                    return SelectDifficulty(ref player);
            }
        }
    }

    class Rest
    {
        private const int RestCost = 500;

        private static bool firstRestFree = true;

        public static void Rested(ref Player player)
        {
            Console.Clear();
            Console.WriteLine("현재 골드: " + player.Gold + "G");
            Console.WriteLine("현재 체력: " + player.Hp + "/" + player.MaxHp);
            Console.WriteLine("");

            if (firstRestFree)
            {
                player.Hp = player.MaxHp;
                firstRestFree = false;
                Console.WriteLine("첫 번째 휴식은 무료입니다. 다음부터는 이용요금 500G 입니다.");
            }
            else if (player.Gold >= RestCost)
            {
                player.Gold -= RestCost;
                player.Hp = player.MaxHp;
                Console.WriteLine("휴식을 완료했습니다. 체력이 100으로 회복되었습니다.");
            }
            else
            {
                Console.WriteLine("보유하신 골드가 부족합니다. 휴식을 진행할 수 없습니다.");
                Console.WriteLine("마을로 돌아갑니다. 아무 키나 누르세요.");
                Console.ReadKey();
                Console.Clear();
                Starter.Startered(ref player);
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("휴식 후 돌아갑니다. 아무 키나 누르세요.");
            Console.ReadKey();
            Console.Clear();
            Starter.Startered(ref player);
        }
    }
}