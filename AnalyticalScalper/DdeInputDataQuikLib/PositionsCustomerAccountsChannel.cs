using XlDde;
using System;

namespace DdeInputDataQuikLib
{
    sealed class PositionsCustomerAccountsChannel : DDEChannelsAbstract
    {
        protected override void SetValues(XlTable _xt, int _col, DDEChannelsMarketEventArgs _ddeMEA)
        {
            switch (_col)
            {
                case 0:
                    _ddeMEA.Account = _xt.StringValue;
                    break;
                case 1:
                    _ddeMEA.Securyti = _xt.StringValue;
                    break;
                case 2:
                    _ddeMEA.CurrentNettoPosition = _xt.FloatValue;
                    break;
                case 3:
                    _ddeMEA.ActiveBuy = _xt.FloatValue;
                    break;
                case 4:
                    _ddeMEA.ActiveSell = _xt.FloatValue;
                    break;
            }
        }
    }
}
