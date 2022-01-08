using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreator.TablesFilling
{

    /*
     EmployeeId      //foreign
     CountryId      //foreign
     Number
     StartDate
     FinishDate
    */

    internal class ForeignPassportFilling : TableFilling
    {
        private Random _rand = Helper.Rand;
        private long _startId = 1;

        public ForeignPassportFilling() : base("ForeignPassport", "(DEFAULT,{0},{1},'{2}','{3}','{4}'),") { }

        internal override string GetSql()
        {
            var party = Helper.Party;
            var sb = new StringBuilder(InsertStart);

            for (int i = 0; i < party; i++)
            {
                if (!HasPassport())
                    continue;

                var passCount = PassportCount();
                var startDate = StartDate();

                for (int j = 0; j < passCount; j++)
                {
                    sb.Append(GetRow(_startId + i,
                                     CountryId(),
                                     Number(),
                                     Helper.GetFormattedDate(startDate),
                                     Helper.GetFormattedDate(FinishDate(startDate))));
                }
            }
            sb.Replace(',', ';', sb.Length - 1, 1);
            return sb.ToString();
        }

        /// <summary>
        /// Много SQL-скриптов для исполнения (заполнения таблицы Employee)
        /// </summary>
        internal List<string> GetSqlArr()
        {
            var quot = Helper.RowsCount / Helper.Party;
            var scripts = new List<string>();
            for (int i = 0; i < quot; i++)
            {
                scripts.Add(GetSql());
                _startId += Helper.Party;
            }
            return scripts;
        }

        #region Приватные методы

        private bool HasPassport()
        {
            var g = _rand.Next(0, 10);
            return g <= 2;
        }

        private int PassportCount()
        {
            var g = _rand.Next(0, 100);
            if (g == 99)
                return 3;
            else if (g == 98 || g == 97)
                return 2;
            else
                return 1;
        }

        private int CountryId()
        {
            return _rand.Next(0, Helper.CountriesCount);
        }

        private string Number()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 14; i++)
                sb.Append(Helper.GetDigit());
            return sb.ToString();
        }

        private DateTime StartDate()
        {
            var year = _rand.Next(1991, 2022);
            var month = _rand.Next(1, 13);
            int day = _rand.Next(1, DateTime.DaysInMonth(year, month) + 1);
            return new DateTime(year, month, day);
        }

        private DateTime FinishDate(DateTime startDate)
        {
            var year = _rand.Next(startDate.Year + 1, 2101);
            var month = _rand.Next(1, 13);
            int day = _rand.Next(1, DateTime.DaysInMonth(year, month) + 1);
            return new DateTime(year, month, day);
        }

        #endregion
    }
}
