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

        private List<PieceControl> _pieces;
        private PieceControl _lastClicked;

        public Chessboard()
        {
            _pieces = new List<PieceControl>();
            DataContext = this;
            InitializeComponent();
            InitializePieces();
        }

        private void InitializePieces()
        {
            _pieces.Clear();

            _pieces.Add(new PieceControl() { Piece = new Rook(1, 1, false) });
            _pieces.Add(new PieceControl() { Piece = new Knight(1, 2, false) });
            _pieces.Add(new PieceControl() { Piece = new Bishop(1, 3, false) });
            _pieces.Add(new PieceControl() { Piece = new Queen(1, 4, false) });
            _pieces.Add(new PieceControl() { Piece = new King(1, 5, false) });
            _pieces.Add(new PieceControl() { Piece = new Bishop(1, 6, false) });
            _pieces.Add(new PieceControl() { Piece = new Knight(1, 7, false) });
            _pieces.Add(new PieceControl() { Piece = new Rook(1, 8, false) });

            for (int i = 1; i <= 8; i++)
            {
                _pieces.Add(new PieceControl() { Piece = new Pawn(2, i, false) });
                _pieces.Add(new PieceControl() { Piece = new Pawn(7, i, true) });
            }

            _pieces.Add(new PieceControl() { Piece = new Rook(8, 1, true) });
            _pieces.Add(new PieceControl() { Piece = new Knight(8, 2, true) });
            _pieces.Add(new PieceControl() { Piece = new Bishop(8, 3, true) });
            _pieces.Add(new PieceControl() { Piece = new Queen(8, 4, true) });
            _pieces.Add(new PieceControl() { Piece = new King(8, 5, true) });
            _pieces.Add(new PieceControl() { Piece = new Bishop(8, 6, true) });
            _pieces.Add(new PieceControl() { Piece = new Knight(8, 7, true) });
            _pieces.Add(new PieceControl() { Piece = new Rook(8, 8, true) });

            foreach (PieceControl p in _pieces)
            {
                p.MouseDown += Click_ShowPieceMoves;

                Pieces.Children.Add(p);
            }
        }

        private void Click_ShowPieceMoves(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _lastClicked = ((PieceControl)sender);

            Overlay.Children.Clear();

            List<Coord> movePositions = _lastClicked.Piece.MovePositions();
            List<Piece> blockingPieces = new List<Piece>();

            foreach (Coord c in movePositions)
                blockingPieces.AddRange(_pieces.Where(x => x.Piece.Position == c).Select(x => x.Piece));

            foreach (Piece pc in blockingPieces)
            {
                // Remove vertical blocked spaces
                if (pc.Position.Column == _lastClicked.Piece.Position.Column) 
                {
                    // remove all move positions above pc
                    if (pc.Position.Row > _lastClicked.Piece.Position.Row)
                        movePositions.RemoveAll(x => x.Row > pc.Position.Row && x.Column == pc.Position.Column);
                    // remove all move positions bellow pc
                    else if (pc.Position.Row < _lastClicked.Piece.Position.Row)
                        movePositions.RemoveAll(x => x.Row < pc.Position.Row && x.Column == pc.Position.Column);
                }
                // Remove horizontal blocked spaces
                else if (pc.Position.Row == _lastClicked.Piece.Position.Row)
                {
                    // Remove possitions right of pc
                    if (pc.Position.Column > _lastClicked.Piece.Position.Column)
                        movePositions.RemoveAll(x => x.Row == pc.Position.Row && x.Column > pc.Position.Column);
                    // Remove positions left of pc
                    else if (pc.Position.Column < _lastClicked.Piece.Position.Column)
                        movePositions.RemoveAll(x => x.Row == pc.Position.Row && x.Column < pc.Position.Column);
                }
                // Remove all diagonal blocked spaces
                else if (Math.Abs(pc.Position.Row - _lastClicked.Piece.Position.Row) == Math.Abs(pc.Position.Column - _lastClicked.Piece.Position.Column))
                {
                    if( pc.Position.Row > _lastClicked.Piece.Position.Row)
                    {
                        // Top Right
                        if(pc.Position.Column > _lastClicked.Piece.Position.Column) 
                        {
                            movePositions.RemoveAll(x => x.Row > pc.Position.Row && x.Column > pc.Position.Column);
                        }
                        // Top Left
                        else if(pc.Position.Column < _lastClicked.Piece.Position.Column)
                        {
                            movePositions.RemoveAll(x => x.Row > pc.Position.Row && x.Column < pc.Position.Column);
                        }
                    }
                    else if(pc.Position.Row < _lastClicked.Piece.Position.Row)
                    {
                        // Bottom Right
                        if (pc.Position.Column > _lastClicked.Piece.Position.Column)
                        {
                            movePositions.RemoveAll(x => x.Row < pc.Position.Row && x.Column > pc.Position.Column);
                        }
                        // Bottom Left
                        else if (pc.Position.Column < _lastClicked.Piece.Position.Column)
                        {
                            movePositions.RemoveAll(x => x.Row < pc.Position.Row && x.Column < pc.Position.Column);
                        }
                    }
                }
            }

            foreach (Coord c in movePositions)
            {
                Rectangle rect = new Rectangle()
                {
                    Fill = Brushes.Green,
                    Opacity = 0.5
                };
                rect.MouseDown += Click_MovePiece;

                PieceControl pc = _pieces.Where(x => x.Piece.Position == c).FirstOrDefault();

                if (pc != null)
                {
                    if (pc.Piece.Color != _lastClicked.Piece.Color)
                        rect.Fill = Brushes.Red;
                    else
                        continue;
                }

                if (_pieces.Any(x => x.Piece.Position == c && x.Piece.Color != _lastClicked.Piece.Color))
                    rect.Fill = Brushes.Red;

                Point pos = CoordinateToGridPoint(c);

                rect.SetValue(Grid.RowProperty, (int)pos.Y);
                rect.SetValue(Grid.ColumnProperty, (int)pos.X);

                Overlay.Children.Add(rect);
            }

            e.Handled = true;
        }

        private void Click_MovePiece(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            int row = (int)rect.GetValue(Grid.RowProperty);
            int column = (int)rect.GetValue(Grid.ColumnProperty);

            Coord pos = GridPointToCoordinate(new Point(column, row));

            PieceControl p = _pieces.Where(x => x.Piece.Position == pos && _lastClicked.Piece.Color != x.Piece.Color).FirstOrDefault();
            if (p != null) // TODO This doesn't remove the PieceControl from the Chessboard
            {
                Pieces.Children.Remove(p);
                _pieces.Remove(p);
            }

            _lastClicked.Piece.Position = pos;
            _lastClicked.SetValue(Grid.RowProperty, row);
            _lastClicked.SetValue(Grid.ColumnProperty, column);

            _lastClicked = null;
            Overlay.Children.Clear();
        }

        private Coord GridPointToCoordinate(Point p)
        {
            return new Coord(8 - ((int)p.Y), ((int)p.X) + 1);
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

        private void ClearOverlay(object sender, MouseButtonEventArgs e)
        {
            if (!e.Handled)
                Overlay.Children.Clear();
        }
    }
}
