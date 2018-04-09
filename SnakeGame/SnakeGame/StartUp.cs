namespace SnakeGame
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class StartUp
    {
        private static Queue<Point> snake;
        private static Point snakeHead;
        private static int rowVelocity = 0;
        private static int colVelocity = 1;
        private static bool isGameOver = false;

        private static Point apple;
        private static Random randomGenerator = new Random();

        private static int score = 0;

        public static void Main()
        {
            SetupTerminal();
            InitializeScore();
            InitializeSnake();
            SpawnApple();

            while (!isGameOver)
            {
                ReadInput();

                ClearSnake();
                ClearApple();

                UpdateSnake();

                DrawSnake();
                DrawApple();

                Thread.Sleep(100);
            }

            WriteAt(Console.WindowHeight / 2, Console.WindowWidth / 2 - 4, ConsoleColor.Red, "Game Over");
            Console.ReadLine();
        }

        // Initialize environment for the game
        private static void SetupTerminal()
        {
            Console.WindowWidth = 70;
            Console.WindowHeight = 30;
            Console.BufferWidth = Console.BufferWidth;
            Console.BufferHeight = Console.BufferHeight;
            Console.CursorVisible = false;
        }
    
        // Write At Method
        private static void WriteAt(int row, int col, ConsoleColor color, string text)
        {
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = color;
            Console.Write(text);
        }

        // Initialize score
        private static void InitializeScore()
        {
            WriteAt(0, 50, ConsoleColor.Cyan, $"Your score: {score}");
        }

        // Initializing start value for our snake
        private static void InitializeSnake()
        {
            snake = new Queue<Point>();

            for (int i = 1; i <= 5; i++)
            {
                snakeHead = new Point(0, i);
                snake.Enqueue(snakeHead);
            }
        }

        // Drawing the Snake
        private static void DrawSnake()
        {
            foreach (Point p in snake)
            {
                WriteAt(p.Row, p.Col, ConsoleColor.Cyan, "*");
            }
        }

        // Updating the Snake
        private static void UpdateSnake()
        {
            snakeHead = new Point(snakeHead.Row + rowVelocity, snakeHead.Col + colVelocity);

            if (!IsInBounds(snakeHead) || IsInsideSnake(snakeHead))
            {
                isGameOver = true;
                return;
            }

            if (IsAppleEaten(snakeHead))
            {
                SpawnApple();
                score++;
                WriteAt(0, 50, ConsoleColor.Cyan, $"Your score: {score}");
            }
            else
            {
                snake.Dequeue();
            }

            snake.Enqueue(snakeHead);
        }

        // Clear Snake
        private static void ClearSnake()
        {
            foreach (Point p in snake)
            {
                WriteAt(p.Row, p.Col, ConsoleColor.Cyan, " ");
            }
        }

        // Check if snake is in bounds
        private static bool IsInBounds(Point p)
        {
            return p.Row >= 0 && p.Row < Console.WindowHeight && p.Col >= 0 && p.Col < Console.WindowWidth;
        }

        // Read Input
        private static void ReadInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    rowVelocity = -1;
                    colVelocity = 0;
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    rowVelocity = 0;
                    colVelocity = 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    rowVelocity = 1;
                    colVelocity = 0;
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    rowVelocity = 0;
                    colVelocity = -1;
                }
            }
        }


        // Check if is inside snake
        private static bool IsInsideSnake(Point p)
        {
            return snake.Contains(p);
        }

        // Spawn Apple
        private static void SpawnApple()
        {
            var newRow = randomGenerator.Next(Console.WindowHeight);
            var newCol = randomGenerator.Next(Console.WindowWidth);

            apple = new Point(newRow, newCol);
        }

        // Draw Apple
        private static void DrawApple()
        {
            WriteAt(apple.Row, apple.Col, ConsoleColor.Red, "@");
        }

        // Clear Apple
        private static void ClearApple()
        {
            WriteAt(apple.Row, apple.Col, ConsoleColor.Red, " ");
        }

        // Is apple eaten
        private static bool IsAppleEaten(Point p)
        {
            return apple.Row == p.Row && apple.Col == p.Col;
        }
    }
}