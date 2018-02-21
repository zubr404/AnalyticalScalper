using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AnalyticalScalper.ViewModels.ChartsModel
{
    /// <summary>
    /// График с положительными и отрицательными значениями по оси У.
    /// </summary>
    class ChartYPlusMinusMain : BaseClasses.PropertyChangedBase
    {
        #region - Данные для привязки к интерфейсу -
        /// <summary>
        /// Графические элементы графика
        /// </summary>
        public CollectionGraphicElementsChart ElementsChart { get; private set; }

        double y;
        /// <summary>
        /// Координата У
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

        double trendValue;
        /// <summary>
        /// Текущее значение тренда
        /// </summary>
        public double TrendValue
        {
            get { return trendValue; }
            set
            {
                trendValue = value;
                base.NotifyPropertyChanged();
            }
        }
        #endregion

        // -Предустановки-
        const double diapason = 2; // диапазон входных значений (от 1 до -1)
        Size sizeAllPoint;
        const double width = SettingsCharts.Width;
        Brush brushAll = Brushes.PaleTurquoise;
        //-----------------

        InitialDataChartList initialDataList;
        Dispatcher dispatcher;
        SizeGlobal sizePanel;

        // Constructor
        public ChartYPlusMinusMain(SizeGlobal _sizePanel)
        {
            sizePanel = _sizePanel;
            sizeAllPoint = new Size(width, 0);

            InitializationCollection();
            dispatcher = Dispatcher.CurrentDispatcher;
        }

        /// <summary>
        /// Рисуем график
        /// </summary>
        /// <param name="_initialeValue">входное значение [от 1 до -1]</param>
        public void ChartDraw(InitialValue _initialeValue)
        {
            double invalue = _initialeValue.Value;

            if (invalue > 1) { invalue = 1; }
            if (invalue < -1) { invalue = -1; }

            ScaleValue scaleValue = new ScaleValue(0, sizePanel.size.Height / diapason);
            Y = (1 - invalue) * scaleValue.Y_scale;
            TrendValue = invalue;
        }

        /// <summary>
        /// Рисуем график гистограм.
        /// </summary>
        /// <param name="_initialeValue"></param>
        public void ChartDrawGistogram(InitialValue _initialeValue)
        {
            double invalue = _initialeValue.Value;
            Point coordinates = new Point();
            double countElement = ElementsChart.Count;

            if (invalue > 1) { invalue = 1; }
            if (invalue < -1) { invalue = -1; }

            ScaleValue scaleValue = new ScaleValue(sizePanel.size.Width / SettingsCharts.maxCountValueCharts.Count, sizePanel.size.Height / diapason);
            coordinates.X = X_Set(countElement, scaleValue.X_scale);
            sizeAllPoint.Height = Math.Abs(invalue) * scaleValue.Y_scale;
            
            if (invalue != 0)
            {
                if (invalue > 0)
                {
                    coordinates.Y = (1 - invalue) * scaleValue.Y_scale;
                }
                else
                {
                    coordinates.Y = sizePanel.size.Height / diapason;
                }
            }
            else
            {
                coordinates.Y = sizePanel.size.Height / diapason;
            }

            dispatcher.Invoke(() =>
            {
                ElementsChart.Add(GistogramGenerate(coordinates, brushAll, sizeAllPoint));
            }, DispatcherPriority.Background);

            initialDataList.Add(_initialeValue);

            // масштабируем по Х
            TrimmingData_X(ElementsChart, initialDataList, scaleValue, ref countElement);
        }

        #region PRIVATE METHOD
        // Инициализатор коллекций
        private void InitializationCollection()
        {
            initialDataList = new InitialDataChartList();
            ElementsChart = new CollectionGraphicElementsChart();
        }

        // график типа: гистограмма
        private Rectangle GistogramGenerate(Point _coordinates, Brush _brush, Size _sizeElementChart)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Stroke = _brush;
            rectangle.Fill = _brush;
            rectangle.Width = _sizeElementChart.Width;
            rectangle.Height = _sizeElementChart.Height;
            Canvas.SetTop(rectangle, _coordinates.Y);
            Canvas.SetLeft(rectangle, _coordinates.X);

            return rectangle;
        }
        //----

        // обрезаем данные по Х с перемасшбированием по 2-м осям
        private void TrimmingData_X(CollectionGraphicElementsChart _chartElements, InitialDataChartList _initialDataChartList, ScaleValue _scaleValue, ref double _countElement)
        {
            if (_countElement > SettingsCharts.maxCountValueCharts.Count)
            {
                dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < SettingsCharts.countDelete.Count; i++)
                    {
                        _chartElements.RemoveAt(0);
                    }
                });

                _initialDataChartList.RemoveRange(0, SettingsCharts.countDelete.Count);

                _countElement = _chartElements.Count;

                double countElem = _countElement;
                dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < countElem; i++)
                    {
                        Canvas.SetLeft(_chartElements[i], X_Set(i, _scaleValue.X_scale));
                    }
                }, DispatcherPriority.Background);
            }
        }

        // устанавливаем текущие X координаты
        private double X_Set(double _countElement, double _scaleValueX)
        {
            return _countElement * _scaleValueX;
        }
        #endregion
    }
}
