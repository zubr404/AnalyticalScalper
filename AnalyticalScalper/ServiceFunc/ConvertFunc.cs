using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalScalper.ServiceFunc
{
    /// <summary>
    /// Конвертация данных
    /// </summary>
    static class ConvertFunc
    {
        /// <summary>
        /// Конвертация времени дд.мм.гггг чч:мм:сс плюс млсек в количество миллисекунд
        /// </summary>
        /// <param name="_datetime">дд.мм.гггг чч:мм:сс</param>
        /// <param name="_time_millisec">миллисекунды</param>
        /// <returns>TotalMilliseconds</returns>
        public static double ToTotalMillisecond(string _datetime, int _time_millisec)
        {
            DateTime dateTime = Convert.ToDateTime(_datetime);
            TimeSpan timeSpan = new TimeSpan(0, dateTime.Hour, dateTime.Minute, dateTime.Second, (int)_time_millisec);
            return timeSpan.TotalMilliseconds;
        }

        /// <summary>
        /// Время сделки, выраженное в миллисекундах
        /// </summary>
        /// <param name="_timeTrade">время сделки 00:00:00</param>
        /// <param name="_time_mcs">время сделки микросекунды</param>
        /// <returns>TotalMillisecondTrade</returns>
        public static double TotalMillisecondTradeCalc(string _timeTrade, double _time_mcs)
        {
            const double _sec = 1000; // секунда, выраженная в миллисекундах
            const string _date_str = "01.01.0001 ";
            double time_millisec = _time_mcs / _sec;
            string datetime_string = _date_str + _timeTrade;
            return ToTotalMillisecond(datetime_string, (int)time_millisec);
        }
    }

    /// <summary>
    /// Проверочные функции
    /// </summary>
    static class CheckFunc
    {
        /// <summary>
        /// Ограничение: только основная сессия
        /// </summary>
        /// <param name="_timeTrade">время сделки</param>
        /// <param name="_session_check"></param>
        /// <returns></returns>
        public static bool CheckTimeSession(string _timeTrade, ref bool _session_check)
        {
            DateTime time = Convert.ToDateTime("01.01.0001 " + _timeTrade);
            DateTime statrtSession = new DateTime(1, 1, 1, 10, 0, 0);
            DateTime finishSession = new DateTime(1, 1, 1, 19, 0, 0);

            if (!_session_check)
            {
                if (time >= statrtSession && time < finishSession)
                {
                    _session_check = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
    }
}
