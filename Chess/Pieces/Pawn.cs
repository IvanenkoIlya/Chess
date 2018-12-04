using System.Collections.Generic;
using System.Windows;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        private bool _firstMove;

        public Pawn(int row, int col, bool color) : base(row, col, color)
        {
            _firstMove = true;
        }

        // TODO Does not take into account En passant
        // Conditions for En Passant
        //      1) Pawn must be on 5th rank (fifth row from its side)
        //      2) Opponent must move their pawn two spaces to be adjacent to your pawn
        //      3) Only one shot, one opportunity to capture using En Passant
        // Your Pawn moves diagonally behind the opponents Pawn and captures it

        // TODO Need to take promotion into account
        public override List<Coord> MovePositions()
        {
            List<Coord> positions = new List<Coord>();

            if (Color)
            {
                if(Position.Row != 8)
                    positions.Add(Position + new Point(0, -1));
                if (_firstMove)
                {
                    positions.Add(Position + new Point(0, -2));
                    _firstMove = false;
                }
            }
            else
            {
                if (Position.Row != 1)
                    positions.Add(Position + new Point(0, 1));
                if (_firstMove)
                {
                    positions.Add(Position + new Point(0, 2));
                    _firstMove = false;
                }
            }

            return positions;
        }
    }
}
