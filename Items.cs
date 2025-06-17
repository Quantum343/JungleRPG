// Items.cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

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
    public static class InventoryExt
    {
        public static bool AddOrFalse(this List<Item> inv, Item item)
        {
            if (inv.Any(i => i.Name == item.Name)) return false;
            inv.Add(item); return true;
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
            "A mysterious book that contains knowledge about the Ruins.",
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
                    Console.WriteLine("You haven't found Page 2 yet. Keep exploring the Ruins to find it!");
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
                    "In the Ruins, you can find resources like water, food, and medicinal herbs. " +
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
                player.Inventory.Add(ItemCatalog.EmptyWaterFlask);
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
            "A small, quick creature that can be found in the Ruins.",
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
            "A map that leads to a hidden treasure in the Ruins.",
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
        public static readonly Item Bark = new Item(
            "Bark",
            "A piece of bark that can be used to craft items.",
            player =>
            {
                Printer.PrintSlow("Its just bark man, what do you want me to say?\n");
                player.Inventory.Add(ItemCatalog.Bark);
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
            "A mysterious tome of Ruins magic—you can cast any unlocked spell from it.",
            player =>
            {
                // keep Grimwore in inventory so it never disappears
                player.Inventory.Add(ItemCatalog.Grimwore);

                // Gather unlocked spells
                var available = SpellCatalog.AllSpells
                    .Where(sp => player.UnlockedSpells.Contains(sp.Name))
                    .ToList();

                if (available.Count == 0)
                {
                    Console.WriteLine(
                        "You haven’t learned any spells yet. Explore more to unlock them!"
                    );
                    return;
                }

                // Navigation loop
                int idx = 0;
                ConsoleKey key;
                do
                {
                    Console.Clear();
                    Console.WriteLine($"-- Spellbook -- Your mana: {player.Mana} --\n");
                    for (int i = 0; i < available.Count; i++)
                    {
                        Console.Write(i == idx ? "> " : "  ");
                        Console.WriteLine(available[i].Name);
                    }

                    var cur = available[idx];
                    Console.WriteLine($"\n{cur.Description}\nCost: {cur.ManaCost} Mana");
                    Console.WriteLine(
                        "\nUse ↑/↓ to navigate, Enter to cast, Backspace to exit."
                    );
                    key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.UpArrow)
                        idx = (idx - 1 + available.Count) % available.Count;
                    else if (key == ConsoleKey.DownArrow)
                        idx = (idx + 1) % available.Count;
                }
                while (key != ConsoleKey.Enter && key != ConsoleKey.Backspace);

                // If they pressed Enter, try to cast
                if (key == ConsoleKey.Enter)
                {
                    var chosen = available[idx];
                    if (player.Mana >= chosen.ManaCost)
                    {
                        player.Mana -= chosen.ManaCost;
                        Printer.PrintSlow($"{player.Name} casts {chosen.Name}!\n");

                    var spell = SpellCatalog.AllSpells.FirstOrDefault(s => s.Name == chosen.Name);
                    if (spell != null)
                        spell.Activate(player);
                    else
                        Printer.PrintSlow("→ The magic book trembles, but nothing happens...\n");

                    }
                    else
                    {
                        Printer.PrintSlow("Not enough Mana to cast that spell.\n");
                    }
                }

                Thread.Sleep(500);
                // Backspace just exits into regular inventory
            }
        );
        
        public static readonly Item SaveAndExit = new Item(
            "Save & Exit",
            "Save your game and quit immediately.",
            player => {
            // mark for save+exit; actual saving happens back in GameEngine
            player.ShouldSaveAndExit = true;
            Printer.PrintSlow("Saving your game and exiting...\n");
            }
        );

        public static readonly Item IronOre = new Item(
            "Iron",
            "A piece of iron that can be used for crafting.",
            player =>
            {
                Printer.PrintSlow("You found some iron. It might be useful for crafting.\n");
                player.Inventory.Add(ItemCatalog.IronOre);
            }
        );

        public static readonly Item Leather = new Item(
            "Leather",
            "A piece of tough leather that can be used for crafting.",
            player =>
            {
                Printer.PrintSlow("You found some leather. It might be useful for crafting.\n");
                player.Inventory.Add(ItemCatalog.Leather);
            }
        );
        public static readonly Item CrocHide = new Item(
            "Croc Hide",
            "Tough hide from a venom-back swamp croc. Used to craft armor.",
            p =>
            {
                Printer.PrintSlow("You flex the hide—it’s incredibly strong.\n");
                p.Inventory.Add(ItemCatalog.CrocHide);   // put it right back
            }
        );

        public static readonly Item AstralLens = new Item(
            "Astral Lens",
            "A crystal lens that reveals hidden glyphs. Keep it to unlock secrets.",
            p =>
            {
                Printer.PrintSlow("Moonlight refracts into shifting sigils.\n");
                p.Inventory.Add(ItemCatalog.AstralLens); // non-consumable
            }
        );

        public static readonly Item GlyphKey = new Item(
            "Glyph Key",
            "Stone shard etched with portal runes. Needed to activate ancient arches.",
            p =>
            {
                Printer.PrintSlow("The runes pulse faintly in your palm.\n");
                p.Inventory.Add(ItemCatalog.GlyphKey);   // stays in inventory
            }
        );
        public static readonly Item Moonstone = new Item(
            "Moonstone",
            "A gem that pulses with lunar energy.",
            p => {
                Printer.PrintSlow("The Moonstone pulses softly.\n");
                p.Inventory.Add(ItemCatalog.Moonstone);
            }
        );

        public static readonly Item RustedKey = new Item(
            "Rusted Key",
            "Opens ancient locks in the wild.",
            p => {
                Printer.PrintSlow("The key is heavy, its teeth worn.\n");
                p.Inventory.Add(ItemCatalog.RustedKey);
            }
        );

        public static readonly Item GlowingGem = new Item(
            "Glowing Gem",
            "Radiant and warm to the touch.",
            p => {
                Printer.PrintSlow("It glows brighter in your hand.\n");
                p.Inventory.Add(ItemCatalog.GlowingGem);
            }
        );

        public static readonly Item ElvenCrystal = new Item(
            "Elven Crystal",
            "A shard of pure elven magic—rare crafting material.",
            p => {
                Printer.PrintSlow("It sings with arcane resonance.\n");
                p.Inventory.Add(ItemCatalog.ElvenCrystal);
            }
        );
        public static readonly Item GoldCoin = new Item(
            "GoldCoin",
            "A shiny piece of gold. It might be valuable.",
            player =>
            {
                Printer.PrintSlow("Shiny.....\n");
                player.Inventory.Add(ItemCatalog.GoldCoin);
            }
        );
        public static readonly Item Recorder = new Item(
            "Recorder",
            "A battered hand-held recorder. Play any Tape you’re carrying.",
            p => {
                Printer.PrintSlow("Nothing to play – select a Tape from your inventory.\n");
                p.Inventory.Add(ItemCatalog.Recorder);        // keep it after use (consumption-hack)
            }
        );

        public static Item MakeTape(string label, string[] rawLines)
        {
            // We capture the tape instance so we can safely re-add it later.
            Item? tapeItem = null;

            tapeItem = new Item(
                label,
                $"A cassette labelled “{label}”.  Use it with a Recorder.",
                p =>
                {
                    /* 0)  Check for a Recorder ------------------------------------- */
                    if (!p.Inventory.Any(i => i.Name == "Recorder"))
                    {
                        Console.WriteLine("You need a Recorder to play this tape.");
                        // Put the tape back if inventory UI removed it
                        if (!p.Inventory.Contains(tapeItem!)) p.Inventory.Add(tapeItem!);
                        return;
                    }

                    /* 1)  Playback with variable speed ---------------------------- */
                    const int BASE = 20;        // base ms/char for Printer.PrintSlow
                    int delay = BASE;

                    void print(string s) => Printer.PrintSlow(s + "\n", delay);

                    foreach (var raw in rawLines)
                    {
                        string line = raw;

                        // opening &nn& tag → increase delay
                        if (line.StartsWith("&"))
                        {
                            int tagEnd = line.IndexOf('&', 1);
                            int extra  = int.Parse(line[1..tagEnd]);
                            delay = BASE + extra;
                            line = line[(tagEnd + 1)..];
                        }

                        // closing $nn$ tag somewhere in line → reset delay after print
                        int close = line.IndexOf('$');
                        if (close >= 0)
                        {
                            int tagEnd = line.IndexOf('$', close + 1);
                            line = line.Remove(close, tagEnd - close + 1);
                            print(line);
                            delay = BASE;        // reset
                        }
                        else
                            print(line);
                    }

                    Printer.PrintSlow("\n—— End of tape ——\n");

                    /* 2)  Return the tape to the player --------------------------- */
                    if (!p.Inventory.Contains(tapeItem!))
                        p.Inventory.Add(tapeItem!);
                });

            return tapeItem;
        }

        // Seven story tapes (feel free to extend / rename)
        public static readonly Item Tape1 = MakeTape("Tape 1 (Act 1)", new[]{
            //  Tag rules:
            //  &nn& slows Printer.PrintSlow by nn ms/char until matching $nn$
            //  BASE speed is 4 ms/char (set in MakeTape)
            "&20&[RECORDER CLICKS] — REWINDING…$20$",

            "&20&Stanly (whisper): Mira, red light’s on.$20$",
            "Dr Mira (low): Keep voices down—John’s ********* again.",

            "&35&John (hoarse): can’t stand the voices… need to ***** them… *** *****…$35$",

            "Lex (engineer): Gate plaque is almost dust.  Riddle verse is barely legible.",
            "Stanly: Read the copy, Mira.",

            "&10&Dr Mira: “I speak without a mouth and hear without ears; I have no body, but I come alive with wind. What am I?”$10$",

            "Lex: Same caretaker trick as the Ossa Ruins—answer’s.",
            "Omar (guard): Copy that.  Rune pedestal’s ready.",

            "&25&John (sudden scream):LET ME OUT—OR I’LL RIP IT OPEN!**$25$",
            "&30&[THUD — John slams his head against stone]$30$",
            "Dr Mira: John, stop!  You’re bleeding—",

            "&25&John: MAKE ***** QUIET!  MAKE *** QUIET!$25$",
            "[SCRAPE] — John grabs a broken ***** *****.",

            "Lex: He’s got a ******, drop it!",
            "[SCUFFLE] — *grunt* —",

            "&30&[METAL CLANG]  — John slices at Lex’s forearm.$30$",
            "Omar: I’m on him—hold still!",
            "[RUSTLE] — rope tightening, muffled shouts —",

            "&20&Dr Mira: He’s ***.  Lex, wrap that arm—pressure.$20$",
            "Lex (tight voice): Just a cut.  **** first, triage later.",

            "Stanly: Stranger, if you hear this, say the answer at the centre rune.",
            "Dr Mira: A bronze key should drop—spiral *****.  Take it; lift needs it.",

            "&35&John (muffled under gag): …eyes in the vines… …*******n…$35$",
            "Stanly: Battery’s ***** , end *****rd.",
            "Dr Mira: Hope this reaches you.  Good luck.",

            "&15&[STATIC]$15$"
        });


        public static readonly Item Tape2 = MakeTape("Tape 2 (Act 2)", new[]{
            "&12&[RECORDER CLICKS] — Solar pack tops us off.  New log begins.$12$",

            "Stanly: We’re through the courtyard gate.  &200&Hall$200$ is vast&100%…$100$ vaulted ceiling… dust like snow.",
            "Omar: Four lanterns ahead, Commander — red, blue, green, yellow.",
            "Lex: They’re old thaumic sconce units, wired to a pressure rune.  Touch the wrong one, you fry.",

            "&25&John (distant mutter): …skin peeling… burn the voices… ***** …$25$",

            "Dr Mira: I’ve scraped the last readable glyphs from the pillars.  Transcribing now:",

            // ——— the riddle, *never* censored ———
            "  “The red lantern lies.”",
            "  “The blue says: \"Green is not safe.\"”",
            "  “The green says: \"I am safe.\"”",
            "  “The yellow says: \"Red lies.\"”",
            "—— End inscription ——",

            "Lex: Classic liar-logic puzzle.  One statement true, others false — or maybe two true…",
            "Stanly: Let’s brute-reason it.  Lanterns wired in series; safe one completes the cold circuit.",

            "&10&Dr Mira (soft): If green says it’s safe and it’s the only truth, then green must be the cold line.$10$",

            "Omar: Fine.  But last time we trusted inscriptions, John nearly gutted Lex.",
            "Lex (dry): Point taken.  We test at range.  Mira, toss a rust-bolt at green.",
            "[CLINK] — metal rebounds, no flare.",
            "Lex: Cold.  Green is safe.",

            "&30&John (gets louder):  MAKE THEM QUIET  *****  MAKE THE LIGHTS DIE *****$30$",
            "[SCRAPE] — John drags chain across the stone.",
            "Omar: Back off!  I said BACK OFF FOR FUCKS SAKE!",
            "[METAL SNAP] — shackle breaks loose.",
            "&20&John:  I’LL RIP OUT THE *** *****  LET IT ALL GO DARK!$20$",
            "[SCUFFLE] — footsteps, grunt, impact against wall.",

            "Stanly (strained): Lex — fuse green.  Omar — restrain him again [sigh]....",
            "Lex: Working.  Cold spark engaged.  Pedestal slot opening.",
            "[CLUNK] — Key drops.",
            "Dr Mira: Bronze key recovered.  Spiral motif matches first gate key.",
            "Stanly: Stranger — if you’re hearing this on playback — ***** is safe.  Use the key beyond.",
            "Lex: Solar pack still at 68 %.  I’ll keep record running till battery dips under 20.",

            // ——— filler chatter while tape remains on ———
            "&15&Dr Mira: Sampled moss spores for later — unusual bioluminescence.$15$",
            "Omar: Hallway branches left and right.  Left smells like ozone, right like rot.",
            "Lex: Picking up residual arc-flashes on the left.  Could be a live mana conduit.",
            "&10&John (hoarse whisper): …they promise silence if I break the green glass… *****…$10$",
            "Dr Mira: John’s fever climbing.  Pulse erratic.",
            "Stanly: We stabilize him first — no more leaps forward blind.",
            "Lex: He was NOT like this when we were abandoned.",
            "Omar: I know.  He was quiet, kept to himself.  This is… something else.",

            "&25&[BATTERY WARNING TONE]$25$",
            "Lex: Pack at 19 %.  Shutting down to conserve.",
            "Stanly: End log.  If this reaches you, proceed with caution.",
            "&12&[RECORDER CLICKS] — STOP$12$"
        });

        public static readonly Item Tape3 = MakeTape("Tape 3", new[]
        {
            "[Recorder clicks · faint echo]",
            "Stanly (whisper):  Day… I’ve lost count.  Lantern sequence solved—green was the key.",
            "We’ve entered a marble gallery.  Two colossal figures flank opposite doors.",
            "",
            "Dr Mira:  Their plaques are intact.  One claims to speak only truth, the other only lies.",
            "Lex, shine the light higher—there’s script across their chests.",
            "",
            "Lex:  Got it.  Symbols match the dialect on Tape 2.  Recording now for later study.",
            "[stone scrape · distant drip]",
            "",
            "Omar (low):  John’s not right.  He keeps staring at the statues like they’re breathing.",
            "",
            "John (hushed mantra):  They know… they know… they know…",
            "",
            "Stanly:  Focus, team.  We’ll test the classic \"ask the liar about the truth-teller\" logic.",
            "Mira, prepare the wording.",
            "",
            "Dr Mira:  Ready—",
            "(if I were to ask the other statue which door leads onward, which would it point to?)",
            "",
            "[silence · faint heartbeat sound]",
            "",
            "Lex:  Both arms just… moved.  They pointed left.",
            "",
            "Stanly:  Then right we go.  Everyone—",
            "",
            "John (shriek):  LIARS!",
            "",
            "[violent scuffle · crunch of stone]",
            "",
            "Omar:  John, NO!",
            "",
            "[impact · statue cracks · rumble]",
            "",
            "Lex:  The truth statue’s splitting—stone dust everywhere!  MOVE!",
            "",
            "Dr Mira (coughing):  He smashed the chest plate—runes dimming.  The other one—",
            "God, its eyes are glowing!",
            "",
            "Stanly:  Retreat!  Through the arch—third chamber ahead!",
            "",
            "[recorder jolts · tape skids]",
            "",
            "Lex (distant):  The tape is loos——",
            "",
            "[static surge · recording ends]"
        });

                /* ────────────────  TAPE 4 • “Clockmaker Descent — Rage-Tick”  ──────────────── */
        public static readonly Item Tape4 = MakeTape("Tape 4", new[]
        {
            "[REC ON · distant ticking]",
            "Stanly:  Lantern trial cleared.  Entering Clockmaker shaft.",
            "Omar (snarl):  Get this shit over with—these echoes crawl under my skin.",
            "Lex:  Dial shadow stutters between hours—fractured gearing.",
            "Dr Mira:  Inscription reads: ‘Twice the hour < thrice the hour three hence.’",
            "John (whisper):  Time bites… time bites…",
            "Omar *bang-BANG* (fist on brass railing):  Shut up, John!",
            "Stanly:  Solve the math.  Three o’clock.",
            "Lex presses III.   *HUGE CLANK*",
            "Mirrors spin; air gets hot.",
            "Omar:  Smell that?  Like cooked blood.",
            "John (gasp) nosebleed begins.",
            "Dr Mira:  Pressure spike.  He’s hemorrhaging!",
            "Omar:  For fuck’s sake—hold him.",
            "Lex:  Hatch opened.  Spiral stair drops into black.",
            "Stanly:  Descend.  Keep tape rolling.",
            "Step… Step… Step…",
            "Omar mutters ‘bullshit clock’ every third step, pounding rail in rhythm.",
            "John screams—eyes leak crimson.",
            "Dr Mira:  He’s stroking his face raw.",
            "Lex:  20 meters left.  Wrist-watch spun an hour in 4 seconds—",
            "Omar:  I swear I’ll smash this damn stair if it twists again!",
            "*STAIR LURCH*",
            "John’s tibia snaps sideways.  **CRACK**.",
            "Omar:  Mother—!  I told you!  (slams fist on wall)   *bang-bang-bang*",
            "Stanly:  Abort descent—climb!",
            "Mirrors reverse; pendulum boom with zero source.",
            "Omar:  Clock!  Eat shit!",
            "*fist clang, metal dents*",
            "Dr Mira:  John bit through tongue; blood sprays.",
            "Lex:  Recorder heating—tape softening.",
            "John wheezes:  ‘Tick-tock—skin unlock—’",
            "Omar repeatedly punching brass post:  BANG!  BANG!  BANG!",
            "Stanly:  Shaft walls bending inward—RUN!",
            "Lex:  Tape spool melting—",
            "[overloaded screech]  Stanly yells:  ‘Clock has TEETH!’",
            "[violent snap - tape tears]",
            "[END OF RECORDING]"
        });

        /* ─────────────── TAPE 5 • “Muffled Symmetry — F-Bomb Geometry”  ─────────────── */
        public static readonly Item Tape5 = MakeTape("Tape 5", new[]
        {
            "[REC START · cloth over mic · banging heard]",
            "Omar (shouting into wall):  Open, you stone asshole!",
            "*BANG-BANG-BANG*",
            "Stanly (hoarse):  Symmetry chamber—five glyphs.  Circle must be key.",
            "Lex:  Writing this down—triangle fails vertical, square diagonal, diamond rotational.",
            "Omar kicks wall.  *THUD*  ‘Circle or I circle-punch you all.’",
            "Dr Mira:  John—God—he’s dripping from ears… joints bending wrong.",
            "John (gurgle):  Choose the zero and bleed clean.",
            "Omar:  Zero this!  (punches glyph at random)  *WRONG—ALARM BUZZ*",
            "Stone needle stabs Omar’s knuckles.  He screams ‘FUCK-FUCK-FUCK!’",
            "Stanly:  Hitting circle!  Stand clear!",
            "*deep rumble*  Floor rotates 90°.",
            "Lex:  Ceiling mirror shows us walking on damn clouds.",
            "Dr Mira sobs:  John’s knees bending backward like a chicken—",
            "Omar:  Smash that bloody glyph again, see if I care!",
            "*BANG*  (he breaks two fingers, keeps punching)",
            "Stanly (gritting):  Passage opening.  MOVE.",
            "John laughs through shredded tongue.",
            "Omar:  Shut your hell-mouth!",
            "*crack*  He slams John’s stretcher against wall—splinters wood.",
            "Lex:  Tape cloth slipping—mic clearer—",
            "Dr Mira (snaps):  Omar STOP!  We need him alive—",
            "Omar pounds corridor frame:  BANG-BANG-BANG.",
            "Stone dust rains.  Mirror shards fall—slice Lex’s forearm.  He yells ‘Shit!’",
            "Stanly:  Keep moving forward before ceiling drops.",
            "Omar:  If another wall closes, I’m ripping its fucking glyph out!",
            "[long march · tape hiss]",
            "John croaks:  ‘Empty symmetry… perfect wound.’",
            "Omar:  Say that again and I’ll perfect YOUR wound!",
            "[static spike · side-B click]",
            "Stanly:  Rest stop.  Omar’s knuckles split to bone.",
            "Omar *bang-bang-bang* smaller fist taps, muttering curses.",
            "Dr Mira wraps his hand.  Swears back.",
            "Lex:  End log before tape melts again.",
            "[tape stops abruptly]"
        });

        /* ─────────────── TAPE 6 • “Doorless Room — Plate-Breaker Rant”  ─────────────── */
        public static readonly Item Tape6 = MakeTape("Tape 6", new[]
        {
            "[REC RESUME · heavy breaths · BANG-BANG-wall hit]",
            "Omar:  Plates numbered 2-4-6-8.  I swear if this room cheats—",
            "Stanly:  Focus.  Hint says six leads.",
            "Dr Mira:  John’s lower legs shattered into dust.  He’s bleeding from pores.",
            "Lex:  He’s laughing through a lipless grin—",
            "John:  Eight completes the circle.  Break the beat, break the beat—",
            "Omar punches plate-8.  *METAL CLANG*  Swears ‘TAKE THAT!’",
            "Stanly:  That wasn’t the sequence—steam hiss—",
            "*scalding jet erupts*",
            "Omar:  FUCK!  My arm!",
            "Dr Mira sprints to valve—burnt skin smell.",
            "Lex:  Use SIX—TWO—FOUR, goddammit!",
            "Omar, half-screamed ‘Do it your damn self!’ *bang* hits wall again.",
            "John cackles, blood bubbles pop from nostrils.",
            "Stanly:  Plate 6 engaged.  *click*  Violation light resets.",
            "Dr Mira:  Plate 2 engaged.  *click*",
            "Lex:  Final plate 4—press!",
            "*deep gear roll*",
            "Room trembles.  Wall slides, exit appears.",
            "Omar:  About bloody time.",
            "John convulses, bones CRUNCH inward like folding straws.",
            "Dr Mira (crying):  His spine just inverted—",
            "Omar slams fist into door lintel:  BANG! ‘MOVE!’",
            "Ceiling dust falls in angry cloud.",
            "Stanly:  We drag John—he’s nothing but red pulp now.",
            "Lex:  His broken legs swing like pendulums—bone shards clicking.",
            "Omar spits blood-tinged saliva onto floor:  ‘Fuck this temple.’",
            "Recorder crackles; smoke from tape motor.",
            "Dr Mira:  Heartbeat faint.  He’s still whispering.",
            "John (wet whisper):  Six-Two-Four… even beats… break the—",
            "Omar bashes recorder casing to shut him up.",
            "*BANG*  Tape skids but keeps rolling.",
            "Stanly:  Corridor ahead.  No doors behind.  Ending log—",
            "Omar (last word):  ‘Screw the doors—let them burn!’",
            "[end recording · motor whine decays]"
        });






        /* ─────────────  TAPE 8 • “Circle Hint – Tension Ignites”  ───────────── */
        public static readonly Item Tape8 = MakeTape("Tape 8", new[]
        {
            "[REC ON · distant glyph hum]",                                            // 01
            "Stanly:  Chamber secure.  Five lit symbols: triangle, square, circle, diamond, star.",// 02
            "Lex (click-click pen):  Reflection test complete—circle stays identical.", // 03
            "Dr Mira:  So *circle* is the key.  Say it clearly for the log.",           // 04
            "Omar (snorts):  Circle, circle, circle.  Happy now?",                      // 05
            "John murmurs: Door 3 we should have gone.",                     // 06
            "&50&Lex (sharply):  Omar, keep sarcasm low.  This mic catches everything.$00$", // 07
            "*CLANG*  (Omar punches wall)  Dust sprinkles down.",                       // 08
            "Omar:  ‘Everything’ includes my fist, professor.",                         // 09
            "Stanly:  Settle down—glyphs brighten when we argue.",                      // 10
            "Dr Mira:  I’m recording: **Answer is the CIRCLE.**  Everyone heard it.",   // 11
            "John whistles a looping tune; ends every bar with the word ‘round’.",      // 12
            "Omar:  John, stop that; sounds like taunting.",                            // 13
            "Lex:  Lantern check—fuel at fifty percent.",                               // 14
            "Stanly:  Activating circle now.",                                          // 15
            "*BWOOM*  Central stair opens.  Wind rushes upward.",                       // 16
            "Dr Mira (steady):  Stairs are wet—watch footing.",                         // 17
            "Omar:  Lex goes first—his pen likes clicking in front.",                   // 18
            "Lex (dry laugh):  After you, wall-puncher.",                               // 19
            "John claps twice—mock applause.  Omar growls, everyone tense.",            // 20
            "Stanly:  Tape eight ends—transition to lower level.",                      // 21
            "[REC OFF]"                                                                 // 22
        });

        /* ─────────────  TAPE 9 • “Stake Room – Panic and Blame”  ───────────── */
        public static readonly Item Tape9 = MakeTape("Tape 9", new[]
        {
            "[REC START · alarm bell]",                                                // 01
            "*THUNK!*  Marble stake fires—pierces Stanly’s side.",                      // 02
            "Stanly (scream):  AAH—keep recording—",                                    // 03
            "Lex:  Hold still!  Barbs everywhere!",                                     // 04
            "Omar:  Wall’s armed again—back away!",                                     // 05
            "Dr Mira:  Pressure on wound—Lex, lantern here!",                           // 06
            "John gasps, then nervous laughter.  Lex snaps:  Shut it, John!",           // 07
            "*Metal creak*  Stake retracts a notch—Stanly groans louder.",              // 08
            "Omar:  We have to MOVE.  Bleeder slows us.",                               // 09
            "Stanly:  I’m not dead—just haul me!",                                      // 10
            "Lex:  Omar, grab his shoulders—on three—",                                 // 11
            "Omar:  If another spear fires, your fancy pen won’t save you, Lex!",       // 12
            "Dr Mira:  Less talk, more lift—ONE—TWO—",                                  // 13
            "*Squelch*  Stake pulls free—Stanly howls—blood hits recorder mic.",        // 14
            "John hisses:  Circle warned us.  You ignored it.",                         // 15
            "Omar (angry):  Shut up about circles!",                                    // 16
            "Lex:  Bandage—Mira?",                                                      // 17
            "Dr Mira:  Using last roll.  No more after this.",                          // 18
            "Omar:  Great.  Maybe Lex will donate pen clicks as stitches.",             // 19
            "&60&Lex (raising voice):  Pen keeps me focused.  Back off.$00$",           // 20
            "Alarm ceases—room quiet except Stanly’s ragged breathing.",                // 21
            "John whispers count of droplets hitting floor—morbid rhythm.",             // 22
            "Dr Mira:  Tape nine log:  Stanly impaled but alive.  Tension high.",       // 23
            "Omar:  Blame sits on Lex.  Lex blames walls.  Walls don’t care.",          // 24
            "Lex:  Next chamber, no surprises—we check every inch.",                    // 25
            "&1500&[REC STOP]$1500$"                                                                // 26


        });

        /* ───────────  TAPE 14 • “The Bargain – Hazy Vault Log”  ─────────── */
        public static readonly Item Tape14 = MakeTape("Tape 14", new[]
        {
            "[REC · echo chamber · rune ticks 60]",                                           // 01
            "Stone Voice:  “Life— for— life.  Will— or —n’t.”  (words distort)",             // 02
            "Lex:  Timer’s live.  60 seconds—make it count.",                                 // 03
            "Dr Mira:  Four of us… one must— ☥ —",                                            // 04
            "Stanly (weak):  Might be— me— I’m already— 𓂀",                                   // 05
            "Omar:  Hell no.  Injured or not he’s more use than *him* (points at John).",     // 06
            "John:  Omar—steady— we can still draw—",                                         // 07
            "*BANG*  Omar slams dagger chip on scale frame.",                                 // 08
            "Omar:  “Draw lots while the clock ****s us?  Move.”",                            // 09
            "Lex:  Calm— portal only accepts *willing* heartbeat.",                           // 10
            "Stone ticks:  50… 49… 48",                                                      // 11
            "Dr Mira breath shaky:  ‘Anyone *volunteer*?’",                                   // 12
            "Stanly groans; blood drip:  *plip* *plip*.",                                     // 13
            "Lex:  Arithmetic says sacrifice least functional.",                              // 14
            "John:  Lex’s math again— ***k your sums!",                                       // 15
            "Stone ticks­­ distort— voices smear into static:  **⛧**",                        // 16
            "&70&Omar (growl):  Tired of chatter.$00$",                                        // 17
            "*SCRAPE*  Omar grabs John’s collar—footsteps scuffle.",                          // 18
            "John:  “Let go!  I’m *not*—willing—”",                                           // 19
            "Lex:  Omar, if he’s unwilling the rune—",                                        // 20
            "Stone ticks jump:  35… 𓃭 … 34",                                                 // 21
            "*GRUNT*  Omar flings John toward pan.  *THUD* metal vibrates.",                  // 22
            "John screaming:  “Not me— *******!”",                                            // 23
            "Rune flashes amber.  Voice garbles:  ‘Un—willing — accepted??’",                 // 24
            "Dr Mira gasps:  “It’s reading heartbeat anyway!”",                               // 25
            "L#######ic count as #######?  We’re about to find out.",                   // 26
            "John pounds pan edge—*clang clang*—timer skips to 20.",                          // 27
            "Stanly from floor:  “Jo#######l— we need this—”",                           // 28
            "John:  “I HOPE circle eats you all!”",                                           // 29
            "Rune sparks ☥ symbols; portal outline flickers half-open.",                      // 30
            "Omar pushes Lex toward portal mouth:  “Move brain-box!”",                        // 31
            "Lex staggers, helps drag Stanly.",                                               // 32
            "Dr Mira ho#########ps after them.",                                        // 33
            "Stone ticks:  12… 11… audio warps—low pitch—",                                   // 34
            "*** s########Still not ****!”",                                      // 35
            "Rune VOICE (clearer):  “Choice… recorded.”",                                     // 36
            "*WHOOSH*  ***** snaps full luminous circle.",                                   // 37
            "**** bolts through first.  Lex & Stanly stumble after him.",                     // 38
            "Dr ##### paus#####ack at John on scale.",                                    // 39
            "John spits at her boots—misses—cries.",                                          // 40
            "Dr Mira########ried … I’m *****.”",                                  // 41
            "##### steps#####al roar ∿∿∿ fades.",                                     // 42
            "Timer hits 00— rune########ps to sub-bass.",                                    // 43
            "John a########## pan sinks one inch.",                                        // 44
            "St#####oice #######ge sea#####ce kept.”",                           // 45
            "Record###########sound warps:  **###**",                              // 46
            "&120&John (distan#######le—circle—”$00$",                      // 47
            "[aud####### 5s]",                                                                // 48
            "Final clunk:  pan drop########, silence.",                              // 49
            "[REC OFF]"                                                                       // 50
        });






























        public static readonly Item GateKey1 = new Item(
            "GateKey1",
            "A bronze key stamped with a spiral rune. It opened the Act 1 gate.",
            _ => {}
        );

        public static readonly Item GateKey2 = new Item(
            "GateKey2",
            "A bronze key stamped with a spiral rune. It opened the Act 2 gate.",
            _ => {}
        );
        
        public static readonly Item GateKey3 = new Item(
            "GateKey3",
            "A bronze key stamped with a spiral rune. It opened the Act 3 gate.",
            _ => {}
        );

        public static readonly Item Gatekey4 = new Item(
            "GateKey4",
            "A bronze key stamped with a spiral rune. It opened the Act 4 gate.",
            _ => {}
        );
        
        public static readonly Item Gatekey5 = new Item(
            "GateKey5",
            "A bronze key stamped with a spiral rune. It opened the Act 5 gate.",
            _ => {}
        );

        public static readonly Item Gatekey6 = new Item(
            "GateKey6",
            "A bronze key stamped with a spiral rune. It opened the Act 6 gate.",
            _ => {}
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
            DeadRadio,
            SaveAndExit,
            Bark,
            Leather,
            IronOre,
            CrocHide,
            AstralLens,
            GlyphKey,
            Moonstone,
            RustedKey,
            GlowingGem,
            ElvenCrystal,
            GoldCoin,
            Recorder,
            Tape1,
            GateKey1,
            Tape2,
            GateKey2

        };

        // Add KnownItems so GameEngine can use it
        public static readonly List<Item> KnownItems = new List<Item>();
    }
}
