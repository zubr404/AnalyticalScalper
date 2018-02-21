using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Данные о сделке из таблицы всех сделок для события DataTradesExchengeUpdate
    /// </summary>
    sealed class DataTradesExchengeEventArgs : EventArgs
    {
        public DataTradesExchenge DataNew { get; private set; }

        public DataTradesExchengeEventArgs(DataTradesExchenge _dataNew)
        {
            DataNew = _dataNew;
        }
    }
}
