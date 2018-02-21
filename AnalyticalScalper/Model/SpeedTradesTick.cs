using AnalyticalScalper.ServiceFunc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Скорость сделок с группировкой по тикам
    /// </summary>
    class SpeedTradesTick
    {
        ExchangeInformation exchangeInformation;
        private double TotalMillisecTradeStart = 0;       // время первой сделки по данной цене
        private double priceTradesPrevios = -1;           // цена предыдущей сделки
        private bool session_check = false;               // только сделки основной сессии
        const double sec = 1000;        // секунда, выраженная в миллисекундах.

        const int countValue = 20;      // количество значений для усреднения
        List<double> speedList;         // для расчета средней

        public SpeedTradesTick(ExchangeInformation _exchangeInformation)
        {
            exchangeInformation = _exchangeInformation;
            speedList = new List<double>();
        }

        /// <summary>
        /// Скорость сделок с группировкой по тикам
        /// </summary>
        /// <param name="_timeTrade">время сделки 00:00:00</param>
        /// <param name="_time_mcs">время сделки микросекунды</param>
        /// <param name="_priceTrade">цена сделки</param>
        public void SpeedTradesTickMeasure(string _timeTrade, double _time_mcs, double _priceTrade)
        {
            if (!ServiceFunc.CheckFunc.CheckTimeSession(_timeTrade, ref session_check)) { return; }

            double speed = 0;
            double TotalMillisec_trade = ConvertFunc.TotalMillisecondTradeCalc(_timeTrade, _time_mcs);

            if (_priceTrade != priceTradesPrevios)
            {
                double delta = TotalMillisec_trade - TotalMillisecTradeStart;
                TotalMillisecTradeStart = TotalMillisec_trade;

                if (delta < 0)
                {
                    exchangeInformation.SpeedTradesTick = 0;
                    MessageBox.Show("delta меньше НУЛЯ!");
                }
                else
                {
                    if (delta == 0) { delta = 1; }      // если delta==0, будем считать, что сделки в тике прошли с минимальным разрывом.
                    speed = sec / delta;
                    
                    double maxspeed_for_view = 100;                                 // ограничение для отображения на графике
                    if (speed > maxspeed_for_view) { speed = maxspeed_for_view; }

                    exchangeInformation.SpeedTradesTick = speed;
                    SpeedAvg(speed);
                }
            }
            else
            {
                /*здесь вариант если буду считать скорость до смены тика*/
            }
            priceTradesPrevios = _priceTrade;
        }

        // Расчет средней скорости
        private void SpeedAvg(double _speedValue)
        {
            speedList.Add(_speedValue);

            if (speedList.Count == countValue)
            {
                exchangeInformation.SpeedTradesAvg = speedList.Average();
                speedList.RemoveAt(0);
            }
        }
    }
}
