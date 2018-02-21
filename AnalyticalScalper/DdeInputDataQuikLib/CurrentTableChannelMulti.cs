using XlDde;
using System;

namespace DdeInputDataQuikLib
{
    /// <summary>
    /// Экспорт из таблицы Текущих параматров.
    /// Эта версия класса больше будет подходить,
    /// если в таблице больше одной бумаги
    /// </summary>
    sealed class CurrentTableChannelMulti : DDEChannelsAbstract
    {
        protected override void SetValues(XlTable _xt, int _col, DDEChannelsMarketEventArgs _ddeMEA)
        {
            switch (_col)
            {
                case 0:
                    _ddeMEA.TimeChange = _xt.StringValue;
                    break;

                case 1:
                    _ddeMEA.Price = (double)_xt.FloatValue;
                    break;

                case 2:
                    _ddeMEA.Bid = (decimal)_xt.FloatValue;
                    break;

                case 3:
                    _ddeMEA.Offer = (decimal)_xt.FloatValue;
                    break;

                case 4:
                    _ddeMEA.High_possible_price = (decimal)_xt.FloatValue;
                    break;

                case 5:
                    _ddeMEA.Minimum_possible_price = (decimal)_xt.FloatValue;
                    break;

                case 6:
                    _ddeMEA.StepPrice = (decimal)_xt.FloatValue;
                    break;

                case 7:
                    _ddeMEA.Securyti = _xt.StringValue;
                    break;

                case 8:
                    _ddeMEA.ClassCode = _xt.StringValue;
                    break;

                case 9:
                    _ddeMEA.Time = _xt.StringValue; // время последней сделки
                    break;
            }
        }
    }
}
