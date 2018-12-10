using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Rook : Piece
    {
        public bool HasMoved;

        public Rook(int row, int col, PieceColor color) : base(row, col, color)
        {
            HasMoved = false;
        }

        // TODO Blocking not taken into account
        public override List<Movement> MovePositions(Piece[,] board)
        {
            List<Movement> positions = new List<Movement>();

            int rowIndex = Position.Row - 1;
            int colIndex = Position.Column - 1;

            bool continueUp = true;
            bool continueRight = true;
            bool continueDown = true;
            bool continueLeft = true;

            for (int i = 1; i < 8; i++)
            {
                if (continueUp)
                {
                    if (rowIndex + i < 8)
                        continueUp = GetMovement(rowIndex + i, colIndex, board[rowIndex + i, colIndex], positions);
                    else
                        continueUp = false;
                }

                if (continueRight)
                {
                    if (colIndex + i < 8)
                        continueRight = GetMovement(rowIndex, colIndex + i, board[rowIndex, colIndex + i], positions);
                    else
                        continueRight = false;
                }

                if (continueDown)
                {
                    if (rowIndex - i >= 0)
                        continueDown = GetMovement(rowIndex - i, colIndex, board[rowIndex - i, colIndex], positions);
                    else
                        continueDown = false;
                }

                if (continueLeft)
                {
                    if (colIndex - i >= 0)
                        continueLeft = GetMovement(rowIndex, colIndex - i, board[rowIndex, colIndex - i], positions);
                    else
                        continueLeft = false;
                }
            }

            return positions;
        }
    }
}
