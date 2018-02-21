using AnalyticalScalper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AnalyticalScalper.ViewModels.ChartsModel
{
    /// <summary>
    /// Базовый класс для графического отображения информации ленты сделок
    /// </summary>
    abstract class TapeTradesDrawing
    {
        const int countElement = 25;
        const double minSize = 5;
        const double maxSize = 200;

        /// <summary>
        /// Коллекция элементов для отображения
        /// </summary>
        public ObservableCollection<Ellipse> drawElementCollection { get; private set; }

        protected Dispatcher dispatcher;
        protected Timer timer;
        private int timerInterval = 1000;

        // Constructor
        public TapeTradesDrawing()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            drawElementCollection = new ObservableCollection<Ellipse>();

            timer = new Timer(timerInterval);
            timer.Elapsed += timer_Elapsed;
        }

        // управление количеством графических элементов
        protected void CollectionElementAdd(Brush _brush, double _valueTrades)
        {
            dispatcher.InvokeAsync(() =>
            {
                drawElementCollection.Insert(0, EllipseVolumeTrades(_brush, _valueTrades));
                if (drawElementCollection.Count > countElement)
                {
                    drawElementCollection.RemoveAt(countElement);
                }
            }, DispatcherPriority.Background);
        }

        // создаем эллипсы
        protected Ellipse EllipseVolumeTrades(Brush _brush, double _valueTrades)
        {
            Ellipse ell = new Ellipse();
            ell.Stroke = Brushes.Black;
            ell.Fill = _brush;
            ell.StrokeThickness = 0.75;

            double size = _valueTrades;
            if (size < minSize)
            {
                size = minSize;
            }
            if (size > maxSize)
            {
                size = maxSize;
            }

            ell.Height = size;
            ell.Width = size;
            ell.Margin = new Thickness(0.5);

            return ell;
        }

        // abstract
        public abstract void GetInitialeValues(DataTradesExchenge _dataTrades);
        // через заданный интервал времени добавляем пустой эллипс
        protected abstract void timer_Elapsed(object sender, ElapsedEventArgs e);
    }
}
