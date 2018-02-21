using AnalyticalScalper.Model;
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
    /// Основная панель графика
    /// </summary>
    class ChartPanelMain : BaseClasses.PropertyChangedBase
    {
        #region - Данные для привязки к интерфейсу -        
        /// <summary>
        /// Графические элементы графика
        /// </summary>
        public CollectionGraphicElementsChart ElementsChart { get; private set; }

        private double Ycoordinate_line;
        /// <summary>
        /// "У" координата линии, указывающей на последнее значение. 
        /// </summary>
        public double YCoordinateLine
        {
            get { return Ycoordinate_line; }
            set
            {
                Ycoordinate_line = value;
                base.NotifyPropertyChanged();
            }
        }

        private double currentValue;
        /// <summary>
        /// Текущее значение
        /// </summary>
        public double CurrentValue
        {
            get { return currentValue; }
            set
            {
                currentValue = value;
                base.NotifyPropertyChanged();
            }
        }
        #endregion

        #region - Свойства генерирующие события не PropertyChangedBase -
        double Yscale;
        /// <summary>
        /// Цена деления У-координаты
        /// </summary>
        public double YScale
        {
            get { return Yscale; }
            private set
            {
                if (value != Yscale)
                {
                    Yscale = value;
                    YScaleUpdate(null, new DoubleValueEventArgs(Yscale));
                }
            }
        }

        double min_value;
        /// <summary>
        /// Минимальное значение
        /// </summary>
        public double MinValue
        {
            get { return min_value; }
            set
            {
                if (value != min_value)
                {
                    min_value = value;
                    MinValueUpdate(null, new DoubleValueEventArgs(min_value));
                }
            }
        }
        #endregion

        // -Предустановки-
        Size sizeAllPoint;
        const double height = 5;
        const double width = SettingsCharts.Width;
        ColorElementsChart colorElemChart;
        Brush brushAll = Brushes.DarkOrange;
        Brush brushLast = Brushes.Red;
        //-----------------

        InitialDataChartList initialDataList;
        ScaleValue scaleValue;
        ExtremumValue extremumInitialeValue;

        Dispatcher dispatcher;

        SizeGlobal sizePanel;                                       // размер панели интерфейса
        List<MultiDependetPanelTrades> FullyDependentPanelList;     // коллекция дополнительных панелей (полностью зависимые)
        List<PartiallyDependentPanel> PartiallyDependentPanelList;  // коллекция дополнительных панелей (частично зависимые)

        public Action<InitialValue> DrawChartAction;

        #region -CONSTRUCTOR с использованием делегата-
        public ChartPanelMain(SizeGlobal _sizePanel, ChartTypeEnum _chartTypeEnum)
        {
            sizePanel = _sizePanel;
            sizeAllPoint = new Size(width, height);
            colorElemChart = new ColorElementsChart(brushAll, brushLast);

            InitializationCollection();
            CheckNunableCollection();
            DefaultMinMaxSet();
            dispatcher = Dispatcher.CurrentDispatcher;

            SetTypeChart(_chartTypeEnum);
        }
        //
        public ChartPanelMain(List<MultiDependetPanelTrades> _fullyDependentPanelList, List<PartiallyDependentPanel> _partiallyDependentPanel, SizeGlobal _sizePanel, ChartTypeEnum _chartTypeEnum)
        {
            sizePanel = _sizePanel;
            FullyDependentPanelList = _fullyDependentPanelList;
            PartiallyDependentPanelList = _partiallyDependentPanel;

            sizeAllPoint = new Size(width, height);
            colorElemChart = new ColorElementsChart(brushAll, brushLast);

            InitializationCollection();
            CheckNunableCollection();
            DefaultMinMaxSet();
            dispatcher = Dispatcher.CurrentDispatcher;

            SetTypeChart(_chartTypeEnum);
        }
        #endregion

        #region -Рисует график-
        /// <summary>
        /// Рисует график
        /// </summary>
        /// <param name="_initialeValue">исходное значение</param>
        private void ChartsDraw(InitialValue _initialeValue, Func<Point, Brush, Size, Rectangle> _generateElementChartAction, Action<Rectangle, InitialValue, double, double> _scaleYTypeAction, Action<Rectangle, double> _setBottomAction)
        {
            Size sizeChart = new Size();
            ChangeMinMax changeMinMax;
            Point coordinates = new Point();

            double countElement = ElementsChart.Count;

            sizeChart = new Size(sizePanel.size.Width, sizePanel.size.Height);
            changeMinMax = new ChangeMinMax(MinCalc(_initialeValue, ref extremumInitialeValue), MaxCalc(_initialeValue, ref extremumInitialeValue));

            scaleValue.X_scale = X_ScaleValueCalc(SettingsCharts.maxCountValueCharts.Count, sizeChart.Width);
            scaleValue.Y_scale = Y_ScaleValueCalc(extremumInitialeValue, sizeChart);
            YScale = scaleValue.Y_scale;
            MinValue = extremumInitialeValue.Min;

            coordinates.Y = Y_Set(_initialeValue, extremumInitialeValue.Min, scaleValue.Y_scale);
            YCoordinateLine = sizePanel.size.Height - coordinates.Y; // для линии начало системы координат находится в верхнем левом углу, мой график сделан для начала координат в нижнем левом углу.
            CurrentValue = _initialeValue.Value;

            // масштабирование по У если изменился макс/мин
            ScaleChart_Y(changeMinMax, countElement, ElementsChart, initialDataList, extremumInitialeValue, scaleValue, _scaleYTypeAction);

            // масштабируем по Х
            TrimmingData_X(ElementsChart, initialDataList, sizeChart, _setBottomAction, ref scaleValue, ref extremumInitialeValue, ref countElement);

            coordinates.X = X_Set(countElement, scaleValue.X_scale);

            dispatcher.Invoke(() =>
            {
                ElementsChart.Add(_generateElementChartAction(coordinates, colorElemChart.BrushAll, sizeAllPoint));
            }, DispatcherPriority.Background);

            initialDataList.Add(_initialeValue);

            // обновляем данные для дополнительных панелей
            foreach (var item in FullyDependentPanelList)
            {
                item.FullyDependentPanel.CoordinatesPanel = new Point(coordinates.X, coordinates.Y);
            }
            foreach (var item in PartiallyDependentPanelList)
            {                
                item.UpdateDependentValue(new ValuesPartiallyDependentPanel(extremumInitialeValue.Min, scaleValue.Y_scale, coordinates.X));
            }
            //--------------------------------------------
        }
        #endregion

        #region -Создание графиков определенного типа-
        private void ChartPointType(InitialValue _initialeValue)        // Point type
        {
            ChartsDraw(_initialeValue, PointGenerate, ScaleY_Point, CanvasSetBottomPoint);
        }

        private void ChartGistogrammType(InitialValue _initialeValue)   // Gistogramm type
        {
            ChartsDraw(_initialeValue, GistogramGenerate, ScaleY_Gistogramm, CanvasSetBottomGistogramm);
        }
        #endregion

        #region -Private method-
        // Инициализатор коллекций
        private void InitializationCollection()
        {
            initialDataList = new InitialDataChartList();
            ElementsChart = new CollectionGraphicElementsChart();
        }

        // проверка kоллекций на null
        private void CheckNunableCollection<T>(ref List<T> _collection) where T: class
        {
            if (_collection == null)
            {
                _collection = new List<T>();
            }
        }
        private void CheckNunableCollection()
        {
            if (FullyDependentPanelList == null)
            {
                FullyDependentPanelList = new List<MultiDependetPanelTrades>();
            }
            if (PartiallyDependentPanelList == null)
            {
                PartiallyDependentPanelList = new List<PartiallyDependentPanel>();
            }
        }
        //----------------------------------

        // установка методов для рисования графика определенного в конструкторе типа
        private void SetTypeChart(ChartTypeEnum _chartTypeEnum)
        {
            switch (_chartTypeEnum)
            {
                case ChartTypeEnum.Point:
                    DrawChartAction = ChartPointType;
                    break;
                case ChartTypeEnum.Gistogramm:
                    DrawChartAction = ChartGistogrammType;
                    break;
                default:
                    break;
            }
        }

        // max value calc
        private bool MaxCalc(InitialValue _currentValue, ref ExtremumValue _extremumValue)
        {
            if (_extremumValue.Max == double.MinValue)
            {
                _extremumValue.Max = _currentValue.Value;
            }
            else
            {
                if (_currentValue.Value > _extremumValue.Max)
                {
                    _extremumValue.Max = _currentValue.Value;
                    return true;
                }
            }

            return false;
        }

        // min value calc
        private bool MinCalc(InitialValue _currentValue, ref ExtremumValue _extremumValue)
        {
            if (_extremumValue.Min == double.MaxValue)
            {
                _extremumValue.Min = _currentValue.Value;
            }
            else
            {
                if (_currentValue.Value < _extremumValue.Min)
                {
                    _extremumValue.Min = _currentValue.Value;
                    return true;
                }
            }

            return false;
        }

        // Yscale value calc
        private double Y_ScaleValueCalc(ExtremumValue _extremumValue, Size _sizeCharts)
        {
            double scale = _sizeCharts.Height;
            double delta = _extremumValue.Max - _extremumValue.Min;

            if (delta > 0)
            {
                scale = _sizeCharts.Height / delta;
            }
            return scale;
        }

        // Xscale value calc
        private double X_ScaleValueCalc(double _countValueHorisontal, double _widthCart)
        {
            return _widthCart / _countValueHorisontal;
        }

        // устанавливаем текущие Y координаты
        private double Y_Set(InitialValue _initialValue, double _minValue, double _scaleValueY)
        {
            return (_initialValue.Value - _minValue) * _scaleValueY;
        }

        // устанавливаем текущие X координаты
        private double X_Set(double _countElement, double _scaleValueX)
        {
            return _countElement * _scaleValueX;
        }

        // default  min/max price/position set
        private void DefaultMinMaxSet()
        {
            extremumInitialeValue = new ExtremumValue(double.MaxValue, double.MinValue);
        }

        // создаем графические элементы графика
        private Ellipse EllipseGenerate(Point _coordinates, Brush _brush, Size _sizeElementChart)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = _brush;
            ellipse.Fill = _brush;
            ellipse.Width = _sizeElementChart.Width;
            ellipse.Height = _sizeElementChart.Height;
            Canvas.SetBottom(ellipse, _coordinates.Y);
            Canvas.SetLeft(ellipse, _coordinates.X);

            return ellipse;
        }

        // график типа: точка
        private Rectangle PointGenerate(Point _coordinates, Brush _brush, Size _sizeElementChart)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Stroke = _brush;
            rectangle.Fill = _brush;
            rectangle.Width = _sizeElementChart.Width;
            rectangle.Height = _sizeElementChart.Height;
            Canvas.SetBottom(rectangle, _coordinates.Y);
            Canvas.SetLeft(rectangle, _coordinates.X);

            return rectangle;
        }

        // график типа: гистограмма
        private Rectangle GistogramGenerate(Point _coordinates, Brush _brush, Size _sizeElementChart)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Stroke = _brush;
            rectangle.Fill = _brush;
            rectangle.Width = _sizeElementChart.Width;
            rectangle.Height = _coordinates.Y;
            Canvas.SetBottom(rectangle, 0);
            Canvas.SetLeft(rectangle, _coordinates.X);

            return rectangle;
        }
        //-------

        // автомасштабирование по оси У
        private void ScaleChart_Y(ChangeMinMax _changeMinMax, double _countValueChart, CollectionGraphicElementsChart _collectionGraphicElementsChart, InitialDataChartList _initialDataChartList, ExtremumValue _extremumValue, ScaleValue _scaleValue, Action<Rectangle, InitialValue, double, double> _scaleYTypeAction)
        {
            if (_changeMinMax.ChangeMin | _changeMinMax.ChangeMax)
            {
                dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < _countValueChart; i++)
                    {
                        _scaleYTypeAction(_collectionGraphicElementsChart[i], _initialDataChartList[i], _extremumValue.Min, _scaleValue.Y_scale);
                    }
                });
            }
        }

        private void ScaleY_Point(Rectangle _point, InitialValue _initialeValue, double _minValue, double _yScale)
        {
            if (_initialeValue.Value < _minValue)
            {
                MessageBox.Show("Вот.");
            }
            Canvas.SetBottom(_point, Y_Set(_initialeValue, _minValue, _yScale));
        }
        private void ScaleY_Gistogramm(Rectangle _gistogramm, InitialValue _initialeValue, double _minValue, double _yScale)
        {
            // -! ЗДЕСЬ КОСЯК (_initialeValue < _minValue так быть не должно)-
            //костыль
            double h = Y_Set(_initialeValue, _minValue, _yScale);
            if (h < 0)
            {
                h = 0;
            }
            //--------------------
            _gistogramm.Height = h;
        }
        //----------------------------------------------------------

        // обрезаем данные по Х с перемасшбированием по 2-м осям
        private void TrimmingData_X(CollectionGraphicElementsChart _chartElements, InitialDataChartList _initialDataChartList, Size _sizeChart, Action<Rectangle, double> _setBottomAction, ref ScaleValue _scaleValue, ref ExtremumValue _extremumValue, ref double _countElement)
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

                //_extremumValue.Min = _initialDataChartList.Min(x => x.Value);
                //_extremumValue.Max = _initialDataChartList.Max(x => x.Value);

                _extremumValue = new ExtremumValue(_initialDataChartList.Min(x => x.Value), _initialDataChartList.Max(x => x.Value));

                _scaleValue.Y_scale = Y_ScaleValueCalc(_extremumValue, _sizeChart);

                double countElem = _countElement;
                double y_scl = _scaleValue.Y_scale;
                double minVal = _extremumValue.Min;
                ScaleValue scalval = _scaleValue;
                dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < countElem; i++)
                    {
                        _setBottomAction(_chartElements[i], Y_Set(_initialDataChartList[i], minVal, y_scl));
                        Canvas.SetLeft(_chartElements[i], X_Set(i, scalval.X_scale));
                    }
                }, DispatcherPriority.Background);
            }
        }

        private void CanvasSetBottomPoint(Rectangle _rectangle, double _Y)
        {
            Canvas.SetBottom(_rectangle, _Y);
        }
        private void CanvasSetBottomGistogramm(Rectangle _rectangle, double _Y)
        {
            _rectangle.Height = _Y;
        }
        //--------------

        // изменение элементов графика
        private void ChangeElementCart(Rectangle _rectangle, Brush _brush, Size _sizeElementChart)
        {
            _rectangle.Fill = _brush;
            _rectangle.Stroke = _brush;
            _rectangle.Width = _sizeElementChart.Width;
            _rectangle.Height = _sizeElementChart.Height;
        }
        #endregion

        #region - Event -
        /// <summary>
        /// Происходит при обновлении цены деления У координаты
        /// </summary>
        public event EventHandler<DoubleValueEventArgs> YScaleUpdate = delegate { };
        /// <summary>
        /// Происходит при обновлении MinValue
        /// </summary>
        public event EventHandler<DoubleValueEventArgs> MinValueUpdate = delegate { };
        #endregion
    }
}
