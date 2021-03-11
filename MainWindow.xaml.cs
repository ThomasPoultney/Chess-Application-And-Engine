
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChessB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        Board board;
        public MainWindow()
        {


            InitializeComponent();
            Window mainWindow = this;
            mainWindow.Title = "Chess";

            mainWindow.AllowsTransparency = AllowsTransparency;
            mainWindow.Background = null;
            mainWindow.Margin = new Thickness(0, 0, 0, 0);

            board = new Board();
            board.generateBoardFromFEN();


            Canvas canvas = new Canvas();
            canvas.Margin = new Thickness(0, 0, 0, 0);
            Viewbox viewBox = new Viewbox();
            canvas.Width = 500;
            canvas.Height = 500;
            mainWindow.Width = 500;
            mainWindow.Height = 500;

            viewBox.Child = canvas;
            mainWindow.Content = viewBox;
            viewBox.Stretch = Stretch.Fill;
            viewBox.StretchDirection = StretchDirection.Both;

            Ui.squareSize = (int)(canvas.Width / board.getBoardSize());

            canvas.HorizontalAlignment = HorizontalAlignment.Stretch;
            canvas.VerticalAlignment = VerticalAlignment.Stretch;

            //drawBoard(board,grid);
            Ui.drawBoard(board, canvas);
            Game.activeBoard = board;


            Ui.canvas = canvas;
            Ui.MainWindow = mainWindow;


        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Image imageSelected = Ui.imageSelected;

            if (imageSelected != null)
            {
                Point point2 = e.GetPosition(imageSelected);
                Canvas.SetTop(imageSelected, (int)(Canvas.GetTop(imageSelected) + point2.Y - MoveableImage.point.Y));
                Canvas.SetLeft(imageSelected, (int)(Canvas.GetLeft(imageSelected) + point2.X - MoveableImage.point.X));
            }
        }



    }



}
