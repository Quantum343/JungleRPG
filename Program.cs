// Program.cs
using System;
using System.Collections.Generic;
using System.Threading;
using JungleSurvivalRPG;  // for Item, ItemCatalog

namespace JungleSurvivalRPG
{
    public class ChestArmor
    {
        public int Defence { get; set; }
        public int Strength { get; set; }
        public int Speed   { get; set; }
        public int Luck    { get; set; }
        public ChestArmor(int defence, int strength, int speed, int luck)
        {
            Defence = defence; Strength = strength; Speed = speed; Luck = luck;
        }
    }

    public class WaistArmor
    {
        public int Defence { get; set; }
        public int Endurance { get; set; }
        public int Speed   { get; set; }
        public int Luck    { get; set; }
        public WaistArmor(int defence, int endurance, int speed, int luck)
        {
            Defence = defence; Endurance = endurance; Speed = speed; Luck = luck;
        }
    }

    public class Weapon
    {
        public int HeavyManaAttack     { get; set; }
        public string HeavyManaAttackName { get; set; }
        public int LightManaAttack     { get; set; }
        public string LightManaAttackName { get; set; }
        public int Speed               { get; set; }
        public int Strength            { get; set; }
        public string HeavyAttackDescription { get; set; }

        public Weapon(int heavyManaAttack, string heavyManaAttackName,
                      int lightManaAttack, string lightManaAttackName,
                      int speed, int strength, string heavyAttackDescription)
        {
            HeavyManaAttack       = heavyManaAttack;
            HeavyManaAttackName   = heavyManaAttackName;
            LightManaAttack       = lightManaAttack;
            LightManaAttackName   = lightManaAttackName;
            Speed                 = speed;
            Strength              = strength;
            HeavyAttackDescription= heavyAttackDescription;
        }
    }

    public static class Equipment
    {
        public static Weapon EmberFang = new Weapon(
            65, "Flame Bite", 25, "Spark Snap", 3, 4,
            "A basic iron sword enchanted with weak fire magic."
        );
        // … other weapons …
    }

    public class Enemy
    {
        public string Name       { get; }
        public int HP            { get; set; }
        public int AttackPower   { get; }
        public Enemy(string name, int hp, int attackPower)
        {
            Name = name; HP = hp; AttackPower = attackPower;
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
        public string Name       { get; set; }
        public float HP          { get; set; }
        public float Defence     { get; set; }
        public int Luck          { get; set; }
        public float Mana        { get; set; }
        public int Speed         { get; set; }
        public int Strength      { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public List<Item> Inventory   { get; } = new();

        public Player(string name)
        {
            Name = name;
            HP = 100f; Defence = 5f; Luck = 1;
            Mana = 20f; Speed = 4; Strength = 5;
            EquippedWeapon = Equipment.EmberFang;
            Inventory.Add(ItemCatalog.EmptyWaterFlask);
        }

        public int Attack() => EquippedWeapon.Strength;

        public void TakeDamage(int dmg)
        {
            int net = Math.Max(0, dmg - (int)Defence);
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
            Console.WriteLine("Use ↑/↓ to navigate, Enter to use & exit.");

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow)
                idx = (idx - 1 + Inventory.Count) % Inventory.Count;
            else if (key == ConsoleKey.DownArrow)
                idx = (idx + 1) % Inventory.Count;
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
        public static void PrintSlow(string text, int delay = 30)
        {
            foreach (var c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
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
            enemies.Add(new Enemy("Panther", 50, 10));
            enemies.Add(new Enemy("Snake", 30, 5));
        }

        private void BuildScenes()
        {
            scenes = new();

            // Intro
            scenes[new SceneID(1, 1)] = new Scene(
                "You awaken on a sandy riverbank...\n" +
                "1) Head into the forest.\n" +
                "2) Follow the river.\n" +
                "3) Climb the outcrop.\n"
            );
            scenes[new SceneID(1, 1)].Choices[1] = new SceneID(1, 2);
            scenes[new SceneID(1, 1)].Choices[2] = new SceneID(1, 3);
            scenes[new SceneID(1, 1)].Choices[3] = new SceneID(1, 4);

            // Forest
            scenes[new SceneID(1, 2)] = new Scene(
                "Thick vines surround you...\n" +
                "1) Track the growl.\n" +
                "2) Follow the river sounds.\n" +
                "3) Open Inventory.\n"
            );
            scenes[new SceneID(1, 2)].Choices[1] = new SceneID(1, 6);
            scenes[new SceneID(1, 2)].Choices[2] = new SceneID(1, 3);
            scenes[new SceneID(1, 2)].Choices[3] = new SceneID(0, 0);

            // River bank
            scenes[new SceneID(1, 3)] = new Scene(
                "Following the river...\n" +
                "1) Fill your empty water flask.\n" +
                "2) Look for fish\n" +
                "2) Open Inventory.\n"
            );
            scenes[new SceneID(1, 3)].Choices[1] = new SceneID(1, 5);
            scenes[new SceneID(1, 3)].Choices[2] = new SceneID(1, 6);
            scenes[new SceneID(1, 3)].Choices[3] = new SceneID(0, 0);

            // Outcrop
            scenes[new SceneID(1, 4)] = new Scene(
                "Atop the outcrop...\n" +
                "1) Investigate campsite.\n" +
                "2) Open Inventory.\n"
            );
            scenes[new SceneID(1, 4)].Choices[1] = new SceneID(2, 1);
            scenes[new SceneID(1, 4)].Choices[2] = new SceneID(0, 0);

            // Fill Flask scene with OnEnter effect
            scenes[new SceneID(1, 5)] = new Scene(
                "You kneel by the river and pour water into your flask...\n" +
                "1) Continue.\n",
                player =>
                {
                    if (player.Inventory.Remove(ItemCatalog.EmptyWaterFlask))
                    {
                        player.Inventory.Add(ItemCatalog.WaterFlask);
                        Console.WriteLine("You filled your flask! It’s now a full Water Flask.\n");
                    }
                    else
                    {
                        Console.WriteLine("You have no empty flask to fill.\n");
                    }
                }
            );
            scenes[new SceneID(1, 5)].Choices[1] = new SceneID(1, 3);

            // Panther encounter
            scenes[new SceneID(1, 6)] = new Scene(
                "You track the growl to a clearing...\n" +
                "1) Fight the panther.\n" +
                "2) Flee back to the forest.\n" +
                "3) Open Inventory.\n" +
                "4) Climb a tree to escape.\n"
            );
            scenes[new SceneID(1, 6)].Choices[1] = new SceneID(-1, 0);
            scenes[new SceneID(1, 6)].Choices[2] = new SceneID(1, 2);
            scenes[new SceneID(1, 6)].Choices[3] = new SceneID(0, 0);
            scenes[new SceneID(1, 6)].Choices[4] = new SceneID(1, 7);
            // Climbing tree escape ACT 2           
            scenes[new SceneID(1, 7)] = new Scene(
                "You climb a tree to escape the panther...\n" +
                "You find a tree house with a rope ladder leading down.\n" +
                "1) Climb down the rope ladder.\n" +
                "2) Explore the tree house.\n"

            );
            scenes[new SceneID(1, 7)].Choices[1] = new SceneID(2, 2);
            scenes[new SceneID(1, 7)].Choices[2] = new SceneID(2, 3);
        }

        private void Run()
        {
            while (true)
            {
                // Combat trigger (Act -1)
                if (current.Act == -1)
                {
                    StartCombat(enemies[0]);
                    current = new SceneID(1, 2);
                    continue;
                }

                // Inventory trigger (Act 0, Plot 0)
                if (current.Act == 0 && current.Plot == 0)
                {
                    player.OpenInventory();
                    current = lastScene;
                    continue;
                }

                var scene = scenes[current];
                // scene-specific effect
                scene.OnEnter(player);
                // display
                Console.WriteLine(scene.Text);

                if (scene.Choices.Count == 0) break;

                Console.Write("Choose: ");
                var input = Console.ReadLine();
                if (!int.TryParse(input, out int choice) ||
                    !scene.Choices.ContainsKey(choice))
                {
                    Console.WriteLine("Invalid choice, try again.\n");
                    continue;
                }

                lastScene = current;
                current   = scene.Choices[choice];
                Console.WriteLine();
            }

            Console.WriteLine("The End.");
        }

        private void StartCombat(Enemy enemy)
        {
            Console.WriteLine($"A wild {enemy.Name} appears!\n");
            while (player.HP > 0 && enemy.HP > 0)
            {
                Console.WriteLine("1) Attack\n2) Flee\n");
                Console.Write("Action: ");
                var cmd = Console.ReadLine();
                if (cmd == "1")
                {
                    int dmg = player.Attack();
                    enemy.HP -= dmg;
                    Console.WriteLine($"You hit {enemy.Name} for {dmg} damage.\n");
                    if (enemy.HP <= 0) break;
                    player.TakeDamage(enemy.AttackPower);
                }
                else if (cmd == "2")
                {
                    Console.WriteLine("You fled back to the previous scene.\n");
                    return;
                }
            }

            if (player.HP <= 0)
            {
                Console.WriteLine("You have been defeated...\n");
                Environment.Exit(0);
            }

            Console.WriteLine($"You defeated the {enemy.Name}!\n");
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
