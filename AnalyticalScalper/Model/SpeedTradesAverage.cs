using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Средняя скорость сделок в сд/сек
    /// </summary>
    class SpeedTradesAverage
    {
        ExchangeInformation exchangeInformation;
        const double timeStartMarket = 36000; // начало торгов, выраженное в секундах        
        bool session_check = false;                 // только сделки основной сессии

        public SpeedTradesAverage(ExchangeInformation _exchangeInformation)
        {
            exchangeInformation = _exchangeInformation;
        }


        int count_all_trades = 0;                   // счетчик всех сделок
        string timeTradePrevios = string.Empty;     // время предыдущей сделки
        /// <summary>
        /// Рассчет средней скорости сделок за день в сд/сек
        /// </summary>
        /// <param name="_timeTrade">время сделки 00:00:00</param>
        public void TradesSpeedAvDaily(string _timeTrade)
        {
            if (!ServiceFunc.CheckFunc.CheckTimeSession(_timeTrade, ref session_check)) { return; }

            count_all_trades++;
            double speed = 0;

            if (_timeTrade != timeTradePrevios) // если время изменилось
            {
                timeTradePrevios = _timeTrade;
                DateTime datetime = Convert.ToDateTime(_timeTrade);
                double totalSecTrade = (new TimeSpan(datetime.Hour, datetime.Minute, datetime.Second)).TotalSeconds;
                double deltaTime = totalSecTrade - timeStartMarket;
                speed = count_all_trades / deltaTime;

                exchangeInformation.SpeedTradesAvgDayli = speed * TradersData.k_Avg;
            }
        }



        double count_trades;
        /// <summary>
        /// Рассчет средней скорости сделок за период (пока сделано за секунду)
        /// </summary>
        /// <param name="_timeTrade">время сделки 00:00:00</param>
        [Obsolete("Пока не использовать.", true)]
        public void TradesSpeedAvPeriod(string _timeTrade)
        {
            if (!ServiceFunc.CheckFunc.CheckTimeSession(_timeTrade, ref session_check)) { return; }

            count_trades++;

            if (_timeTrade != timeTradePrevios) // если время изменилось (чаще всего секунда)
            {
                timeTradePrevios = _timeTrade;
                exchangeInformation.SpeedTradesAvg = count_trades;
                count_trades = 0;
            }
        }
    }
}
