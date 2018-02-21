using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Содержит значения текущих ТОРГОВЫХ параметров по инструменту:
    /// те параметры которые изменяются в ходе торгов.
    /// </summary>
    struct DataMarketCurrentParametrExchenge
    {
        public readonly string Seccode;
        public readonly double Bid;
        public readonly double Offer;

        public DataMarketCurrentParametrExchenge(string _seccode, double _bid, double _offer)
        {
            Seccode = _seccode;
            Bid = _bid;
            Offer = _offer;
        }
    }

    /// <summary>
    /// Содержит значения текущих ТЕХНИЧЕСКИХ параметров по инструменту:
    /// те параметры которые не изменяются в ходе торгов.
    /// </summary>
    struct DataConstatntCurrentParametrExchenge
    {
        public readonly string Seccond;
        public readonly double StepPrice;

        public DataConstatntCurrentParametrExchenge(string _seccode, double _step_price)
        {
            Seccond = _seccode;
            StepPrice = _step_price;
        }
    }
}
