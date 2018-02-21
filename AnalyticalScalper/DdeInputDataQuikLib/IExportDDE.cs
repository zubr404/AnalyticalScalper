using System;

namespace DdeInputDataQuikLib
{
    /// <summary>
    /// Определяет события и свойства для класоов dde-экспорта
    /// </summary>
    interface IExportDDE
    {
        event System.EventHandler<DDEChannelsMarketEventArgs> LoadedLineEvent;
        event System.EventHandler<DDeChannelsServiceEventArgs> ObtainingDataCompletedEvent;
        event System.EventHandler<DDeChannelsServiceEventArgs> ObtainingDataStartedEvent;
    }
}
