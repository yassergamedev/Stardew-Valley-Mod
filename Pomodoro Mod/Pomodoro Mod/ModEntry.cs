using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using GenericModConfigMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Menus;

namespace Pomodoro_Mod
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {

   
       
       
         
        private bool ClickedYet = false;
        /// <summary>The mod configuration from the player.</summary>
        private ModConfig Config;
        private SpriteFont font;
        private bool StartedPomodoro = false;
        private bool onTime = false;
        private bool onRest = false;
        private bool isTimePaused = false;
        private int SessionIndex = 0;
        private bool clickedDefault = false;
        private int originalGameTimeInterval;
        
        public class Session
        {
            public int[] regularItems = {
    0, 2, 16, 246, 313, 314, 315, 316, 317, 318, 319, 320, 321, 882, 883, 884,
    769, 589, 229, 815, 402, 245, 75, 76, 77, 95, 290, 816, 817, 818, 819, 843,
    844, 845, 846, 847, 849, 850, 137, 478, 338, 150, 485, 490, 115, 120, 453,
    879, 288, 102, 297, 535, 893, 894, 895, 176, 180, 86, 326, 710, 270, 718,
    372, 848, 702, 113, 105, 286, 874, 287, 258, 597, 540, 634, 496, 141, 256,
    720, 706
};

            public int[] semiRegularItems = {
    720, 829, 138, 305, 721, 412, 918, 919, 84, 90, 139, 146, 154, 164, 227, 282,
    420, 487, 542, 569, 700, 701, 705, 716, 734, 68, 151, 192, 211, 278, 283, 393,
    396, 398, 406, 421, 555, 556, 586, 638, 708, 926, 148, 576, 259, 264, 408, 593,
    174, 182, 442
};
            public int[] goodItems = {
    66, 82, 88, 109, 117, 119, 121, 123, 130, 136, 144, 198, 207, 219, 244, 247,
    267, 277, 284, 292, 340, 342, 343, 386, 416, 419, 423, 425, 447, 456, 458, 463,
    464, 516, 518, 536, 544, 552, 558, 579, 580, 581, 582, 583, 584, 585, 588, 599,
    613, 614, 635, 704, 717, 726, 796, 798, 820, 821, 822, 823, 824, 825, 826, 827,
    828, 830, 837, 880, 891, 140, 196, 250, 567, 814, 549, 199, 200, 210, 224, 225,
    233, 238, 335, 392, 400, 493, 557, 559, 572, 707, 715, 838, 184, 195, 541, 566,
    729, 834, 727, 223, 376, 636, 637, 91, 149, 165, 202, 209, 214, 234, 240, 257,
    269, 300, 350, 395, 414, 457, 537, 538, 546, 548
};

            public int[] veryGoodItems = {
    563, 564, 573, 587, 692, 699, 725, 795, 921, 274, 280, 281, 344, 397, 733, 239,
    190, 226, 570, 612, 728, 62, 241, 836, 186, 306, 104, 70, 122, 128, 143, 205,
    208, 215, 220, 231, 243, 341, 346, 347, 459, 461, 486, 517, 519, 528, 529, 530,
    545, 551, 554, 561, 575, 694, 698, 724, 839, 859, 860, 861, 862, 863, 888, 903,
    913, 915, 917, 730, 605, 618, 228, 237, 242, 252, 560, 799, 203, 436, 424, 833,
    60, 64, 73
};

            public int[] excellentItems = {
    125, 155, 204, 218, 254, 336, 422, 444, 550, 577, 651, 686, 693, 695, 858, 905,
    232, 266, 604, 611, 904, 607, 308, 562, 732, 595, 106, 108, 158, 197, 206, 212,
    236, 265, 303, 394, 539, 543, 609, 731, 832, 873, 877, 909, 276, 606, 440, 438,
    648, 107, 201, 235, 565, 649, 307, 608, 222
};
            public int[] amazingItems = {
    222, 230, 348, 426, 531, 532, 553, 851, 906, 253, 547, 610, 621, 428, 221, 124,
    161, 213, 251, 349, 351, 445, 578
};
            public int[] greatItems = {
    629, 687, 691, 773, 787, 800, 809, 852, 856, 872, 887, 907, 928, 454, 446, 289,
    533, 534, 430, 162, 520, 72, 268, 807, 69, 628, 835, 160, 899
};
            public int[] ultraItems = {
    126, 127, 337, 630, 633, 645, 680, 682, 772, 775, 901, 902, 432, 159, 521, 522,
    523, 524, 525, 526, 631
};
            public int[] specialItems = {
    632, 898, 413, 74, 527, 801, 810, 811, 373, 437, 460, 797, 417, 910
};

            public int[] legendaryItems = {
    911, 910, 925, 808, 163, 166, 279, 439, 896, 900, 434, 857
};

            public string name;
            public string activity;
            public int time;
            public int restTime;
            public int timeInSeconds;
            public int restTimeInSeconds;
            public int itemId;
            

            public Session(string name, string activity, int time, int restTime, int itemId)
            {
                this.name = name;
                this.activity = activity;
                this.time = time;
                this.restTime = restTime;
                this.restTimeInSeconds = restTime * 60;
                this.timeInSeconds = time * 60;

                switch (itemId)
                {
                    case 0:
                        SelectRandomItemFromArray(regularItems);
                        break;
                    case 1:
                        SelectRandomItemFromArray(semiRegularItems);
                        break;
                    case 2:
                        SelectRandomItemFromArray(goodItems);
                        break;
                    case 3:
                        SelectRandomItemFromArray(veryGoodItems);
                        break;
                    case 4:
                        SelectRandomItemFromArray(excellentItems);
                        break;
                    case 5:
                        SelectRandomItemFromArray(amazingItems);
                        break;
                    case 6:
                        SelectRandomItemFromArray(greatItems);
                        break;
                    case 7:
                        SelectRandomItemFromArray(ultraItems);
                        break;
                    case 8:
                        SelectRandomItemFromArray(specialItems);
                        break;
                    case 9:
                        SelectRandomItemFromArray(legendaryItems);
                        break;
                    default:
                        // Handle other cases or provide a default behavior
                        break;
                }
            }

            // Helper method to select a random item from the given array
            private void SelectRandomItemFromArray(int[] itemArray)
            {
                int length = itemArray.Length;
                int randomIndex = new Random().Next(0, length);
                this.itemId = itemArray[randomIndex];
            }
        }
 
       
        private List<Session> Sessions = new List<Session>
        {
            new Session("Session ", "", 25, 5, 0)
        };
        private Session currentSession = null;

        private static void AddItemToPlayerInventory(int id)
        {
            
            Item itemInstance = new StardewValley.Object(id, 1); // Replace 499 with the ID of an existing item

            // Add the item directly to the player's inventory by ID
            Game1.player.addItemToInventory(itemInstance);
            
        }

        private void UpdateSession()
        {
            SessionIndex += 1;

            // Check if SessionIndex is within the bounds of the Sessions array
            if (SessionIndex >= 0 && SessionIndex < Sessions.Count)
            {
                currentSession = Sessions[SessionIndex];

                // Update HUD for rest time
                hud2 = new HUDMessage($"Rest : {FormatTime(currentSession.restTimeInSeconds)}", Color.Black, 1000000);
                
                Game1.addHUDMessage(hud2);

                // Update HUD for activity time
                hud = new HUDMessage($"{currentSession.activity} : {FormatTime(currentSession.timeInSeconds)}", Color.Black, 1000000);
                
                Game1.addHUDMessage(hud);
                isTimePaused = true;
            }
            else
            {
                isTimePaused = false;
                currentSession = null;
                StartedPomodoro = false;
              
            }
            
        }

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            this.Config = this.Helper.ReadConfig<ModConfig>();
            bool exampleBool = this.Config.ExampleBoolean;
            this.Helper.WriteConfig(this.Config);
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;


            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.OneSecondUpdateTicked += OnOneSecondUpdateTicked;
            originalGameTimeInterval = Game1.gameTimeInterval;

        }
        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            // Check if the time is paused
            if (isTimePaused)
            {
                // Set the game time speed to 0 to effectively freeze time
                Game1.gameTimeInterval = 0;
            }
        
        }
        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            // Subscribe to WorldReady event when the game is launched
            if (Context.IsWorldReady)
            {
                OnWorldReady();
            }
        }

        private void OnWorldReady()
        {
            // World is ready, display text on the screen

        }


        // Create or update the dynamic HUD message in the top left corner with a long duration
        HUDMessage hud;
        HUDMessage hud2;

        private void OnOneSecondUpdateTicked(object sender, OneSecondUpdateTickedEventArgs e)
        {
            if (StartedPomodoro  && !(Game1.activeClickableMenu is GameMenu ))
            {
                if(onTime)
                {
                    
                    currentSession.timeInSeconds -= 1;
                    hud.message = $"{currentSession.activity} : {FormatTime(currentSession.timeInSeconds)}"; // Update text dynamically
                    hud.timeLeft = currentSession.timeInSeconds * 1000;
                    if (currentSession.timeInSeconds <= 0 )
                    {
                        
                        AddItemToPlayerInventory(currentSession.itemId);
                        Game1.playSound("select");
                        isTimePaused = false;
                        onRest = true;
                        onTime = false;
                    }
                }
                if (onRest)
                {
                    
                    currentSession.restTimeInSeconds -= 1;
                    hud2.message = $"Rest : {FormatTime(currentSession.restTimeInSeconds)}"; // Update text dynamically
                    hud2.timeLeft = currentSession.restTimeInSeconds * 1000;
                    if (currentSession.restTimeInSeconds <= 0)
                    {
                       
                        onRest = false;
                        onTime = true;
                        UpdateSession();
                    }
                }
             
            }
        }


        private string FormatTime(float seconds)
        {
            int minutes = (int)(seconds / 60);
            int remainingSeconds = (int)(seconds % 60);
            return $"{minutes:D2}:{remainingSeconds:D2}";
        }





        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private HUDMessage dynamicHUDMessage;

        // Your other code...

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // Ignore if the player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            if (!ClickedYet)
            {
                InitiateMenu();
                ClickedYet = true;
            }

            // Print button presses to the console window
       

         
        }




        private void InitiateMenu()
        {

            var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            // register mod
            configMenu.Register(
          mod: this.ModManifest,
          reset: () =>
          {
              clickedDefault = true;
              Game1.addHUDMessage(new HUDMessage("Canceled Sessions"));
              hud.timeLeft = 0;
              hud2.timeLeft = 0;
              StartedPomodoro = false;
              isTimePaused = false;
          },
         
             save: () =>
             {
                 if(!clickedDefault) { 
            configMenu.Unregister(mod: this.ModManifest);

                int targetSessionCount = this.Config.SessionNum;

         // If Config.SessionNum is higher than the current count of Sessions, add sessions
         while (Sessions.Count < targetSessionCount)
         {
             int nextSessionIndex = Sessions.Count + 1;
             Session newSession = new Session("Session " + nextSessionIndex, "", 25, 5, Sessions.Count + 1);
             Sessions.Add(newSession);
         }

         // If Config.SessionNum is lower than the current count of Sessions, remove sessions
         while (Sessions.Count > targetSessionCount)
         {
             int lastIndex = Sessions.Count - 1;
             Sessions.RemoveAt(lastIndex);
         }
                 SessionIndex = 0;
                 currentSession = Sessions[SessionIndex];
                 
                 if (hud != null)
                 {
                     hud.timeLeft = 0;
                 }
                 if (hud2 != null)
                 {
                     hud2.timeLeft = 0;
                 }
                
                isTimePaused = true;
                 hud2 = new HUDMessage($"Rest : {FormatTime(currentSession.restTimeInSeconds)}", Color.Black, 1000000);
                 Game1.addHUDMessage(hud2);

                 hud = new HUDMessage($"{currentSession.activity} : {FormatTime(currentSession.timeInSeconds)}", Color.Black, 1000000);
                 Game1.addHUDMessage(hud);
                 Game1.addHUDMessage(new HUDMessage("Sessions Started"));

                 onTime = true;
                 StartedPomodoro = true;
      
         InitiateMenu();
                 }
                 else
                 {
                     clickedDefault = false;

                 }
           
             
             }
 );

            configMenu.AddNumberOption(
                   mod: this.ModManifest,
                   name: () => "Sessions ",

                   getValue: () => this.Config.SessionNum,
                   setValue: value => this.Config.SessionNum = (int)value,
                   min: 1,
                   max: 10,
                     fieldId: "Session"
               );



            foreach (var session in Sessions)
            {
                // Add some config options
                configMenu.AddSectionTitle(
                    mod: this.ModManifest,
                    text: () => session.name
                );

                configMenu.AddTextOption(
                    mod: this.ModManifest,
                    name: () => "Activity",
                    getValue: () => session.activity,
                    setValue: value => session.activity = value
                );

                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => "Time",
                    getValue: () => session.time,
                    setValue: value => {session.time = value;
                       session.timeInSeconds = value * 60;
                     },
                    min: 1,
                    max: 45
                );

                configMenu.AddNumberOption(
                    mod: this.ModManifest,
                    name: () => "Rest Time",
                    getValue: () => session.restTime,
                    setValue: value => {
                        session.restTime = value;
                        session.restTimeInSeconds = value*60;
                    },
                    min: 1,
                    max: 10
                );
            }

        }
    }
}