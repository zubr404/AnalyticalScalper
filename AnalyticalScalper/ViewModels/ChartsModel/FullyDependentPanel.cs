using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnalyticalScalper.ViewModels.ChartsModel
{
    /// <summary>
    /// Позиционирование полнстью зависимой дополнительной панели
    /// (зависимость относительно координат последней точки основного
    /// графика)
    /// </summary>
    class FullyDependentPanel : BaseClasses.PropertyChangedBase
    {
        private Thickness margin;
        private SizeGlobal sizePanel;

        public FullyDependentPanel(Thickness _margin)
        {
            margin = new Thickness(_margin.Left, _margin.Top, _margin.Right, _margin.Bottom);
        }
        public FullyDependentPanel(SizeGlobal _sizepanel)
        {
            sizePanel = _sizepanel;
        }

        private Point coordinatesPanel;
        /// <summary>
        /// Конечные координаты панели
        /// </summary>
        public Point CoordinatesPanel
        {
            get { return coordinatesPanel; }
            set
            {
                coordinatesPanel = CoordinatesCalc(value);
            }
        }

        #region -For binding-
        private double x = 0;
        /// <summary>
        /// Binding Canvas.Left
        /// </summary>
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                base.NotifyPropertyChanged();
            }
        }

        private double y = 0;
        /// <summary>
        /// Binding Canvas.Bottom
        /// </summary>
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                base.NotifyPropertyChanged();
            }
        }
        #endregion
        

        //
        private Point CoordinatesCalc(Point _coordinatesInput)
        {
            Point coordReturn;

            double _x = _coordinatesInput.X - sizePanel.size.Width;
            double _y = _coordinatesInput.Y - sizePanel.size.Height / 2;
            X = _x;
            Y = _y;

            return coordReturn = new Point(_x, _y);
        }
    }
}
