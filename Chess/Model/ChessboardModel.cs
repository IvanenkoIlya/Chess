using Chess.Pieces;
using System.Collections.Generic;

namespace Chess.Model
{
    public class ChessboardModel
    {
        public Piece[,] _pieces = new Piece[8, 8];

        public ChessboardModel()
        {
            _pieces[0, 0] = new Rook(1, 1, PieceColor.White);
            _pieces[0, 1] = new Knight(1, 2, PieceColor.White);
            _pieces[0, 2] = new Bishop(1, 3, PieceColor.White);
            _pieces[0, 3] = new Rook(1, 4, PieceColor.White);
            _pieces[0, 4] = new Rook(1, 5, PieceColor.White);
            _pieces[0, 5] = new Bishop(1, 6, PieceColor.White);
            _pieces[0, 6] = new Knight(1, 7, PieceColor.White);
            _pieces[0, 7] = new Rook(1, 8, PieceColor.White);

            for(int i = 0; i < 8; i++)
            {
                _pieces[1, i] = new Pawn(2, i + 1, PieceColor.White);
                _pieces[6, i] = new Pawn(7, i + 1, PieceColor.Black);
            }

            _pieces[7, 0] = new Rook(8, 1, PieceColor.Black);
            _pieces[7, 1] = new Knight(8, 2, PieceColor.Black);
            _pieces[7, 2] = new Bishop(8, 3, PieceColor.Black);
            _pieces[7, 3] = new Rook(8, 4, PieceColor.Black);
            _pieces[7, 4] = new Rook(8, 5, PieceColor.Black);
            _pieces[7, 5] = new Bishop(8, 6, PieceColor.Black);
            _pieces[7, 6] = new Knight(8, 7, PieceColor.Black);
            _pieces[7, 7] = new Rook(8, 8, PieceColor.Black);
        }

        public List<Movement> GetMovePositions(Coord piece)
        {
            return _pieces[piece.Row - 1, piece.Column - 1].MovePositions(_pieces);
        }

        public void MovePiece(Coord oldPos, Coord newPos)
        {
            _pieces[newPos.Row - 1, newPos.Column - 1] = _pieces[oldPos.Row - 1, oldPos.Column - 1];
            _pieces[oldPos.Row - 1, oldPos.Column - 1] = null;
        }
    }
}

    