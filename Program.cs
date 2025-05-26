using System;
using System.Collections.Generic;
using System.Threading;

namespace JungleSurvivalRPG
{
    public class ChestArmor
    {
        public int Defence { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Luck { get; set; }

        public ChestArmor(int defence, int strength, int speed, int luck)
        {
            Defence = defence;
            Strength = strength;
            Speed = speed;
            Luck = luck;
        }
    }

    public class WaistArmor
    {
        public int Defence { get; set; }
        public int Endurance { get; set; }
        public int Speed { get; set; }
        public int Luck { get; set; }

        public WaistArmor(int defence, int endurance, int speed, int luck)
        {
            Defence = defence;
            Endurance = endurance;
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
        public string HeavyAttackDescription { get; set; }

        public Weapon(int heavyManaAttack, string heavyManaAttackName, int lightManaAttack, string lightManaAttackName, int speed, int strength, string heavyAttackDescription)
        {
            HeavyManaAttack = heavyManaAttack;
            HeavyManaAttackName = heavyManaAttackName;
            LightManaAttack = lightManaAttack;
            LightManaAttackName = lightManaAttackName;
            Speed = speed;
            Strength = strength;
            HeavyAttackDescription = heavyAttackDescription;
        }
    }

public static Weapon EmberFang = new Weapon(10, "Flame Bite", 5, "Spark Snap", 3, 4, "A basic iron sword enchanted with weak fire magic. Ideal for beginner adventurers.");
public static Weapon StormSplitter = new Weapon(25, "Thunder Slash", 15, "Static Arc", 5, 10, "A steel longsword that crackles with electrical energy. Strong against metal-armored foes.");
public static Weapon NightReaver = new Weapon(45, "Void Rend", 20, "Shadow Slice", 7, 16, "Forged in darkness, this blade weakens enemies' vision and lowers their defense briefly.");
public static Weapon DragonspineExecutioner = new Weapon(75, "Infernal Decapitation", 40, "Ember Swipe", 6, 28, "A massive greatsword made from dragon bones. Delivers heavy fire-based damage.");
public static Weapon MirrorEdge = new Weapon(60, "Radiant Slash", 45, "Glint Pierce", 12, 22, "A crystal blade that reflects some light-based magic back at the attacker. Ideal against mages.");
public static Weapon Bloodthirster = new Weapon(70, "Life Leech", 55, "Crimson Swipe", 10, 30, "A cursed weapon that steals a bit of the target's vitality with each strike.");
public static Weapon ChronoFang = new Weapon(65, "Time Rend", 50, "Clock Pierce", 14, 24, "An arcane blade said to distort time. Occasionally delays an enemy’s next move.");
public static Weapon VoidwalkerBlade = new Weapon(80, "Blink Cleave", 60, "Slipstream Cut", 15, 32, "A blade infused with void energy. Allows the wielder to strike from a short distance instantly.");

    


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
        public Scene(string text) { Text = text; }
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

        public Player(string name)
        {
            Name = name;
            HP = 100f;
            Defence = 5f;
            Luck = 1;
            Mana = 20f;
            Speed = 4;
            Strength = 5;
        }
    }

    public class GameEngine
    {
        private Player player = null!;
        private Dictionary<SceneID, Scene> scenes = null!;
        private SceneID current;

        public void Start()
        {
            Printer.PrintSlow("Welcome to Jungle Survival RPG!\n");
            Console.Write("Enter your name: ");
            var input = Console.ReadLine() ?? string.Empty;
            player = new Player(input);
            BuildScenes();
            current = new SceneID(1, 1);
            Run();
        }

        private void BuildScenes()
        {
            scenes = new Dictionary<SceneID, Scene>();
            Printer.PrintSlow("Scenes initialized.\n", 80);
            Printer.PrintSlow("You awaken on a sandy riverbank, the sun beating down. Your memories are hazy; you recall a crashed plane and calls for help that never came.\n", 20);


            var intro = new Scene(
                "You are at the sandy beach.\n" +
                "1) Head into the forest.\n" +
                "2) Follow the river.\n" +
                "3) Climb the outcrop.");
            intro.Choices[1] = new SceneID(1, 2);
            intro.Choices[2] = new SceneID(1, 3);
            intro.Choices[3] = new SceneID(1, 4);
            scenes[new SceneID(1, 1)] = intro;

            var forest = new Scene(
                "Thick vines surround you in the forest. You hear a growl nearby.\n" +
                "1) Explore the cave.\n" +
                "2) Track the sound.");
            forest.Choices[1] = new SceneID(1, 5);
            forest.Choices[2] = new SceneID(1, 6);
            scenes[new SceneID(1, 2)] = forest;

            var river = new Scene(
                "Following the river, you find a fresh stream. The water looks safe to drink.\n" +
                "1) Fill water flask.\n" +
                "2) Keep moving.");
            river.Choices[1] = new SceneID(1, 7);
            river.Choices[2] = new SceneID(1, 8);
            scenes[new SceneID(1, 3)] = river;

            var outcrop = new Scene(
                "Atop the outcrop, you see smoke rising on the horizon.\n" +
                "1) Investigate campsite.\n" +
                "2) Return to riverbank.");
            outcrop.Choices[1] = new SceneID(2, 1);
            outcrop.Choices[2] = new SceneID(1, 1);
            scenes[new SceneID(1, 4)] = outcrop;

            var cave = new Scene(
                "You enter a dark cave, the air is cool and damp.\n" +
                "1) Light a torch.\n" +
                "2) Retreat.");
            cave.Choices[1] = new SceneID(1, 9);
            cave.Choices[2] = new SceneID(1, 2);
            scenes[new SceneID(1, 5)] = cave;

            var sound = new Scene(
                "You track the growl and find a panther.\n" +
                "1) Fight.\n" +
                "2) Flee.");
            sound.Choices[1] = new SceneID(3, 1);
            sound.Choices[2] = new SceneID(1, 2);
            scenes[new SceneID(1, 6)] = sound;

            var camp = new Scene(
                "You find an abandoned campsite with useful supplies.\n" +
                "1) Search tents.\n" +
                "2) Move on.\n" +
                "3) Explore deeper into the jungle.");
            camp.Choices[1] = new SceneID(2, 2);
            camp.Choices[2] = new SceneID(1, 1);
            camp.Choices[3] = new SceneID(2, 3);
            scenes[new SceneID(2, 1)] = camp;

            var plane = new Scene(
                "You find the wreckage of a plane. There are some old supplies scattered around.\n" +
                "1) Search for supplies.\n" +
                "2) Leave.");
            plane.Choices[1] = new SceneID(2, 4);
            plane.Choices[2] = new SceneID(1, 1);
            scenes[new SceneID(2, 3)] = plane;
        }
        var torch = new Scene(
            "You light a torch and see ancient carvings on the walls. There is a narrow passage ahead.\n" +
            "1) Enter the passage.\n" +
            "2) Leave the cave.");
        torch.Choices[1] = new SceneID(1, 10);
        torch.Choices[2] = new SceneID(1, 5);
        scenes[new SceneID(1, 9)] = torch;

        var passage = new Scene(
            "The passage leads to a hidden chamber filled with treasure. However, you hear a faint hissing sound.\n" +
            "1) Investigate the treasure.\n" +
            "2) Retreat.");
        passage.Choices[1] = new SceneID(3, 2);
        passage.Choices[2] = new SceneID(1, 9);
        scenes[new SceneID(1, 10)] = passage;

        var pantherFight = new Scene(
            "You engage the panther in a fierce battle. Your survival depends on your strength and speed.\n" +
            "1) Use a heavy attack.\n" +
            "2) Use a quick attack.");
        pantherFight.Choices[1] = new SceneID(3, 3);
        pantherFight.Choices[2] = new SceneID(3, 4);
        scenes[new SceneID(3, 1)] = pantherFight;

        var supplies = new Scene(
            "You search the wreckage and find a first aid kit and some canned food.\n" +
            "1) Take the supplies and leave.\n" +
            "2) Search further.");
        supplies.Choices[1] = new SceneID(1, 1);
        supplies.Choices[2] = new SceneID(2, 5);
        scenes[new SceneID(2, 4)] = supplies;

        var deeperSearch = new Scene(
            "You search deeper into the wreckage and find a map of the jungle.\n" +
            "1) Take the map and leave.\n" +
            "2) Stay and rest.");
        deeperSearch.Choices[1] = new SceneID(1, 1);
        deeperSearch.Choices[2] = new SceneID(2, 6);
        scenes[new SceneID(2, 5)] = deeperSearch;

        var rest = new Scene(
            "You decide to rest in the wreckage. The night is uneventful, and you feel refreshed.\n" +
            "1) Head back to the riverbank.");
        rest.Choices[1] = new SceneID(1, 1);
        scenes[new SceneID(2, 6)] = rest;

        var treasureOutcome = new Scene(
            "As you approach the treasure, a snake lunges at you. You narrowly avoid it and decide to leave the chamber.\n" +
            "1) Return to the cave entrance.");
        treasureOutcome.Choices[1] = new SceneID(1, 5);
        scenes[new SceneID(3, 2)] = treasureOutcome;

        var heavyAttack = new Scene(
            "You deliver a powerful blow to the panther, but it retaliates fiercely. You manage to defeat it, but you are injured.\n" +
            "1) Tend to your wounds and move on.");
        heavyAttack.Choices[1] = new SceneID(1, 2);
        scenes[new SceneID(3, 3)] = heavyAttack;

        var quickAttack = new Scene(
            "You use your speed to outmaneuver the panther and strike it down. You emerge victorious and unscathed.\n" +
            "1) Continue deeper into the forest.");
        quickAttack.Choices[1] = new SceneID(1, 2);
        scenes[new SceneID(3, 4)] = quickAttack;
        

        private void Run()
        {
            while (true)
            {
                var scene = scenes[current];
                Console.WriteLine(scene.Text);
                if (scene.Choices.Count == 0) break;
                Console.Write("Choose: ");
                var input = Console.ReadLine();
                if (!int.TryParse(input, out int choice) || !scene.Choices.ContainsKey(choice))
                {
                    Console.WriteLine("Invalid, try again.\n");
                    continue;
                }
                current = scene.Choices[choice];
                Console.WriteLine();
            }
            Console.WriteLine("The End.");
        }
    }

    public static class Printer
    {
        public static void PrintSlow(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
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