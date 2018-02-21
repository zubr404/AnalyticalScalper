using DdeInputDataQuikLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Получение данных от DDE Current table.
    /// Расчет показателей на их основе
    /// </summary>
    class ProcessingCurrentTable
    {
        ExchangeInformation exchangeInfomation;

        //-Constructor-
        public ProcessingCurrentTable(IExportDDE _currentTableChannel, ExchangeInformation _exchangeInfomation)
        {
            exchangeInfomation = _exchangeInfomation;
            _currentTableChannel.LoadedLineEvent += currentTableChannel_LoadedLineEvent;
        }

        // -handler Current table-
        void currentTableChannel_LoadedLineEvent(object sender, DdeInputDataQuikLib.DDEChannelsMarketEventArgs e)
        {
            exchangeInfomation.DataMarketParametrExchenge = new DataMarketCurrentParametrExchenge(e.Securyti, (double)e.Bid, (double)e.Offer);
            exchangeInfomation.DataConstatntParametrExchenge = new DataConstatntCurrentParametrExchenge(e.Securyti, (double)e.StepPrice);
        }
    }
}
