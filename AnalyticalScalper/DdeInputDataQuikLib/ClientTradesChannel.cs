using XlDde;
using System;

namespace DdeInputDataQuikLib
{
    sealed class ClientTradesChannel : DDEChannelsAbstract
    {
        protected override void SetValues(XlTable _xt, int _col, DDEChannelsMarketEventArgs _ddeMEA)
        {
            switch (_col)
            {
                case 0:
                    _ddeMEA.Date = _xt.StringValue;
                    break;

                case 1:
                    _ddeMEA.Number = _xt.FloatValue;
                    break;

                case 2:
                    _ddeMEA.Time = _xt.StringValue;
                    break;

                case 3:
                    _ddeMEA.Securyti = _xt.StringValue;
                    break;

                case 4:
                    _ddeMEA.Operation = _xt.StringValue;
                    break;

                case 5:
                    _ddeMEA.Price = (double)_xt.FloatValue;
                    break;

                case 6:
                    _ddeMEA.Quantity = _xt.FloatValue;
                    break;

                case 7:
                    _ddeMEA.NumberOrders = _xt.FloatValue;
                    break;

                case 8:
                    _ddeMEA.Comment = _xt.StringValue;
                    break;

                case 9:
                    _ddeMEA.Account = _xt.StringValue;
                    break;
            }
        }
    }
}
