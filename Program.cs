using System;
using System.Collections.Generic;

namespace JungleSurvivalRPG
{
    // Represents a point in the story with branching choices
    public class PlotNode
    {
        public string Description { get; }
        public Dictionary<string, PlotNode> Choices { get; } = new Dictionary<string, PlotNode>();

        public PlotNode(string description)
        {
            Description = description;
        }

        public void AddChoice(string optionText, PlotNode nextNode)
        {
            Choices[optionText] = nextNode;
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public float HP { get; set; }
        public float Defence { get; set; }
        public int Luck { get; set; }
        public float Mana { get; set; }

        public Player(string name)
        {
            Name = name;
            HP = 100f;
            Defence = 5f;
            Luck = 1;
            Mana = 20f;
        }
    }

    public class GameEngine
    {
        private Player player;
        private PlotNode currentNode;
        private bool isRunning = true;

        public void Start()
        {
            SetupPlayer();
            BuildPlotGraph();
            RunStoryLoop();
            Console.WriteLine("Game Over. Thanks for playing!");
        }

        private void SetupPlayer()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Jungle Survival RPG!");
            Console.Write("Enter your name: ");
            var inputName = Console.ReadLine();
            player = new Player(inputName);
            Console.WriteLine($"Greetings, {player.Name}. Your adventure begins now...\n");
        }

        // Constructs the branching plot graph
        private void BuildPlotGraph()
        {
            var intro = new PlotNode("You awaken on a sandy riverbank, the sun beating down. Your memories are hazy; you recall a crashed plane and calls for help that never came.");
            var forest = new PlotNode("Thick vines surround you in the forest. You hear a growl nearby.");
            var river = new PlotNode("Following the river, you find a fresh stream. The water looks safe to drink.");
            var peak = new PlotNode("Atop the rocky outcrop, you spot smoke rising on the horizon.");

            // Link choices
            intro.AddChoice("Head into the dense forest.", forest);
            intro.AddChoice("Follow the river downstream.", river);
            intro.AddChoice("Climb a rocky outcrop.", peak);

            // Example further branching
            var cave = new PlotNode("Deeper in the forest you find a dark cave. It could shelter you or hide danger.");
            var hunt = new PlotNode("Beside the stream you catch sight of fish. You could try fishing or keep moving.");
            var smoke = new PlotNode("The smoke leads you to an abandoned campsite with useful supplies.");

            forest.AddChoice("Explore a nearby cave.", cave);
            forest.AddChoice("Try to track the growl.", hunt);
            river.AddChoice("Fill your water flask and fish.", hunt);
            river.AddChoice("Keep following the river.", peak);
            peak.AddChoice("Investigate the campsite.", smoke);
            peak.AddChoice("Return to the riverbank.", intro);

            currentNode = intro;
        }

        // Core loop presenting descriptions and handling choices
        private void RunStoryLoop()
        {
            while (isRunning)
            {
                Console.WriteLine(currentNode.Description + "\n");
                if (currentNode.Choices.Count == 0)
                {
                    isRunning = false; // End if no further branches
                    break;
                }

                int i = 1;
                foreach (var choice in currentNode.Choices.Keys)
                {
                    Console.WriteLine($"{i}) {choice}");
                    i++;
                }

                Console.Write("Choose an option: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var idx) && idx > 0 && idx <= currentNode.Choices.Count)
                {
                    var selected = new List<string>(currentNode.Choices.Keys)[idx - 1];
                    currentNode = currentNode.Choices[selected];
                }
                else
                {
                    Console.WriteLine("Invalid choice, try again.\n");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var engine = new GameEngine();
            engine.Start();
        }
    }
}
