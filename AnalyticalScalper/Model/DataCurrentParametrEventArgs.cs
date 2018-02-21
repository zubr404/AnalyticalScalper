using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Данные о значениях текущих параметров для события DataMarketParametrUpdate
    /// </summary>
    sealed class DataMarketParametrEventArgs : EventArgs
    {
        public DataMarketCurrentParametrExchenge DataNew { get; private set; }

        public DataMarketParametrEventArgs(DataMarketCurrentParametrExchenge _dataNew)
        {
            DataNew = _dataNew;
        }
    }

    /// <summary>
    /// Данные о значениях текущих параметров для события DataConstatntParametrUpdate
    /// </summary>
    sealed class DataConstantParametrEventArgs : EventArgs
    {
        public DataConstatntCurrentParametrExchenge DataNew { get; private set; }

        public DataConstantParametrEventArgs(DataConstatntCurrentParametrExchenge _dataNew)
        {
            DataNew = _dataNew;
        }
    }
}
