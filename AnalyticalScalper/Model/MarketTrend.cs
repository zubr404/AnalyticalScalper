using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Определение рыночного тренда
    /// </summary>
    class MarketTrend
    {
        ExchangeInformation exchangeInformation;

        const int count_tick = 30;  //количество тиков для рассчета тренда

        double step_price;     // шаг цены по инструменту
        double price_previos = 0;   // предыдущая цена
        double trend = 0;

        List<double> pricesList = new List<double>();

        public MarketTrend(ExchangeInformation _exchangeInformation)
        {
            exchangeInformation = _exchangeInformation;
            exchangeInformation.DataConstatntParametrUpdate += exchangeInformation_DataConstatntParametrUpdate;
        }

        void exchangeInformation_DataConstatntParametrUpdate(object sender, DataConstantParametrEventArgs e)
        {
            step_price = e.DataNew.StepPrice;
        }        

        /// <summary>
        /// Расчет тренда
        /// </summary>
        /// <param name="_priceTrade"></param>
        public void TrendMeasure(double _priceTrade)
        {
            if (_priceTrade != price_previos)
            {
                price_previos = _priceTrade;
                double delta_price = 0;
                pricesList.Add(_priceTrade);

                if (pricesList.Count == count_tick + 1)
                {
                    delta_price = pricesList[count_tick] - pricesList[0];
                    trend = delta_price / (count_tick * step_price);
                    pricesList.RemoveAt(0);
                    exchangeInformation.Market_Trend = trend;
                }
                else
                {
                    exchangeInformation.Market_Trend = trend;
                }
            }
        }
    }
}
