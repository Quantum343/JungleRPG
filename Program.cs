// Program.cs
using System;
using System.Collections.Generic;
using System.Threading;
using JungleSurvivalRPG;  // for Item, ItemCatalog 
using NAudio.Wave;

namespace JungleSurvivalRPG
{
    public static class AudioPlayer
{
    private static IWavePlayer? waveOut;
    private static AudioFileReader? audioFile;

    public static void PlayMusic(string path)
    {
        waveOut = new WaveOutEvent();
        audioFile = new AudioFileReader(path);
        waveOut.Init(audioFile);
        waveOut.Play();
    }

    public static void StopMusic()
    {
        waveOut?.Stop();
        waveOut?.Dispose();
        audioFile?.Dispose();
    }
}


    public class Armor
    {
        public int Defence { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Luck { get; set; }
        public string Description{ get; set; }
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
        public int RegularAttack { get; set; } // New property
        public string Description{ get; set; }

        public Weapon(
            int heavyManaAttack,
            string heavyManaAttackName,
            int lightManaAttack,
            string lightManaAttackName,
            int speed,
            int strength,
            string Description,
            int regularAttack
        )
        {
            HeavyManaAttack = heavyManaAttack;
            HeavyManaAttackName = heavyManaAttackName;
            LightManaAttack = lightManaAttack;
            LightManaAttackName = lightManaAttackName;
            Speed = speed;
            Strength = strength;
            this.Description = Description;
            RegularAttack = regularAttack;
        }
    }

    public static class Equipment
    {
        public static Armor NoArmor = new Armor(0, 0, 0, 0); // No armor equipped
        public static Weapon NoWeapon = new Weapon(0, "", 0, "", 0, 0, "No weapon equipped", 0); // No weapon equipped

        public static Armor BarkhideVest = new Armor(8, 2, 1, 1);            // Balanced LVL5
        public static Armor WornScoutJacket = new Armor(6, 1, 0, 2);          // Nimble, higher luck/speed LVL20
        public static Armor IronScaleMail = new Armor(20, 5, 4, 2);          // Heavy defense
        public static Armor RogueTunic = new Armor(14, 3, 7, 6);             // Agile, higher luck/speed
        public static Armor PhantomCoat = new Armor(35, 15, 5, 4);            // Stealth-focused, decent all-around LVL35
        public static Armor BattleForgedPlates = new Armor(40, 9, 7, 2);      // Tankier variant
        public static Armor DrakeskinPlate = new Armor(55, 15, 5, 6);         // Sturdy, magical resistance implied LVL60
        public static Armor ValkyrieShroud = new Armor(48, 11, 12, 10);        // Luck and speed boosted
        public static Armor RadiantPlate = new Armor(70, 18,7, 10);          // Light-infused, aura-based defense LV90
        public static Armor ShadowRaiment = new Armor(60, 22, 14, 8);         // Shadow-enhanced, offensive agility

        

        public static Weapon RustyDagger = new Weapon(0, "", 0, "", 1, 3, "Rusty dagger", 5);
        public static Weapon EmberFang = new Weapon(65, "Flame Bite", 25, "Spark Snap", -1, 4, "A basic iron sword enchanted with weak fire magic.", 15);
        public static Weapon StormSplitter = new Weapon(85, "Thunder Slash", 45, "Static Arc", -5, 10, "A steel longsword that crackles with electrical energy.", 20);
        public static Weapon NightReaver = new Weapon(120, "Void Rend", 65, "Shadow Slice", -2, 16, "Forged in darkness, this blade weakens enemies' vision.", 25);
        public static Weapon DragonspineExecutioner = new Weapon(150, "Infernal Decapitation", 85, "Ember Swipe", -7, 28, "A massive greatsword made from dragon bones. Delivers heavy fire-based damage.", 35);
        public static Weapon MirrorEdge = new Weapon(160, "Radiant Slash", 100, "Glint Pierce", -12, 22, "A crystal blade that reflects some light-based magic back at the attacker.", 30);
        public static Weapon Bloodthirster = new Weapon(160, "Life Leech", 95, "Crimson Swipe", -3, 30, "A cursed weapon that steals a bit of the target's vitality with each strike.", 33);
        public static Weapon ChronoFang = new Weapon(175, "Time Rend", 80, "Clock Pierce", -3, 24, "An arcane blade said to distort time. Occasionally delays an enemy’s next move.", 28);
        public static Weapon VoidwalkerBlade = new Weapon(1000, "Hakai", 0, "Slipstream Cut", 15, 15, "A blade infused with void energy. Allows the wielder to strike from a short distance instantly.", 40);
    }

    public class Enemy
    {
        public string Name { get; set; }
        public float HP { get; set; }
        public int AttackPower { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }

        public Enemy(string name, float hp, int attackPower, int defence = 0, int speed = 5)
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
        public float HP { get; set; }
        public int AttackPower { get; set; }
        public string spAttackName { get; set; }
        public int specialmove { get; set; }
        public int Defence { get; set; }
        public int Speed{ get; set; }

        public BossEnemy(string name, float hp, int attackPower, string spAttackName, int specialmove, int defence, int Speed)
        {
            Name = name;
            HP = hp;
            AttackPower = attackPower;
            this.spAttackName = spAttackName;
            this.specialmove = specialmove;
            Defence = defence;
            this.Speed = Speed;
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
        public float MaxHP => 100f; // Default max HP
        private float _maxMana = 60f; // Backing field for MaxMana
        public float MaxMana
        {
            get => _maxMana;
            private set => _maxMana = value;
        }
        public int Experience { get; private set; } = 0;
        public List<string> UnlockedSpells { get; } = new List<string>();
        public void GainExperience(int amount)
        {
            Experience += amount;
            UnlockAvailableSpells();
        }
        public Weapon EquippedWeapon { get; set; } = Equipment.NoWeapon; // Default no weapon equipped
        public Armor EquippedArmor { get; set; } = Equipment.NoArmor;    // Default no armor equipped

        public List<Item> Inventory { get; } = new();
        public bool HasFoundPage2 { get; set; } = false;

        public void IncreaseMaxMana(float amount)
        {
            if (amount > 0)
            {
                MaxMana += amount;
                Console.WriteLine($"Max Mana increased by {amount}. New Max Mana: {MaxMana}");
            }
            else
            {
                Console.WriteLine("Invalid amount. Max Mana increase must be positive.");
            }
        }

        public void DecreaseMaxMana(float amount)
        {
            if (amount > 0 && MaxMana - amount >= 0)
            {
                MaxMana -= amount;
                Console.WriteLine($"Max Mana decreased by {amount}. New Max Mana: {MaxMana}");
            }
            else
            {
                Console.WriteLine("Invalid amount. Max Mana decrease must be positive and not reduce below zero.");
            }
        }


        // To store list of known/previously found items
        private List<Item> knownItems = new List<Item>();
            private void UnlockAvailableSpells()
        {
            foreach (var spell in SpellCatalog.AllSpells)
            {
                if (Experience >= spell.RequiredXP 
                && !UnlockedSpells.Contains(spell.Name))
                {
                    UnlockedSpells.Add(spell.Name);
                    Console.WriteLine($"\n*** New spell learned: {spell.Name}! ***\n");
                }
            }
        }

        public Player(string name)
        {
            Name = name;
            HP = MaxHP;
            Defence = 5f;
            Luck = 1;
            Mana = MaxMana;
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

            // Group items by name
            var grouped = Inventory
                .GroupBy(item => item.Name)
                .Select(g => new { Name = g.Key, Count = g.Count(), Sample = g.First() })
                .ToList();

            int idx = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("-- Inventory --");
                for (int i = 0; i < grouped.Count; i++)
                {
                    string prefix = (i == idx) ? "> " : "  ";
                    Console.WriteLine($"{prefix}{grouped[i].Name} x{grouped[i].Count}");
                }

                Console.WriteLine("\n" + grouped[idx].Sample.Description);
                Console.WriteLine("Use ↑/↓ to navigate, Enter to use, Backspace to exit.");

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                    idx = (idx - 1 + grouped.Count) % grouped.Count;
                else if (key == ConsoleKey.DownArrow)
                    idx = (idx + 1) % grouped.Count;
                else if (key == ConsoleKey.Backspace)
                    return;
            }
            while (key != ConsoleKey.Enter);

            var chosenName = grouped[idx].Name;
            var chosenItem = Inventory.First(item => item.Name == chosenName);

            Console.Clear();
            chosenItem.Use(this);
            Inventory.Remove(chosenItem);
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
        private List<BossEnemy> boss = new();
        private SceneID current;
        private SceneID lastScene;

        public static void DisplayArmors(Armor armor1, Armor armor2, string armorName1, string armorName2)
        {
            Console.WriteLine("┌─────────────────────────────┬─────────────────────────────┐");
            Console.WriteLine($"│ {armorName1,-27} │ {armorName2,-27} │");
            Console.WriteLine("├─────────────────────────────┼─────────────────────────────┤");
            Console.WriteLine($"│ Defence: {armor1.Defence,-17} │ Defence: {armor2.Defence,-17} │");
            Console.WriteLine($"│ Strength: {armor1.Strength,-16} │ Strength: {armor2.Strength,-16} │");
            Console.WriteLine($"│ Speed: {armor1.Speed,-18} │ Speed: {armor2.Speed,-18} │");
            Console.WriteLine($"│ Luck: {armor1.Luck,-19} │ Luck: {armor2.Luck,-19} │");
            Console.WriteLine("└─────────────────────────────┴─────────────────────────────┘");
         }

        public static void DisplayWeapons(Weapon w1, Weapon w2, string name1, string name2)
        {
            Console.WriteLine("┌────────────────────────────────────────────────────────────┐");
            Console.WriteLine($"│ {name1,-28} │ {name2,-28} │");
            Console.WriteLine("├────────────────────────────────────────────────────────────┤");
            Console.WriteLine($"│ Normal Attack: {w1.RegularAttack,-10} │ Normal Attack: {w2.RegularAttack,-10} │");
            Console.WriteLine($"│ Light Mana: {w1.LightManaAttackName} ({w1.LightManaAttack})   │ Light Mana: {w2.LightManaAttackName} ({w2.LightManaAttack})   │");
            Console.WriteLine($"│ Heavy Mana: {w1.HeavyManaAttackName} ({w1.HeavyManaAttack})   │ Heavy Mana: {w2.HeavyManaAttackName} ({w2.HeavyManaAttack})   │");
            Console.WriteLine($"│ Speed:         {w1.Speed,-10} │ Speed:         {w2.Speed,-10} │");
            Console.WriteLine($"│ Strength:      {w1.Strength,-10} │ Strength:      {w2.Strength,-10} │");
            Console.WriteLine($"│ {w1.Description,-28} │ {w2.Description,-28} │");
            Console.WriteLine("└────────────────────────────────────────────────────────────┘");
        }




        public void Start()
        {
            Console.WriteLine("Welcome to Jungle Survival RPG!\n");
            Console.Write("Enter your name: ");
            var name = Console.ReadLine() ?? "Player";
            player = new Player(name);

            InitializeEnemies();
            BuildScenes();

            current = new SceneID(1, 1);
            Thread musicLoopThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        using (var reader = new AudioFileReader("Music.mp3")) // Use WAV to avoid MP3 decoding issues
                        using (var waveOut = new WaveOutEvent())
                        {
                            waveOut.Init(reader);
                            waveOut.Play();

                            while (waveOut.PlaybackState == PlaybackState.Playing)
                            {
                                Thread.Sleep(500);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Music Error] {ex.Message}");
                }
            });
            musicLoopThread.IsBackground = true;
            musicLoopThread.Start();
            Run();

        }

        private void InitializeEnemies()
        {
            enemies.Add(new Enemy("Panther", 50f, 10, 3, 8));
            enemies.Add(new Enemy("Snake", 30f, 5, 2, 10));
            BossEnemy panther = new BossEnemy("Panther of the Wilds", 150f, 25, "Shadow Pounce", 1, 10, 40);
            BossEnemy forestWraith = new BossEnemy("Forest Wraith", 180f, 30, "Spectral Slash", 2, 15, 35);
            BossEnemy arcaneGolem = new BossEnemy("Arcane Golem", 300f, 40, "Rune Smash", 3, 40, 10);
            BossEnemy voidwalker = new BossEnemy("The Voidwalker", 500f, 50, "Void Pulse", 4, 30, 45);
            boss.Add(panther);
            boss.Add(forestWraith);
            boss.Add(arcaneGolem);
            boss.Add(voidwalker);


        }

        private void BuildScenes()
        {



            //gain experience
            player.GainExperience(20); // Initialize experience to 0




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
                                "You can use it to restore some health or as a crafting ingredient. +0.1XP \n");
                                //use integer
                            player.GainExperience(1); // Gain 1 experience point
                                Console.WriteLine($"Experience: {player.Experience}");
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
                ".........." +
                "1) Go back.\n" +
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
            //Find crafting table at storage
            scenes[new SceneID(2, 2)] = new Scene(
                "You explore the tree house\n" +
                "1) Storage room \n" +
                "2) Open Inventory.\n" +
                "3) Return to the clearing.\n"
            );
            scenes[new SceneID(2, 2)].Choices[1] = new SceneID(10, 1); // Storage room with crafting table
            scenes[new SceneID(2, 2)].Choices[2] = new SceneID(0, 0); // Open Inventory
            scenes[new SceneID(2, 2)].Choices[3] = new SceneID(2, 1); // Return to clearing

            // Crafting table found at the treehouse
            scenes[new SceneID(10, 1)] = new Scene(
                "You check out the storage.....\n" +
                "1) Use the Crafting Table.\n" +
                "2) Return to the clearing.\n" +
                "0) Open Inventory.\n",
                player =>
                {
                    for (int i = 0; i < 4 ; i++)
                    {

                    player.Inventory.Add(ItemCatalog.Bark);

                    }
                });
            scenes[new SceneID(10, 1)].Choices[0] = new SceneID(10, 2); // Crafting table scene
            scenes[new SceneID(10, 1)].Choices[1] = new SceneID(2, 1); // Return to clearing
            scenes[new SceneID(10, 1)].Choices[2] = new SceneID(0, 0); // Open Inventory
            

            // ── CRAFTING TABLE ──────────────────────────────────────────────────────
            scenes[new SceneID(10, 2)] = new Scene(
                "You approach the Crafting Table...\n",
                player =>
                {
                    //Write Options, armor, weapons, exit
                    Console.WriteLine(
                        "1) Craft Armor\n" +
                        "2) Craft Weapon\n" +
                        "3) Exit Crafting Table\n" +
                        "4) Open Inventory\n"
                    );
                    //Check chosen option
                    int choice;
                    do
                    {
                        Console.Write("Choose an option: ");
                        string? input = Console.ReadLine();
                        if (int.TryParse(input, out choice) && choice >= 1 && choice <= 4)
                        {
                            break;
                        }
                        Console.WriteLine("Invalid choice. Please try again.");
                    } while (true);

                    switch (choice)
                    {
                        case 1:
                            CraftArmor(player);
                            break;
                        case 2:
                            CraftWeapon(player);
                            break;
                        case 3:
                            Console.WriteLine("Exiting Crafting Table.");
                            break;
                        case 4:
                            player.OpenInventory();
                            break;
                    }
                    
                }
            );
        }
            private void CraftWeapon(Player player){
                Console.WriteLine(
                    "Choose a weapon to craft:\n" +
                    "1) Rusty Dagger (requires 2x Iron Ore)\n" +
                    "2) Ember Fang (requires 3x Iron Ore, 1x Fish)\n" +
                    "3) Storm Splitter (requires 5x Iron Ore)\n"
                );
                int weaponChoice;
                do
                {
                    Console.Write("Choose a weapon to craft (1-3): ");
                    string? input = Console.ReadLine();
                    if (int.TryParse(input, out weaponChoice) && weaponChoice >= 1 && weaponChoice <= 3)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid choice. Please try again.");
                } while (true);
                switch (weaponChoice)
                {
                    case 1:
                        if (player.Inventory.Count(item => item.Name == "Iron Ore") >= 2)
                        {
                            player.Inventory.RemoveAll(item => item.Name == "Iron Ore");
                            player.EquippedWeapon = Equipment.RustyDagger;
                            Console.WriteLine("You crafted a Rusty Dagger!");
                        }
                        else
                        {
                            Console.WriteLine("You need 2x Iron Ore to craft this weapon.");
                        }
                        break;
                    case 2:
                        if (player.Inventory.Count(item => item.Name == "Iron Ore") >= 3 &&
                            player.Inventory.Count(item => item.Name == "Fish") >= 1)
                        {
                            player.Inventory.RemoveAll(item => item.Name == "Iron Ore");
                            player.Inventory.RemoveAll(item => item.Name == "Fish");
                            player.EquippedWeapon = Equipment.EmberFang;
                            Console.WriteLine("You crafted an Ember Fang!");
                        }
                        else
                        {
                            Console.WriteLine("You need 3x Iron Ore and 1x Fish to craft this weapon.");
                        }
                        break;
                    case 3:
                        if (player.Inventory.Count(item => item.Name == "Iron Ore") >= 5)
                        {
                            player.Inventory.RemoveAll(item => item.Name == "Iron Ore");
                            player.EquippedWeapon = Equipment.StormSplitter;
                            Console.WriteLine("You crafted a Storm Splitter!");
                        }
                        else
                        {
                            Console.WriteLine("You need 5x Iron Ore to craft this weapon.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            //First option (craft armor)
            private void CraftArmor(Player player)
            {
                Console.WriteLine(
                    "Choose an armor to craft:\n" +
                    "1) Barkhide Vest (requires 2x fish, 3x leather, and 4x Bark)\n" +
                    "2) Worn Scout Jacket (requires 3x leather, 1x fish, and 2x chainmail)\n" +
                    "3) Iron Scale Mail (requires 5x Iron Ore)\n"
                );
                int armorChoice;
                do
                {
                    Console.Write("Choose an armor to craft (1-3): ");
                    string? input = Console.ReadLine();
                    if (int.TryParse(input, out armorChoice) && armorChoice >= 1 && armorChoice <= 3)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid choice. Please try again.");
                } while (true);
                switch (armorChoice)
                {
                    case 1:
                    //craft Barkhide Vest
                        if (player.Inventory.Count(item => item.Name == "Bark") >= 4 &&
                            player.Inventory.Count(item => item.Name == "Fish") >= 2 &&
                            player.Inventory.Count(item => item.Name == "Leather") >= 3)
                        { // for each to remvoe specific item count
                            for (int i = 0; i < 4; i++)
                            {
                                player.Inventory.RemoveAll(item => item.Name == "Bark");
                            }
                            for (int i = 0; i < 2; i++)
                            {
                                player.Inventory.RemoveAll(item => item.Name == "Fish");
                            }
                            for (int i = 0; i < 3; i++)
                            {
                                player.Inventory.RemoveAll(item => item.Name == "Leather");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You need 4x Bark, 2x Fish, and 3x Leather to craft this armor.");
                        }
                        break;
                    case 2:
                        if (player.Inventory.Count(item => item.Name == "Worn Cloth") >= 3)
                        {
                            player.Inventory.RemoveAll(item => item.Name == "Worn Cloth");
                            player.EquippedArmor = Equipment.WornScoutJacket;
                            Console.WriteLine("You crafted a Worn Scout Jacket!");
                        }
                        else
                        {
                            Console.WriteLine("You need 3x Worn Cloth to craft this armor.");
                        }
                        break;
                    case 3:
                        if (player.Inventory.Count(item => item.Name == "Iron Ore") >= 5)
                        {
                            player.Inventory.RemoveAll(item => item.Name == "Iron Ore");
                            player.EquippedArmor = Equipment.IronScaleMail;
                            Console.WriteLine("You crafted an Iron Scale Mail!");
                        }
                        else
                        {
                            Console.WriteLine("You need 5x Iron Ore to craft this armor.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
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


                // Regenerate Mana every 3 seconds by 2 until max Mana reached, Time is imporant
                if (player.Mana < player.MaxMana)
                {
                    for (int i = 0; i < 3 && player.Mana < player.MaxMana; i++)
                    {
                        Thread.Sleep(1000); 
                        player.Mana = Math.Min(player.MaxMana, player.Mana + 2);
                    }
                    // Set mana to max mana if it exceeds it
                    if (player.Mana > player.MaxMana)
                    {
                        player.Mana = player.MaxMana;
                        Console.WriteLine($"Your Mana has been fully restored\n");
                    }
                }
                


            }
        }

        private void StartCombat(Enemy enemy)
        {
            Console.WriteLine($"A wild {enemy.Name} appears!\n");

            int instanceSpeed = player.Speed + player.EquippedWeapon.Speed + player.EquippedArmor.Speed;
            bool playerGoesFirst = instanceSpeed >= enemy.Speed;
            int round = 0;
            float playerdamage=0f;

            while (player.HP > 0 && enemy.HP > 0)
            {
                if (playerGoesFirst)
                {
                    playerdamage=PlayerAction(round,out _);
                    playerdamage -= enemy.Defence;
                    enemy.HP -= Math.Max(0, playerdamage);
                    if (enemy.HP <= 0) break;
                    EnemyAction(enemy);
                    round++;
                }
                else
                {
                    EnemyAction(enemy);
                    if (player.HP <= 0) break;
                    playerdamage=PlayerAction(round,out _ );
                    playerdamage -= enemy.Defence;
                    enemy.HP -= Math.Max(0, playerdamage);
                    round++;
                }
            }

            if (player.HP <= 0)
            {
                Console.WriteLine("You have been defeated...\n");
                Environment.Exit(0);
            }

            Console.WriteLine($"You defeated the {enemy.Name}!\n");
        }
        
         private void StartCombat2(BossEnemy boss)
{
    Console.WriteLine($"A wild {boss.Name} appears!\n");

    int instanceSpeed = player.Speed + player.EquippedWeapon.Speed + player.EquippedArmor.Speed;
    bool playerGoesFirst = instanceSpeed >= boss.Speed;
    int round = 0;

    Random rng = new Random();

    while (player.HP > 0 && boss.HP > 0)
    {
        if (playerGoesFirst)
        {
            float playerDamage = PlayerAction(round, out bool isHeavyMana);

            // Boss has a 25% chance to dodge non-heavy mana attacks
            if (!isHeavyMana && rng.Next(1, 101) <= 25)
            {
                Console.WriteLine($"{boss.Name} dodged your attack!");
            }
            else
            {
                playerDamage -= boss.Defence;
                boss.HP -= Math.Max(0, playerDamage);
            }

            if (boss.HP <= 0) break;

            BossEnemyAction(boss);
            round++;
        }
        else
        {
            BossEnemyAction(boss);
            if (player.HP <= 0) break;

            float playerDamage = PlayerAction(round, out bool isHeavyMana);

            if (!isHeavyMana && rng.Next(1, 101) <= 25)
            {
                Console.WriteLine($"{boss.Name} dodged your attack!");
            }
            else
            {
                playerDamage -= boss.Defence;
                boss.HP -= Math.Max(0, playerDamage);
            }

            round++;
        }
    }

    if (player.HP <= 0)
    {
        Console.WriteLine("You have been defeated...\n");
        Environment.Exit(0);
    }

    Console.WriteLine($"You defeated the {boss.Name}!\n");
}


        

        private float PlayerAction(int round, out bool isHeavyMana)
        {
            isHeavyMana = false;
            Console.WriteLine("Choose your action:");
            Console.WriteLine("1) Normal Attack");

            var weapon = player.EquippedWeapon;
            bool hasManaWeapon = weapon.HeavyManaAttack > 0 || weapon.LightManaAttack > 0;

            bool canUseLightMana = (round % 3) < 2;
            bool canUseHeavyMana = round % 6 == 0;

            if (hasManaWeapon)
            {
                Console.WriteLine(canUseLightMana ? "2) Light Mana Attack" : "2) Light Mana Attack (Not available)");
                Console.WriteLine(canUseHeavyMana ? "3) Heavy Mana Attack" : "3) Heavy Mana Attack (Not available)");
            }

            Console.WriteLine("4) Flee");
            Console.Write("Action: ");
            var cmd = Console.ReadLine() ?? "";

            float totalDamage = 0;
            float instanceSTR = weapon.Strength + player.Strength + player.EquippedArmor.Strength;

            switch (cmd)
            {
                case "1":
                    totalDamage = (int)(weapon.RegularAttack * (1 + (instanceSTR / 100.0f)));
                    Console.WriteLine($"You strike the enemy with your weapon for {totalDamage} damage!");
                    break;

                case "2" when hasManaWeapon && canUseLightMana:
                    totalDamage = (int)(weapon.LightManaAttack * (1 + (instanceSTR / 100.0f)));
                    Console.WriteLine($"You strike the enemy with {weapon.LightManaAttackName} on for {totalDamage} damage!");
                    break;

                case "3" when hasManaWeapon && canUseHeavyMana:
                    isHeavyMana = true;
                    totalDamage = (int)(weapon.HeavyManaAttack * (1 + (instanceSTR / 100.0f)));
                    Console.WriteLine($"You unleash {weapon.HeavyManaAttackName} on for {totalDamage} damage!");
                    break;

                case "4":
                    Console.WriteLine("You fled back to the previous scene.\n");
                    Environment.Exit(0);
                    return totalDamage;

                default:
                    Console.WriteLine("Invalid choice or move unavailable. You fumble your turn.");
                    return totalDamage;
            }

            return totalDamage;
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
        
        private void BossEnemyAction(BossEnemy boss)
{
    float incomingDamage;
    Random rng = new Random();
    int attackChoice = rng.Next(1, 3); // 1 = regular, 2 = special

    Console.WriteLine($"{boss.Name} is preparing an attack!");

    if (attackChoice == 2)
    {
        incomingDamage = boss.specialmove - player.Defence;
        Console.WriteLine($"{boss.Name} uses their special attack: {boss.spAttackName}!");
    }
    else
    {
        incomingDamage = boss.AttackPower - player.Defence;
        Console.WriteLine($"{boss.Name} performs a regular attack!");
    }

    incomingDamage = Math.Max(0, incomingDamage);

    Console.WriteLine("Do you want to try to dodge? (y/n): ");
    string dodge = Console.ReadLine()?.ToLower() ?? "n";

    if (dodge == "y")
    {
        int chance = rng.Next(1, 101);
        if (chance <= player.Luck)
        {
            Console.WriteLine("You dodged the attack!");
            return;
        }
        Console.WriteLine("Dodge failed!");
        incomingDamage *= 1.5f;
    }

    player.TakeDamage(incomingDamage);
    Console.WriteLine($"{boss.Name} hits you for {incomingDamage} damage.\n");
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
