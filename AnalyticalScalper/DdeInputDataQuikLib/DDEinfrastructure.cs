using System;
using System.Collections.Generic;
using XlDde;

namespace DdeInputDataQuikLib
{
    /// <summary>
    /// Регистрация dde каналов
    /// </summary>
    sealed class DDEinfrastructure : IDisposable
    {
        // Channels
        AllTradesChannel allTradesChannel;
        CurrentTableChannelMulti currentTableChannel;
        OrdersChannel orderChannel;
        ClientTradesChannel tradesChannel;
        PositionsCustomerAccountsChannel positionCustAccChannel;

        XlDdeServer server_AllTrades;
        XlDdeServer server_CurTable;

        // Идентификатор DDE сервера. Его следует задать так же при настройке экспорта таблиц в Квике
        /// <summary>
        /// Идентификатор сервера
        /// </summary>
        const string SERVICE_ALLTRADES = "DDE_AT";
        const string SERVICE_CURRENTTABLE = "DDE_CT";

        // Идентификаторы каналов. В Квике это поле "Рабочая книга", при этом
        // поле "Лист" следует оставить пустым.
        /// <summary>
        /// Таблица текущих параметров
        /// </summary>
        const string curtabTopic = "CurrentTable";
        /// <summary>
        /// Таблица всех сделок
        /// </summary>
        const string allTradesTopic = "AllTrades";
        /// <summary>
        /// Сделки
        /// </summary>
        const string clienttradTopic = "client_trad";
        /// <summary>
        /// Заявки
        /// </summary>
        const string ordTopic = "orders";
        /// <summary>
        /// Позиции по клиентским счетам (ФОРТС)
        /// </summary>
        const string posCustomTopic = "position_customer";

        // Constructor
        public DDEinfrastructure()
        {
            server_AllTrades = new XlDdeServer(SERVICE_ALLTRADES);
            allTradesChannel = new AllTradesChannel();
            server_AllTrades.AddChannel(allTradesTopic, allTradesChannel);
            server_AllTrades.Register();

            server_CurTable = new XlDdeServer(SERVICE_CURRENTTABLE);
            currentTableChannel = new CurrentTableChannelMulti();
            server_CurTable.AddChannel(curtabTopic, currentTableChannel);
            server_CurTable.Register();
        }

        #region Properties
        public CurrentTableChannelMulti CurrentTable
        {
            get { return currentTableChannel; }
        }
        public AllTradesChannel AllTradesTable
        {
            get { return allTradesChannel; }
        }
        public PositionsCustomerAccountsChannel PositionsCustomerAccountsTable
        {
            get { return positionCustAccChannel; }
        }
        public ClientTradesChannel TradesTable
        {
            get { return tradesChannel; }
        }
        public OrdersChannel OrdersTable
        {
            get { return orderChannel; }
        }
        #endregion

        #region DDE Disconnect
        public void Dispose()
        {
            server_AllTrades.Disconnect();
            server_AllTrades.Dispose();
        }
        #endregion
    }
}
