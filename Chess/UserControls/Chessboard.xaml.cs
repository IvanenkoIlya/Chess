using Chess.Model;
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

        private ChessboardModel chessboard;

        private List<PieceControl> _pieces;
        private PieceControl _lastClicked;

        public Chessboard()
        {
            chessboard = new ChessboardModel();
            
            DataContext = this;
            InitializeComponent();
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

            // Casteling
            // TODO Need to see if King is in check 
            if(_lastClicked.Piece is King && !((King)_lastClicked.Piece).HasMoved)
            {
                var temp = _pieces.Where(x => x.Piece.Position == _lastClicked.Piece.Position + new Point(1, 0) ||
                                 x.Piece.Position == _lastClicked.Piece.Position + new Point(2, 0));

                if (!temp.Any())
                {
                    var rook = _pieces.Where(x => x.Piece.Position == _lastClicked.Piece.Position + new Point(3, 0)).FirstOrDefault();
                    if (rook.Piece is Rook && !((Rook)rook.Piece).HasMoved)
                    {
                        Rectangle rect = new Rectangle()
                        {
                            Fill = Brushes.Yellow,
                            Opacity = 0.5
                        };
                        rect.MouseDown += Click_Castle;

                        rect.SetValue(Grid.RowProperty,_lastClicked.GetValue(Grid.RowProperty));
                        rect.SetValue(Grid.ColumnProperty,(int)_lastClicked.GetValue(Grid.ColumnProperty) + 2);

                        Overlay.Children.Add(rect);
                    }
                }
            }

            // Pawn attack
            if(_lastClicked.Piece is Pawn)
            {
                if(_lastClicked.Piece.Color)
                {
                    // check bottom right and bottom left
                    foreach(PieceControl p in _pieces.Where(x => !x.Piece.Color && 
                        (x.Piece.Position == _lastClicked.Piece.Position + new Point(-1,-1) || x.Piece.Position == _lastClicked.Piece.Position + new Point(1, -1))))
                    {
                        Rectangle rect = new Rectangle()
                        {
                            Fill = Brushes.Red,
                            Opacity = 0.5
                        };
                        rect.MouseDown += Click_MovePiece;

                        rect.SetValue(Grid.RowProperty, p.GetValue(Grid.RowProperty));
                        rect.SetValue(Grid.ColumnProperty, p.GetValue(Grid.ColumnProperty));

                        Overlay.Children.Add(rect);
                    }
                }
                else
                {
                    //check top right and top left
                    foreach (PieceControl p in _pieces.Where(x => x.Piece.Color &&
                         (x.Piece.Position == _lastClicked.Piece.Position + new Point(-1, 1) || x.Piece.Position == _lastClicked.Piece.Position + new Point(1, 1))))
                    {
                        Rectangle rect = new Rectangle()
                        {
                            Fill = Brushes.Red,
                            Opacity = 0.5
                        };
                        rect.MouseDown += Click_MovePiece;

                        rect.SetValue(Grid.RowProperty, p.GetValue(Grid.RowProperty));
                        rect.SetValue(Grid.ColumnProperty, p.GetValue(Grid.ColumnProperty));

                        Overlay.Children.Add(rect);
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

        private void Click_Castle(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
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

        private void ClearOverlay(object sender, MouseButtonEventArgs e)
        {
            if (!e.Handled)
                Overlay.Children.Clear();
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

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
