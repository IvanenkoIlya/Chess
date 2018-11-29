using Chess.Pieces;
using System;
using System.Windows;
using System.Windows.Shapes;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Chessboard cb = new Chessboard(ChessboardGrid);
            cb.SetupChessboard();
            cb.RenderChessboard();
        }

        private void MouseHover(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point p = e.GetPosition((Rectangle)sender);
            try
            {
                Coord c = new Coord(((399 - (int)p.Y) / 50) + 1, ((int)p.X / 50) + 1);
                Coordinate.Content = c.ToString();
            } catch(ArgumentException ex)
            {
                Console.WriteLine(p);
            }
        }
    }
}
