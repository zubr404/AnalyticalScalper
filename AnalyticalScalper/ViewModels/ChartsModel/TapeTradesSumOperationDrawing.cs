using AnalyticalScalper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AnalyticalScalper.ViewModels.ChartsModel
{
    /// <summary>
    /// Рисуем количество последовательных покупок/продаж
    /// </summary>
    class TapeTradesSumOperationDrawing : TapeTradesDrawing
    {
        Brush brushBuy = Brushes.Green;
        Brush brushSell = Brushes.Red;

        // Implement abstract class TapeTradesDrawing
        public override void GetInitialeValues(DataTradesExchenge _dataTrades)
        {
            int countElement = 0;

            base.dispatcher.Invoke(() =>
            {
                countElement = base.drawElementCollection.Count;
            }, DispatcherPriority.Background);

            if (countElement == 0)
            {
                if (_dataTrades.VolumeBuy > 0) // Buy
                {
                    CollectionElementAdd(brushBuy, 0);
                }
                if (_dataTrades.VolumeSell > 0) // Sell
                {
                    CollectionElementAdd(brushSell, 0);
                }
            }
            else
            {
                Ellipse getEll = new Ellipse();

                base.dispatcher.InvokeAsync(() =>
                {
                    getEll = base.drawElementCollection[0];

                    if (_dataTrades.VolumeBuy > 0) // Buy
                    {
                        if (getEll.Fill == brushBuy)
                        {
                            getEll.Height += 1;
                            getEll.Width += 1;
                        }
                        else
                        {
                            CollectionElementAdd(brushBuy, 0);
                        }
                    }

                    if (_dataTrades.VolumeSell > 0) // Sell
                    {
                        if (getEll.Fill == brushSell)
                        {
                            getEll.Height += 1;
                            getEll.Width += 1;
                        }
                        else
                        {
                            CollectionElementAdd(brushSell, 0);
                        }
                    }
                });
            }
        }

        protected override void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // здесь таймер не нужен
            // это просто заглушка
        }
    }
}
