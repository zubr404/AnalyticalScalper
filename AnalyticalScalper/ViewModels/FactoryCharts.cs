using AnalyticalScalper.Model;
using AnalyticalScalper.ViewModels.ChartsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.ViewModels
{
    /// <summary>
    /// Фабрика графиков. Предоставляет данные для биндинга графиков.
    /// </summary>
    class FactoryCharts
    {
        //############## ЦЕНА ВСЕХ СДЕЛОК ##############
        //----Главная панель----
        public ChartPanelMain PriceChart { get; private set; }
        List<MultiDependetPanelTrades> ListMultiDependet;
        List<PartiallyDependentPanel> ListPartiallyDependet;
        //----------------------

        //----Дополнительная панель по ленте сделок----
        public MultiDependetPanelTrades MultiDependetPanelTrades { get; private set; }
        public TapeTradesEveryVolumeDraving TapeTradesEveryVolume { get; private set; }
        public TapeTradesSumOperationDrawing TapeTradesSumOperation { get; private set; }
        List<TapeTradesDrawing> childrenPanel;
        //----------------------------------------------

        //----Дополнительная панель спроса и предложения----
        public PartiallyDependentPanel BidPanel { get; private set; }
        public PartiallyDependentPanel OfferPanel { get; private set; }
        //##############################################

        //############## СКОРОСТЬ СДЕЛОК ##############
        public ChartPanelMain SpeedPriceChart { get; private set; }
        //##############################################

        //############## СКОРОСТЬ ТИКОВ ##############
        public ChartPanelMain SpeedTickBuyChart { get; private set; }
        //##############################################

        //############## СКОРОСТЬ СДЕЛОК В ТИКE ##############
        public ChartPanelMain SpeedTradeTickChart { get; private set; }
        //##############################################

        //############## СРЕДНЯЯ СКОРОСТЬ СДЕЛОК ЗА ДЕНЬ ##############
        public DependentYScalePanel SpeedTradesDayAvChart { get; private set; }
        //##############################################

        //############## СРЕДНЯЯ СКОРОСТЬ СДЕЛОК ЗА ПЕРИОД ##############
        public DependentYScalePanel SpeedTradesPerAvChart { get; private set; }
        //##############################################

        //############## РЫНОЧНЫЙ ТРЕНД ##############
        public ChartYPlusMinusMain MarketTrendChart { get; private set; }
        public ChartYPlusMinusMain MarketTrendGisgramChart { get; private set; }
        //##############################################

        //############## ЦЕНА ПОЗИЦИИ ##############
        public DependentYScalePanel PriceCurrentPosition { get; private set; }
        //##############################################

        #region -Constructor-
        public FactoryCharts(ExchangeInformation _exchangeInf)
        {
            #region -ЦЕНА ВСЕХ СДЕЛОК-
            // I
            childrenPanel = new List<TapeTradesDrawing>();
            TapeTradesEveryVolume = new TapeTradesEveryVolumeDraving();
            TapeTradesSumOperation = new TapeTradesSumOperationDrawing();
            childrenPanel.Add(TapeTradesEveryVolume);
            childrenPanel.Add(TapeTradesSumOperation);
            //---------------------------------------

            // II
            MultiDependetPanelTrades = new MultiDependetPanelTrades(childrenPanel, MainWindow.SizeTapeTradesClobal);
            //---------------------------------------

            // III
            ListMultiDependet = new List<MultiDependetPanelTrades>();
            ListMultiDependet.Add(MultiDependetPanelTrades);

            ListPartiallyDependet = new List<PartiallyDependentPanel>();
            BidPanel = new PartiallyDependentPanel(new System.Windows.Thickness(40, -20, 0, 0));
            OfferPanel = new PartiallyDependentPanel(new System.Windows.Thickness(40, 0, 0, 0));
            ListPartiallyDependet.Add(BidPanel);
            ListPartiallyDependet.Add(OfferPanel);
            //---------------------------------------

            // IV
            PriceChart = new ChartPanelMain(ListMultiDependet, ListPartiallyDependet, MainWindow.SizePriceChartClobal, ChartTypeEnum.Point);
            //---------------------------------------
            #endregion

            #region - СКОРОСТЬ СДЕЛОК -
            SpeedPriceChart = new ChartPanelMain(MainWindow.SizeSpeedTradesChartClobal, ChartTypeEnum.Gistogramm);
            #endregion

            #region - СКОРОСТЬ ТИКОВ -
            SpeedTickBuyChart = new ChartPanelMain(MainWindow.SizeSpeedTickBuyChartsClobal, ChartTypeEnum.Gistogramm);
            #endregion

            #region -СКОРОСТЬ СДЕЛОК В ТИКE-
            SpeedTradeTickChart = new ChartPanelMain(MainWindow.SizeSpeedTradesChartClobal, ChartTypeEnum.Gistogramm);
            #endregion

            #region - СРЕДНЯЯ СКОРОСТЬ СДЕЛОК ЗА ДЕНЬ -
            SpeedTradesDayAvChart = new DependentYScalePanel(SpeedTradeTickChart);
            #endregion

            #region - СРЕДНЯЯ СКОРОСТЬ СДЕЛОК ЗА ПЕРИОД -
            SpeedTradesPerAvChart = new DependentYScalePanel(SpeedTradeTickChart);
            #endregion

            #region - РЫНОЧНЫЙ ТРЕНД -
            MarketTrendChart = new ChartYPlusMinusMain(MainWindow.SizeTrendMarketChartsGlobal);
            MarketTrendGisgramChart = new ChartYPlusMinusMain(MainWindow.SizeTrendGistogramChartsGlobal);
            #endregion

            #region - ЦЕНА ПОЗИЦИИ -
            PriceCurrentPosition = new DependentYScalePanel(PriceChart);
            #endregion

            AnalyticalScalperModel.ExchangeInfomationGLOBAL.DataTradesExchengeUpdate += ExchangeInfomationGLOBAL_DataTradesExchengeUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.DataMarketParametrUpdate += ExchangeInfomationGLOBAL_DataCurrentParametrUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.SpeedTradesUpdate += ExchangeInfomationGLOBAL_SpeedTradesUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.SpeedTickBuyUpdate += ExchangeInfomationGLOBAL_SpeedTickBuyUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.SpeedTradesTickUpdate += ExchangeInfomationGLOBAL_SpeedTradesTickUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.SpeedTradesAvgDayliUpdate += ExchangeInfomationGLOBAL_SpeedTradesAvgDayliUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.SpeedTradesAvgUpdate += ExchangeInfomationGLOBAL_SpeedTradesAvgUpdate;
            AnalyticalScalperModel.ExchangeInfomationGLOBAL.Market_TrendUpdate += ExchangeInfomationGLOBAL_Market_TrendUpdate;
            AnalyticalScalperModel.AccountInformationGLOBAL.PricePositionUpdate += AccountInformationGGLOBAL_PricePositionUpdate;
        }
        #endregion

        #region -Event handler-
        // костыль для синхронизации графиков
        bool on_chart = false;

        // -PriceChart-
        InitialValue initialePrevios = new InitialValue(0);
        void ExchangeInfomationGLOBAL_DataTradesExchengeUpdate(object sender, DataTradesExchengeEventArgs e)
        {
            if (!CheckTimeTrades(e.DataNew.DateTime)) { return; }
            on_chart = true;

            // цена основная
            double _price = e.DataNew.Price;
            if (initialePrevios.Value != _price)
            {
                PriceChart.DrawChartAction(new InitialValue(_price));
                initialePrevios.Value = _price;
            }
            
            // дополнительны панели
            MultiDependetPanelTrades.UpdatePanels(e.DataNew);
        }
        void ExchangeInfomationGLOBAL_DataCurrentParametrUpdate(object sender, DataMarketParametrEventArgs e)
        {
            BidPanel.UpdateFreeValue(e.DataNew.Bid);
            OfferPanel.UpdateFreeValue(e.DataNew.Offer);
        }
        private void ExchangeInfomationGLOBAL_SpeedTradesUpdate(object sender, DoubleValueEventArgs e)
        {
            if (!on_chart) { return; }
            SpeedPriceChart.DrawChartAction(new InitialValue(e.DoubleValueNew));
        }
        void ExchangeInfomationGLOBAL_SpeedTickBuyUpdate(object sender, DoubleValueEventArgs e)
        {
            if (!on_chart) { return; }
            SpeedTickBuyChart.DrawChartAction(new InitialValue(e.DoubleValueNew));
        }
        void ExchangeInfomationGLOBAL_SpeedTradesTickUpdate(object sender, DoubleValueEventArgs e)
        {
            if (!on_chart) { return; }
            SpeedTradeTickChart.DrawChartAction(new InitialValue(e.DoubleValueNew));
        }
        void ExchangeInfomationGLOBAL_SpeedTradesAvgDayliUpdate(object sender, DoubleValueEventArgs e)
        {
            SpeedTradesDayAvChart.ChartsDraw(e.DoubleValueNew);
        }
        void ExchangeInfomationGLOBAL_SpeedTradesAvgUpdate(object sender, DoubleValueEventArgs e)
        {
            SpeedTradesPerAvChart.ChartsDraw(e.DoubleValueNew);
        }
        void ExchangeInfomationGLOBAL_Market_TrendUpdate(object sender, DoubleValueEventArgs e)
        {
            if (!on_chart) { return; }
            MarketTrendChart.ChartDraw(new InitialValue(e.DoubleValueNew));
            MarketTrendGisgramChart.ChartDrawGistogram(new InitialValue(e.DoubleValueNew));
        }
        void AccountInformationGGLOBAL_PricePositionUpdate(object sender, DoubleValueEventArgs e)
        {
            PriceCurrentPosition.ChartsDraw(e.DoubleValueNew);
        }
        #endregion

        #region -Private method-
        bool checkTimeTradeResult;
        private bool CheckTimeTrades(DateTime _timeTrade) // Сравниваем время сделки и время старта программы с заданным отступом
        {
            if (!checkTimeTradeResult)
            {
                if (_timeTrade > MainWindow.gl_TimeStartApp)
                {
                    checkTimeTradeResult = true;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
