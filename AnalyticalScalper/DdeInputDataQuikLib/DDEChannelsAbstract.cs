using XlDde;
using System;

namespace DdeInputDataQuikLib
{
    /// <summary>
    /// Абстрактный класс для dde каналов
    /// </summary>
    abstract class DDEChannelsAbstract : XlDdeChannel, IExportDDE
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

                for (int col = 0; col < xt.Columns; col++)
                {
                    xt.ReadValue();
                    SetValues(xt, col, ddeMarketEventArgs);
                }
                LoadedLineEvent(this, ddeMarketEventArgs);
            }
            ObtainingDataCompletedEvent(this, ddeServiceEventArgs);
        }

        // abstract metods
        protected abstract void SetValues(XlTable _xt, int _col, DDEChannelsMarketEventArgs _ddeMEA);

        #region реализация IExportDDE
        public event EventHandler<DDEChannelsMarketEventArgs> LoadedLineEvent = delegate { };
        public event EventHandler<DDeChannelsServiceEventArgs> ObtainingDataCompletedEvent = delegate { };
        public event EventHandler<DDeChannelsServiceEventArgs> ObtainingDataStartedEvent = delegate { };
        #endregion
    }
}
