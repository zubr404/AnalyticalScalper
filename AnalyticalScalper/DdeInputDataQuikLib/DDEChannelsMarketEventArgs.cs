using System;

namespace DdeInputDataQuikLib
{
    /// <summary>
    /// Данные(с торгов) о событии для dde экспорта
    /// </summary>
    sealed class DDEChannelsMarketEventArgs : EventArgs
    {
        #region -Market infomation-
        /// <summary>
        /// Дата
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Номер заявки/сделки
        /// </summary>
        public double Number { get; set; }
        /// <summary>
        /// Время
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// Время милисекунды (в таблице Всех сделок)
        /// </summary>
        public double TimeMsc { get; set; }
        /// <summary>
        /// Код инструмента
        /// </summary>
        public string Securyti { get; set; }
        /// <summary>
        /// Операция К/П
        /// </summary>
        public string Operation { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public double Quantity { get; set; }
        /// <summary>
        /// Баланс: только для заявок
        /// </summary>
        public double Balance { get; set; }
        /// <summary>
        /// Статус: только для заявок
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Номер счета
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// ID транзакции
        /// </summary>
        public string Trans_ID { get; set; }
        /// <summary>
        /// Номер заявки: только для сделок
        /// </summary>
        public double NumberOrders { get; set; }
        /// <summary>
        /// Время последнего изменения: только для таблицы текущих параметров
        /// </summary>
        public string TimeChange { get; set; }
        /// <summary>
        /// Предложение: цена
        /// </summary>
        public decimal Bid { get; set; }
        /// <summary>
        /// Спрос: цена
        /// </summary>
        public decimal Offer { get; set; }
        /// <summary>
        /// Макс. возм. цена
        /// </summary>
        public decimal High_possible_price { get; set; }
        /// <summary>
        /// Миним. возм. цена
        /// </summary>
        public decimal Minimum_possible_price { get; set; }
        /// <summary>
        /// Шаг цены
        /// </summary>
        public decimal StepPrice { get; set; }
        /// <summary>
        /// Код кдасса
        /// </summary>
        public string ClassCode { get; set; }
        /// <summary>
        /// Текущая чистая позиция
        /// </summary>
        public double CurrentNettoPosition { get; set; }
        /// <summary>
        /// Активная покупка
        /// </summary>
        public double ActiveBuy { get; set; }
        /// <summary>
        /// Активная продажа
        /// </summary>
        public double ActiveSell { get; set; }
        #endregion

        ///Метод UpdateObject должен использоваться,
        ///например, для обновлении сохраненных данных
        ///из Таблицы текущих параметров
        /// <summary>
        /// Обновление полей входящего экземпляра
        /// </summary>
        public void UpdateObject(DDEChannelsMarketEventArgs _updater)
        {
            _updater.Account = this.Account;
            _updater.ActiveBuy = this.ActiveBuy;
            _updater.ActiveSell = this.ActiveSell;
            _updater.Balance = this.Balance;
            _updater.Bid = this.Bid;
            _updater.ClassCode = this.ClassCode;
            _updater.Comment = this.Comment;
            _updater.CurrentNettoPosition = this.CurrentNettoPosition;
            _updater.Date = this.Date;
            _updater.High_possible_price = this.High_possible_price;
            _updater.Minimum_possible_price = this.Minimum_possible_price;
            _updater.Number = this.Number;
            _updater.NumberOrders = this.NumberOrders;
            _updater.Offer = this.Offer;
            _updater.Operation = this.Operation;
            _updater.Price = this.Price;
            _updater.Quantity = this.Quantity;
            _updater.Securyti = this.Securyti;
            _updater.Status = this.Status;
            _updater.StepPrice = this.StepPrice;
            _updater.Time = this.Time;
            _updater.TimeMsc = this.TimeMsc;
            _updater.TimeChange = this.TimeChange;
            _updater.Trans_ID = this.Trans_ID;
        }
    }
}
