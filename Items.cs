// Items.cs
using System;
using System.Collections.Generic;

namespace JungleSurvivalRPG
{
    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public Action<Player> UseAction { get; }

        public Item(string name, string description, Action<Player> useAction)
        {
            Name = name;
            Description = description;
            UseAction = useAction;
        }

        public void Use(Player player)
        {
            UseAction(player);
        }
    }

    public static class ItemCatalog
    {
        public static readonly Item RadioBatteries = new Item(
            "Batteries",
            "A pair of AA batteries. Might power something.",
            player =>
            {
                Printer.PrintSlow("You pocket the batteries. They might power something.\n");
            }
        );

        public static readonly Item DeadRadio = new Item(
            "Hand-held Radio",
            "A silent hand-held radio that needs Batteries to work.",
            player =>
            {
                if (player.Inventory.Remove(ItemCatalog.RadioBatteries))
                {
                    Console.Write("Powering on ");
                    Printer.ShowProgressBar();
                    Console.WriteLine(
                        "\nA crackling voice cuts through the static:\n" +
                        "\"…survivors, head north to the old research station…\"\n"
                    );
                }
                else
                {
                    Console.WriteLine("Nothing happens – it still needs Batteries.");
                    if (!player.Inventory.Contains(ItemCatalog.DeadRadio))
                    {
                        player.Inventory.Add(ItemCatalog.DeadRadio);
                    }
                }
            }
        );

        public static readonly Item TheBook = new Item(
            "The Book",
            "A mysterious book that contains knowledge about the jungle.",
            player =>
            {
                Printer.PrintSlow(
                    "Journal Guide Page 1:\n" +
                    "1. Always keep your water flask filled.\n" +
                    "2. Use your inventory wisely.\n" +
                    "3. Enemies can be tough, so prepare before combat.\n" +
                    "4. Explore thoroughly to find useful items.\n" +
                    "5. Crafting can help you survive longer.\n"
                );
                Printer.PrintSlow("Press any key to continue to Page 2 or Backspace to exit...\n");
                if (player.HasFoundPage2)
                {
                    Printer.PrintSlow("You have found Page 2. Press any key to fix the book...\n");
                }
                else
                {
                    Console.WriteLine("You haven't found Page 2 yet. Keep exploring the jungle to find it!");
                    return;
                }
                while (true)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Backspace || key == ConsoleKey.Enter)
                        break;
                }
                Printer.ClearScreen();
                Printer.PrintSlow(
                    "Page 2: Resources\n" +
                    "In the jungle, you can find resources like water, food, and medicinal herbs. " +
                    "Knowing how to use these resources effectively is crucial for survival.\n"
                );
            }
        );

        public static readonly Item WaterFlask = new Item(
            "Water Flask",
            "Restores 50 HP.",
            player =>
            {
                player.HP = Math.Min(100f, player.HP + 50);
                Printer.PrintSlow($"{player.Name} restores 50 HP. HP: {player.HP}\n");
            }
        );

        public static readonly Item PoisonBerry = new Item(
            "Poison Berry",
            "Deals 50 HP damage.",
            player =>
            {
                player.HP = Math.Max(0f, player.HP - 50);
                Printer.PrintSlow($"{player.Name} takes 50 poison damage. HP: {player.HP}\n");
            }
        );

        public static readonly Item ManaPotion = new Item(
            "Mana Potion",
            "Restores 30 Mana.",
            player =>
            {
                player.Mana += 30;
                Printer.PrintSlow($"{player.Name} restores 30 Mana. Mana: {player.Mana}\n");
            }
        );

        public static readonly Item LootBox = new Item(
            "Loot Box",
            "A mysterious box that contains random items.",
            player =>
            {
                Printer.PrintSlow($"{player.Name} opens the Loot Box and finds some items!\n");
                Random random = new Random();
                List<Item> randomItems = new List<Item>();
                for (int i = 0; i < 4; i++)
                {
                    int index = random.Next(ItemCatalog.All.Count);
                    randomItems.Add(ItemCatalog.All[index]);
                }
                foreach (var item in randomItems)
                {
                    player.Inventory.Add(item);
                    Printer.PrintSlow($"You found: {item.Name}!\n");
                }
            }
        );

        public static readonly Item StrengthPotion = new Item(
            "Strength Potion",
            "Increases Strength by 5.",
            player =>
            {
                player.Strength += 5;
                Printer.PrintSlow($"{player.Name}'s Strength increases by 5. Strength: {player.Strength}\n");
            }
        );

        public static readonly Item DefenseHerb = new Item(
            "Defense Herb",
            "Increases Defence by 3.",
            player =>
            {
                player.Defence += 3;
                Printer.PrintSlow($"{player.Name}'s Defence increases by 3. Defence: {player.Defence}\n");
            }
        );

        public static readonly Item SpeedBoots = new Item(
            "Speed Boots",
            "Increases Speed by 2.",
            player =>
            {
                player.Speed += 2;
                Printer.PrintSlow($"{player.Name}'s Speed increases by 2. Speed: {player.Speed}\n");
            }
        );

        public static readonly Item LuckCharm = new Item(
            "Luck Charm",
            "Increases Luck by 2.",
            player =>
            {
                player.Luck += 2;
                Printer.PrintSlow($"{player.Name}'s Luck increases by 2. Luck: {player.Luck}\n");
            }
        );

        public static readonly Item Rat = new Item(
            "Rat",
            "A small, quick creature that can be found in the jungle.",
            player =>
            {
                Printer.PrintSlow($"{player.Name} is scratched by the Rat.\n");
                player.HP = Math.Max(0f, player.HP - 10);
            }
        );

        public static readonly Item EmptyWaterFlask = new Item(
            "Empty Water Flask",
            "A flask that once contained water, now empty.",
            player =>
            {
                Printer.PrintSlow($"{player.Name} found no water :c \n");
                player.Inventory.Add(EmptyWaterFlask);
            }
        );

        public static readonly Item TreasureMap = new Item(
            "Treasure Map",
            "A map that leads to a hidden treasure in the jungle.",
            player =>
            {
                Printer.PrintSlow($"{player.Name} studies the Treasure Map. It might lead to something valuable!\n");
                Printer.PrintSlow(
                    "To find the treasure, follow the river's bend,\n" +
                    "Where the ancient tree stands tall and grand.\n" +
                    "Beneath its roots, the secret lies,\n" +
                    "Dig deep and claim your prize!\n"
                );
            }
        );

        public static readonly Item PoisonDagger = new Item(
            "Poison Dagger",
            "A dagger coated with a deadly poison that deals 20 damage over time.",
            player =>
            {
                player.ApplyPoison(20, 5);
                Printer.PrintSlow(
                    $"{player.Name} uses the Poison Dagger. The target will take 20 poison damage over the next 5 turns.\n"
                );
            }
        );

        public static readonly Item Fish = new Item(
            "Fish",
            "A fresh fish that can be eaten for health. Beware, it's quite raw",
            player =>
            {
                Console.WriteLine($"{player.Name} You have eaten the poor fish.\n");
                player.HP = Math.Min(100f, player.HP + 10);
                Random random = new Random();
                if (random.Next(0, 100) < (20 - player.Luck * 2)) // 20% chance minus luck factor
                {
                    Console.WriteLine($"{player.Name} feels sick from the fish! You take 5 damage.\n");
                    player.HP = Math.Max(0f, player.HP - 5);
                }
                else
                {
                    Console.WriteLine($"{player.Name} feels fine after eating the fish.\n");
                }
            }
        );

        public static readonly Item CookedFish = new Item(
            "Cooked Fish",
            "A delicious cooked fish that restores 30 HP.",
            player =>
            {
                Printer.PrintSlow($"{player.Name} You have eaten the cooked fish.\n");
                player.HP = Math.Min(100f, player.HP + 30);
            }
        );

        public static readonly Item Grimwore = new Item(
            "Grimwore",
            "A mysterious item that has ancient spells inscribed on it.",
            player =>
            {
                int experience = player.Experience;

                if (experience >= 0)
                {
                    player.UnlockSpell("Healing");
                    player.UnlockSpell("Flame");
                }
                if (experience >= 30)
                {
                    player.UnlockSpell("Manifestation");
                    player.UnlockSpell("Sin of Gluttony");
                }
                if (experience >= 50)
                {
                    player.UnlockSpell("Sin of Greed");
                    player.UnlockSpell("Sin of Sloth");
                }
                if (experience >= 70)
                {
                    player.UnlockSpell("Sin of Lust");
                }
                if (experience >= 100)
                {
                    player.UnlockSpell("Earthquake");
                    player.UnlockSpell("Tornado");
                }
                if (experience >= 120)
                {
                    player.UnlockSpell("Fireball");
                    player.UnlockSpell("Lightning Strike");
                }

                Printer.PrintSlow($"{player.Name} feels energized by the mana.\n");
                if (!player.Inventory.Contains(ItemCatalog.Grimwore))
                    player.Inventory.Add(ItemCatalog.Grimwore);

                Printer.PrintSlow("Unlocked Spells:\n");
                foreach (var spell in player.UnlockedSpells)
                {
                    Printer.PrintSlow($"- {spell}\n");
                }
            }
        );

        public static readonly List<Item> All = new List<Item>
        {
            WaterFlask,
            PoisonBerry,
            ManaPotion,
            StrengthPotion,
            DefenseHerb,
            SpeedBoots,
            LuckCharm,
            Rat,
            EmptyWaterFlask,
            TreasureMap,
            PoisonDagger,
            TheBook,
            Fish,
            CookedFish,
            LootBox,
            Grimwore,
            RadioBatteries,
            DeadRadio
        };

        // Add KnownItems so GameEngine can use it
        public static readonly List<Item> KnownItems = new List<Item>();
    }
}
