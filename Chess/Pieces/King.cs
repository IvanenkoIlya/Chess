using System.Collections.Generic;
using System.Windows;

namespace Chess.Pieces
{
    public class King : Piece
    {
        public bool HasMoved;

        List<Point> offsets = new List<Point>()
        {
            new Point(0,1),
            new Point(1,1),
            new Point(1,0),
            new Point(1,-1),
            new Point(0,-1),
            new Point(-1,-1),
            new Point(-1,0),
            new Point(-1,1)
        };

        public King(int row, int col, bool color) : base(row, col, color)
        {
            HasMoved = false;
        }

        // TODO Castling not taken into account
        // Rules for castling:
        //      1) Neither King nor Rook has moved
        //      2) Squares between Rook and King are empty
        //      3) King cannot be in check and King cannot move through a checkec position
        // Steps to castling:
        //      1) Move King 2 spaces toward Rook
        //      2) Hop Rook over King
        public override List<Coord> MovePositions()
        {
            List<Coord> positions = new List<Coord>();

            foreach(Point p in offsets)
            {
                int row = Position.Row + (int)p.Y;
                int col = Position.Column + (int)p.X;

                if (row < 1 || row > 8 || col < 1 || col > 8)
                    continue;

                positions.Add(new Coord(row, col));
            }

            return positions;
        }
    }
}
