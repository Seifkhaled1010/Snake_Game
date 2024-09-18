using Snake_Game;

Coord gridDimentions = new Coord(50, 20);
Coord snakePosition = new Coord(25, 5);
Random rand = new Random();
Coord applePosition = new Coord(rand.Next(1, gridDimentions.X - 1), rand.Next(1, gridDimentions.Y - 1));
Directions movementDirections = Directions.Down;
int frameDelay = 100;
int score = 0;

List<Coord> snakePositionHistory = new List<Coord>();
int tailLength = 1;

while (true)
{
    Console.Clear();
    Console.WriteLine($"Score: {score} ");

    snakePosition.movement(movementDirections);

    for (int y = 0; y < gridDimentions.Y; y++)
        {
	    for (int x = 0; x < gridDimentions.X; x++)
	        {
		        Coord currentCoord = new Coord(x, y);

                if (snakePosition.Equals(currentCoord) || snakePositionHistory.Contains(currentCoord))
                    Console.Write("■");
                else if(applePosition.Equals(currentCoord))
                    Console.Write("*");
                else if (x == 0 || y == 0 || x == gridDimentions.X - 1 || y == gridDimentions.Y - 1)
                    Console.Write("#");
		        else
			        Console.Write(" ");
            }
            Console.WriteLine();
        }

    //Thread.Sleep(frameDelay);

    if(snakePosition.Equals(applePosition))
    {
        tailLength++;
        score++;
        applePosition = new Coord(rand.Next(1, gridDimentions.X - 1), rand.Next(1, gridDimentions.Y - 1)); 
    }
    else if(snakePosition.X == 0 || snakePosition.Y == 0 || snakePosition.X == gridDimentions.X - 1 || snakePosition.Y == gridDimentions.Y - 1 
        || snakePositionHistory.Contains(snakePosition))
    {
        Thread.Sleep(1000);
        Console.WriteLine("Game Over!");
        Thread.Sleep(1500);
        Console.Write("Please press F for new game or D for display your score: ");
        char f = ((char)Console.Read());
        Console.WriteLine();
        if (f == 'f')
        {
            score = 0;
            tailLength = 1;
            snakePosition = new Coord(25, 5);
            snakePositionHistory.Clear();
            movementDirections = Directions.Down;
            continue;
        }
        else if(f == 'd')
        {
            Console.WriteLine($"Total score is: {score}");
            break;
        }
        else
        {
            break;
        }
       
    }

    snakePositionHistory.Add(new Coord(snakePosition.X, snakePosition.Y));
    if (snakePositionHistory.Count > tailLength)
        snakePositionHistory.RemoveAt(0);

    DateTime time = DateTime.Now;

    while((DateTime.Now - time).Milliseconds <= frameDelay)
    {
        if(Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey().Key;

            switch(key)
            {
                case ConsoleKey.LeftArrow:
                    movementDirections = Directions.Left;
                    break;
                case ConsoleKey.RightArrow:
                    movementDirections = Directions.Right;
                    break;
                case ConsoleKey.UpArrow:
                    movementDirections = Directions.Up;
                    break;
                case ConsoleKey.DownArrow:
                    movementDirections = Directions.Down;
                    break;
            }
        }
    }
}