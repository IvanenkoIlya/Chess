﻿using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Rook : Piece
    {
        public bool HasMoved

        public Rook(int row, int col, bool color) : base(row, col, color)
        {
            HasMoved = false;
        }

        // TODO Blocking not taken into account
        public override List<Coord> MovePositions()
        {
            List<Coord> positions = new List<Coord>();

            for(int i = 1; i < 8; i++)
            {
                if (Position.Row + i <= 8)
                    positions.Add(new Coord(Position.Row + i, Position.Column));
                if (Position.Row - i > 0)
                    positions.Add(new Coord(Position.Row - i, Position.Column));
                if (Position.Column + i <= 8)
                    positions.Add(new Coord(Position.Row, Position.Column + i));
                if (Position.Column - i > 0)
                    positions.Add(new Coord(Position.Row, Position.Column - i));
            }

            return positions;
        }
    }
}
