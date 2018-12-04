using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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

        private List<Piece> pieces;
        
        public Chessboard()
        {
            pieces = new List<Piece>();
            DataContext = this;
            InitializeComponent();
            InitializePieces();
        }

        private void InitializePieces()
        {
            pieces.Clear();

            pieces.Add(new Rook(1, 1, false));
            pieces.Add(new Knight(1, 2, false));
            pieces.Add(new Bishop(1, 3, false));
            pieces.Add(new Queen(1, 4, false));
            pieces.Add(new King(1, 5, false));
            pieces.Add(new Bishop(1, 6, false));
            pieces.Add(new Knight(1, 7, false));
            pieces.Add(new Rook(1, 8, false));

            for (int i = 1; i <= 8; i++)
            {
                pieces.Add(new Pawn(2, i, false));
                pieces.Add(new Pawn(7, i, true));
            }

            pieces.Add(new Rook(8, 1, true));
            pieces.Add(new Knight(8, 2, true));
            pieces.Add(new Bishop(8, 3, true));
            pieces.Add(new Queen(8, 4, true));
            pieces.Add(new King(8, 5, true));
            pieces.Add(new Bishop(8, 6, true));
            pieces.Add(new Knight(8, 7, true));
            pieces.Add(new Rook(8, 8, true));

            foreach(Piece p in pieces)
            {
                Pieces.Children.Add(new PieceControl() { Piece = p });
            }
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
