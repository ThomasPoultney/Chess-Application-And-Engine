
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChessB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        Point startingPosition = new Point(0, 0);
        Board board;
        public MainWindow()
        {

            InitializeComponent();

            board = new Board();
            board.generateBoardFromFEN();
            Game.activeBoard = board;
            Ui.canvas = this.boardCanvas;
            Ui.squareSize = (int)(this.boardCanvas.Width / board.getBoardSize());

            //drawBoard(board,grid);
            Ui.drawBoardGrid(board, this.boardCanvas);
            Ui.drawBoardPieces(board, this.boardCanvas);
            Ui.MainWindow = this;
            Ui.grid = this.UiGrid;
            Ui.whiteImageGrid = this.whiteCapturedGrid;
            Ui.blackImageGrid = this.blackCapturedGrid;
            Ui.whiteScoreLabel = this.whiteCapturedLabel;
            Ui.blackScoreLabel = this.blackCapturedLabel;
            Ui.moveListBox = moveListBox;
            Window window = new Window();
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

        private void rectangleMouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }
        /*-------------------------------------------------
        ---------------Black Radio Buttons-----------------
        -------------------------------------------------*/

        private void BRRB_Checked(object sender, RoutedEventArgs e)
        {
            Ui.blackUpgradeChoice = "promoteToRook";
        }

        private void BQRB_Checked(object sender, RoutedEventArgs e)
        {
            Ui.blackUpgradeChoice = "promoteToQueen";
        }

        private void BNRB_Checked(object sender, RoutedEventArgs e)
        {
            Ui.blackUpgradeChoice = "promoteToKnight";
        }

        private void BBRB_Checked(object sender, RoutedEventArgs e)
        {
            Ui.blackUpgradeChoice = "promoteToBishop";
        }

        /*-------------------------------------------------
        ---------------White Radio Buttons-----------------
        -------------------------------------------------*/

        private void WRRB_Checked(object sender, RoutedEventArgs e)
        {
            Ui.whiteUpgradeChoice = "promoteToRook";
        }

        private void WQRB_Checked(object sender, RoutedEventArgs e)
        {
            Ui.whiteUpgradeChoice = "promoteToQueen";
        }

        private void WNRB_Checked(object sender, RoutedEventArgs e)
        {
            Ui.whiteUpgradeChoice = "promoteToKnight";
        }

        private void WBRB_Checked(object sender, RoutedEventArgs e)
        {
            Ui.whiteUpgradeChoice = "promoteToBishop";
        }

    }
}
