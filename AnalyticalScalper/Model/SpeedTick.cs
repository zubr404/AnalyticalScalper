using AnalyticalScalper.ServiceFunc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Измеряем время прохождения тиков вверх или вниз
    /// </summary>
    class SpeedTick
    {
        ExchangeInformation exchangeInformation;

        const string operationBuy = "Купля";
        const string operationSell = "Продажа";

        const double sec = 1000;        // секунда, выраженная в миллисекундах.
        double startTimeBuy = 0;
        double previosPriceBuy = 0;

        List<double> speedListBuy = new List<double>();
        List<double> speedListSell = new List<double>();
        const int countSpeedList = 25;  // количество периодов для средней

        // Constructor
        public SpeedTick(ExchangeInformation _exchangeInformation)
        {
            exchangeInformation = _exchangeInformation;
        }

        /// <summary>
        /// Измеряем скорость тиков
        /// </summary>
        /// <param name="_timeTrade">время сделки 00:00:00</param>
        /// <param name="_time_mcs">время микросекунды</param>
        /// <param name="_priceTrade">цена сделки</param>
        /// <param name="_operationTrade">операция сделки</param>
        public void TickSpeedMeasure(string _timeTrade, double _time_mcs, double _priceTrade, string _operationTrade)
        {
            if (_operationTrade == operationBuy)
            {
                TickBuySpeed(_timeTrade, _time_mcs, _priceTrade);
            }
            else
            {

            }
        }

        // скорость тиков вверх
        private void TickBuySpeed(string _timeTrade, double _time_mcs, double _price)
        {
            double speed = 0;
            double TotalMillisec_trade = ConvertFunc.TotalMillisecondTradeCalc(_timeTrade, _time_mcs);

            if (_price > previosPriceBuy)
            {
                double delta_millisec = TotalMillisec_trade - startTimeBuy;
                startTimeBuy = TotalMillisec_trade;

                if (delta_millisec <= 0) 
                { 
                    return; 
                }

                speed = sec / delta_millisec;
                exchangeInformation.SpeedTickBuy = AverageSpeed(speed, speedListBuy);

                previosPriceBuy = _price;
            }
            else
            {
                if (_price < previosPriceBuy)
                {
                    previosPriceBuy = _price;
                }
            }
        }

        // средняя скорость
        private double AverageSpeed(double _speed, List<double> _listSpeed)
        {
            double out_value = -1;

            _listSpeed.Add(_speed);

            if (_listSpeed.Count >= countSpeedList)
            {
                out_value = _listSpeed.Average();
                _listSpeed.RemoveAt(0);
            }

            return out_value;
        }
    }
}
