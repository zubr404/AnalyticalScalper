using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Торговые данные (текущ. позиция, вариац. маржа и т.д.)
    /// Этот класс должен быть виден в MainWindow
    /// </summary>
    class TradersData : BaseClasses.PropertyChangedBase
    {
        #region -рыночная информация-
        double priceMarket = 0; // последня цена на бирже
        double bid = 0;         // bid
        double offer = 0;       // offer
        #endregion

        #region - информация по позициям -
        int currentPosition = 0;    // текущая позиция
        double pricePosition = 0;   // цена позиции
        double varitional = 0;      // вариационная маржа
        double profitTotal = 0;     // прибыль по всем сделкам
        int countTrades = 0;        // количество всех сделок

        double priceStopUp = 0;     // фиксация прибыли/убытка цена выше рынка
        double priceStopDown = 0;   // фиксация прибыли/убытка цена ниже рынка

        double profitValue = 10;    // размер прибыли на одну сделку
        double lossValue = 50;      // размер убытка на одну сделку
        int lots = 1;               // количество контрактов в сделке
        #endregion

        #region - ExchengeInformation -
        double speedTradesTick;
        double speedTradesAvgDayli;
        double speedTradesAvg;
        double marketTrend;
        #endregion

        #region -Constructor-
        public TradersData()
        {
            k_Avg = 2;

            AnalyticalScalperModel.ExchangeInfomationGLOBAL.DataTradesExchengeUpdate += ExchengeInf_DataTradesExchengeUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.DataMarketParametrUpdate += ExchengeInf_DataCurrentParametrUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.SpeedTradesTickUpdate += ExchangeInfomationGLOBAL_SpeedTradesTickUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.SpeedTradesAvgDayliUpdate += ExchangeInfomationGLOBAL_SpeedTradesAvgDayliUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.SpeedTradesAvgUpdate += ExchangeInfomationGLOBAL_SpeedTradesAvgUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.Market_TrendUpdate += ExchangeInfomationGLOBAL_Market_TrendUpdate;
        }
        #endregion

        #region -Event handler-
        // Обработка обновления информации о последней сделке в Таблице всех сделок
        void ExchengeInf_DataTradesExchengeUpdate(object sender, DataTradesExchengeEventArgs e)
        {
            priceMarket = e.DataNew.Price;
            AutoTrading();
            AutoClosePosition();
        }

        // Обработка обновления значений текущих параметров по бумаге (Таблица текущих параметров)
        void ExchengeInf_DataCurrentParametrUpdate(object sender, DataMarketParametrEventArgs e)
        {
            bid = e.DataNew.Bid;
            offer = e.DataNew.Offer;
            VaritionalCalc(bid, offer);
            AutoClosePosition();
        }

        // Market_TrendUpdate
        void ExchangeInfomationGLOBAL_Market_TrendUpdate(object sender, DoubleValueEventArgs e)
        {
            marketTrend = e.DoubleValueNew;
        }

        // SpeedTradesAvgDayli
        void ExchangeInfomationGLOBAL_SpeedTradesAvgDayliUpdate(object sender, DoubleValueEventArgs e)
        {
            speedTradesAvgDayli = e.DoubleValueNew;
        }

        // SpeedTradesAvg
        void ExchangeInfomationGLOBAL_SpeedTradesAvgUpdate(object sender, DoubleValueEventArgs e)
        {
            speedTradesAvg = e.DoubleValueNew;
        }

        // SpeedTradesTickUpdate
        void ExchangeInfomationGLOBAL_SpeedTradesTickUpdate(object sender, DoubleValueEventArgs e)
        {
            speedTradesTick = e.DoubleValueNew;
        }
        #endregion

        #region -Properties-
        /// <summary>
        /// Установки прибыли на одну сделку
        /// </summary>
        public  double ProfitValue
        {
            get { return profitValue; }
            set
            {
                if (value < 0)
                {
                    profitValue = 0;
                    base.NotifyPropertyChanged();
                }
                else
                {
                    profitValue = value;
                    base.NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Установка убытка на одну сделку
        /// </summary>
        public  double LossValue
        {
            get { return lossValue; }
            set
            {
                if (value < 0)
                {
                    lossValue = 0;
                    base.NotifyPropertyChanged();
                }
                else
                {
                    lossValue = value;
                    base.NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// текущая позиция
        /// </summary>
        public  int CurrentPosition
        {
            get { return currentPosition; }
            private set
            {
                currentPosition = value;
                base.NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// цена позиции
        /// </summary>
        public  double PricePosition
        {
            get { return pricePosition; }
            set
            {
                pricePosition = value;
                AnalyticalScalperModel.AccountInformationGLOBAL.PricePosition = pricePosition;
                base.NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// вариационная маржа
        /// </summary>
        public  double Varitional
        {
            get { return varitional; }
            private set
            {
                varitional = value;
                base.NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// прибыль по всем сделкам
        /// </summary>
        public  double ProfitTotal
        {
            get { return profitTotal; }
            private set
            {
                profitTotal = value;
                base.NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// количество всех сделок
        /// </summary>
        public  int CountTrades
        {
            get { return countTrades; }
            private set
            {
                countTrades = value;
                base.NotifyPropertyChanged();
            }
        }
        #endregion

        #region -Public method-
        /// <summary>
        /// Обработка торговых команд
        /// </summary>
        public void TradesCommandProcessing(TradesCommandEnum _tradesCommand)
        {
            if (priceMarket == 0 | bid == 0 | offer == 0)
            {
                System.Windows.MessageBox.Show("Рыночная цена не определена.");
                return;
            }

            switch (_tradesCommand)
            {
                case TradesCommandEnum.LongOpen:
                    LongOpen();
                    break;
                case TradesCommandEnum.LongClose:
                    LongClose(bid);
                    break;
                case TradesCommandEnum.ShortOpen:
                    ShortOpen();
                    break;
                case TradesCommandEnum.ShortClose:
                    ShortClose(offer);
                    break;
                default:
                    System.Windows.MessageBox.Show("Каманда не найдена (TradesCommandProcessing).\nВозможно изменен TradesCommandEnum");
                    break;
            }
        }
        #endregion

        #region -Private Method-
        private void LongOpen()
        {
            if (currentPosition == 0)
            {
                CurrentPosition += lots;
                PricePosition = offer;
                priceStopUp = pricePosition + profitValue;
                priceStopDown = pricePosition - lossValue;
            }
        }
        private void ShortOpen()
        {
            if (currentPosition == 0)
            {
                CurrentPosition -= lots;
                PricePosition = bid;
                priceStopUp = pricePosition + lossValue;
                priceStopDown = pricePosition - profitValue;
            }
        }
        private void LongClose(double _closePrice)
        {
            if (currentPosition > 0)
            {
                int _qty = currentPosition;
                ProfitTotal += (_closePrice - pricePosition) * _qty;
                CountTrades++;

                CurrentPosition -= _qty;
                PricePosition = 0;
                priceStopUp = 0;
                priceStopDown = 0;
            }
        }
        private void ShortClose(double _closePrice)
        {
            if (currentPosition < 0)
            {
                int _qty = Math.Abs(currentPosition);
                ProfitTotal += (pricePosition - _closePrice) * _qty;
                CountTrades++;

                CurrentPosition += _qty;
                PricePosition = 0;
                priceStopUp = 0;
                priceStopDown = 0;
            }
        }

        /// <summary>
        /// Рассчет вар. маржи
        /// </summary>
        /// <param name="_priceMarket"></param>
        private void VaritionalCalc(double _priceMarket)
        {
            if (currentPosition > 0)
            {
                Varitional = _priceMarket - pricePosition;
            }
            if (currentPosition < 0)
            {
                Varitional = pricePosition - _priceMarket;
            }
        }
        private void VaritionalCalc(double _bid, double _offer)
        {
            if (currentPosition > 0)
            {
                Varitional = _bid - pricePosition;
            }
            if (currentPosition < 0)
            {
                Varitional = pricePosition - _offer;
            }
        }

        /// <summary>
        /// Автоматическое закрытие позиций
        /// </summary>
        private void AutoClosePosition()
        {
            if (currentPosition > 0) // long
            {
                if ((priceMarket >= priceStopUp && offer > priceStopUp) | priceMarket > priceStopUp | bid > priceStopUp) // profit
                {
                    LongClose(priceMarket);
                }
                if (bid <= priceStopDown && priceMarket == bid) // loss
                {
                    LongClose(bid);
                }
            }

            if (currentPosition < 0) // short
            {
                if ((priceMarket <= priceStopDown && bid < priceStopDown) | priceMarket < priceStopDown | offer < priceStopDown) // profit
                {
                    ShortClose(priceMarket);
                }
                if (offer >= priceStopUp && priceMarket == offer) // loss
                {
                    ShortClose(offer);
                }
            }
        }

        #region Запуск автоторговли
        public static double k_Avg { get; private set; }
        const double trendUp = 0;
        const double trendDown = 0;

        private void AutoTrading()
        {
            if (MainWindow.gl_OnAutoTrading)
            {
                if (speedTradesAvg > speedTradesAvgDayli)
                {
                    if (speedTradesTick > speedTradesAvgDayli)
                    {
                        if (marketTrend > trendUp)
                        {
                            LongOpen();
                        }

                        if (marketTrend < trendDown)
                        {
                            ShortOpen();
                        }
                    }
                }
            }
        }

        double speedTradesPrevios;
        [Obsolete("No use!", true)]
        private void AutoTrading000()
        {
            if (MainWindow.gl_OnAutoTrading)
            {
                if (speedTradesTick > speedTradesPrevios)
                {
                    if (marketTrend > trendUp)
                    {
                        LongOpen();
                    }

                    if (marketTrend < trendDown)
                    {
                        ShortOpen();
                    }
                }
            }

            speedTradesPrevios = speedTradesTick;
        }
        #endregion
        #endregion
    }
}
