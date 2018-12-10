using System.Collections.Generic;
using System.Windows;

namespace Chess.Pieces
{
    public class Knight : Piece
    {
        private List<Point> offsets = new List<Point>()
        {
            new Point(-1,2),
            new Point(1,2),
            new Point(2,1),
            new Point(2,-1),
            new Point(1,-2),
            new Point(-1,-2),
            new Point(-2,-1),
            new Point(-2,1)
        };

        public Knight(int row, int col, PieceColor color) : base(row, col, color) { }

        public override List<Movement> MovePositions(Piece[,] board)
        {
            List<Movement> positions = new List<Movement>();

            //foreach(Point p in offsets)
            //{
            //    int row = Position.Row + (int)p.Y;
            //    int col = Position.Column + (int)p.X;

            //    if (row < 1 || row > 8 || col < 1 || col > 8)
            //        continue;

            //    positions.Add(new Coord(row, col));
            //}

            return positions;
        }
    }
}
