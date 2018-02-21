using DdeInputDataQuikLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Получение данных от DDE All Trades.
    /// Расчет показателей на их основе
    /// </summary>
    class ProcessingAllTrades
    {
        ExchangeInformation exchangeInfomation;
        SpeedTrades speedTrades;
        SpeedTick speedTick;
        SpeedTradesTick speedTradesTick;
        SpeedTradesAverage speedTradesAvgDayli;
        MarketTrend marketTrend;

        // -Constructor-
        public ProcessingAllTrades(IExportDDE _alltradesChannel, ExchangeInformation _exchangeInfomation)
        {
            exchangeInfomation = _exchangeInfomation;
            speedTrades = new SpeedTrades(exchangeInfomation);
            speedTick = new SpeedTick(exchangeInfomation);
            speedTradesTick = new SpeedTradesTick(exchangeInfomation);
            speedTradesAvgDayli = new SpeedTradesAverage(exchangeInfomation);
            marketTrend = new MarketTrend(exchangeInfomation);

            _alltradesChannel.LoadedLineEvent += AllTradesChannel_LoadedLineEvent;
        }

        // -handler All Trades-
        void AllTradesChannel_LoadedLineEvent(object sender, DdeInputDataQuikLib.DDEChannelsMarketEventArgs e)
        {
            DataTradesExchenge dataTrades;
            double volume = e.Quantity;
            double volume_buy = 0;
            double volume_sell = 0;
            
            if (e.Operation == "Купля")
            {
                volume_buy = e.Quantity;
            }
            else
            {
                volume_sell = e.Quantity;
            }
            
            dataTrades = new DataTradesExchenge(e.Price, Convert.ToDateTime(e.Date + " " + e.Time), volume, volume_buy, volume_sell);

            exchangeInfomation.DataTradesExcheng = dataTrades;
            exchangeInfomation.CountOpenPosition += ConvertQtyToOperation(e.Quantity, e.Operation);
            speedTrades.TradesSpeed(e.Time, e.TimeMsc);
            speedTick.TickSpeedMeasure(e.Time, e.TimeMsc, e.Price, e.Operation);
            speedTradesTick.SpeedTradesTickMeasure(e.Time, e.TimeMsc, e.Price);
            speedTradesAvgDayli.TradesSpeedAvDaily(e.Time);
            marketTrend.TrendMeasure(e.Price);
        }

        // -calc open position-
        private double ConvertQtyToOperation(double _quantity, string _operation)
        {
            double qty = _quantity;

            if (_operation == "Продажа")
            {
                qty *= -1;
            }

            return qty;
        }
    }
}
