using System;
using System.Collections.Generic;
using System.Linq;

namespace JungleSurvivalRPG
{
    public class Spell
    {
        public int Index { get; }
        public string Name { get; }
        public string Description { get; }
        public int ManaCost { get; }
        public int RequiredXP { get; }
        public Action<Player> Activate { get; }

        public Spell(
            int index,
            string name,
            string description,
            int manaCost,
            int requiredXP,
            Action<Player> activate)
        {
            Index       = index;
            Name        = name;
            Description = description;
            ManaCost    = manaCost;
            RequiredXP  = requiredXP;
            Activate    = activate;
        }
    }

    public static class SpellCatalog
    {
        public static readonly List<Spell> AllSpells = new List<Spell>
        {
            new Spell(
                0,
                "Healing",
                "Restores a moderate amount of HP to the caster.",
                10,
                0,
                player =>
                {
                    if (player.HP >= player.MaxHP)
                    {
                        Printer.PrintSlow($"{player.Name} is already at full health!\n");
                        return;
                    }
                    player.Heal(30f);
                }
            ),

            new Spell(
                1,
                "Flame",
                "Conjures a burst of flame to cook raw fish.",
                12,
                0,
                player =>
                {
                    Printer.PrintSlow($"{player.Name} conjures a flame...\n");
                    for (int i = 0; i < player.Inventory.Count; i++)
                    {
                        var item = player.Inventory[i];
                        if (item.Name == ItemCatalog.Fish.Name)
                        {
                            player.Inventory[i] = ItemCatalog.CookedFish;
                            Printer.PrintSlow($"{player.Name} cooks a fish into a cooked meal!\n");
                            return;
                        }
                    }
                    Printer.PrintSlow($"{player.Name} has no fish to cook!\n");
                }
            ),

            new Spell(
                2,
                "Manifestation",
                "Attempt to manifest an item you know.",
                120,
                0,
                player =>
                {
                    Printer.PrintSlow($"{player.Name} draws energy from the void...\n");
                    var items = ItemCatalog.KnownItems.ToList();
                    if (items.Count == 0)
                    {
                        Printer.PrintSlow("No known items to manifest.\n");
                        return;
                    }
                    Printer.PrintSlow("Known items:\n");
                    for (int i = 0; i < items.Count; i++)
                        Printer.PrintSlow($"{i + 1}: {items[i].Name}\n");

                    Printer.PrintSlow($"Choose (1-{items.Count}): ");
                    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > items.Count)
                    {
                        Printer.PrintSlow("Invalid selection.\n");
                        return;
                    }
                    var chosen = items[choice - 1];
                    if (new Random().NextDouble() < 0.5)
                    {
                        player.Inventory.Add(chosen);
                        Printer.PrintSlow($"Manifested {chosen.Name}!\n");
                    }
                    else
                    {
                        Printer.PrintSlow($"Failed to manifest {chosen.Name}.\n");
                    }
                }
            ),

            new Spell(
                3,
                "Sin of Gluttony",
                "Boost Strength by 2 and heal 5 HP.",
                180,
                100,
                player =>
                {
                    player.Strength += 2;
                    player.Heal(5f);
                    Printer.PrintSlow($"{player.Name} gains +2 STR and +5 HP.\n");
                }
            ),

            new Spell(
                4,
                "Sin of Greed",
                "Temporarily increases Luck by 2.",
                200,
                150,
                player =>
                {
                    player.Luck += 2;
                    Printer.PrintSlow($"{player.Name} feels greed: +2 Luck (temporary)!\n");
                }
            ),

            new Spell(
                5,
                "Sin of Sloth",
                "Regenerate extra Mana for next turns.",
                140,
                50,
                player =>
                {
                    Printer.PrintSlow($"{player.Name} enters a meditative sloth...\n");
                    // TODO: flag for increased regen
                }
            ),

            new Spell(
                6,
                "Mana Meditation",
                "Heal 2 HP to restore 5 Mana.",
                180,
                100,
                player =>
                {
                    player.HP = Math.Max(0f, player.HP - 2f);
                    player.RestoreMana(5f);
                    Printer.PrintSlow($"{player.Name} trades 2 HP for 5 Mana.\n");
                }
            ),

            new Spell(
                9,
                "Enchanting",
                "Increase equipped armor's Strength by 2.",
                125,
                75,
                player =>
                {
                    player.EquippedArmor.Strength += 2;
                    Printer.PrintSlow($"{player.Name}'s armor sparkles: +2 STR!\n");
                }
            ),

            new Spell(
                10,
                "Revival",
                "One-time second life if you die.",
                255,
                200,
                player =>
                {
                    player.HasRevival = true; // assume you've added this flag
                    Printer.PrintSlow($"{player.Name} binds a second chance!\n");
                }
            ),
        };
    }
}