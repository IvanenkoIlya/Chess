using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public class Rook : Piece
    {
        public Rook(int row, int col, bool color) : base(row, col, color) { }

        // TODO Blocking not taken into account
        public override List<Coord> MovePositions()
        {
            List<Coord> positions = new List<Coord>();

            for(int i = 1; i < 8; i++)
            {
                if (Position.Row + i <= 8)
                    positions.Add(new Coord(Position.Row + i, Position.Column));
                if (Position.Row - i > 0)
                    positions.Add(new Coord(Position.Row - i, Position.Column));
                if (Position.Column + i <= 8)
                    positions.Add(new Coord(Position.Row, Position.Column + i));
                if (Position.Column - i > 0)
                    positions.Add(new Coord(Position.Row, Position.Column - i));
            }

            return positions;
        }
    }
}
