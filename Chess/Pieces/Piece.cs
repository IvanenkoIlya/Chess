using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public abstract class Piece
    {
        public Coord Position;
        public bool Color; // true is black

        public Piece(int row, int col, bool color)
        {
            Position = new Coord(row, col);
            Color = color;
        }

        public abstract List<Coord> MovePositions();

        public override string ToString()
        {
            return (Color ? "Black" : "White") + $" {GetType().Name} at " + Position.ToString();
        }
    }
}
