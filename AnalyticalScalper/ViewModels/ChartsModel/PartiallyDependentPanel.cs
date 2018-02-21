using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnalyticalScalper.ViewModels.ChartsModel
{
    /// <summary>
    /// Частично зависимая панель (зависимость от координаты Х и цены деления Y основного графика)
    /// Класс изначально разработан для Bid/Offer
    /// </summary>
    class PartiallyDependentPanel : BaseClasses.PropertyChangedBase
    {
        private Thickness margin;
        private double inputValue;          // входящее значение, источник, отличный от основного графика
        private double inputMinValueMain;   // входящее минимальное значение исходных данных основного графика
        private double inputScaleValueY;    // входящее цена деления шкалы У основного графика

        private double y_panel;
        private double x_panel;

        public PartiallyDependentPanel(Thickness _margin)
        {
            margin = new Thickness(_margin.Left, _margin.Top, _margin.Right, _margin.Bottom);
        }

        /// <summary>
        /// Bходящее значение, источник, отличный от основного графика
        /// </summary>
        public double InputValue
        {
            get { return inputValue; }
            private set
            {
                inputValue = value;
                Y_tapeCalc(inputValue);
                base.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Конечная X координата панели
        /// </summary>
        public double X_panel
        {
            get { return x_panel; }
            private set
            {
                x_panel = X_panelCalc(value);
                base.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Конечная У координата панели
        /// </summary>
        public double Y_tape
        {
            get { return y_panel; }
            private set
            {
                y_panel = value;
                base.NotifyPropertyChanged();
            }
        }

        //
        private double X_panelCalc(double x_main)
        {
            return x_main + margin.Left;
        }

        /// <summary>
        /// Рассчет и обновление конечной координаты панели
        /// </summary>
        /// <param name="_inputValue">входящее значение для позиционирования</param>
        private void Y_tapeCalc(double _inputValue)
        {
            Y_tape = (_inputValue - inputMinValueMain) * inputScaleValueY + margin.Top;
        }

        /// <summary>
        /// Обновление информации, источником которой является основной график
        /// </summary>
        public void UpdateDependentValue(ValuesPartiallyDependentPanel _newValues)
        {
            inputMinValueMain = _newValues.MinValueMain;
            inputScaleValueY = _newValues.ScaleValueY;
            X_panel = _newValues.Xinput;
        }
        /// <summary>
        /// Обновление информации мз несависимого от основного графика источника
        /// </summary>
        public void UpdateFreeValue(double _value)
        {
            InputValue = _value;
        }
    }

    struct ValuesPartiallyDependentPanel
    {
        public readonly double MinValueMain;
        public readonly double ScaleValueY;
        public readonly double Xinput;

        public ValuesPartiallyDependentPanel(double _minValMain, double _scaleValY, double _xInp)
        {
            MinValueMain = _minValMain;
            ScaleValueY = _scaleValY;
            Xinput = _xInp;
        }
    }
}
