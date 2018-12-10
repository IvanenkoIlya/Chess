using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(int row, int col, PieceColor color) : base(row, col, color) { }

        public override List<Movement> MovePositions(Piece[,] board)
        {
            List<Movement> positions = new List<Movement>();

            int rowIndex = Position.Row - 1;
            int colIndex = Position.Column - 1;

            bool continueTopLeft = true;
            bool continueTopRight = true;
            bool continueBottomRight = true;
            bool continueBottomLeft = true;

            for(int i = 1; i < 8; i++)
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

                if(continueBottomRight)
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
            }

            return positions;
        }
    }
}
