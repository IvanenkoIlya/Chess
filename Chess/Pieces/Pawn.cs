using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(int row, int col, bool color) : base(row, col, color) { }

        public override List<Coord> MovePositions()
        {
            throw new NotImplementedException();
        }
    }
}
