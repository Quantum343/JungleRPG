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
                "Restores up to 30 HP, not exceeding max.",
                10,
                0,
                player =>
                {
                    if (player.HP >= player.MaxHP)
                    {
                        Printer.PrintSlow($"{player.Name} is already at full health!\n");
                        return;
                    }
                    float healAmount = Math.Min(30f, player.MaxHP - player.HP);
                    player.HP += healAmount;
                    Printer.PrintSlow($"{player.Name} glows with life! (+{healAmount} HP)\n");
                }
            ),

            new Spell(
                1,
                "Flame",
                "Conjures a flame to cook one raw fish in inventory.",
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
                            Printer.PrintSlow($"{player.Name} cooks fish into a cooked meal!\n");
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
                    var items = ItemCatalog.KnownItems.Distinct().ToList();
                    if (!items.Any())
                    {
                        Printer.PrintSlow("No known items to manifest.\n");
                        return;
                    }
                    Printer.PrintSlow("Known items:\n");
                    for (int i = 0; i < items.Count; i++)
                        Printer.PrintSlow($"{i + 1}: {items[i].Name}\n");

                    Printer.PrintSlow($"Choose 1-{items.Count}: ");
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
                "Boost Strength by 2 and heal up to 5 HP.",
                180,
                100,
                player =>
                {
                    player.Strength += 2;
                    float heal = Math.Min(5f, player.MaxHP - player.HP);
                    player.HP += heal;
                    Printer.PrintSlow($"{player.Name} gains +2 STR and +{heal} HP.\n");
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
                    // TODO: schedule a revert of Luck
                }
            ),

            new Spell(
                5,
                "Sin of Sloth",
                "Increase mana capacity.",
                10,
                0,
                player =>
                {
                    Printer.PrintSlow($"{player.Name} enters a meditative state...\n");
                    for (int i = 0; i < 20; i++)
                    {
                        //make backspace cancel
                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Backspace)
                        {
                            Printer.PrintSlow($"{player.Name} cancels meditation.\n");
                            return;

                        }
                        else{
                             if (player.Mana < player.MaxMana)
                            {
                                player.IncreaseMaxMana(1f);
                                Printer.PrintSlow($"{player.Name} gains +1 Mana.\n");
                                Thread.Sleep(500); // Simulate meditation time
                            }
                            else if (player.Mana >= player.MaxMana)
                            {
                                Printer.PrintSlow($"{player.Name} is already at max Mana!\n");
                                break;
                            }
                        }

                    }
                }
            ),

            new Spell(
                6,
                "Mana Meditation",
                "Trade 2 HP for up to 5 Mana.",
                180,
                100,
                player =>
                {
                    float loss = Math.Min(2f, player.HP);
                    player.HP -= loss;
                    float gain = Math.Min(5f, player.MaxMana - player.Mana);
                    player.Mana += gain;
                    Printer.PrintSlow($"{player.Name} trades {loss} HP for {gain} Mana.\n");
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
                    Printer.PrintSlow($"{player.Name} binds a second chance!\n");
                    // TODO: add death-intercept logic in Player
                }
            ),
        };
    }
}
