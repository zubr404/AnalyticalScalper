using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Содержит данные о сделке из AllTrades
    /// </summary>
    struct DataTradesExchenge
    {
        public readonly double Price;        // Цена сделки из таб. всех сделок
        public readonly DateTime DateTime;        // Дата и время сделки (дд.мм.гггг чч:мм:сс)
        public readonly double Volume;          // Объем последней сделки (контракты)
        public readonly double VolumeBuy;        // Объем последней сделки (контракты)
        public readonly double VolumeSell;       // Объем последней сделки (контракты)

        public DataTradesExchenge(double _price, DateTime _dateTime, double _volume, double _volumeBuy, double _volumeSell)
        {
            Price = _price;
            DateTime = _dateTime;
            Volume = _volume;
            VolumeBuy = _volumeBuy;
            VolumeSell = _volumeSell;
        }
    }
}
