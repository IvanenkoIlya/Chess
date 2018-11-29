using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess
{
    public class Chessboard
    {
        Grid _chessboardGrid;
        Piece[,] _board;

        public Chessboard(Grid chessboardGrid)
        {
            _chessboardGrid = chessboardGrid;
            _board = new Piece[8, 8];
        }

        public void SetupChessboard()
        {
            // White pieces
            _board[0, 0] = new Rook(1, 1, false);
            _board[0, 1] = new Knight(1, 2, false);
            _board[0, 2] = new Bishop(1, 3, false);
            _board[0, 3] = new Queen(1, 4, false);
            _board[0, 4] = new King(1, 5, false);
            _board[0, 5] = new Bishop(1, 6, false);
            _board[0, 6] = new Knight(1, 7, false);
            _board[0, 7] = new Rook(1, 8, false);

            for (int i = 0; i < 8; i++)
                _board[1, i] = new Pawn(2, i+1, false);

            // Black pieces
            _board[7, 0] = new Rook(8, 1, true);
            _board[7, 1] = new Knight(8, 2, true);
            _board[7, 2] = new Bishop(8, 3, true);
            _board[7, 3] = new Queen(8, 4, true);
            _board[7, 4] = new King(8, 5, true);
            _board[7, 5] = new Bishop(8, 6, true);
            _board[7, 6] = new Knight(8, 7, true);
            _board[7, 7] = new Rook(8, 8, true);

            for (int i = 0; i < 8; i++)
                _board[6, i] = new Pawn(7, i+1, true);
        }

        /*
         <Label Content="P" FontSize="40" Padding="0" Width="50" Height="50" Foreground="Red"
                           HorizontalContentAlignment="Center" VerticalContentAlignment="Center"/>
             */

        public void RenderChessboard()
        {
            _chessboardGrid.Children.Clear();

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(_board[i,j] != null)
                    {
                        if (_board[i, j].Color)
                        {
                            Label piece = new Label
                            {
                                FontSize = 40,
                                Padding = new System.Windows.Thickness(0),
                                Width = 50,
                                Height = 50,
                                Foreground = _board[i, j].Color ? Brushes.Black : Brushes.LightGray,
                                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                                Content = _board[i, j].GetType().Name.ToUpper()[0]
                            };
                            piece.SetValue(Grid.RowProperty, 7 - i);
                            piece.SetValue(Grid.ColumnProperty, j);
                            if (_board[i, j].GetType().Name == "Knight")
                                piece.Content = "Kn";

                            _chessboardGrid.Children.Add(piece);
                        }
                        else
                        {
                            Image piece = new Image();
                            piece.Width = 50;
                            piece.Height = 50;
                            piece.Margin = new System.Windows.Thickness(0);

                            BitmapImage image = new BitmapImage();
                            image.BeginInit();
                            string color = _board[i, j].Color ? "Black" : "White";
                            string imageUri = @"Resources/Pieces/" + color + "/" + _board[i,j].GetType().Name + ".png";
                            image.StreamSource = Application.GetResourceStream(new Uri(imageUri, UriKind.Relative)).Stream;
                            image.EndInit();

                            piece.Source = image;

                            piece.SetValue(Grid.RowProperty, 7 - i);
                            piece.SetValue(Grid.ColumnProperty, j);

                            _chessboardGrid.Children.Add(piece);
                        }
                    }
                }
            }
        }
    }
}
