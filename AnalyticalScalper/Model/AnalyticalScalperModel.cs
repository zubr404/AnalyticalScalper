using DdeInputDataQuikLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    class AnalyticalScalperModel
    {
        readonly DDEinfrastructure ddeInfrastructure;

        // нужна видимость по всему приложению (!!!?)
        // замечание: при static может тогда не нужно его передавать в качестве параметра.
        private static ExchangeInformation exchangeInfomation;
        private static AccountInfomation accountInformation;
        //---------------------------------------

        public AllTradesChannel allTradesChannel { get; private set; }
        public CurrentTableChannelMulti currentTableChannel { get; private set; }

        public ProcessingAllTrades processAllTrad { get; private set; }
        public ProcessingCurrentTable processCurrenTable { get; private set; }

        // -Counstructor-
        public AnalyticalScalperModel()
        {
            ddeInfrastructure = new DDEinfrastructure();
            exchangeInfomation = new ExchangeInformation();
            accountInformation = new AccountInfomation();

            allTradesChannel = ddeInfrastructure.AllTradesTable;
            currentTableChannel = ddeInfrastructure.CurrentTable;

            processAllTrad = new ProcessingAllTrades(allTradesChannel, exchangeInfomation);
            processCurrenTable = new ProcessingCurrentTable(currentTableChannel, exchangeInfomation);
        }

        // Properties
        public static ExchangeInformation ExchangeInfomationGLOBAL
        {
            get
            {
                if (exchangeInfomation == null)
                {
                    System.Windows.MessageBox.Show("Не создан экземпляр класса AnalyticalScalperModel.");
                    return null;
                }
                return exchangeInfomation;
            }
            private set
            {
                if (value != null)
                {
                    exchangeInfomation = value;
                }
            }
        }

        public static AccountInfomation AccountInformationGLOBAL
        {
            get
            {
                if (accountInformation == null)
                {
                    System.Windows.MessageBox.Show("Не создан экземпляр класса AnalyticalScalperModel.");
                    return null;
                }
                return accountInformation;
            }
            private set
            {
                if (value != null)
                {
                    accountInformation = value;
                }
            }
        }
    }
}
