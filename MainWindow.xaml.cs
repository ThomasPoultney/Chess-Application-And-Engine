
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class DataObject
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
    }

    public partial class MainWindow : Window
    {
        Point startingPosition = new Point(0, 0);
        Board board;
        Image hoveredTileImage = new Image();
        public MainWindow()
        {

            InitializeComponent();

            board = new Board();
            board.generateBoardFromFEN();

            board.setValidMoves(board.generateValidMoves(board));
            Game.activeBoard = board;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine(board.moveGenerationTest(2));
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs + " ms spent generating moves.");
            Ui.canvas = this.boardCanvas;
            Ui.squareSize = (int)(this.boardCanvas.Width / board.getBoardSize());
            Thickness boardThickness = new Thickness();
            boardThickness.Left = Ui.squareSize;
            boardThickness.Bottom = Ui.squareSize;
            this.boardVB.Margin = boardThickness;
            Ui.MainWindow = this;
            Ui.grid = this.UiGrid;
            Ui.whiteImageGrid = this.whiteCapturedGrid;
            Ui.blackImageGrid = this.blackCapturedGrid;
            Ui.whiteScoreLabel = this.whiteCapturedLabel;
            Ui.blackScoreLabel = this.blackCapturedLabel;
            Ui.dataGrid = this.dataGrid1;
            Ui.whiteImageVB = this.whiteCapturedVB;
            Ui.blackImageVB = this.blackCapturedVB;
            //Ui.moveListBox = moveListBox;


            //drawBoard(board,grid);
            Ui.drawBoardGrid(board, Ui.canvas);
            Ui.drawUi(board, Ui.canvas);
            Ui.initializeUpgradeButtons();
            //initialise hoveredTileImage
            hoveredTileImage.Width = Ui.squareSize;
            hoveredTileImage.Height = Ui.squareSize;
            String hoveredTileImageURL = "C:/Users/tompo/source/repos/ChessB/Images/hoveredTileImage.PNG";
            ImageSource hoveredTileImageSource = new BitmapImage(new Uri(hoveredTileImageURL));
            hoveredTileImage.Source = hoveredTileImageSource;
            hoveredTileImage.IsHitTestVisible = false;
            Ui.hoveredTileImage = hoveredTileImage;

        }




        protected override void OnMouseMove(MouseEventArgs e)
        {
            Image imageSelected = Ui.imageSelected;

            if (imageSelected != null)
            {
                Point point2 = e.GetPosition(imageSelected);

                Canvas.SetTop(imageSelected, (int)((Canvas.GetTop(imageSelected)) + point2.Y - MoveableImage.point.Y));
                Canvas.SetLeft(imageSelected, (int)((Canvas.GetLeft(imageSelected)) + point2.X - MoveableImage.point.X));
                Ui.drawHoveredTileImage();

            }
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            Ui.removeHoveredTileImage();

            if (Ui.imageSelected != null)
            {
                resetImage();
            }
        }

        public void resetImage()
        {
            if (Ui.imageSelected != null)
            {
                Ui.removeValidMovesImages();
                Canvas.SetTop(Ui.imageSelected, Ui.resetPositionY);
                Canvas.SetLeft(Ui.imageSelected, Ui.resetPositionX);

                Ui.pieceSelected = null;
                Ui.imageSelected = null;

            }
        }

        /*-------------------------------------------------
        ---------------Black Radio Buttons-----------------
        -------------------------------------------------*/



    }
}
