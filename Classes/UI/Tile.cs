using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;
using System.Windows.Media;

namespace ChessB
{
    public class Tile : Image
    {

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            if (Ui.tileSelected == null)
            {
                Ui.tileSelected = this;
                Ui.ArrowStartingPosition = new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
            }
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {

            if (Ui.tileSelected != null)
            {
                Ui.drawArrow(this);
                Ui.tileSelected = null;
            }
        }


    }
}
