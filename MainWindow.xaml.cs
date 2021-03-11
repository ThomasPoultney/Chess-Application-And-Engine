
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
            mainWindow.Background = new SolidColorBrush(Color.FromArgb(255, 90, 88, 92));

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


            viewBox.Stretch = Stretch.Fill;
            viewBox.StretchDirection = StretchDirection.Both;
            Grid grid = new Grid();
            //grid.ShowGridLines = true;
            /* ------------------------------------------
            -------------- spagetti fix------------------
            ---------------------------------------------*/
            ColumnDefinition colDef0 = new ColumnDefinition();
            ColumnDefinition colDef1 = new ColumnDefinition();
            RowDefinition rowDef0 = new RowDefinition();
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            grid.ColumnDefinitions.Add(colDef0);
            grid.ColumnDefinitions.Add(colDef1);


            grid.RowDefinitions.Add(rowDef0);
            grid.RowDefinitions.Add(rowDef1);
            grid.RowDefinitions.Add(rowDef2);

            colDef0.Width = new GridLength(9.0, GridUnitType.Star);
            colDef1.Width = new GridLength(2.0, GridUnitType.Star);
            rowDef1.Height = new GridLength(9.0, GridUnitType.Star);

            Grid.SetColumn(viewBox, 0);
            Grid.SetRow(viewBox, 1);
            grid.Children.Add(viewBox);
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;

            Viewbox blackImageGridVb = new Viewbox();
            Viewbox whiteImageGridVb = new Viewbox();
            Viewbox blackLabelViewBox = new Viewbox();
            Viewbox whiteLabelViewBox = new Viewbox();

            blackLabelViewBox.Child = Ui.blackScoreLabel;
            whiteLabelViewBox.Child = Ui.whiteScoreLabel;

            blackLabelViewBox.Stretch = Stretch.Fill;
            blackLabelViewBox.StretchDirection = StretchDirection.Both;

            whiteLabelViewBox.Stretch = Stretch.Fill;
            whiteLabelViewBox.StretchDirection = StretchDirection.Both;
            Ui.whiteImageGrid = new Grid();
            Ui.blackImageGrid = new Grid();

            Ui.blackImageGrid.ColumnDefinitions.Add(new ColumnDefinition());
            Ui.whiteImageGrid.ColumnDefinitions.Add(new ColumnDefinition());
            Ui.blackImageGrid.RowDefinitions.Add(new RowDefinition());
            Ui.whiteImageGrid.RowDefinitions.Add(new RowDefinition());

            Grid.SetColumn(blackLabelViewBox, 0);
            Grid.SetRow(blackLabelViewBox, 0);
            Ui.blackImageGrid.Children.Add(blackLabelViewBox);
            Grid.SetColumn(whiteLabelViewBox, 0);
            Grid.SetRow(whiteLabelViewBox, 0);
            Ui.whiteImageGrid.Children.Add(whiteLabelViewBox);

            blackImageGridVb.Child = Ui.blackImageGrid;
            blackImageGridVb.HorizontalAlignment = HorizontalAlignment.Left;

            whiteImageGridVb.Child = Ui.whiteImageGrid;
            whiteImageGridVb.HorizontalAlignment = HorizontalAlignment.Left;


            Grid.SetColumn(blackImageGridVb, 0);
            Grid.SetRow(blackImageGridVb, 0);
            grid.Children.Add(blackImageGridVb);

            Grid.SetColumn(whiteImageGridVb, 0);
            Grid.SetRow(whiteImageGridVb, 2);
            grid.Children.Add(whiteImageGridVb);




            Ui.squareSize = (int)(canvas.Width / board.getBoardSize());

            canvas.HorizontalAlignment = HorizontalAlignment.Stretch;
            canvas.VerticalAlignment = VerticalAlignment.Stretch;

            //drawBoard(board,grid);
            Ui.drawBoard(board, canvas);
            Game.activeBoard = board;

            Ui.canvas = canvas;
            Ui.MainWindow = mainWindow;
            Ui.grid = grid;
            mainWindow.Content = grid;


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
