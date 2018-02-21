using XlDde;
using System;

namespace DdeInputDataQuikLib
{
    // Не наследуем от DDEChannelsAbstract, что бы убрать второй цикл for(по колонкам)
    sealed class AllTradesChannel : XlDdeChannel, IExportDDE
    {
        protected override void ProcessTable(XlTable xt)
        {
            DDeChannelsServiceEventArgs ddeServiceEventArgs = new DDeChannelsServiceEventArgs();
            ddeServiceEventArgs.SetCountRowsExport(xt.Rows);

            ObtainingDataStartedEvent(this, ddeServiceEventArgs);

            int xtRows = xt.Rows;

            for (int row = 0; row < xtRows; row++)
            {
                DDEChannelsMarketEventArgs ddeMarketEventArgs = new DDEChannelsMarketEventArgs();

                xt.ReadValue();
                ddeMarketEventArgs.Number = xt.FloatValue;

                xt.ReadValue();
                ddeMarketEventArgs.Securyti = xt.StringValue;

                xt.ReadValue();
                ddeMarketEventArgs.Price = (double)xt.FloatValue;

                xt.ReadValue();
                ddeMarketEventArgs.Date = xt.StringValue;

                xt.ReadValue();
                ddeMarketEventArgs.Time = xt.StringValue;

                xt.ReadValue();
                ddeMarketEventArgs.TimeMsc = xt.FloatValue;

                xt.ReadValue();
                ddeMarketEventArgs.Operation = xt.StringValue;

                xt.ReadValue();
                ddeMarketEventArgs.Quantity = (double)xt.FloatValue;

                LoadedLineEvent(this, ddeMarketEventArgs);
            }
            ObtainingDataCompletedEvent(this, ddeServiceEventArgs);
        }

        #region реализация IExportDDE
        public event EventHandler<DDEChannelsMarketEventArgs> LoadedLineEvent = delegate { };
        public event EventHandler<DDeChannelsServiceEventArgs> ObtainingDataCompletedEvent = delegate { };
        public event EventHandler<DDeChannelsServiceEventArgs> ObtainingDataStartedEvent = delegate { };
        #endregion
    }
}
