using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.Model
{
    /// <summary>
    /// Данные типа Double для событий
    /// </summary>
    sealed class DoubleValueEventArgs : EventArgs
    {
        public double DoubleValueNew { get; set; }

        public DoubleValueEventArgs(double _doubleValueNew)
        {
            DoubleValueNew = _doubleValueNew;
        }
    }
}
