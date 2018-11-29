using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public class Queen : Piece
    {
        public Queen(int row, int col, bool color) : base(row, col, color) { }

        public override List<Coord> MovePositions()
        {
            throw new NotImplementedException();
        }
    }
}
