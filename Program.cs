using System;
using System.Collections.Generic;

namespace JungleSurvivalRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new GameEngine();
            engine.Start();
        }
    }

    public class GameEngine
    {
        private Player _player;
        private bool _isRunning = true;

        public void Start()
        {
            InitPlayer();
            ShowIntro();

            while (_isRunning)
            {
                Update();
            }

            Console.WriteLine("Game Over. Thanks for playing!");
        }

        private void InitPlayer()
        {
            Console.WriteLine("Enter your name:");
            var name = Console.ReadLine();
            _player = new Player(name, 100);
        }

        private void ShowIntro()
        {
            Console.WriteLine("You wake up alone in a dense jungle, the sounds of wildlife all around.");
            Console.WriteLine("Branches crack under unseen footsteps. What will you do?");
            Console.WriteLine("1) Stay still and observe\n2) Move in the direction of running water\n3) Climb a tree to look around");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    BranchObserve();
                    break;
                case "2":
                    BranchWater();
                    break;
                case "3":
                    BranchClimb();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    ShowIntro();
                    break;
            }
        }

        private void BranchObserve()
        {
            Console.WriteLine("You remain still and see a panther watching you from the shadows.");
            StartCombat(new Enemy("Panther", 50, 15));
        }

        private void BranchWater()
        {
            Console.WriteLine("You find a stream. You can refill your water but hear insects buzzing.");
            _player.AddItem(new Item("Water Flask"));
            ContinueJourney();
        }

        private void BranchClimb()
        {
            Console.WriteLine("From the treetop, you spot smoke rising in the distance.");
            ContinueJourney();
        }

        private void ContinueJourney()
        {
            Console.WriteLine("Your journey continues...");
            _isRunning = false; // Placeholder to end loop
        }

        private void StartCombat(Enemy enemy)
        {
            Console.WriteLine($"A wild {enemy.Name} appears!\n");
            while (_player.IsAlive && enemy.IsAlive)
            {
                Console.WriteLine("Choose action: 1) Attack 2) Flee");
                var input = Console.ReadLine();
                if (input == "1")
                {
                    enemy.TakeDamage(_player.Attack());
                    if (enemy.IsAlive)
                        _player.TakeDamage(enemy.Attack());
                }
                else if (input == "2")
                {
                    Console.WriteLine("You fled safely.");
                    break;
                }
            }

            if (!enemy.IsAlive)
                Console.WriteLine($"You defeated the {enemy.Name}!");

            _isRunning = false; // Placeholder
        }

        private void Update()
        {
            // Future game loop logic: exploration, events, resource management
        }
    }

    public class Player
    {
        public string Name { get; }
        public int Health { get; private set; }
        public List<Item> Inventory { get; } = new List<Item>();

        public Player(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public int Attack()
        {
            return 10; // Base damage
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            Console.WriteLine($"{Name} takes {amount} damage. Remaining health: {Health}");
        }

        public bool IsAlive => Health > 0;

        public void AddItem(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine($"Picked up: {item.Name}");
        }
    }

    public class Enemy
    {
        public string Name { get; }
        public int Health { get; private set; }
        private readonly int _damage;

        public Enemy(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            _damage = damage;
        }

        public int Attack()
        {
            Console.WriteLine($"{Name} attacks for {_damage} damage.");
            return _damage;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            Console.WriteLine($"{Name} takes {amount} damage. Remaining health: {Health}");
        }

        public bool IsAlive => Health > 0;
    }

    public class Item
    {
        public string Name { get; }

        public Item(string name)
        {
            Name = name;
        }
    }
}
