// Program.cs
using System;
using System.Collections.Generic;
using System.Threading;
using JungleSurvivalRPG;  // for Item, ItemCatalog 
using NAudio.Wave;
using System.IO;

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

        

        public static Weapon RustyDagger = new Weapon(0, "", 0, "", 1, 5, "A trusty—but very sharp—rusted dagger.", 30);
        public static Weapon EmberFang = new Weapon(65, "Flame Bite", 25, "Spark Snap", -1, 4, "A basic iron sword enchanted with weak fire magic.", 15);
        public static Weapon StormSplitter = new Weapon(85, "Thunder Slash", 45, "Static Arc", -5, 10, "A steel longsword that crackles with electrical energy.", 20);
        public static Weapon NightReaver = new Weapon(120, "Void Rend", 65, "Shadow Slice", -2, 16, "Forged in darkness, this blade weakens enemies' vision.", 25);
        public static Weapon DragonspineExecutioner = new Weapon(150, "Infernal Decapitation", 85, "Ember Swipe", -7, 28, "A massive greatsword made from dragon bones. Delivers heavy fire-based damage.", 35);
        public static Weapon MirrorEdge = new Weapon(160, "Radiant Slash", 100, "Glint Pierce", -12, 22, "A crystal blade that reflects some light-based magic back at the attacker.", 30);
        public static Weapon Bloodthirster = new Weapon(160, "Life Leech", 95, "Crimson Swipe", -3, 30, "A cursed weapon that steals a bit of the target's vitality with each strike.", 33);
        public static Weapon ChronoFang = new Weapon(175, "Time Rend", 80, "Clock Pierce", -3, 24, "An arcane blade said to distort time. Occasionally delays an enemy’s next move.", 28);
        public static Weapon VoidwalkerBlade = new Weapon(1000, "Hakai", 0, "Slipstream Cut", 15, 15, "A blade infused with void energy. Allows the wielder to strike from a short distance instantly.", 40);

        // For load: lookup by Description string
        public static readonly List<Weapon> AllWeapons = new List<Weapon>
        {
            NoWeapon,
            RustyDagger,
            EmberFang,
            StormSplitter,
            NightReaver,
            DragonspineExecutioner,
            MirrorEdge,
            Bloodthirster,
            ChronoFang,
            VoidwalkerBlade
        };

        public static readonly List<Armor> AllArmors = new List<Armor>
        {
            NoArmor,
            BarkhideVest,
            WornScoutJacket,
            IronScaleMail,
            RogueTunic,
            PhantomCoat,
            BattleForgedPlates,
            DrakeskinPlate,
            ValkyrieShroud,
            RadiantPlate,
            ShadowRaiment
        };
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
        // Public properties get picked up by System.Text.Json automatically
        public int Act  { get; set; }
        public int Plot { get; set; }

        public SceneID(int act, int plot)
        {
            Act  = act;
            Plot = plot;
        }

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

        // --- Snapshot of starting stats for growth calculation ---
        public int InitialStrength { get; private set; }
        public int InitialLuck     { get; private set; }
        public int InitialSpeed    { get; private set; }

        // Revival support
        private bool _revivalUsed;
        public bool HasRevival { get; private set; } = false;

        public bool ShouldSaveAndExit { get; set; } = false;
        public int Experience { get; private set; } = 0;
        public List<string> UnlockedSpells { get; } = new List<string>();
        public Weapon EquippedWeapon { get; set; } = Equipment.NoWeapon;
        public Armor EquippedArmor { get; set; } = Equipment.NoArmor;
        public List<Item> Inventory { get; } = new();
        public bool HasFoundPage2 { get; set; } = false;

        public float MaxMana
        {
            get => _maxMana;
            private set => _maxMana = value;
        }

        public Player(string name)
        {
            Name    = name;
            HP      = MaxHP;
            Defence = 5f;
            Luck    = 1;
            Mana    = MaxMana;
            Speed   = 4;
            Strength = 5;

            // Initialize inventory
            Inventory.Add(ItemCatalog.EmptyWaterFlask);

            // Capture initial stats for growth
            InitialStrength = Strength;
            InitialLuck     = Luck;
            InitialSpeed    = Speed;

            // Initialize revival
            HasRevival   = false;
            _revivalUsed = false;
        }
        public int Gold => Inventory.Count(it => it.Name == ItemCatalog.GoldCoin.Name);

        // Add N coins
        public void AddGold(int amount)
        {
            for (int i = 0; i < amount; i++) Inventory.Add(ItemCatalog.GoldCoin);
        }

        // Spend N coins (returns true if successful, false if not enough)
        public bool SpendGold(int amount)
        {
            if (Gold < amount) return false;
            int removed = 0;
            // Remove exactly `amount` coins
            for (int i = Inventory.Count - 1; i >= 0 && removed < amount; i--)
            {
                if (Inventory[i].Name == ItemCatalog.GoldCoin.Name)
                {
                    Inventory.RemoveAt(i);
                    removed++;
                }
            }
            return true;
        }

        // Experience and spells
        public void GainExperience(int amount)
        {
            Experience += amount;
            UnlockAvailableSpells();
        }

        private void UnlockAvailableSpells()
        {
            foreach (var spell in SpellCatalog.AllSpells)
            {
                if (Experience >= spell.RequiredXP && !UnlockedSpells.Contains(spell.Name))
                {
                    UnlockedSpells.Add(spell.Name);
                }
            }
        }

        public void UnlockSpell(string spell)
        {
            if (!UnlockedSpells.Contains(spell))
                UnlockedSpells.Add(spell);
        }

        // Mana adjustments
        public void IncreaseMaxMana(float amount)
        {
            if (amount > 0)
            {
                MaxMana += amount;
                Console.WriteLine($"Max Mana increased by {amount}. New Max Mana: {MaxMana}");
            }
            else
                Console.WriteLine("Invalid amount. Max Mana increase must be positive.");
        }

        public void DecreaseMaxMana(float amount)
        {
            if (amount > 0 && MaxMana - amount >= 0)
            {
                MaxMana -= amount;
                Console.WriteLine($"Max Mana decreased by {amount}. New Max Mana: {MaxMana}");
            }
            else
                Console.WriteLine("Invalid amount. Max Mana decrease must be positive and not reduce below zero.");
        }

        // Damage and attacks
        public int Attack() => EquippedWeapon.RegularAttack;

        public void TakeDamage(float dmg)
        {
            float instanceDef = Defence + EquippedArmor.Defence;
            int net = (int)Math.Max(0f, dmg - instanceDef);
            HP -= net;
            Console.WriteLine($"{Name} takes {net} damage. HP: {HP}\n");
        }

        public void ApplyPoison(int totalDamage, int turns)
        {
            HP = Math.Max(0f, HP - totalDamage);
            Console.WriteLine($"{Name} is poisoned and takes {totalDamage} damage over {turns} turns. HP: {HP}, Strength +5 \n");
            Strength += 5;
        }

        // Inventory UI
        public void OpenInventory()
        {
            if (!Inventory.Any())
            {
                Console.WriteLine("Inventory is empty.");
                return;
            }

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
                if (key == ConsoleKey.UpArrow) idx = (idx - 1 + grouped.Count) % grouped.Count;
                else if (key == ConsoleKey.DownArrow) idx = (idx + 1) % grouped.Count;
                else if (key == ConsoleKey.Backspace) return;
            }
            while (key != ConsoleKey.Enter);

            var chosenItem = Inventory.First(item => item.Name == grouped[idx].Name);
            Console.Clear();
            chosenItem.Use(this);
            Inventory.Remove(chosenItem);
        }

        // Revival methods
        public void GrantRevival()
        {
            HasRevival   = true;
            _revivalUsed = false;
        }

        public bool TryUseRevival()
        {
            if (HasRevival && !_revivalUsed && HP <= 0)
            {
                _revivalUsed = true;
                HP = MaxHP * 0.5f;
                Printer.PrintSlow($"{Name}'s revival magic shatters death—HP restored to {HP:F0}!\n");
                return true;
            }
            return false;
        }

        // Generic stat accessors for Judgement logic
        public int GetStatValue(string statName) => statName switch
        {
            "Strength" => Strength,
            "Luck"     => Luck,
            "Speed"    => Speed,
            _ => 0
        };

        public void ModifyStat(string statName, int delta)
        {
            switch (statName)
            {
                case "Strength": Strength += delta; break;
                case "Luck":     Luck     += delta; break;
                case "Speed":    Speed    += delta; break;
            }
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

        private bool _skipInventoryOnce = false;
        private Player player = null!;
        private Dictionary<SceneID, Scene> scenes = null!;
        private List<Enemy> enemies = new();
        private List<BossEnemy> boss = new();
        private SceneID current;
        private SceneID lastScene;
        private static bool Judgement(Player player, ref SceneID currentScene, SceneID beforeDeathScene)
        {
            // Revival buffer: if player has Revival buff, consume it and rewind to just-before-death
            if (player.TryUseRevival())
            {
                Printer.PrintSlow("A protective divine buffer restores you to life!\n");
                currentScene = beforeDeathScene;
                return true;
            }

            // No revival: go to the gods’ board
            return ShowBoardOfGods(player, ref currentScene, beforeDeathScene);
        }
        // inside Program.cs

        private static bool ShowBoardOfGods(Player player, ref SceneID currentScene, SceneID beforeDeathScene)
        {
            Printer.PrintSlow("You stand before the Board of Gods...\n");

            // helper to calc growth %
            double GrowthPct(int init, int curr) => init > 0
                ? (double)(curr - init) / init * 100.0
                : 0.0;

            // assume you captured these at character creation:
            int strG  = (int)Math.Round(GrowthPct(player.InitialStrength, player.Strength));
            int luckG = (int)Math.Round(GrowthPct(player.InitialLuck,     player.Luck));
            int spdG  = (int)Math.Round(GrowthPct(player.InitialSpeed,    player.Speed));

            Printer.PrintSlow($"Stat Growth: STR +{strG}%, LUCK +{luckG}%, SPD +{spdG}%\n");

            // pick a random existing stat to tax
            var rng   = new Random();
            var stats = new[] { "Strength", "Luck", "Speed" };
            int  idx  = rng.Next(stats.Length);
            string stat        = stats[idx];
            int     currentVal = player.GetStatValue(stat);

            // gods demand 20–60%
            int demandP = rng.Next(20, 61);
            int demandV = (int)Math.Round(currentVal * demandP / 100.0);

            Printer.PrintSlow($"The gods demand {demandP}% of your {stat} ({demandV} points).\n");
            Printer.PrintSlow("1) Accept their demand\n2) Counter-offer (20–60%)\nChoose: ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
                Printer.PrintSlow("Invalid. Enter 1 or 2: ");

            if (choice == 1)
            {
                player.ModifyStat(stat, -demandV);
                Printer.PrintSlow($"You bow to their will—they take {demandV} {stat}.\n");
                currentScene = beforeDeathScene;
                return true;
            }

            // counter-offer flow
            Printer.PrintSlow("Enter your counter-offer percent (20–60): ");
            int offerP;
            while (!int.TryParse(Console.ReadLine(), out offerP) || offerP < 20 || offerP > 60)
                Printer.PrintSlow("Invalid. Enter 20–60: ");

            int godsIfDeny = rng.Next(20, 61);
            if (offerP <= godsIfDeny)
            {
                int taken = (int)Math.Round(currentVal * offerP / 100.0);
                player.ModifyStat(stat, -taken);
                Printer.PrintSlow($"They accept! You lose {taken} {stat}.\n");
                currentScene = beforeDeathScene;
                return true;
            }
            else
            {
                Printer.PrintSlow($"They’re angered and seize {demandV} {stat} instead—your journey ends.\n");
                currentScene = new SceneID(-1, 0);
                return false;
            }
        }



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




        public void StartNew()
        {
            Console.WriteLine("Welcome to Survival RPG!\n");
            Console.Write("Enter your name: ");
            var name = Console.ReadLine() ?? "Player";
            player = new Player(name);
            if (!player.Inventory.Contains(ItemCatalog.SaveAndExit)){
                player.Inventory.Add(ItemCatalog.SaveAndExit); // Add Save and Exit item
            }
            InitializeEnemies();
            BuildScenes();

            current   = new SceneID(1, 1);
            lastScene = current;
            player = new Player(name);
            player.EquippedWeapon = Equipment.RustyDagger;   // auto-equip the dagger


            StartMusicLoop();
            Run();
        }

        public void StartFromSave(SaveData data)
        {
            // 1) Rehydrate player, enemies & bosses
            player  = data.Player.ToPlayer();
            enemies = data.Enemies
                            .Select(e => new Enemy(e.Name, e.HP, e.AttackPower, e.Defence, e.Speed))
                            .ToList();
            boss    = data.Bosses
                            .Select(b => new BossEnemy(
                                b.Name,
                                b.HP,
                                b.AttackPower,
                                b.SpecialAttackName,
                                b.SpecialMove,
                                b.Defence,
                                b.Speed))
                            .ToList();

            // 2) Rebuild all scenes so they hook into this new player instance
            BuildScenes();
            //check if inventory has saveexit
            if (!player.Inventory.Contains(ItemCatalog.SaveAndExit)){
                player.Inventory.Add(ItemCatalog.SaveAndExit); // Add Save and Exit item
            }


            // 3) Jump straight into the one saved resume‐scene
            current   = data.ResumeScene;
            lastScene = data.ResumeScene;

            // 4) Fire up music and start the loop
            StartMusicLoop();
            Run();
        }


        private void StartMusicLoop()
        {
            Thread musicLoop = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        using var reader = new AudioFileReader("Music.mp3");
                        using var wave   = new WaveOutEvent();
                        wave.Init(reader);
                        wave.Play();
                        while (wave.PlaybackState == PlaybackState.Playing)
                            Thread.Sleep(200);
                    }
                }
                catch { /* ignore */ }
            });
            musicLoop.IsBackground = true;
            musicLoop.Start();
        }



        private void InitializeEnemies()
        {
            enemies.Add(new Enemy("Panther", 50f, 10, 3, 8));
            enemies.Add(new Enemy("Snake",   30f,  5, 2,10));

            enemies.Add(new Enemy("Bandit",        40f,  8, 4, 6));
            enemies.Add(new Enemy("Stone Guardian",80f, 15, 8, 4));
            enemies.Add(new Enemy("Venomback Croc",90f, 18, 5, 6));
            enemies.Add(new Enemy("Bat Swarm",     60f, 10, 1,20));
            enemies.Add(new Enemy("Grizzled Hunter", 70f, 12, 5, 7)); // HP, ATK, DEF, SPD

        
            boss.Add(new BossEnemy("Panther of the Wilds",150f,25,"Shadow Pounce",1,10,40));
            boss.Add(new BossEnemy("Forest Wraith",      180f,30,"Spectral Slash",2,15,35));
            boss.Add(new BossEnemy("Arcane Golem",       300f,40,"Rune Smash",    3,40,10));
            boss.Add(new BossEnemy("The Voidwalker",     500f,50,"Void Pulse",    4,30,45));
            boss.Add(new BossEnemy("Corrupted Dryad",    250f,35,"Root Lash",     5,12,25));
            
        }
        private bool InvKey(string? raw, Player p)
        {
            var s = raw?.Trim().ToLower() ?? "";
            if (s is "5" or "i" or "inv") { p.OpenInventory(); return true; }
            return false;
        }
        /* ── helpers visible to every scene ───────────────────────── */
        private static void Pause()
        {
            Console.WriteLine("[press any key]");
            Console.ReadKey(true);
        }
        // ───────────────────────────────────────────────────────────────────────────────
        //  GameEngine.BuildScenes()   –  COMPLETE VERSION 
        // ───────────────────────────────────────────────────────────────────────────────
        private readonly Dictionary<SceneID, SceneID> _forward = new();


private void BuildScenes()
{
    Action<Player> noop = _ => { };
    scenes = new();

    bool storeUnlocked = player.Inventory.Any(i => i.Name == "GateKey2");
    bool pantherDown   = false;
    bool statueDown    = false;
    Random rng         = new();
    var   bossPool     = boss;              // list already in GameEngine

    /*  easy helper to drop merchant key  */
        void ShopKey(Scene caller)
        {
            if (!storeUnlocked) return;

            /* inject the key if it isn’t there yet */
            if (!caller.Choices.ContainsKey(6))
                caller.Choices[6] = new SceneID(99, 0);

            /* figure out which SceneID this caller actually has */
            var callerID = scenes.First(kv => kv.Value == caller).Key;

            /* choose the forward destination:
            prefer the option labelled 1, otherwise first non-merchant choice */
            SceneID forward = caller.Choices.ContainsKey(1)
                ? caller.Choices[1]
                : caller.Choices
                        .Where(kv => kv.Key != 6 && kv.Key != 5)   // skip merchant & inventory
                        .Select(kv => kv.Value)
                        .First();

            _forward[callerID] = forward;     // remember where to jump after shop
        }
        
    /*──────────────────  ACT-1  Courtyard  ──────────────────*/
    scenes[new SceneID(1,1)] = new(
        " Night rain lashes shattered flagstones.  Silver puddles ripple around\n" +
        "fallen masonry.  Beside you lie a battered **portable recorder** and a\n" +
        "damp **cassette marked “TAPE 1”.**  A bronze plaque, cracked in half,\n" +
        "rests near a waist-high spiral pedestal.  Looming beyond, a slab-gate\n" +
        "bars the only visible archway.\n\n" +
        "1) Examine the broken plaque fragment\n" +
        "2) Step onto the spiral pedestal\n" +
        "3) Test the massive bronze gate\n" +
        "5) Check your inventory",
        p =>
        {
            if (!p.Inventory.Any(i => i.Name == "Recorder"))
                p.Inventory.Add(ItemCatalog.Recorder);
            if (!p.Inventory.Any(i => i.Name == "Save/Exit"))
                p.Inventory.Add(ItemCatalog.SaveAndExit);
            if (!p.Inventory.Any(i => i.Name == "Tape 1"))
                p.Inventory.Add(ItemCatalog.Tape1);
        });
    scenes[new SceneID(1,1)].Choices.Add(1,new SceneID(1,2));
    scenes[new SceneID(1,1)].Choices.Add(2,new SceneID(1,3));
    scenes[new SceneID(1,1)].Choices.Add(3,new SceneID(1,4));
    scenes[new SceneID(1,1)].Choices.Add(5,new SceneID(0,0));

    scenes[new SceneID(1,2)] = new(
        "Amber patina blankets the sundered plaque, leaving only a single line\n" +
        "readable:\n\n      “I speak without a mouth and hear without ears …”\n\n" +
        "The rest has been devoured by time.  Perhaps the cassette will finish\n" +
        "the thought.\n\n9) Return to the centre of the courtyard");
    scenes[new SceneID(1,2)].Choices[9] = new SceneID(1,1);

    scenes[new SceneID(1,3)] = new(string.Empty, p =>
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(
                "Fine sand drifts across the pedestal’s spiral groove.  A faint\n" +
                "hum tickles your palms as you approach.\n" +
                "Type the four-letter answer the riddle demands, or:\n" +
                "   5) Inventory      9) Step back");
            Console.Write("> ");
            var guess = Console.ReadLine();
            if (InvKey(guess,p)) continue;
            if (guess == "9") return;

            if (guess?.Trim().ToLower() == "echo")
            {
                if (!p.Inventory.Any(i => i.Name == "GateKey1"))
                {
                    p.Inventory.Add(ItemCatalog.GateKey1);
                    p.AddGold(30);
                    Console.WriteLine(
                        "\nResonant chimes swirl around you.  Specks of bronze converge and\n" +
                        "harden into a heavy key in your hand.  (+30 gold)");
                }
                Pause(); break;
            }
            Console.WriteLine("A hush answers you — the pedestal remains inert."); Pause();
        }

        var ped = scenes[new SceneID(1,3)];
        ped.Choices.Clear();
        ped.Choices[1] = new SceneID(1,4);
        ped.Choices[9] = new SceneID(1,1);
        Console.Clear();
        Console.WriteLine(
            "With the key forming warm in your grasp you may now try the gate.\n" +
            "1) Approach the bronze gate\n" +
            "");
    });

    scenes[new SceneID(1,4)] = new(string.Empty, p =>
    {
        var gate = scenes[new SceneID(1,4)];
        gate.Choices.Clear();

        if (p.Inventory.Any(i => i.Name == "GateKey1"))
        {
            gate.Text =
                "You fit the newly-forged key into the ancient lock.  Gears grind\n" +
                "like distant thunder; the monolithic doors split open to reveal a\n" +
                "narrow passage flickering with dim torch-light.\n\n" +
                "1) Step into the corridor";
            gate.Choices[1] = new SceneID(2,0);
        }
        else
        {
            gate.Text =
                "You heave against the bronze slab, but it refuses to shift.  A\n" +
                "bronze keyhole gleams mockingly at eye-level.\n\n" +
                "9) Return to the centre of the courtyard";
            gate.Choices[9] = new SceneID(1,1);
        }
    });

    /*──────────────────  ACT-2  Lantern Hall  ──────────────────*/
    scenes[new SceneID(2,0)] = new(
        "A small antechamber smelling of ash leads onward.  On the dusty floor\n" +
        "rests **“TAPE 2.”**  Somewhere ahead colourful light pulses softly.\n\n" +
        "1) Enter the great hall of lanterns\n" +
        "4) Trace the sound of dripping water\n" +
        "5) Inventory" + (storeUnlocked? "   6) Travelling merchant":""),
        p=>{ if(!p.Inventory.Any(i=>i.Name=="Tape 2")) p.Inventory.Add(ItemCatalog.Tape2);});
    scenes[new SceneID(2,0)].Choices[1]=new SceneID(2,1);
    scenes[new SceneID(2,0)].Choices[4]=new SceneID(50,0);
    scenes[new SceneID(2,0)].Choices[5]=new SceneID(0,0); ShopKey(scenes[new SceneID(2,0)]);

    scenes[new SceneID(2,1)] = new(
        "Columns vanish into darkness above, each crowned by an ornate lantern.\n" +
        "The flames burn without fuel:   Red   ·   Blue   ·   Green   ·   Yellow.\n\n" +
        "1) Examine the scarlet blaze more closely\n" +
        "2) Study the frigid blue flame\n" +
        "3) Observe the steady green glow\n" +
        "4) Watch the nervous yellow flicker\n" +
        "6) Risk touching a lantern\n" +
        "5) Inventory"+
        (storeUnlocked?"   6) Merchant":""), noop);
    var hall=scenes[new SceneID(2,1)];
    hall.Choices.Add(1,new SceneID(2,5));
    hall.Choices.Add(2,new SceneID(2,6));
    hall.Choices.Add(3,new SceneID(2,7));
    hall.Choices.Add(4,new SceneID(2,8));
    hall.Choices.Add(5,new SceneID(0,0));
    hall.Choices[6]=new SceneID(2,2);
    hall.Choices[9]=new SceneID(2,0); ShopKey(hall);

    void AddLantern(int id,string desc)
    {
        scenes[new SceneID(2,id)] = new(desc + "\n\n9) Back");
        scenes[new SceneID(2,id)].Choices[9] = new SceneID(2,1); ShopKey(scenes[new SceneID(2,id)]);
    }
    AddLantern(5,"Red heat shimmers, warping the air like desert mirage.");
    AddLantern(6,"Blue frost feathers across the pedestal beneath this flame.");
    AddLantern(7,"Green radiance feels calm, almost heartbeat-steady.");
    AddLantern(8,"Yellow light jerks and sputters, unpredictable as lightning bugs.");

    scenes[new SceneID(2,2)] = new(string.Empty,p=>{
        while(true){
            Console.Write("Which flame do you grasp? (green?)   5=Inv   9=Back: ");
            var col=Console.ReadLine(); if(InvKey(col,p))continue; if(col=="9")return;
            if(col?.Trim().ToLower()=="green"){
                if(!p.Inventory.Any(i=>i.Name=="GateKey2"))
                {
                    p.Inventory.Add(ItemCatalog.GateKey2);
                    p.AddGold(30); storeUnlocked=true;
                }
                int xp=rng.Next(3,13); player.GainExperience(xp);
                Console.WriteLine(
                    "\nThe green lantern cools; stone grinds as a spiral stair descends\n" +
                    "beneath the plinth.  (+30 gold, +{0} XP)", xp);
                Pause(); break;
            }
            Console.WriteLine("A blistering flash forces you back."); Pause();
        }
        var pu=scenes[new SceneID(2,2)];
        pu.Choices.Clear(); pu.Choices[1]=new SceneID(3,0); pu.Choices[5]=new SceneID(0,0);
        pu.Choices[9]=new SceneID(2,1); ShopKey(pu);
        Console.Clear();
        Console.WriteLine("1) Descend the newly-revealed stair\n5) Inventory   6) Merchant");
    });

    /* water-pool side room */
    scenes[new SceneID(50,0)] = new(string.Empty,p=>{
        Console.WriteLine(
            "Droplets fall from unseen fissures, feeding a mirror-clear pool.\n" +
            "1) Fill an empty flask with fresh water\n" +
            "2) Attempt to snatch one of the darting fish\n" +
            "5) Inventory"+(storeUnlocked?"   6) Merchant":""));
        var choice=Console.ReadLine();
        if(choice=="1"){
            if(p.Inventory.Remove(ItemCatalog.EmptyWaterFlask))
                { p.Inventory.Add(ItemCatalog.WaterFlask); Console.WriteLine("You refill the flask with cool, sweet water."); }
            else Console.WriteLine("You have no empty flasks.");
        }
        else if(choice=="2"){
            if(rng.NextDouble() < .5){ p.Inventory.Add(ItemCatalog.Fish); Console.WriteLine("Your hands close around a slippery fish!"); }
            else Console.WriteLine("The fish slip between your fingers and vanish.");
        }
        else if(choice=="6" && storeUnlocked){ current=new SceneID(99,0); return; }
        Pause();
    });
    scenes[new SceneID(50,0)].Choices[9]=new SceneID(2,0); ShopKey(scenes[new SceneID(50,0)]);

    /*──────────────────  ACT-3  Panther  ──────────────────*/
    scenes[new SceneID(3,0)] = new(string.Empty,p=>{
        if(!pantherDown){
            Console.WriteLine("Somewhere in the dark a throat snarls.  A jet-black panther leaps!");Pause();
            var cat = enemies.First(e=>e.Name=="Panther");
            StartCombat(cat);                                    // (your StartCombat forbids XP on flee)
            if(cat.HP<=0 && player.HP>0){
                pantherDown=true;
                if(!p.Inventory.Any(i=>i.Name=="Tape 3")) p.Inventory.Add(ItemCatalog.Tape3);
                p.AddGold(30); int xp=rng.Next(3,13); player.GainExperience(xp);
                Console.WriteLine($"The beast collapses.  You retrieve TAPE 3. (+30 g, +{xp} XP)"); Pause();
            }else return;
        }
        var sc=scenes[new SceneID(3,0)];
        sc.Choices.Clear();
        sc.Choices[1]=new SceneID(4,0);
        sc.Choices[5]=new SceneID(0,0);
        sc.Choices[9]=new SceneID(2,2); ShopKey(sc);
        Console.WriteLine(
            "A rough-hewn tunnel beckons onward.\n" +
            "1) Advance into the gloom   5) Inventory   6) Merchant");
    });

    /*──────────────────  ACT-4  Statue  ──────────────────*/
    scenes[new SceneID(4,0)]=new(string.Empty,p=>{
        if(!statueDown){
            Console.WriteLine(
                "You step through a vaulted chamber where one colossal statue lies in\n" +
                "ruins.  Its twin, unmarred, suddenly animates—stone fingers cracking\n" +
                "like tree limbs in frost."); Pause();
            var st=new Enemy("Avenging Seer Statue",150,20,6,5);
            StartCombat(st);
            if(st.HP<=0 && player.HP>0){
                statueDown=true;
                if(!p.Inventory.Any(i=>i.Name=="Grimwore")) p.Inventory.Add(ItemCatalog.Grimwore);
                if(!p.Inventory.Any(i=>i.Name=="Tape 4"))   p.Inventory.Add(ItemCatalog.Tape4);
                p.AddGold(30); int xp=rng.Next(6,24); player.GainExperience(xp);
                Console.WriteLine(
                    "The stone guardian collapses into lifeless shards.  Amid the rubble\n" +
                    "you find an arcane **Grimwore** and TAPE 4.  (+30 g, +{0} XP)", xp);
                Pause();
            } else return;
        }
        var sc=scenes[new SceneID(4,0)];
        sc.Choices.Clear();
        sc.Choices[1]=new SceneID(5,0);
        sc.Choices[5]=new SceneID(0,0);
        sc.Choices[9]=new SceneID(3,0); ShopKey(sc);
        Console.WriteLine(
            "A jagged breach beside the shattered statue leads downward.\n" +
            "1) Squeeze through the breach\n" +
            "5) Inventory   6) Merchant");
    });

    /*──────────────────  ACT-5  Sundial  ──────────────────*/
    scenes[new SceneID(5,0)] = new(
        "Moon-mirrors orbit a cracked sundial, casting fractured shafts of light.\n" +
        "1) Examine riddle on the dial   5) Inventory");
    scenes[new SceneID(5,0)].Choices[1]=new SceneID(5,1);
    scenes[new SceneID(5,0)].Choices[5]=new SceneID(0,0);
    scenes[new SceneID(5,0)].Choices[9]=new SceneID(4,0); ShopKey(scenes[new SceneID(5,0)]);

    scenes[new SceneID(5,1)] = new(string.Empty,p=>{
        while(true){
            Console.Write(
                "Riddle-inscription:\n" +
                "\"If twice my hour is less than thrice the hour three hours hence,\n" +
                "what hour is now?\"\n" +
                "Enter hour (1-12)   5=Inv   9=Back: ");
            var h=Console.ReadLine(); if(InvKey(h,p))continue; if(h=="9")return;
            if(h=="3"){
                if(!p.Inventory.Any(i=>i.Name=="Tape 5")) p.Inventory.Add(ItemCatalog.Tape5);
                p.AddGold(30); int xp=rng.Next(3,13); player.GainExperience(xp);
                Console.WriteLine("Mirrors align; a hatch yawns wide. (+30 g, +{0} XP)", xp); Pause(); break;
            }
            Console.WriteLine("Nothing shifts — perhaps another hour."); Pause();
        }
        var r=scenes[new SceneID(5,1)];
        r.Choices.Clear();
        r.Choices[1]=new SceneID(6,0);
        r.Choices[5]=new SceneID(0,0);
        r.Choices[9]=new SceneID(5,0); ShopKey(r);
        Console.Clear();
        Console.WriteLine("1) Descend the iron ladder\n5) Inventory   6) Merchant");
    });

    /*──────────────────  ACT-6  Sigil  ──────────────────*/
    scenes[new SceneID(6,0)] = new(
        "Five glyphs glow on the inner wall:  △   □   ○   ◇   ★\n" +
        "Their pale light paints drifting dust.\n\n" +
        "1) Touch a glyph   5) Inventory");
    scenes[new SceneID(6,0)].Choices[1]=new SceneID(6,1);
    scenes[new SceneID(6,0)].Choices[5]=new SceneID(0,0);
    scenes[new SceneID(6,0)].Choices[9]=new SceneID(5,1); ShopKey(scenes[new SceneID(6,0)]);

    scenes[new SceneID(6,1)] = new(string.Empty,p=>{
        while(true){
            Console.Write("Which glyph keeps perfect symmetry?  (name or symbol): ");
            var ans=Console.ReadLine(); if(InvKey(ans,p))continue; if(ans=="9")return;
            if(ans?.Trim().ToLower() is "circle" or "○" or "o"){
                if(!p.Inventory.Any(i=>i.Name=="Tape 6")) p.Inventory.Add(ItemCatalog.Tape6);
                p.AddGold(30); int xp=rng.Next(3,13); player.GainExperience(xp);
                Console.WriteLine(
                    "\nStone rumble echoes; a spiral stair unspools beneath the floor.\n" +
                    "(+30 gold, +{0} XP)", xp); Pause(); break;
            }
            Console.WriteLine("The glyph flares crimson and repels your hand."); Pause();
        }
        var s=scenes[new SceneID(6,1)];
        s.Choices.Clear();
        s.Choices[1]=new SceneID(7,0);
        s.Choices[5]=new SceneID(0,0);
        s.Choices[9]=new SceneID(6,0); ShopKey(s);
        Console.Clear();
        Console.WriteLine("1) Descend the spiral stair\n5) Inventory   6) Merchant");
    });

    /*──────────────────  ACT-7  Four-door Hall  ──────────────────*/
    scenes[new SceneID(7,0)] = new(string.Empty,p=>{
        while(true){
            Console.Clear();
            Console.WriteLine(
                "A cruciform chamber extends in four directions.  Each iron door is\n" +
                "engraved with a faint rune, unreadable even by torch-light.\n\n" +
                "Pick your path:\n" +
                "   1) North door   2) East door   3) South door   4) West door\n" +
                "   5) Inventory   6) Merchant");
            var k=Console.ReadLine();
            if(k=="5"){ p.OpenInventory(); continue; }
            if(k=="6" && storeUnlocked){ current=new SceneID(99,0); return; }
            if(k=="9"){ current=new SceneID(6,1); return; }
            if(!int.TryParse(k,out int d) || d<1 || d>4){ Console.WriteLine("Choose 1-4,5,6,9."); Pause(); continue; }

            RunPopUpArsenal(p);                // one-time shop before the door seals

            var proto=bossPool[rng.Next(bossPool.Count)];
            var foe=new BossEnemy(proto.Name,proto.HP,proto.AttackPower,
                                  proto.spAttackName,proto.specialmove,
                                  proto.Defence,proto.Speed);

            Console.WriteLine($"\nThe chosen door roars shut behind you — {foe.Name} strides forth!");
            Pause();

            while(player.HP>0 && foe.HP>0) StartCombat2(foe);   // fleeing disabled inside StartCombat2

            if(foe.HP<=0 && player.HP>0){
                int xp=rng.Next(6,24); player.GainExperience(xp);
                Console.WriteLine($"Breathless victory. (+{xp} XP)"); Pause();
            }
            if(player.HP<=0) return;           // judgement system handles death

            current=new SceneID(8,0); return;
        }
    });

    /*──────────────────  ACT-8  Plate Room  ──────────────────*/
    scenes[new SceneID(8,0)] = new(
        "A square room of polished stone.  In the centre lie four sunken plates\n" +
        "etched with the numerals 2, 4, 6, and 8.\n\n" +
        "1) Test a stepping sequence on the plates\n" +
        "5) Inventory   6) Merchant");
    scenes[new SceneID(8,0)].Choices[1]=new SceneID(8,1);
    scenes[new SceneID(8,0)].Choices[5]=new SceneID(0,0);
    scenes[new SceneID(8,0)].Choices[9]=new SceneID(7,0); ShopKey(scenes[new SceneID(8,0)]);

    scenes[new SceneID(8,1)] = new(string.Empty,p=>{
        while(true){
            Console.Write("Enter numeric order (example “6 2 4”)  5=Inv  9=Back: ");
            var seq=Console.ReadLine();
            if(seq=="5"){ p.OpenInventory(); continue; }
            if(seq=="9") return;
            if(seq=="6 2 4"){
                int xp=rng.Next(3,13); player.GainExperience(xp);
                Console.WriteLine(
                    "\nWith a thunderous click the plates descend together.  A hidden\n" +
                    "archway slides open, revealing a cool night breeze from beyond.\n" +
                    "(+{0} XP)", xp);
                Pause();
                current=new SceneID(1,1);      // send player back to courtyard loop
                return;
            }
            Console.WriteLine("A surge of energy shocks your legs!"); player.TakeDamage(10); Pause();
        }
    });
    scenes[new SceneID(8,1)].Choices[9]=new SceneID(8,0);

    /* ───────── Travelling Merchant  (scene 99,0) ───────── */
    scenes[new SceneID(99, 0)] = new Scene(string.Empty, p =>
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Travelling Merchant ---\n");
            Console.WriteLine($"Clinking purse: {p.Gold} gold\n");

            Console.WriteLine("1) Water Flask (10 g)");
            Console.WriteLine("2) Speed Boots  (25 g)");
            Console.WriteLine("3) Luck Charm   (25 g)");
            Console.WriteLine("4) Tape 8  – Circle Hint   (40 g)");
            Console.WriteLine("5) Tape 9  – ‘Stake’       (40 g)");
            Console.WriteLine("7) Tape 14 – The Bargain   (75 g)");
            Console.WriteLine("\n6) Leave shop and continue");

            var choice = Console.ReadLine();

            bool bought = choice switch
            {
                "1" => p.SpendGold(10) && p.Inventory.AddOrFalse(ItemCatalog.WaterFlask),
                "2" => p.SpendGold(25) && p.Inventory.AddOrFalse(ItemCatalog.SpeedBoots),
                "3" => p.SpendGold(25) && p.Inventory.AddOrFalse(ItemCatalog.LuckCharm),
                "4" => p.SpendGold(40) && p.Inventory.AddOrFalse(ItemCatalog.Tape8),
                "5" => p.SpendGold(40) && p.Inventory.AddOrFalse(ItemCatalog.Tape9),
                "7" => p.SpendGold(75) && p.Inventory.AddOrFalse(ItemCatalog.Tape14),
                _   => false
            };

            if (choice == "6") break;          // exit the shop loop

            Console.WriteLine(bought
                ? "Merchant: Pleasure doing business."
                : "Merchant: Either you lack coin or already own that.");
            Pause();
        }

        /* jump straight to the forward-scene recorded for whoever opened the shop */
        if (_forward.TryGetValue(lastScene, out SceneID forward))
            current = forward;
        else
            current = lastScene;   // fallback—shouldn’t happen

        /* ensure main loop sees at least ONE choice so it keeps running */
        var shopScene = scenes[new SceneID(99, 0)];
        shopScene.Choices.Clear();
        shopScene.Choices[1] = current;
    });
}

/*──────────────────  Pop-Up Arsenal  ──────────────────*/
private void RunPopUpArsenal(Player p)
{
    Random rng = new();

    /* build a list of (label, action) pairs */
    var raw = new List<(string label, Action pick)>();

    foreach (var w in Equipment.AllWeapons.Where(x => x != Equipment.NoWeapon))
        raw.Add(($"Weapon: {w.Description}", () =>
        {
            p.EquippedWeapon = w;
            Console.WriteLine($"You grip the {w.Description}.");
        }));

    foreach (var a in Equipment.AllArmors.Where(x => x != Equipment.NoArmor))
        raw.Add(($"Armor: {a.Description}", () =>
        {
            p.EquippedArmor = a;
            Console.WriteLine($"You strap on the {a.Description}.");
        }));

    /* ensure every label is unique */
    var stock = new List<(string label, Action pick)>();
    var seen  = new HashSet<string>();
    int index = 1;
    foreach (var item in raw)
    {
        string lbl = item.label;
        while (seen.Contains(lbl))
            lbl = $"{item.label} #{index++}";
        seen.Add(lbl);
        stock.Add((lbl, item.pick));
    }

    /* assign prices */
    var price = new Dictionary<string, int>();
    foreach (var (lbl, _) in stock) price[lbl] = rng.Next(30, 91);

    /* shop loop */
    while (true)
    {
        Console.Clear();
        Console.WriteLine($"--- Pop-Up Arsenal ---    Gold: {p.Gold}\n");
        for (int i = 0; i < stock.Count; i++)
            Console.WriteLine($"{i + 1}) {stock[i].label}   {price[stock[i].label]} g");
        Console.WriteLine("6) Leave shop and continue");

        var sel = Console.ReadLine();
        if (sel == "6") break;

        if (int.TryParse(sel, out int k) && k >= 1 && k <= stock.Count)
        {
            var (lbl, act) = stock[k - 1];
            int cost = price[lbl];
            if (p.SpendGold(cost))
            {
                act();
                Pause();
            }
            else
            {
                Console.WriteLine("You don’t have enough coin."); Pause();
            }
        }
    }
}


        // ─────────────────────────────────────────────────────────────── Craft Methods ────────────────────────────────────────────────

            /// <summary>
            /// Presents the player with weapon‐crafting options and handles material removal + equipping.
            /// </summary>
            private void CraftWeapon(Player player)
            {
                Console.WriteLine("Choose a weapon to craft:");
                Console.WriteLine("1) Rusty Dagger (requires 2× Iron Ore)");
                Console.WriteLine("2) Ember Fang (requires 3× Iron Ore, 1× Fish)");
                Console.WriteLine("3) Storm Splitter (requires 5× Iron Ore)");
                Console.WriteLine("4) Exit Crafting Table");
                Console.WriteLine("5) Forge Rune Blade (requires 2× Iron Ore, 1× Moonstone, 1× Bark)");

                int weaponChoice;
                do
                {
                    Console.Write("Enter choice (1–5): ");
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out weaponChoice) && weaponChoice >= 1 && weaponChoice <= 5)
                        break;
                    Console.WriteLine("Invalid choice. Please try again.");
                } while (true);

                switch (weaponChoice)
                {
                    case 1:
                        // Rusty Dagger
                        if (player.Inventory.Count(i => i.Name == "Iron Ore") >= 2)
                        {
                            for (int i = 0; i < 2; i++)
                                player.Inventory.Remove(player.Inventory.First(item => item.Name == "Iron Ore"));

                            player.EquippedWeapon = Equipment.RustyDagger;
                            Console.WriteLine("You crafted a Rusty Dagger!");
                        }
                        else Console.WriteLine("You need 2× Iron Ore to craft a Rusty Dagger.");
                        break;

                    case 2:
                        // Ember Fang
                        if (player.Inventory.Count(i => i.Name == "Iron Ore") >= 3 &&
                            player.Inventory.Any(i => i.Name == "Fish"))
                        {
                            for (int i = 0; i < 3; i++)
                                player.Inventory.Remove(player.Inventory.First(item => item.Name == "Iron Ore"));
                            player.Inventory.Remove(player.Inventory.First(item => item.Name == "Fish"));

                            player.EquippedWeapon = Equipment.EmberFang;
                            Console.WriteLine("You crafted an Ember Fang!");
                        }
                        else Console.WriteLine("You need 3× Iron Ore and 1× Fish to craft Ember Fang.");
                        break;

                    case 3:
                        // Storm Splitter
                        if (player.Inventory.Count(i => i.Name == "Iron Ore") >= 5)
                        {
                            for (int i = 0; i < 5; i++)
                                player.Inventory.Remove(player.Inventory.First(item => item.Name == "Iron Ore"));

                            player.EquippedWeapon = Equipment.StormSplitter;
                            Console.WriteLine("You crafted a Storm Splitter!");
                        }
                        else Console.WriteLine("You need 5× Iron Ore to craft Storm Splitter.");
                        break;

                    case 4:
                        // Exit
                        Console.WriteLine("Exiting Crafting Table.");
                        break;

                    case 5:
                        // Rune Blade
                        if (player.Inventory.Count(i => i.Name == "Iron Ore") >= 2 &&
                            player.Inventory.Any(i => i.Name == "Moonstone") &&
                            player.Inventory.Any(i => i.Name == "Bark"))
                        {
                            for (int i = 0; i < 2; i++)
                                player.Inventory.Remove(player.Inventory.First(item => item.Name == "Iron Ore"));
                            player.Inventory.Remove(player.Inventory.First(item => item.Name == "Moonstone"));
                            player.Inventory.Remove(player.Inventory.First(item => item.Name == "Bark"));

                            var runeBlade = new Weapon(
                                heavyManaAttack: 0,
                                heavyManaAttackName: "",
                                lightManaAttack: 0,
                                lightManaAttackName: "",
                                speed: 2,
                                strength: 12,
                                Description: "A blade etched with arcane runes.",
                                regularAttack: 20
                            );
                            player.EquippedWeapon = runeBlade;
                            Console.WriteLine("You forge the Rune Blade! It hums with arcane power.");
                        }
                        else Console.WriteLine("You need 2× Iron Ore, 1× Moonstone, and 1× Bark to forge the Rune Blade.");
                        break;
                }
            }

            /// <summary>
            /// Presents the player with armor‐crafting options and handles material removal + equipping.
            /// </summary>
            private void CraftArmor(Player player)
            {
                Console.WriteLine("Choose an armor to craft:");
                Console.WriteLine("1) Barkhide Vest (requires 4× Bark, 2× Fish, 3× Leather)");
                Console.WriteLine("2) Worn Scout Jacket (requires 3× Leather, 1× Fish, 2× Chainmail)");
                Console.WriteLine("3) Iron Scale Mail (requires 5× Iron Ore)");
                Console.WriteLine("4) Croc-Hide Armor (requires 1× Croc Hide, 3× Bark)");
                Console.WriteLine("5) Exit Crafting Table");

                int armorChoice;
                do
                {
                    Console.Write("Enter choice (1–5): ");
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out armorChoice) && armorChoice >= 1 && armorChoice <= 5)
                        break;
                    Console.WriteLine("Invalid choice. Please try again.");
                } while (true);

                switch (armorChoice)
                {
                    case 1:
                        // Barkhide Vest
                        if (player.Inventory.Count(i => i.Name == "Bark") >= 4 &&
                            player.Inventory.Count(i => i.Name == "Fish") >= 2 &&
                            player.Inventory.Count(i => i.Name == "Leather") >= 3)
                        {
                            for (int i = 0; i < 4; i++)
                                player.Inventory.Remove(player.Inventory.First(item => item.Name == "Bark"));
                            for (int i = 0; i < 2; i++)
                                player.Inventory.Remove(player.Inventory.First(item => item.Name == "Fish"));
                            for (int i = 0; i < 3; i++)
                                player.Inventory.Remove(player.Inventory.First(item => item.Name == "Leather"));

                            player.EquippedArmor = Equipment.BarkhideVest;
                            Console.WriteLine("You crafted a Barkhide Vest!");
                        }
                        else Console.WriteLine("You need 4× Bark, 2× Fish, and 3× Leather to craft Barkhide Vest.");
                        break;

                    case 2:
                        // Worn Scout Jacket
                        if (player.Inventory.Count(i => i.Name == "Leather") >= 3 &&
                            player.Inventory.Any(i => i.Name == "Fish") &&
                            player.Inventory.Count(i => i.Name == "Chainmail") >= 2)
                        {
                            for (int i = 0; i < 3; i++)
                                player.Inventory.Remove(player.Inventory.First(item => item.Name == "Leather"));
                            player.Inventory.Remove(player.Inventory.First(item => item.Name == "Fish"));
                            for (int i = 0; i < 2; i++)
                                player.Inventory.Remove(player.Inventory.First(item => item.Name == "Chainmail"));

                            player.EquippedArmor = Equipment.WornScoutJacket;
                            Console.WriteLine("You crafted a Worn Scout Jacket!");
                        }
                        else Console.WriteLine("You need 3× Leather, 1× Fish, and 2× Chainmail to craft Worn Scout Jacket.");
                        break;

                    case 3:
                        // Iron Scale Mail
                        if (player.Inventory.Count(i => i.Name == "Iron Ore") >= 5)
                        {
                            for (int i = 0; i < 5; i++)
                                player.Inventory.Remove(player.Inventory.First(item => item.Name == "Iron Ore"));

                            player.EquippedArmor = Equipment.IronScaleMail;
                            Console.WriteLine("You crafted Iron Scale Mail!");
                        }
                        else Console.WriteLine("You need 5× Iron Ore to craft Iron Scale Mail.");
                        break;

                    case 4:
                        // Croc-Hide Armor
                        if (player.Inventory.Any(i => i.Name == "Croc Hide") &&
                            player.Inventory.Count(i => i.Name == "Bark") >= 3)
                        {
                            player.Inventory.Remove(player.Inventory.First(item => item.Name == "Croc Hide"));
                            for (int i = 0; i < 3; i++)
                                player.Inventory.Remove(player.Inventory.First(item => item.Name == "Bark"));

                            // You can map this to an existing Equipment or define a new one
                            player.EquippedArmor = Equipment.DrakeskinPlate; 
                            Console.WriteLine("You crafted Croc-Hide Armor!");
                        }
                        else Console.WriteLine("You need 1× Croc Hide and 3× Bark to craft Croc-Hide Armor.");
                        break;

                    case 5:
                        // Exit
                        Console.WriteLine("Exiting Crafting Table.");
                        break;
                }
            }


            

            // GameEngine.cs — Run()
            private void Run()
            {
                while (true)
                {
                    // ─ Combat trigger (Act -1) ─
                    if (current.Act == -1)
                    {
                        StartCombat(enemies[0]);
                        current = new SceneID(1, 2);
                        continue;
                    }

                    // ─ Inventory trigger (Act 0, Plot 0) ─
                    if (current.Act == 0 && current.Plot == 0)
                    {
                        if (_skipInventoryOnce)
                        {
                            _skipInventoryOnce = false;
                        }
                        else
                        {
                            player.OpenInventory();

                            if (player.ShouldSaveAndExit)
                            {
                                // save the last story‐scene so we resume there
                                var sd = new SaveData(player, enemies, boss, lastScene);
                                SaveManager.SaveGame("savegame.json", sd);
                                Console.WriteLine("Game saved. Exiting…");
                                Environment.Exit(0);
                            }
                        }

                        // always jump back to the last real scene
                        current = lastScene;
                        continue;
                    }

                    // ─ Enter the story scene ─
                    Scene scene = scenes[current];
                    scene.OnEnter(player);
                    Console.WriteLine(scene.Text);

                    if (scene.Choices.Count == 0)
                        break;  // end if no choices

                    Console.Write("Choose: ");
                    var userInput = Console.ReadLine();

                    if (userInput == "0")
                    {
                        current = lastScene;
                        continue;
                    }

                    if (int.TryParse(userInput, out int choice) && scene.Choices.ContainsKey(choice))
                    {
                        lastScene = current;
                        current   = scene.Choices[choice];
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please try again.");
                        Thread.Sleep(1000);
                    }


                    // Check if player

                    // update KnownItems for spells
                    ItemCatalog.KnownItems.AddRange(player.Inventory);

                    // regenerate Mana
                    if (player.Mana < player.MaxMana)
                    {
                        for (int i = 0; i < 3 && player.Mana < player.MaxMana; i++)
                        {
                            Thread.Sleep(1000);
                            player.Mana = Math.Min(player.MaxMana, player.Mana + 2);
                        }
                        if (player.Mana >= player.MaxMana)
                            Console.WriteLine("Your Mana has been fully restored\n");
                    }
                }
            }




// GameEngine.cs  –  replace the entire StartCombat method
private bool StartCombat(Enemy enemy)
{
    SceneID beforeDeathScene = current;

    Console.WriteLine($"A wild {enemy.Name} appears!\n");

    int  instanceSpeed  = player.Speed + player.EquippedWeapon.Speed + player.EquippedArmor.Speed;
    bool playerGoesFirst = instanceSpeed >= enemy.Speed;
    int  round           = 0;

    while (player.HP > 0 && enemy.HP > 0)
    {
        if (playerGoesFirst)
        {
            float dmg = PlayerAction(round, out _, out bool fled);
            if (fled) return false;              // flee ⇒ no reward

            enemy.HP -= Math.Max(0, dmg - enemy.Defence);
            if (enemy.HP <= 0) break;

            EnemyAction(enemy);
            round++;
        }
        else
        {
            EnemyAction(enemy);
            if (player.HP <= 0) break;

            float dmg = PlayerAction(round, out _, out bool fled);
            if (fled) return false;

            enemy.HP -= Math.Max(0, dmg - enemy.Defence);
            round++;
        }
    }

    /* ---------- player died ---------- */
    if (player.HP <= 0)
    {
        Console.WriteLine("You have been defeated...\n");
        if (!Judgement(player, ref current, beforeDeathScene))
        {
            current = new SceneID(-1, 0);   // true death
            return false;
        }
        return false;                      // revival / gods’ board: battle ends
    }

    /* ---------- victory ---------- */
    Console.WriteLine($"You defeated the {enemy.Name}!\n");
    return true;                           // caller awards loot
}

        
private bool StartCombat2(BossEnemy boss)
{
    SceneID beforeDeathScene = current;

    Console.WriteLine($"A wild {boss.Name} appears!\n");

    int  instanceSpeed   = player.Speed + player.EquippedWeapon.Speed + player.EquippedArmor.Speed;
    bool playerGoesFirst = instanceSpeed >= boss.Speed;
    int  round           = 0;
    Random rng           = new Random();

    while (player.HP > 0 && boss.HP > 0)
    {
        if (playerGoesFirst)
        {
            float dmg = PlayerAction(round, out bool heavy, out bool fled);
            if (fled) return false;                           // fled ⇒ no reward

            // Boss has 25 % dodge vs non-heavy mana
            bool dodged = !heavy && rng.Next(100) < 25;
            if (!dodged) boss.HP -= Math.Max(0, dmg - boss.Defence);
            else Console.WriteLine($"{boss.Name} dodged!");

            if (boss.HP <= 0) break;

            BossEnemyAction(boss);
            round++;
        }
        else
        {
            BossEnemyAction(boss);
            if (player.HP <= 0) break;

            float dmg = PlayerAction(round, out bool heavy, out bool fled);
            if (fled) return false;

            bool dodged = !heavy && rng.Next(100) < 25;
            if (!dodged) boss.HP -= Math.Max(0, dmg - boss.Defence);
            else Console.WriteLine($"{boss.Name} dodged!");

            round++;
        }
    }

    /* ------------ player died ------------ */
    if (player.HP <= 0)
    {
        Console.WriteLine("You have been defeated...\n");
        if (!Judgement(player, ref current, beforeDeathScene))
        {
            current = new SceneID(-1, 0);      // true death
            return false;
        }
        return false;                          // revival / gods’ board: battle ends
    }

    /* ------------ victory ------------ */
    Console.WriteLine($"You defeated the {boss.Name}!\n");
    return true;                               // caller may drop loot / advance story
}

        

    private float PlayerAction(int round, out bool isHeavyMana, out bool fled)
    {
        isHeavyMana = false;
        fled = false;

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
                Console.WriteLine($"You strike the enemy with {weapon.LightManaAttackName} for {totalDamage} damage!");
                break;

            case "3" when hasManaWeapon && canUseHeavyMana:
                isHeavyMana = true;
                totalDamage = (int)(weapon.HeavyManaAttack * (1 + (instanceSTR / 100.0f)));
                Console.WriteLine($"You unleash {weapon.HeavyManaAttackName} for {totalDamage} damage!");
                break;

            case "4":
                Console.WriteLine("You fled back to the previous scene.\n");
                fled = true;
                return 0;

            default:
                Console.WriteLine("Invalid choice or move unavailable. You fumble your turn.");
                return 0;
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
            int baseChance = 10; // base 10% dodge chance
            int chance = rng.Next(1, 101);

            if (chance <= baseChance + player.Luck)
            {
                Console.WriteLine("You dodged the attack!");
                return;
            }
            Console.WriteLine("Dodge!?!!");
            incomingDamage *= 1.5f;
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
        private static SceneID current = new SceneID(1, 1); // or your intro scene
        static void Main()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=== Jungle Survival RPG ===");
                    Console.WriteLine("1) New Game");
                    Console.WriteLine("2) Load Game");
                    Console.WriteLine("3) Exit");
                    Console.Write("Choice: ");
                    var choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        var engine = new GameEngine();
                        engine.StartNew();
                        break;
                    }
                    else if (choice == "2")
                    {
                        const string path = "savegame.json";
                        if (!File.Exists(path))
                        {
                            Console.WriteLine("No save file found. Press any key to return.");
                            Console.ReadKey(true);
                            continue;
                        }
                        var data   = SaveManager.LoadGame(path);
                        var engine = new GameEngine();
                        engine.StartFromSave(data);
                        break;
                    }
                    else if (choice == "3")
                    {
                        return;
                    }
                }
            }
    }
}
