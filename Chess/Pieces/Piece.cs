using System.Collections.Generic;

namespace Chess.Pieces
{
    public abstract class Piece
    {
        public Coord Position;
        public PieceColor Color; // true is black

        public Piece(int row, int col, PieceColor color)
        {
            Position = new Coord(row, col);
            Color = color;
        }

        public abstract List<Movement> MovePositions(Piece[,] board);

        protected bool GetMovement(int newRowIndex, int newColIndex, Piece piece, List<Movement> positions)
        {
            Movement m;

            if (piece != null)
            {
                if (piece.Color != Color)
                    m = new Movement() { Type = MovementType.Attack, Position = new Coord(newRowIndex + 1, newColIndex + 1) };
            }
            else
                m = new Movement() { Type = MovementType.Move, Position = new Coord(newRowIndex + 1, newColIndex + 1) };

            if (m != null)
            {
                positions.Add(m);
                if (m.Type == MovementType.Attack)
                    return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{Color} {GetType().Name} at {Position}";
        }
    }

    public class Movement
    {
        public Coord Position;
        public MovementType Type;
    }

    public enum MovementType
    {
        Move, Attack, Special
    }

    public enum PieceColor
    {
        White, Black
    }
}
