using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.ViewModels.ChartsModel
{
    /// <summary>
    /// Панель, зависимая от цены деления У основной панели.
    /// </summary>
    class DependentYScalePanel : BaseClasses.PropertyChangedBase
    {
        private double y_scale;
        private double min_valueMain; // минимальное значение из основного графика
        private double current_value; // храним текущее значение, являющееся исходным для рассчета У

        public DependentYScalePanel(ChartPanelMain _chartPanelMain)
        {
            _chartPanelMain.YScaleUpdate += _chartPanelMain_YScaleUpdate;
            _chartPanelMain.MinValueUpdate += _chartPanelMain_MinValueUpdate;
        }

        void _chartPanelMain_MinValueUpdate(object sender, Model.DoubleValueEventArgs e)
        {
            min_valueMain = e.DoubleValueNew;
        }

        void _chartPanelMain_YScaleUpdate(object sender, Model.DoubleValueEventArgs e)
        {
            y_scale = e.DoubleValueNew;
            ChartsDraw(current_value); // перерасчет при змен. в основном графике
        }

        public void ChartsDraw(double value)
        {
            current_value = value;
            Y = (value - min_valueMain) * y_scale;
        }


        private double y;
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                base.NotifyPropertyChanged();
            }
        }
    }
}
