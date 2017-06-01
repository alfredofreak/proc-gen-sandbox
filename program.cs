using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;

namespace proc_gen_sandbox
{
    class Program
    {

        public const int Width = 60;
        public const int Height = 60;

        static void Main(string[] args)
        {
            // Setup the engine and creat the main window.
            SadConsole.Game.Create("cp437.font", Width, Height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Hook the update event that happens each frame so we can trap keys and respond.
            SadConsole.Game.OnUpdate = Update;
                        
            // Start the game.
            SadConsole.Game.Instance.Run();

            //
            // Code here will not run until the game window closes.
            //
        }
        
        private static void Update(GameTime time)
        {
            // Called each logic update.

            // As an example, we'll use the F5 key to make the game full screen
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                SadConsole.Settings.ToggleFullScreen();
            }
        }

        private static void Init()
        {
            // Any custom loading and prep. We will use a sample console for now
            Console startingConsole = new Console(Width, Height);
            FontMaster fontMaster = SadConsole.Global.LoadFont("cp437.font");
            startingConsole.TextSurface.Font = fontMaster.GetFont(SadConsole.Font.FontSizes.One);
            SadRex.Image tempImage = SadRex.Image.Load(TitleContainer.OpenStream("temp.xp"));
            //tempImage.Layers[0].Cells[0].Background and Character
            SadRex.Layer imageLayer = tempImage.Layers[0];
            for (int yPos = 0; yPos < tempImage.Height; yPos++)
            {
                
                for (int xPos = 0; xPos < tempImage.Width; xPos++)
                {
                    //2-D to 1-D coordinate: (yPos*Width) + xPos
                    SadRex.Cell currentCell = imageLayer.Cells[(yPos * Width) + xPos];
                    startingConsole.SetGlyph(xPos, yPos, currentCell.Character, 
                        new Color(
                            currentCell.Foreground.R,
                            currentCell.Foreground.G,
                            currentCell.Foreground.B),
                        new Color(currentCell.Background.R,
                        currentCell.Background.G,
                        currentCell.Background.B));
                }
            }
            /*
            startingConsole.FillWithRandomGarbage();
            startingConsole.Fill(new Rectangle(3, 3, 27, 5), null, Color.Black, 0);
            startingConsole.Print(6, 5, "Hello from SadConsole", ColorAnsi.CyanBright);//*/

            // Set our new console as the thing to render and process
            SadConsole.Global.CurrentScreen = startingConsole;
        }
    }
}
