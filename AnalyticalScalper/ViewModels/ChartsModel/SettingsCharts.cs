using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.ViewModels.ChartsModel
{
    /// <summary>
    /// Содержаться параметры, кот. должны быть общими для всех графиков
    /// </summary>
    class SettingsCharts
    {
        public static MaxCountValueChart maxCountValueCharts = new MaxCountValueChart(300);
        public static CountDeleteValueChart countDelete = new CountDeleteValueChart(1);
        public const double Width = 2;
    }
}
