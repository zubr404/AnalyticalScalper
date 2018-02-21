using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AnalyticalScalper.ViewModels.ChartsModel
{
    /// <summary>
    /// Рисуем объем каждой сделки
    /// </summary>
    class TapeTradesEveryVolumeDraving : TapeTradesDrawing
    {
        Brush brushVolume = Brushes.PaleVioletRed;
        Brush brushNotVolume = Brushes.White;
        const double sizeNotVolume = 7;

        //**********************************************
        //* Implement abstract class TapeTradesDrawing *
        //**********************************************
        public override void GetInitialeValues(Model.DataTradesExchenge _dataTrades)
        {
            base.timer.Stop();
            base.CollectionElementAdd(brushVolume, _dataTrades.Volume);
            base.timer.Start();
        }
        protected override void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            base.CollectionElementAdd(brushNotVolume, sizeNotVolume);
        }
        //**********************************************
    }
}
