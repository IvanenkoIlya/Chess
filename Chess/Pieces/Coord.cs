using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Pieces
{
    public class Coord
    {
        private int _row;
        public int Row
        {
            get { return _row; }
            set
            {
                if (value < 1 || value > 8)
                    throw new ArgumentException("value", "Row value must be in range 1-8");
                _row = value;
            }
        }

        private int _column;
        public int Column
        {
            get { return _column; }
            set
            {
                if (value < 1 || value > 8)
                    throw new ArgumentException("value", "Column value must be in range 1-8");
                _column = value;
            }
        }

        public Coord(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static Coord operator +(Coord c1, Coord c2)
        {
            return new Coord(c1.Row + c2.Row, c1.Column + c2.Column);
        }

        public static Coord operator +(Coord c, Point p)
        {
            int row = c.Row + (int)p.Y;
            int col = c.Column + (int)p.X;

            if (row < 1 || row > 8 || col < 1 || col > 8)
                throw new ArgumentOutOfRangeException("p", "Coordinate - Point is out of range");

            return new Coord(row, col);
        }

        public static Coord operator -(Coord c1, Coord c2)
        {
            return new Coord(c1.Row - c2.Row, c1.Column - c2.Column);
        }

        public static Coord operator -(Coord c, Point p)
        {
            int row = c.Row - (int)p.Y;
            int col = c.Column - (int)p.X;

            if (row < 1 || row > 8 || col < 1 || col > 8)
                throw new ArgumentOutOfRangeException("p", "Coordinate - Point is out of range");

            return new Coord(row, col);
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.Row == c2.Row && c1.Column == c2.Column;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return c1.Row != c2.Row || c1.Column != c2.Column;
        }

        public override string ToString()
        {
            return $"{(char)(Column + 64)}{Row}";
        }
    }
}
