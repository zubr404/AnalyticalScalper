using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Объединяет в себе данные из DDe каналов и расчитанные на их осноке показатели:
    /// таблица всех сделок, таблица текущих значений, стакан и другие таблицы Квика,
    /// содержащие биржевую информацию.
    /// </summary>
    class ExchangeInformation
    {
        #region -All trades-
        DataTradesExchenge dataTradesExchenge;
        /// <summary>
        /// Содержит данные о сделке из AllTrades
        /// </summary>
        public DataTradesExchenge DataTradesExcheng
        {
            get { return dataTradesExchenge; }
            set
            {
                dataTradesExchenge = value;
                DataTradesExchengeUpdate(null, new DataTradesExchengeEventArgs(dataTradesExchenge));
            }
        }

        double countOpenPosition;
        /// <summary>
        /// Текущая чистая позиция всего рынка (на началo дня = 0)
        /// </summary>
        public double CountOpenPosition
        {
            get { return countOpenPosition; }
            set
            {
                countOpenPosition = value;
                CountOpenPositionUpdate(null, new DoubleValueEventArgs(countOpenPosition));
            }
        }

        double speed_trades;
        /// <summary>
        /// Скорость сделок в сдел/сек
        /// </summary>
        public double SpeedTrades
        {
            get { return speed_trades; }
            set
            {
                speed_trades = value;
                SpeedTradesUpdate(null, new DoubleValueEventArgs(speed_trades));
            }
        }

        double speed_tick_buy;
        /// <summary>
        /// Скорость тиков вверх сд/сек
        /// </summary>
        public double SpeedTickBuy
        {
            get { return speed_tick_buy; }
            set
            {
                if (value > 0)
                {
                    speed_tick_buy = value;
                    SpeedTickBuyUpdate(null, new DoubleValueEventArgs(speed_tick_buy));
                }
            }
        }

        double speed_trades_tick;
        /// <summary>
        /// Скорость сделок с группировкой по тикам
        /// </summary>
        public double SpeedTradesTick
        {
            get { return speed_trades_tick; }
            set
            {
                if (value >= 0)
                {
                    speed_trades_tick = value;
                    SpeedTradesTickUpdate(null, new DoubleValueEventArgs(speed_trades_tick));
                }
            }
        }

        double speedTradesAvgDayli;
        /// <summary>
        /// средняя скорость сделок за день
        /// </summary>
        public double SpeedTradesAvgDayli
        {
            get { return speedTradesAvgDayli; }
            set
            {
                if (value > 0)
                {
                    speedTradesAvgDayli = value;
                    SpeedTradesAvgDayliUpdate(null, new DoubleValueEventArgs(speedTradesAvgDayli));
                }
            }
        }

        double speedTradesAvg;
        /// <summary>
        /// средняя скорость сделок
        /// </summary>
        public double SpeedTradesAvg
        {
            get { return speedTradesAvg; }
            set
            {
                if (value > 0)
                {
                    speedTradesAvg = value;
                    SpeedTradesAvgUpdate(null, new DoubleValueEventArgs(speedTradesAvg));
                }
            }
        }

        double marketTrend;
        /// <summary>
        /// Рыночный тренд
        /// </summary>
        public double Market_Trend
        {
            get { return marketTrend; }
            set
            {
                marketTrend = value;
                Market_TrendUpdate(null, new DoubleValueEventArgs(marketTrend));
            }
        }
        #endregion

        #region -Current table-
        DataMarketCurrentParametrExchenge dataCurrentParametrExchenge;
        /// <summary>
        /// Содержит значения текущих ТОРГОВЫХ параметров по инструменту
        /// </summary>
        public DataMarketCurrentParametrExchenge DataMarketParametrExchenge
        {
            get { return dataCurrentParametrExchenge; }
            set
            {
                dataCurrentParametrExchenge = value;
                DataMarketParametrUpdate(null, new DataMarketParametrEventArgs(dataCurrentParametrExchenge));
            }
        }

        DataConstatntCurrentParametrExchenge dataConstatntCurrentParametrExchenge;
        /// <summary>
        /// Содержит значения текущих ТЕХНИЧЕСКИХ параметров по инструменту
        /// </summary>
        public DataConstatntCurrentParametrExchenge DataConstatntParametrExchenge
        {
            get { return dataConstatntCurrentParametrExchenge; }
            set
            {
                dataConstatntCurrentParametrExchenge = value;
                DataConstatntParametrUpdate(null, new DataConstantParametrEventArgs(dataConstatntCurrentParametrExchenge));
            }
        }
        #endregion        

        #region -Events-
        /// <summary>
        /// Происходит при обновлении DataTradesExcheng
        /// </summary>
        public event EventHandler<DataTradesExchengeEventArgs> DataTradesExchengeUpdate = delegate { };
        /// <summary>
        /// Происходит при обновлении DataMarketCurrentParametrExchenge
        /// </summary>
        public event EventHandler<DataMarketParametrEventArgs> DataMarketParametrUpdate = delegate { };
        /// <summary>
        /// Происходит при обновлении DataConstatntParametrExchenge
        /// </summary>
        public event EventHandler<DataConstantParametrEventArgs> DataConstatntParametrUpdate = delegate { };
        /// <summary>
        /// Происходит при обновлении CountOpenPosition
        /// </summary>
        public event EventHandler<DoubleValueEventArgs> CountOpenPositionUpdate = delegate { };
        /// <summary>
        /// Происходит при обновлении SpeedTrades
        /// </summary>
        public event EventHandler<DoubleValueEventArgs> SpeedTradesUpdate = delegate { };
        /// <summary>
        /// Происходит при обновлении SpeedTickBuy
        /// </summary>
        public event EventHandler<DoubleValueEventArgs> SpeedTickBuyUpdate = delegate { };
        /// <summary>
        /// Происходит при обновлении SpeedTradesTick
        /// </summary>
        public event EventHandler<DoubleValueEventArgs> SpeedTradesTickUpdate = delegate { };
        /// <summary>
        /// Происходит при обновлении SpeedTradesAvgDayli
        /// </summary>
        public event EventHandler<DoubleValueEventArgs> SpeedTradesAvgDayliUpdate = delegate { };
        /// <summary>
        /// Происходит при обновлении SpeedTradesAvg
        /// </summary>
        public event EventHandler<DoubleValueEventArgs> SpeedTradesAvgUpdate = delegate { };
        /// <summary>
        /// Происходит при обновлении Market_Trend
        /// </summary>
        public event EventHandler<DoubleValueEventArgs> Market_TrendUpdate = delegate { };
        #endregion
    }
}
