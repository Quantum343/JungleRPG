// Program.cs
using System;
using System.Collections.Generic;
using System.Threading;
using JungleSurvivalRPG;  // for Item, ItemCatalog 

namespace JungleSurvivalRPG
{
    public class Armor
    {
        public int Defence { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Luck { get; set; }
        public Armor(int defence, int strength, int speed, int luck)
        {
            Defence = defence;
            Strength = strength;
            Speed = speed;
            Luck = luck;
        }
    }

    public class Weapon
    {
        public int HeavyManaAttack { get; set; }
        public string HeavyManaAttackName { get; set; }
        public int LightManaAttack { get; set; }
        public string LightManaAttackName { get; set; }
        public int Speed { get; set; }
        public int Strength { get; set; }
        public string Description { get; set; }
        public int RegularAttack { get; set; } // New property

        public Weapon(
            int heavyManaAttack,
            string heavyManaAttackName,
            int lightManaAttack,
            string lightManaAttackName,
            int speed,
            int strength,
            string description,
            int regularAttack
        )
        {
            HeavyManaAttack = heavyManaAttack;
            HeavyManaAttackName = heavyManaAttackName;
            LightManaAttack = lightManaAttack;
            LightManaAttackName = lightManaAttackName;
            Speed = speed;
            Strength = strength;
            Description = description;
            RegularAttack = regularAttack;
        }
    }

    public static class Equipment
    {
        public static Armor NoArmor = new Armor(0, 0, 0, 0); // No armor equipped
        public static Weapon NoWeapon = new Weapon(0, "", 0, "", 0, 0, "No weapon equipped", 0); // No weapon equipped

        public static Armor BarkhideVest = new Armor(8, 2, -1, 1);            // Balanced LVL5
        public static Armor WornScoutJacket = new Armor(6, 1, 0, 2);          // Nimble, higher luck/speed LVL20
        public static Armor IronScaleMail = new Armor(20, 5, -5, 2);          // Heavy defense
        public static Armor RogueTunic = new Armor(14, 3, -1, 6);             // Agile, higher luck/speed
        public static Armor PhantomCoat = new Armor(35, 15, 5, 4);            // Stealth-focused, decent all-around LVL35
        public static Armor BattleForgedPlates = new Armor(40, 9, 2, 2);      // Tankier variant
        public static Armor DrakeskinPlate = new Armor(55, 15, 0, 6);         // Sturdy, magical resistance implied LVL60
        public static Armor ValkyrieShroud = new Armor(48, 11, 7, 10);        // Luck and speed boosted
        public static Armor RadiantPlate = new Armor(70, 18, 0, 10);          // Light-infused, aura-based defense LV90
        public static Armor ShadowRaiment = new Armor(60, 22, 14, 8);         // Shadow-enhanced, offensive agility

        public static Weapon RustyDagger = new Weapon(0, "", 0, "", 1, 3, "Rusty dagger", 5);
    }

    public class Enemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int AttackPower { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }

        public Enemy(string name, int hp, int attackPower, int defence = 0, int speed = 5)
        {
            Name = name;
            HP = hp;
            AttackPower = attackPower;
            Defence = defence;
            Speed = speed;
        }
    }

    public class BossEnemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int AttackPower { get; set; }
        public string spAttackName { get; set; }
        public int specialmove { get; set; }
        public int Defence { get; set; }

        public BossEnemy(string name, int hp, int attackPower, string spAttackName, int specialmove, int defence)
        {
            Name = name;
            HP = hp;
            AttackPower = attackPower;
            this.spAttackName = spAttackName;
            this.specialmove = specialmove;
            Defence = defence;
        }
    }

    public struct SceneID
    {
        public int Act, Plot;
        public SceneID(int act, int plot) { Act = act; Plot = plot; }
        public override string ToString() => $"{Act}.{Plot}";
    }

    public class Scene
    {
        public string Text { get; set; }
        public Dictionary<int, SceneID> Choices { get; } = new();
        public Action<Player> OnEnter { get; }

        public Scene(string text, Action<Player>? onEnter = null)
        {
            Text = text;
            OnEnter = onEnter ?? (_ => { });
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public float HP { get; set; }
        public float Defence { get; set; }
        public int Luck { get; set; }
        public float Mana { get; set; }
        public int Speed { get; set; }
        public int Strength { get; set; }

        public Weapon EquippedWeapon { get; set; } = Equipment.NoWeapon; // Default no weapon equipped
        public Armor EquippedArmor { get; set; } = Equipment.NoArmor;    // Default no armor equipped

        public List<Item> Inventory { get; } = new();
        public bool HasFoundPage2 { get; set; } = false;

        // For Grimwore usage
        public int Experience { get; set; } = 0;
        public List<string> UnlockedSpells { get; } = new();

        // To store list of known/previously found items
        private List<Item> knownItems = new List<Item>();

        public Player(string name)
        {
            Name = name;
            HP = 100f;
            Defence = 5f;
            Luck = 1;
            Mana = 20f;
            Speed = 4;
            Strength = 5;
            Inventory.Add(ItemCatalog.EmptyWaterFlask);
        }

        public void ApplyPoison(int totalDamage, int turns)
        {
            HP = Math.Max(0f, HP - totalDamage);
            Console.WriteLine($"{Name} is poisoned and takes {totalDamage} damage over {turns} turns. HP: {HP}, Strength +5 \n");
            Strength += 5;
        }

        public int Attack() => EquippedWeapon.RegularAttack;

        public void UnlockSpell(string spell)
        {
            if (!UnlockedSpells.Contains(spell))
                UnlockedSpells.Add(spell);
        }

        public void TakeDamage(float dmg)
        {
            float instanceDef = Defence + EquippedArmor.Defence;
            int net = (int)Math.Max(0f, dmg - instanceDef);
            HP -= net;
            Console.WriteLine($"{Name} takes {net} damage. HP: {HP}\n");
        }

        public void OpenInventory()
        {
            if (Inventory.Count == 0)
            {
                Console.WriteLine("Inventory is empty.");
                return;
            }

            int idx = 0;
            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.WriteLine("-- Inventory --");
                for (int i = 0; i < Inventory.Count; i++)
                {
                    Console.Write(i == idx ? "> " : "  ");
                    Console.WriteLine(Inventory[i].Name);
                }
                Console.WriteLine("\n" + Inventory[idx].Description);
                Console.WriteLine("Use ↑/↓ to navigate, Enter to use, Backspace to exit.");

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                    idx = (idx - 1 + Inventory.Count) % Inventory.Count;
                else if (key == ConsoleKey.DownArrow)
                    idx = (idx + 1) % Inventory.Count;
                else if (key == ConsoleKey.Backspace)
                    return; // Exit inventory without using an item
            }
            while (key != ConsoleKey.Enter);

            Console.Clear();
            var chosen = Inventory[idx];
            chosen.Use(this);
            Inventory.RemoveAt(idx);
        }
    }

    public static class Printer
    {
        public static void PrintSlow(string text, int delay = 5)
        {
            foreach (var c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }

        public static void ShowProgressBar()
        {
            for (int i = 0; i <= 100; i += 10)
            {
                Console.Write($"[{new string('#', i / 10)}{new string(' ', 10 - i / 10)}] {i}%\r");
                Thread.Sleep(100);
            }
            Console.WriteLine();
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }
    }

    public class GameEngine
    {
        private Player player = null!;
        private Dictionary<SceneID, Scene> scenes = null!;
        private List<Enemy> enemies = new();
        private SceneID current;
        private SceneID lastScene;

        public void Start()
        {
            Console.WriteLine("Welcome to Jungle Survival RPG!\n");
            Console.Write("Enter your name: ");
            var name = Console.ReadLine() ?? "Player";
            player = new Player(name);

            InitializeEnemies();
            BuildScenes();

            current = new SceneID(1, 1);
            Run();
        }

        private void InitializeEnemies()
        {
            enemies.Add(new Enemy("Panther", 50, 10, 3, 8));
            enemies.Add(new Enemy("Snake", 30, 5, 2, 10));
        }

        private void BuildScenes()
        {
            scenes = new Dictionary<SceneID, Scene>();

            // ── GAME-OVER ────────────────────────────────────────────────────────────
            scenes[new SceneID(-1, 0)] = new Scene(
                "You have died a very deathly death.\n" +
                "**Game Over.**\n" +
                "1) Restart from the beginning.\n"
            );
            scenes[new SceneID(-1, 0)].Choices[1] = new SceneID(1, 1);

            // ── INTRO / START ────────────────────────────────────────────────────────
            scenes[new SceneID(1, 1)] = new Scene(
                "You awaken on a sandy riverbank beside a silent hand-held radio.\n" +
                "1) Head into the forest.\n" +
                "2) Follow the river.\n" +
                "3) Climb the outcrop.\n" +
                "4) Inspect the radio.\n"
            );
            scenes[new SceneID(1, 1)].Choices[1] = new SceneID(1, 2);
            scenes[new SceneID(1, 1)].Choices[2] = new SceneID(1, 3);
            scenes[new SceneID(1, 1)].Choices[3] = new SceneID(1, 4);
            scenes[new SceneID(1, 1)].Choices[4] = new SceneID(1, 11);   // radio path

            // ── FOREST ───────────────────────────────────────────────────────────────
            scenes[new SceneID(1, 2)] = new Scene(
                "Thick vines surround you...\n" +
                "1) Track the growl.\n" +
                "2) Follow the river sounds.\n" +
                "3) Open Inventory.\n" +
                "0) Go back.\n"
            );
            scenes[new SceneID(1, 2)].Choices[1] = new SceneID(1, 6);
            scenes[new SceneID(1, 2)].Choices[2] = new SceneID(1, 3);
            scenes[new SceneID(1, 2)].Choices[3] = new SceneID(0, 0);

            // ── RIVER BANK ───────────────────────────────────────────────────────────
            scenes[new SceneID(1, 3)] = new Scene(
                "Following the river...\n" +
                "1) Fill your empty water flask.\n" +
                "2) Look for fish.\n" +
                "3) Open Inventory.\n" +
                "0) Go back.\n"
            );
            scenes[new SceneID(1, 3)].Choices[1] = new SceneID(1, 5);
            scenes[new SceneID(1, 3)].Choices[2] = new SceneID(1, 8);
            scenes[new SceneID(1, 3)].Choices[3] = new SceneID(0, 0);

            // ── OUTCROP ──────────────────────────────────────────────────────────────
            scenes[new SceneID(1, 4)] = new Scene(
                "Atop the outcrop...\n" +
                "1) Investigate campsite.\n" +
                "2) Open Inventory.\n" +
                "0) Go back.\n"
            );
            scenes[new SceneID(1, 4)].Choices[1] = new SceneID(2, 1);
            scenes[new SceneID(1, 4)].Choices[2] = new SceneID(0, 0);

            // ── FILL FLASK ───────────────────────────────────────────────────────────
            scenes[new SceneID(1, 5)] = new Scene(
                "You kneel by the river and pour water into your flask...\n" +
                "1) Continue.\n" +
                "0) Go back.\n",
                player =>
                {
                    if (player.Inventory.Remove(ItemCatalog.EmptyWaterFlask))
                    {
                        player.Inventory.Add(ItemCatalog.WaterFlask);
                        // Add Grimwore (not “Grimoire”)
                        if (!player.Inventory.Contains(ItemCatalog.Grimwore))
                        {
                            player.Inventory.Add(ItemCatalog.Grimwore);
                        }
                        Console.WriteLine("You filled your flask! It’s now a full Water Flask.\n");
                        Console.WriteLine("You have found a book with leaking mana. It’s a Grimwore.");
                    }
                    else
                    {
                        Console.WriteLine("You have no empty flask to fill.\n");
                    }
                }
            );
            scenes[new SceneID(1, 5)].Choices[1] = new SceneID(1, 3);

            // ── PANTHER CLEARING ─────────────────────────────────────────────────────
            scenes[new SceneID(1, 6)] = new Scene(
                "You track the growl to a clearing...\n" +
                "1) Find the noise.\n" +
                "2) Flee back to the forest.\n" +
                "3) Open Inventory.\n" +
                "4) Climb a tree to escape.\n" +
                "0) Go back.\n"
            );
            scenes[new SceneID(1, 6)].Choices[1] = new SceneID(-1, 0); // combat
            scenes[new SceneID(1, 6)].Choices[2] = new SceneID(1, 2);
            scenes[new SceneID(1, 6)].Choices[3] = new SceneID(0, 0);
            scenes[new SceneID(1, 6)].Choices[4] = new SceneID(1, 7);

            // ── TREEHOUSE ────────────────────────────────────────────────────────────
            scenes[new SceneID(1, 7)] = new Scene(
                "You climb a tree to escape the panther...\n" +
                "You find a tree house with a rope ladder leading down.\n" +
                "1) Climb down the rope ladder.\n" +
                "2) Explore the tree house.\n" +
                "3) Open Inventory.\n" +
                "0) Go back.\n"
            );
            scenes[new SceneID(1, 7)].Choices[1] = new SceneID(2, 1);
            scenes[new SceneID(1, 7)].Choices[2] = new SceneID(2, 2);
            scenes[new SceneID(1, 7)].Choices[3] = new SceneID(0, 0);

            // ── FISH SPOTTING ────────────────────────────────────────────────────────
            scenes[new SceneID(1, 8)] = new Scene(
                "You spot some fish swimming in the river...\n" +
                "1) Try to catch a fish.\n" +
                "2) Leave the riverbank.\n" +
                "3) Open Inventory.\n" +
                "0) Go back.\n"
            );
            scenes[new SceneID(1, 8)].Choices[1] = new SceneID(1, 9);
            scenes[new SceneID(1, 8)].Choices[2] = new SceneID(1, 2);
            scenes[new SceneID(1, 8)].Choices[3] = new SceneID(0, 0);

            // ── CATCH FISH (treasure map) ────────────────────────────────────────────
            scenes[new SceneID(1, 9)] = new Scene(
                "You try to catch a fish...\n" +
                "1) Continue.\n" +
                "0) Go back.\n",
                player =>
                {
                    if (player.Inventory.Contains(ItemCatalog.TreasureMap))
                    {
                        Random rng = new Random();
                        int chance = rng.Next(1, 101);
                        if (chance <= 50) // 50% chance to find nothing
                        {
                            Console.WriteLine("You found nothing but mud and weeds. You tired yourself mentally -10 Mana \n");
                            player.Mana = Math.Max(0f, player.Mana - 10);
                        }
                        else
                        {
                            Console.WriteLine($"You caught a fish! It’s a small one, but it will do.\n" +
                                "You can use it to restore some health or as a crafting ingredient.\n");
                            player.Inventory.Add(ItemCatalog.Fish);
                        }
                        Console.WriteLine("You see no fish");
                    }
                    else
                    {
                        Console.WriteLine("You found a treasure map!\n");
                        player.Inventory.Add(ItemCatalog.TreasureMap);
                    }
                }
            );
            scenes[new SceneID(1, 9)].Choices[1] = new SceneID(1, 8);

            // ── RADIO – EXAMINE (scene 1.11) ─────────────────────────────────────────
            scenes[new SceneID(1, 11)] = new Scene(
                "The radio screen is dark.\n" +
                "1) Try to power it on.\n" 
        );
            scenes[new SceneID(1, 11)].Choices[1] = new SceneID(1, 12);

            // ── RADIO – USE ITEM (scene 1.12) ────────────────────────────────────────
            scenes[new SceneID(1, 12)] = new Scene(
                "You try to power on the radio...\n" +
                string.Empty,
                p => ItemCatalog.DeadRadio.Use(p)     // attempt power-on
            );
            scenes[new SceneID(1, 12)].Choices[1] = new SceneID(1, 1);    // return to start

            // ── CLEARING WITH FLOATING BOOK ──────────────────────────────────────────
            scenes[new SceneID(2, 1)] = new Scene(
                "You find a clearing with a floating book...\n" +
                "1) Read the book.\n" +
                "2) Leave the clearing.\n" +
                "3) Open Inventory.\n" +
                "0) Go back.\n"
            );
            scenes[new SceneID(2, 1)].Choices[1] = new SceneID(10, 1);
            scenes[new SceneID(2, 1)].Choices[2] = new SceneID(1, 7);
            scenes[new SceneID(2, 1)].Choices[3] = new SceneID(0, 0);

            // ── JOURNAL GUIDE & LOOTBOX ──────────────────────────────────────────────
            scenes[new SceneID(10, 1)] = new Scene(
                "You open the book and find a journal guide and a LootBox...\n",
                player =>
                {
                    Console.WriteLine(
                        "Journal Guide Page 1:\n" +
                        "1. Always keep your water flask filled.\n" +
                        "2. Use your inventory wisely.\n" +
                        "3. Enemies can be tough, so prepare before combat.\n" +
                        "4. Explore thoroughly to find useful items.\n" +
                        "5. Crafting can help you survive longer.\n" +
                        "The next pages seem to be torn out, but you find a treasure map leading to a hidden location.\n"
                    );
                    player.Inventory.Add(ItemCatalog.TreasureMap);
                    player.Inventory.Add(ItemCatalog.LootBox);
                }
            );

            // Crafting table found at the treehouse
            scenes[new SceneID(10, 1)].Text +=
                "You find a Crafting Table here.....\n" +
                "1) Use the Crafting Table.\n" +
                "2) Return to the clearing.\n" +
                "3) Open Inventory.\n";
            scenes[new SceneID(10, 1)].Choices[0] = new SceneID(10, 2); // Crafting table scene
            scenes[new SceneID(10, 1)].Choices[1] = new SceneID(2, 1); // Return to clearing
        }

        private void Run()
        {
            while (true)
            {
                // — Combat trigger (Act -1) —
                if (current.Act == -1)
                {
                    StartCombat(enemies[0]);
                    current = new SceneID(1, 2);
                    continue;
                }

                // — Inventory trigger (Act 0, Plot 0) —
                if (current.Act == 0 && current.Plot == 0)
                {
                    player.OpenInventory();
                    current = lastScene;
                    continue;
                }

                // Enter scene
                Scene scene = scenes[current];
                scene.OnEnter(player);
                Console.WriteLine(scene.Text);

                // No choices → end
                if (scene.Choices.Count == 0)
                    break;

                Console.Write("Choose: ");
                string? userInput = Console.ReadLine();

                if (userInput == "0")
                {
                    current = lastScene;
                    continue;
                }

                if (int.TryParse(userInput, out int choice) && scene.Choices.ContainsKey(choice))
                {
                    lastScene = current;
                    current = scene.Choices[choice];
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    Thread.Sleep(1000);
                }

                // Add obtained items to list of known items
                ItemCatalog.KnownItems.AddRange(player.Inventory);
            }
        }

        private void StartCombat(Enemy enemy)
        {
            Console.WriteLine($"A wild {enemy.Name} appears!\n");

            int instanceSpeed = player.Speed + player.EquippedWeapon.Speed + player.EquippedArmor.Speed;
            bool playerGoesFirst = instanceSpeed >= enemy.Speed;

            while (player.HP > 0 && enemy.HP > 0)
            {
                if (playerGoesFirst)
                {
                    PlayerAction(enemy);
                    if (enemy.HP <= 0) break;
                    EnemyAction(enemy);
                }
                else
                {
                    EnemyAction(enemy);
                    if (player.HP <= 0) break;
                    PlayerAction(enemy);
                }
            }

            if (player.HP <= 0)
            {
                Console.WriteLine("You have been defeated...\n");
                Environment.Exit(0);
            }

            Console.WriteLine($"You defeated the {enemy.Name}!\n");
        }

        private void PlayerAction(Enemy enemy)
        {
            Console.WriteLine("Choose your action:");
            Console.WriteLine("1) Normal Attack");

            bool hasManaWeapon =
                player.EquippedWeapon.HeavyManaAttack > 0 ||
                player.EquippedWeapon.LightManaAttack > 0;

            if (hasManaWeapon)
            {
                Console.WriteLine("2) Light Mana Attack");
                Console.WriteLine("3) Heavy Mana Attack");
            }
            Console.WriteLine("4) Flee");

            Console.Write("Action: ");
            var cmd = Console.ReadLine() ?? "";

            int totalDamage = 0;
            var weapon = player.EquippedWeapon;
            float instanceSTR = weapon.Strength + player.Strength + player.EquippedArmor.Strength;

            switch (cmd)
            {
                case "1":
                    totalDamage = (int)(weapon.RegularAttack * (1 + (instanceSTR / 100.0f)));
                    Console.WriteLine($"You strike {enemy.Name} with your {weapon.Description} for {totalDamage} damage!");
                    totalDamage -= enemy.Defence;
                    enemy.HP -= Math.Max(0, totalDamage);
                    break;

                case "2" when hasManaWeapon:
                    totalDamage = (int)(weapon.LightManaAttack * (1 + (instanceSTR / 100.0f)));
                    Console.WriteLine($"You cast {weapon.LightManaAttackName} on {enemy.Name} for {totalDamage} damage!");
                    totalDamage -= enemy.Defence;
                    enemy.HP -= Math.Max(0, totalDamage);
                    break;

                case "3" when hasManaWeapon:
                    totalDamage = (int)(weapon.HeavyManaAttack * (1 + (instanceSTR / 100.0f)));
                    Console.WriteLine($"You unleash {weapon.HeavyManaAttackName} on {enemy.Name} for {totalDamage} damage!");
                    totalDamage -= enemy.Defence;
                    enemy.HP -= Math.Max(0, totalDamage);
                    break;

                case "4":
                    Console.WriteLine("You fled back to the previous scene.\n");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. You fumble your turn.");
                    break;
            }
        }

        private void EnemyAction(Enemy enemy)
        {
            float incomingDamage = Math.Max(0, enemy.AttackPower - (int)player.Defence);

            Console.WriteLine($"{enemy.Name} is attacking you!");
            Console.WriteLine("Do you want to try to dodge? (y/n): ");
            string dodge = Console.ReadLine()?.ToLower() ?? "n";

            if (dodge == "y")
            {
                Random rng = new Random();
                int chance = rng.Next(1, 101);

                if (chance <= player.Luck)
                {
                    Console.WriteLine("You dodged the attack!");
                    return;
                }
                Console.WriteLine("Dodge failed!");
                incomingDamage = incomingDamage * 1.5f;
            }

            player.TakeDamage(incomingDamage);
            Console.WriteLine($"{enemy.Name} hits you for {incomingDamage} damage.\n");
        }
    }

    class Program
    {
        static void Main()
        {
            new GameEngine().Start();
        }
    }
}
