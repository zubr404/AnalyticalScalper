using AnalyticalScalper.ServiceFunc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Вычисляем скорость каждой сделки в сд/сек
    /// </summary>
    class SpeedTrades
    {
        ExchangeInformation exchangeInformation;

        private double TotalMillisec_trade_previos = double.MaxValue;
        List<double> speedList = new List<double>();

        const double sec = 1000;        // секунда, выраженная в миллисекундах.
        const int countSpeedList = 25;  // количество периодов для средней

        public SpeedTrades(ExchangeInformation _exchangeInformation)
        {
            exchangeInformation = _exchangeInformation;
        }

        /// <summary>
        /// Pасчет скорости сделок в сд/сек
        /// </summary>
        /// <param name="_timeTrade">время сделки 00:00:00</param>
        /// <param name="_time_mcs">время микросекунды</param>
        public void TradesSpeed(string _timeTrade, double _time_mcs)
        {
            double speed = 0;
            double TotalMillisec_trade = ConvertFunc.TotalMillisecondTradeCalc(_timeTrade, _time_mcs);

            double delta_millisec = TotalMillisec_trade - TotalMillisec_trade_previos;
            TotalMillisec_trade_previos = TotalMillisec_trade;

            if (delta_millisec <= 0)
            {
                return;
            }

            speed = sec / delta_millisec;
            speedList.Add(speed);

            if (speedList.Count < countSpeedList)
            {
                return;
            }
            else
            {
                exchangeInformation.SpeedTrades = speedList.Average();
                speedList.RemoveAt(0);
            }
        }
        //----------------------------------------------------
    }
}
