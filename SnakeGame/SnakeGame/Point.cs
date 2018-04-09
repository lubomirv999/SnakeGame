namespace SnakeGame
{
    public class Point
    {
        public Point(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Point)
            {
                Point other = (Point)obj;
                return this.Row == other.Row && this.Col == other.Col;
            }

            return false;
        }
    }
}