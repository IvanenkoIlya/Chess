using Chess.Pieces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess.UserControls
{
    /// <summary>
    /// Interaction logic for PieceControl.xaml
    /// </summary>
    public partial class PieceControl : UserControl
    {
        #region PieceProperty
        public static readonly DependencyProperty PieceProperty =
            DependencyProperty.Register("Piece", typeof(Piece), typeof(PieceControl),
                new PropertyMetadata(null));

        public Piece Piece
        {
            get { return (Piece)GetValue(PieceProperty); }
            set
            {
                SetValue(PieceProperty, value);

                if (value != null)
                    SetupPiece(); 
            }
        }
        #endregion

        public PieceControl()
        {
            InitializeComponent();

            if (Piece != null)
                SetupPiece();
        }

        private void SetupPiece()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            string color = Piece.Color ? "Black" : "White";
            string imageUri = @"Resources/Pieces/" + color + "/" + Piece.GetType().Name + ".png";
            image.StreamSource = Application.GetResourceStream(new Uri(imageUri, UriKind.Relative)).Stream;
            image.EndInit();

            PieceImage.Source = image;

            Point p = CoordinateToGridPoint(Piece.Position);
            SetValue(Grid.ColumnProperty, (int)p.X);
            SetValue(Grid.RowProperty, (int)p.Y);

        }

        private Point CoordinateToGridPoint(Coord c)
        {
            return new Point(c.Column - 1, 8 - c.Row);
        }

        private void ShowMoves(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(Piece != null)
            {
                //foreach (Coord movePos in Piece.MovePositions())
                //{
                //    // TODO highlight movable spaces
                //}
            }
        }

        private void ClearMoves(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // TODO clear moves
        }
    }
}
