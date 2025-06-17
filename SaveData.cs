// SaveData.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

namespace JungleSurvivalRPG
{
    public class SaveData
    {
        public PlayerData      Player       { get; set; }
        public List<EnemyData> Enemies      { get; set; }
        public List<BossData>  Bosses       { get; set; }
        public SceneID         ResumeScene  { get; set; }

        public SaveData() { }

        // Call this when saving: pass in the one scene you want to pick back up in.
        public SaveData(Player player, List<Enemy> enemies, List<BossEnemy> bosses, SceneID resume)
        {
            Player      = new PlayerData(player);
            Enemies     = enemies.Select(e => new EnemyData(e)).ToList();
            Bosses      = bosses.Select(b => new BossData(b)).ToList();
            ResumeScene = resume;
        }
    }

    public class PlayerData
    {
        public string   Name            { get; set; }
        public float    HP              { get; set; }
        public float    Defence         { get; set; }
        public int      Luck            { get; set; }
        public float    Mana            { get; set; }
        public float    MaxMana         { get; set; }
        public int      Speed           { get; set; }
        public int      Strength        { get; set; }
        public int      Experience      { get; set; }
        public List<string> UnlockedSpells { get; set; }
        public string   EquippedWeapon  { get; set; }
        public string   EquippedArmor   { get; set; }
        public List<string> Inventory    { get; set; }
        public bool     HasFoundPage2   { get; set; }

        public PlayerData() { }

        public PlayerData(Player p)
        {
            Name           = p.Name;
            HP             = p.HP;
            Defence        = p.Defence;
            Luck           = p.Luck;
            Mana           = p.Mana;
            MaxMana        = p.MaxMana;
            Speed          = p.Speed;
            Strength       = p.Strength;
            Experience     = p.Experience;
            UnlockedSpells = new List<string>(p.UnlockedSpells);
            EquippedWeapon = p.EquippedWeapon.Description;
            EquippedArmor  = p.EquippedArmor.Description;
            Inventory      = p.Inventory.Select(i => i.Name).ToList();
            HasFoundPage2  = p.HasFoundPage2;
        }

        /// <summary>
        /// Rebuild a live Player from these saved fields.
        /// </summary>
        public Player ToPlayer()
        {
            var p = new Player(Name)
            {
                HP            = HP,
                Defence       = Defence,
                Luck          = Luck,
                Mana          = Mana,
                Speed         = Speed,
                Strength      = Strength,
                HasFoundPage2 = HasFoundPage2
            };

            // Restore MaxMana
            float extraMana = MaxMana - p.MaxMana;
            if (extraMana > 0) p.IncreaseMaxMana(extraMana);

            // Restore XP & spells
            int xpGap = Experience - p.Experience;
            if (xpGap > 0) p.GainExperience(xpGap);
            p.UnlockedSpells.Clear();
            p.UnlockedSpells.AddRange(UnlockedSpells);

            // Re-equip by Description lookup
            p.EquippedWeapon = Equipment.AllWeapons
                .FirstOrDefault(w => w.Description == EquippedWeapon)
                ?? Equipment.NoWeapon;
            p.EquippedArmor = Equipment.AllArmors
                .FirstOrDefault(a => a.Description == EquippedArmor)
                ?? Equipment.NoArmor;

            // Restore inventory items by name
            p.Inventory.Clear();
            foreach (var name in Inventory)
            {
                var item = ItemCatalog.All.FirstOrDefault(i => i.Name == name);
                if (item != null)
                    p.Inventory.Add(item);
            }

            return p;
        }
    }

    public class EnemyData
    {
        public string Name        { get; set; }
        public float  HP          { get; set; }
        public int    AttackPower { get; set; }
        public int    Defence     { get; set; }
        public int    Speed       { get; set; }

        public EnemyData() { }
        public EnemyData(Enemy e)
        {
            Name        = e.Name;
            HP          = e.HP;
            AttackPower = e.AttackPower;
            Defence     = e.Defence;
            Speed       = e.Speed;
        }
    }

    public class BossData
    {
        public string Name              { get; set; }
        public float  HP                { get; set; }
        public int    AttackPower       { get; set; }
        public string SpecialAttackName { get; set; }
        public int    SpecialMove       { get; set; }
        public int    Defence           { get; set; }
        public int    Speed             { get; set; }

        public BossData() { }
        public BossData(BossEnemy b)
        {
            Name              = b.Name;
            HP                = b.HP;
            AttackPower       = b.AttackPower;
            SpecialAttackName = b.spAttackName;
            SpecialMove       = b.specialmove;
            Defence           = b.Defence;
            Speed             = b.Speed;
        }
    }

    public static class SaveManager
    {
        private static readonly JsonSerializerOptions _opts = new JsonSerializerOptions { WriteIndented = true };

        public static void SaveGame(string path, SaveData data)
        {
            var json = JsonSerializer.Serialize(data, _opts);
            File.WriteAllText(path, json);
        }

        public static SaveData LoadGame(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<SaveData>(json, _opts)
                ?? throw new InvalidOperationException("Failed to deserialize save data.");
        }
    }
}
