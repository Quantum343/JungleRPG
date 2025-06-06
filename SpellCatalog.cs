// SpellCatalog.cs
using System.Collections.Generic;

namespace JungleSurvivalRPG
{
    public class Spell
    {
        public int Index { get; }
        public string Name { get; }
        public string Description { get; }
        public int ManaCost { get; }
        // Bool for combat class spell or regular, unchanged throughout the game
        public bool IsCombatSpell { get; set; };

        public Spell(int index, string name, string description, int manaCost, bool isCombatSpell)
        {
            Index = index;
            Name = name;
            Description = description;
            ManaCost = manaCost;
            IsCombatSpell = isCombatSpell;

        }
    }

    public static class SpellCatalog
    {
        public static readonly List<Spell> AllSpells = new List<Spell>
        {
            new Spell(0,  "Healing",         "Restores a moderate amount of HP to the caster or an ally.",                 10, false),
            new Spell(1,  "Flame",           "Conjures a burst of fire that deals medium fire damage to a single enemy.", 12, false),
            new Spell(2,  "Manifestation",   "Summons spectral weapons to strike all enemies, dealing light physical damage.", 15, false),
            new Spell(3,  "Sin of Gluttony", "Drain HP from target: deals damage and heals caster by half that amount.", 180, false),
            new Spell(4,  "Sin of Greed",    "Temporarily steals some of the target’s gold—or in combat, steals stats.", 200, false),
            new Spell(5,  "Sin of Sloth",    "Reduces an enemy’s Speed drastically for a few turns.",                       140, false),
            new Spell(6,  "Sin of Lust",     "Charms a single enemy—forces them to skip their next action.",                220, false),
            new Spell(9,  "Fireball",        "Launches a large fireball at a single target for very high fire damage.",     25, true),
            new Spell(10, "Lightning Strike","Calls down a bolt of lightning on one enemy, dealing very high electric damage and has a chance to stun.", 25, true),
        };
    }
}
