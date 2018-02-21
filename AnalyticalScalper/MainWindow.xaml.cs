using AnalyticalScalper.Model;
using AnalyticalScalper.ViewModels;
using BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AnalyticalScalper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /* Размеры графиков интерфейса будем получать
         * здесь, реагируя на событие загрузки формы.
         * По-хорошему это надо сделать на стороне модели
         * представления через биндинг к ширине и высоте
         * канвас.
         */

        #region -Global-
        /// <summary>
        /// Размер основного графика цены
        /// </summary>
        public static SizeGlobal SizePriceChartClobal;

        /// <summary>
        /// Актуальный размер TapeTrades
        /// </summary>
        public static SizeGlobal SizeTapeTradesClobal;

        /// <summary>
        /// Актуальный размер графика скорости сделок.
        /// </summary>
        public static SizeGlobal SizeSpeedTradesChartClobal;

        /// <summary>
        /// Актуальный размер графика скорости тиков вверх
        /// </summary>
        public static SizeGlobal SizeSpeedTickBuyChartsClobal;

        /// <summary>
        /// Актуальный размер графика тренда рынка
        /// </summary>
        public static SizeGlobal SizeTrendMarketChartsGlobal;
        public static SizeGlobal SizeTrendGistogramChartsGlobal;
        //-------------------------

        public static DateTime gl_TimeStartApp { get; private set; }
        public static bool gl_OnAutoTrading { get; private set; }
        #endregion

        

        Line verticalLine;
        Line horizontLine;

        public MainWindow()
        {
            SizeTapeTradesClobal = new SizeGlobal();
            SizePriceChartClobal = new SizeGlobal();
            SizeSpeedTradesChartClobal = new SizeGlobal();
            SizeSpeedTickBuyChartsClobal = new SizeGlobal();
            SizeTrendMarketChartsGlobal = new SizeGlobal();
            SizeTrendGistogramChartsGlobal = new SizeGlobal();

            gl_TimeStartApp = DateTime.Now.Subtract(new TimeSpan(0, 1, 0));

            InitializeComponent();

            CrossHairCreate();
        }

        #region Hendler events
        //---после загрузки Canvas получаем актуальные Высоту и Ширину---
        private void CanvasPrice_Loaded(object sender, RoutedEventArgs e)
        {
            SizePriceChartClobal.size = new Size(CanvasPrice.ActualWidth, CanvasPrice.ActualHeight);
        }
        private void CanvasSpeedPrice_Loaded(object sender, RoutedEventArgs e)
        {
            SizeSpeedTradesChartClobal.size = new Size(CanvasSpeedPrice.ActualWidth, CanvasSpeedPrice.ActualHeight);
        }
        private void CanvasSpeedTickBuy_Loaded(object sender, RoutedEventArgs e)
        {
            //SizeSpeedTickBuyChartsClobal.size = new Size(CanvasSpeedTickBuy.ActualWidth, CanvasSpeedTickBuy.ActualHeight); ВРЕМЕННО УБРАЛ С ИНТЕРФЕЙСА
        }
        private void CanvasTrend_Loaded(object sender, RoutedEventArgs e)
        {
            SizeTrendMarketChartsGlobal.size = new Size(0, CanvasTrend.ActualHeight);
        }
        private void CanvasTrendGisgram_Loaded(object sender, RoutedEventArgs e)
        {
            SizeTrendGistogramChartsGlobal.size = new Size(CanvasTrendGisgram.ActualWidth, CanvasTrendGisgram.ActualHeight);
        }
        //----------------------------------------------------------------

        // обработка перемещения мыши над специально созданной Canvas
        private void CanvasGlass_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(CanvasGlass);

            verticalLine.X1 = point.X;
            verticalLine.Y1 = 0;
            verticalLine.X2 = point.X;
            verticalLine.Y2 = CanvasGlass.ActualHeight;

            horizontLine.X1 = 0;
            horizontLine.Y1 = point.Y;
            horizontLine.X2 = CanvasGlass.ActualWidth;
            horizontLine.Y2 = point.Y;
        }

        // создаем перекрестие
        private void CrossHairCreate()
        {
            Brush brush_line = Brushes.White;
            verticalLine = new Line();
            horizontLine = new Line();

            verticalLine.Stroke = brush_line;
            verticalLine.StrokeThickness = 0.25;
            verticalLine.SnapsToDevicePixels = true;
            CanvasGlass.Children.Add(verticalLine);

            horizontLine.Stroke = brush_line;
            horizontLine.StrokeThickness = 0.25;
            horizontLine.SnapsToDevicePixels = true;
            CanvasGlass.Children.Add(horizontLine);
        }
        //---------------------------end-

        // Происходит, когда окно попадает на передний план.
        private void Window_Activated(object sender, EventArgs e)
        {
            TradesPanel.Opacity = 1;
        }
        // Происходит при перемещении окна на задний план
        private void Window_Deactivated(object sender, EventArgs e)
        {
            TradesPanel.Opacity = 0.25;
        }
        // Возникает при нажатии клавиши, если фокус установлен на данном элементе.
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // ТОРГОВЫЕ КОМАНДЫ
            MarketCommand(e);

            // УСТАНОВОЧНЫЕ КОМАНДЫ
            SettingsCommand(e);
        }
        private void MarketCommand(KeyEventArgs e)
        {
            if (e.Key == Key.NumPad7)
            {
                //TradersData.TradesCommandProcessing(TradesCommandEnum.LongOpen);
            }
            if (e.Key == Key.NumPad9)
            {
                //TradersData.TradesCommandProcessing(TradesCommandEnum.ShortOpen);
            }
            if (e.Key == Key.NumPad4)
            {
                //TradersData.TradesCommandProcessing(TradesCommandEnum.LongClose);
            }
            if (e.Key == Key.NumPad6)
            {
                //TradersData.TradesCommandProcessing(TradesCommandEnum.ShortClose);
            }
        }
        private void SettingsCommand(KeyEventArgs e)
        {
            // установка прибыли на сделку
            if (e.Key == Key.Q) // up
            {
                //TradersData.ProfitValue += 1;
            }
            if(e.Key == Key.A) // down
            {
                //TradersData.ProfitValue -= 1;
            }

            // установка убытка на сделку
            if (e.Key == Key.W) // up
            {
                //TradersData.LossValue += 1;
            }
            if (e.Key == Key.S) // down
            {
                //TradersData.LossValue -= 1;
            }

            // разрешить/запретить автотрейдинг
            if (e.Key == Key.R)
            {
                gl_OnAutoTrading = true;
            }
            if (e.Key == Key.F)
            {
                gl_OnAutoTrading = false;
            }
            //---------------------------
        }
        //-end-

        // Получение актуальной ширины и высоты TapeTrades при изменении размера
        private void TapeTrades_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SizeTapeTradesClobal.size = e.NewSize;
        }
        #endregion

        
    }



    /// <summary>
    /// Этот класс нужен, что бы была возможность передать размеры
    /// панелей интерфейса в классы графиков или др. по ссылке.
    /// </summary>
    public class SizeGlobal
    {
        public Size size { get; set; }
    }
}
