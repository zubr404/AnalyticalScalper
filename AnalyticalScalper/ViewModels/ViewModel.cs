using AnalyticalScalper.Model;
using BaseClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AnalyticalScalper.ViewModels
{
    /*
    class ViewModel : PropertyChangedBase
    {
        Dispatcher dispatcher;
        private int currentPosition; // текущая позиция
        private SolidColorBrush brushPosition; // цвет для шотов/лонгов

        // -TradeDate-
        private double bid; // bid
        private double offer; // offer
        private double pricePosition; // цена позиции
        private double varitional; // вариационная маржа
        private double profitTotal; // прибыль по всем сделкам
        private int countTrades = 0; // количество всех сделок
        private double profitValue = 0; // установка прибыли на одну сделку
        private double lossValue = 0; // установка убытка на одну сделку

        //-I-
        AnalyticalScalperModel analytScalperModel;
        //-II-
        //FactoryChartsAutoScale factoryChartsAutoScale;
        FactoryChartsAllTrades1 factoryChartsAllTrades;

        public ObservableCollection<Ellipse> EllipsePriceList { get; private set; }
        public ObservableCollection<Ellipse> EllipsePositionList { get; private set; }


        public ObservableCollection<Ellipse> EllipseTESTList { get; private set; }

        
        // -Constructor-
        public ViewModel()
        {
            dispatcher = Dispatcher.CurrentDispatcher;

            bid = 0;
            offer = 0;
            currentPosition = 0; // текущая позиция
            brushPosition = null;
            pricePosition = 0; // цена позиции
            varitional = 0; // вариационная маржа
            profitTotal = 0; // прибыль по всем сделкам
            countTrades = 0; // количество всех сделок
            ProfitValue = TradersData.ProfitValue;
            LossValue = TradersData.LossValue;

            analytScalperModel = new AnalyticalScalperModel(); //-I-
            //factoryChartsAutoScale = new FactoryChartsAutoScale(analytScalperModel.processAllTrad); //-II-
            factoryChartsAllTrades = new FactoryChartsAllTrades1(analytScalperModel.processAllTrad); //-II-

            EllipsePriceList = factoryChartsAllTrades.autoScaleChartPrice.elementsChart;
            //EllipsePriceList = factoryChartsAutoScale.ChartPriceElements;
            //EllipsePositionList = factoryChartsAutoScale.ChartPositionElements;

            TradersData.PropertyChanged += TradeData_PropertyChanged;



            // test
            EllipseTESTList = new ObservableCollection<Ellipse>();
            for (int i = 10; i < 35; i++)
            {
                Ellipse el = new Ellipse();
                el.Height = i;
                el.Width = i;
                el.Fill = Brushes.White;
                EllipseTESTList.Add(el);
            }
            //----
        }

        // Обработка PropertyChanged статического класса TradeData для обновления интерфейса
        void TradeData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Bid = TradersData.Bid;
            Offer = TradersData.Offer;
            CurrentPosition = TradersData.CurrentPosition;
            PricePosition = TradersData.PricePosition;
            Varitional = TradersData.Varitional;
            ProfitTotal = TradersData.ProfitTotal;
            CountTrades = TradersData.CountTrades;
            ProfitValue = TradersData.ProfitValue;
            LossValue = TradersData.LossValue;

            if (CurrentPosition > 0) { BrushesPosition = Brushes.YellowGreen; }
            if (CurrentPosition == 0) { BrushesPosition = null; }
            if (CurrentPosition < 0) { BrushesPosition = Brushes.Coral; }
        }

        #region -Properties-
        public double ProfitValue /// Установки прибыли на одну сделку
        {
            get { return profitValue; }
            set
            {
                profitValue = value;
                base.NotifyPropertyChanged();
            }
        }
        public double LossValue /// Установка убытка на одну сделку
        {
            get { return lossValue; }
            set
            {
                lossValue = value;
                base.NotifyPropertyChanged();
            }
        } 
        public double Bid /// BID
        {
            get { return bid; }
            set
            {
                bid = value;
                base.NotifyPropertyChanged();
            }
        }
        public double Offer /// OFFER
        {
            get { return offer; }
            set
            {
                offer = value;
                base.NotifyPropertyChanged();
            }
        }
        public int CurrentPosition /// текущая позиция
        {
            get { return currentPosition; }
            set
            {
                currentPosition = value;
                base.NotifyPropertyChanged();
            }
        }
        public SolidColorBrush BrushesPosition /// цвет для шотов/лонгов
        {
            get { return brushPosition; }
            set
            {
                brushPosition = value;
                base.NotifyPropertyChanged();
            }
        }
        public double PricePosition /// цена позиции
        {
            get { return pricePosition; }
            set
            {
                pricePosition = value;
                base.NotifyPropertyChanged();
            }
        }
        public double Varitional /// вариационная маржа
        {
            get { return varitional; }
            private set
            {
                varitional = value;
                base.NotifyPropertyChanged();
            }
        }
        public double ProfitTotal /// прибыль по всем сделкам
        {
            get { return profitTotal; }
            private set
            {
                profitTotal = value;
                base.NotifyPropertyChanged();
            }
        }
        public int CountTrades /// количество всех сделок
        {
            get { return countTrades; }
            private set
            {
                countTrades = value;
                base.NotifyPropertyChanged();
            }
        }
        #endregion
    }
     */

    class ViewModel : PropertyChangedBase
    {
        AnalyticalScalperModel analytScalperModel;
        public FactoryCharts FactoryCharts { get; set; }
        public TradersData TraderData { get; set; }

        public ViewModel()
        {
            analytScalperModel = new AnalyticalScalperModel();
            FactoryCharts = new FactoryCharts(AnalyticalScalperModel.ExchangeInfomationGLOBAL);
            TraderData = new TradersData();
        }
    }
}
