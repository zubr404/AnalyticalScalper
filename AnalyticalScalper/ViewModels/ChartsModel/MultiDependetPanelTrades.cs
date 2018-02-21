using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnalyticalScalper.ViewModels.ChartsModel
{
    /// <summary>
    /// Дополнительная мультипанель, размещающая внутри себя
    /// панели типа TapeTradesDrawing.
    /// Степень зависимости: полная.
    /// </summary>
    class MultiDependetPanelTrades
    {
        public readonly List<TapeTradesDrawing> ChildrenPanelList;
        public FullyDependentPanel FullyDependentPanel { get; private set; }

        public MultiDependetPanelTrades(List<TapeTradesDrawing> _childrenPanel)
        {
            ChildrenPanelList = _childrenPanel;
            FullyDependentPanel = new FullyDependentPanel(new System.Windows.Thickness());
        }
        public MultiDependetPanelTrades(List<TapeTradesDrawing> _childrenPanel, SizeGlobal _sizepanel)
        {
            ChildrenPanelList = _childrenPanel;
            FullyDependentPanel = new FullyDependentPanel(_sizepanel);
        }

        /// <summary>
        /// Обновление дочерних панелей
        /// </summary>
        /// <param name="_dataTrades">DataTradesExchenge</param>
        public void UpdatePanels(Model.DataTradesExchenge _dataTrades)
        {
            foreach (TapeTradesDrawing item in ChildrenPanelList)
            {
                item.GetInitialeValues(_dataTrades);
            }
        }
    }
}
