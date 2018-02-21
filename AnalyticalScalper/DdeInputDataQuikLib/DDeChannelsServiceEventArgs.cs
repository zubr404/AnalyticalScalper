using System;

namespace DdeInputDataQuikLib
{
    /// <summary>
    /// Данные(сервисные) о событии для dde экспорта
    /// </summary>
    sealed class DDeChannelsServiceEventArgs : EventArgs
    {
        #region -Service information-
        int countRowsExport;

        /// <summary>
        /// Текущее количество строк в экспорте
        /// </summary>
        public int CountRowsExport
        {
            get { return countRowsExport; }
        }

        public void SetCountRowsExport(int value)
        {
            countRowsExport = value;
        }
        #endregion
    }
}
