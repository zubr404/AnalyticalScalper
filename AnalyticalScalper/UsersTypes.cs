using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AnalyticalScalper
{
    /// <summary>
    /// Исходное значение для графика
    /// </summary>
    public struct InitialValue
    {
        public double Value;
        public InitialValue(double _value)
        {
            Value = _value;
        }
    }

    /// <summary>
    /// Исходные данные для построения основных графиков
    /// </summary>
    public class InitialDataChartList : List<InitialValue> { }

    /// <summary>
    /// Kоллекция графических элементов графика
    /// </summary>
    public class CollectionGraphicElementsChart : ObservableCollection<Rectangle> { }

    /// <summary>
    /// Цена деления для осей X/Y
    /// </summary>
    public struct ScaleValue
    {
        public double X_scale;
        public double Y_scale;

        public ScaleValue(double _scaleX, double _scaleY)
        {
            X_scale = _scaleX;
            Y_scale = _scaleY;
        }
    }

    /// <summary>
    /// Минимальные/максимальные значения
    /// </summary>
    public struct ExtremumValue
    {
        public double Min;
        public double Max;

        public ExtremumValue(double _min, double _max)
        {
            Min = _min;
            Max = _max;
        }
    }

    /// <summary>
    /// Булево значение, отражающее изменение максимального/минимального значения
    /// </summary>
    public struct ChangeMinMax
    {
        public bool ChangeMin;
        public bool ChangeMax;

        public ChangeMinMax(bool _changeMin, bool _changeMax)
        {
            ChangeMin = _changeMin;
            ChangeMax = _changeMax;
        }
    }

    /// <summary>
    /// Отступы графика
    /// </summary>
    public struct ChartMargin
    {
        public readonly double MarginTop;
        public readonly double MarginRight;
        public readonly double MarginBottom;

        public ChartMargin(double _top, double _right, double _bottom)
        {
            MarginTop = _top;
            MarginRight = _right;
            MarginBottom = _bottom;
        }
    }

    /// <summary>
    /// Максимальное количество значений на графике
    /// </summary>
    public struct MaxCountValueChart
    {
        public readonly double Count;
        public MaxCountValueChart(double _count)
        {
            Count = _count;
        }
    }

    /// <summary>
    /// Количество удаляемых элементов графика, при количестве элементов,
    /// достигшем CountValueChart
    /// </summary>
    public struct CountDeleteValueChart
    {
        public readonly int Count;
        public CountDeleteValueChart(int _count)
        {
            Count = _count;
        }
    }

    /// <summary>
    /// Цвет элементов, составляющих график
    /// </summary>
    public struct ColorElementsChart
    {
        public readonly Brush BrushAll;
        public readonly Brush BrushLast;

        public ColorElementsChart(Brush _brushAll, Brush _brushLast)
        {
            BrushAll = _brushAll;
            BrushLast = _brushLast;
        }
    }
}
