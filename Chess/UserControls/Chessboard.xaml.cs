using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess.UserControls
{
    /// <summary>
    /// Interaction logic for Chessboard.xaml
    /// </summary>
    public partial class Chessboard : UserControl, INotifyPropertyChanged
    {
        #region WhiteColorProperty
        public static readonly DependencyProperty WhiteColorProperty =
            DependencyProperty.Register("WhiteColor", typeof(SolidColorBrush), typeof(Chessboard),
                new PropertyMetadata(Brushes.White));

        public SolidColorBrush WhiteColor
        {
            get { return (SolidColorBrush)GetValue(WhiteColorProperty); }
            set { SetValue(WhiteColorProperty, value); OnPropertyChanged("WhiteColor"); }
        }
        #endregion

        #region BlackColorProperty
        public static readonly DependencyProperty BlackColorProperty =
            DependencyProperty.Register("BlackColor", typeof(SolidColorBrush), typeof(Chessboard),
                new PropertyMetadata(Brushes.Black));

        public SolidColorBrush BlackColor
        {
            get { return (SolidColorBrush)GetValue(BlackColorProperty); }
            set { SetValue(BlackColorProperty, value); OnPropertyChanged("BlackColor"); }
        }
        #endregion

        private List<Piece> _pieces;
        private PieceControl _lastClicked;
        
        public Chessboard()
        {
            _pieces = new List<Piece>();
            DataContext = this;
            InitializeComponent();
            InitializePieces();
        }

        private void InitializePieces()
        {
            _pieces.Clear();

            _pieces.Add(new Rook(1, 1, false));
            _pieces.Add(new Knight(1, 2, false));
            _pieces.Add(new Bishop(1, 3, false));
            _pieces.Add(new Queen(1, 4, false));
            _pieces.Add(new King(1, 5, false));
            _pieces.Add(new Bishop(1, 6, false));
            _pieces.Add(new Knight(1, 7, false));
            _pieces.Add(new Rook(1, 8, false));

            for (int i = 1; i <= 8; i++)
            {
                _pieces.Add(new Pawn(2, i, false));
                _pieces.Add(new Pawn(7, i, true));
            }

            _pieces.Add(new Rook(8, 1, true));
            _pieces.Add(new Knight(8, 2, true));
            _pieces.Add(new Bishop(8, 3, true));
            _pieces.Add(new Queen(8, 4, true));
            _pieces.Add(new King(8, 5, true));
            _pieces.Add(new Bishop(8, 6, true));
            _pieces.Add(new Knight(8, 7, true));
            _pieces.Add(new Rook(8, 8, true));

            foreach(Piece p in _pieces)
            {
                PieceControl pc = new PieceControl() { Piece = p };
                pc.MouseDown += Click_ShowPieceMoves;

                Pieces.Children.Add(pc);
            }
        }

        private void Click_ShowPieceMoves(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _lastClicked = ((PieceControl)sender);


            Overlay.Children.Clear();
            foreach(Coord c in _lastClicked.Piece.MovePositions())
            {
                // This works for Knight, Pawns, and Kings
                // Does not check sightlines for Queen, Rook, or Bishop
                if (_pieces.Any(x => x.Position == c)) 
                    continue;

                Rectangle rect = new Rectangle()
                {
                    Fill = Brushes.Green,
                    Opacity = 0.5
                };
                rect.MouseDown += Click_MovePiece;

                Point pos = CoordinateToGridPoint(c);

                rect.SetValue(Grid.RowProperty, (int)pos.Y);
                rect.SetValue(Grid.ColumnProperty, (int)pos.X);

                Overlay.Children.Add(rect);
            }
        }

        private void Click_MovePiece(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            int row = (int)rect.GetValue(Grid.RowProperty);
            int column = (int)rect.GetValue(Grid.ColumnProperty);

            _lastClicked.Piece.Position = GridPointToCoordinate(new Point(column, row));
            _lastClicked.SetValue(Grid.RowProperty, row);
            _lastClicked.SetValue(Grid.ColumnProperty, column);

            _lastClicked = null;
            Overlay.Children.Clear();
        }

        private Coord GridPointToCoordinate(Point p)
        {
            return new Coord(8 - ((int)p.Y),((int)p.X) + 1);
        }

        private Point CoordinateToGridPoint(Coord c)
        {
            return new Point(c.Column - 1, 8 - c.Row);
        }

        private Coord BoardPointToCoordinate(Point p)
        {
            return new Coord(((399 - (int)p.Y) / 50) + 1, ((int)p.X / 50) + 1);
        }

        private void MouseHover(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point p = e.GetPosition((Grid)sender);
            try
            {
                Coord c = BoardPointToCoordinate(p);
                Coordinates.Content = c.ToString();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(p);
            }
        }

        private void MouseExitBoard(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Coordinates.Content = "";
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
