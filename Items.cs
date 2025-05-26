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
        public static readonly Item WaterFlask = new Item(
            "Water Flask",
            "Restores 50 HP.",
            player =>
            {
                player.HP = Math.Min(100f, player.HP + 50);
                Printer.PrintSlow($"{player.Name} restores 50 HP. HP: {player.HP}\n");
            });

        public static readonly Item PoisonBerry = new Item(
            "Poison Berry",
            "Deals 50 HP damage.",
            player =>
            {
                player.HP = Math.Max(0f, player.HP - 50);
                Printer.PrintSlow($"{player.Name} takes 50 poison damage. HP: {player.HP}\n");
            });

        public static readonly Item ManaPotion = new Item(
            "Mana Potion",
            "Restores 30 Mana.",
            player =>
            {
                player.Mana = player.Mana + 30;
                Printer.PrintSlow($"{player.Name} restores 30 Mana. Mana: {player.Mana}\n");
            });

        public static readonly Item StrengthPotion = new Item(
            "Strength Potion",
            "Increases Strength by 5.",
            player =>
            {
                player.Strength += 5;
                Printer.PrintSlow($"{player.Name}'s Strength increases by 5. Strength: {player.Strength}\n");
            });

        public static readonly Item DefenseHerb = new Item(
            "Defense Herb",
            "Increases Defence by 3.",
            player =>
            {
                player.Defence += 3;
                Printer.PrintSlow($"{player.Name}'s Defence increases by 3. Defence: {player.Defence}\n");
            });

        public static readonly Item SpeedBoots = new Item(
            "Speed Boots",
            "Increases Speed by 2.",
            player =>
            {
                player.Speed += 2;
                Printer.PrintSlow($"{player.Name}'s Speed increases by 2. Speed: {player.Speed}\n");
            });

        public static readonly Item LuckCharm = new Item(
            "Luck Charm",
            "Increases Luck by 2.",
            player =>
            {
                player.Luck += 2;
                Printer.PrintSlow($"{player.Name}'s Luck increases by 2. Luck: {player.Luck}\n");
            });
        public static readonly Item Rat = new Item(
            "Rat",
            "A small, quick creature that can be found in the jungle.",
            player => Printer.PrintSlow($"{player.Name} encounters a Rat! This is not an item to use.\n"
        )

        public static readonly List<Item> All = new List<Item>
        {
            WaterFlask,
            PoisonBerry,
            ManaPotion,
            StrengthPotion,
            DefenseHerb,
            SpeedBoots,
            LuckCharm
        };
    }
}
