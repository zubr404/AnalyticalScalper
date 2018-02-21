using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Объединяет в себе данные о торговом счете: цена позиции, текущая позиция и т.д. 
    /// </summary>
    class AccountInfomation
    {
        double price_position;
        /// <summary>
        /// Цена позиции
        /// </summary>
        public double PricePosition
        {
            get { return price_position; }
            set
            {
                price_position = value;
                PricePositionUpdate(null, new DoubleValueEventArgs(price_position));
            }
        }

        #region - Event -
        /// <summary>
        /// Происходит при обновлении PricePosition
        /// </summary>
        public event EventHandler<DoubleValueEventArgs> PricePositionUpdate = delegate { };
        #endregion
    }
}
