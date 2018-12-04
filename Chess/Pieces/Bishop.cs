using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(int row, int col, bool color) : base(row, col, color) { }

        public override List<Coord> MovePositions()
        {
            List<Coord> positions = new List<Coord>();

            for (int i = 1; i < 8; i++)
            {
                if (Position.Row + i <= 8 && Position.Column + i <= 8)
                    positions.Add(new Coord(Position.Row + i, Position.Column + i));
                if (Position.Row - i > 0 && Position.Column + i <= 8)
                    positions.Add(new Coord(Position.Row - i, Position.Column + i));
                if (Position.Row + i <= 8 && Position.Column - i > 0)
                    positions.Add(new Coord(Position.Row + i, Position.Column - i));
                if (Position.Row - i > 0 && Position.Column - i > 0)
                    positions.Add(new Coord(Position.Row - i, Position.Column - i));
            }

            return positions;
        }
    }
}
