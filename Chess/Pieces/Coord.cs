using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString()
        {
            return $"{Row},{(char)(Column + 64)}";
        }
    }
}
