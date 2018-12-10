using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Queen : Piece
    {
        public Queen(int row, int col, PieceColor color) : base(row, col, color) { }

        public override List<Movement> MovePositions(Piece[,] board)
        {
            List<Movement> positions = new List<Movement>();

            int rowIndex = Position.Row - 1;
            int colIndex = Position.Column - 1;

            bool continueUp = true;
            bool continueRight = true;
            bool continueDown = true;
            bool continueLeft = true;

            bool continueTopLeft = true;
            bool continueTopRight = true;
            bool continueBottomRight = true;
            bool continueBottomLeft = true;

            for (int i = 1; i < 8; i++)
            {
                if (continueTopLeft)
                {
                    if (rowIndex + i + i < 8 && colIndex - i >= 0)
                        continueTopLeft = GetMovement(rowIndex + i, colIndex - i, board[rowIndex + i, colIndex - i], positions);
                    else
                        continueTopLeft = false;
                }

                if (continueTopRight)
                {
                    if (rowIndex + i < 8 && colIndex + i < 8)
                        continueTopRight = GetMovement(rowIndex + i, colIndex + i, board[rowIndex + i, colIndex + i], positions);
                    else
                        continueTopRight = false;
                }

                if (continueBottomRight)
                {
                    if (rowIndex - i >= 0 && colIndex + i < 8)
                        continueBottomRight = GetMovement(rowIndex - i, colIndex + i, board[rowIndex - i, colIndex + i], positions);
                    else
                        continueBottomRight = false;
                }

                if (continueBottomLeft)
                {
                    if (rowIndex - i >= 0 && colIndex - i >= 0)
                        continueBottomLeft = GetMovement(rowIndex - i, colIndex - i, board[rowIndex - i, colIndex - i], positions);
                    else
                        continueBottomLeft = false;
                }

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
