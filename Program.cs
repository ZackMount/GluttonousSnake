using System.Diagnostics;
using System.Text;
using Zack;

Console.Title = "GluttonousSnake";
Console.CursorVisible = false;

while (true)
{
    var Snake = new GluttonousSnake();
    
    Snake.StartPage();
    Snake.Initialize();

    var thread = new Thread(Snake.FixedUpdate);
    thread.Start();

    while (Snake.isAlive)
    {
        if (Snake.isTurning == false)
        {
            var key = Console.ReadKey();
            
            Snake.Direction = key.KeyChar switch
            {
                'w' => Snake.Direction == 1 ? 1 : 0,
                'W' => Snake.Direction == 1 ? 1 : 0,
                's' => Snake.Direction == 0 ? 0 : 1,
                'S' => Snake.Direction == 0 ? 0 : 1,
                'a' => Snake.Direction == 3 ? 3 : 2,
                'A' => Snake.Direction == 3 ? 3 : 2,
                'd' => Snake.Direction == 2 ? 2 : 3,
                'D' => Snake.Direction == 2 ? 2 : 3,
                _ => Snake.Direction
            };

            if (key.KeyChar == ' ') { Snake.Pause(); }
        }
    }
}

