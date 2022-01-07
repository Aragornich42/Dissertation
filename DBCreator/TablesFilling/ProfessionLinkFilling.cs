using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreator.TablesFilling
{
    /*
     EmployeeId      //foreign
     ProfessionId      //foreign
     IsMain
     */

    internal class ProfessionLinkFilling : TableFilling
    {
        private Random _rand = Helper.Rand;
        private long _startId = 1;

        public ProfessionLinkFilling() : base("ProfessionLink", "({0},{1},{2}),") { }

        internal override string GetSql()
        {
            var party = Helper.Party;
            var sb = new StringBuilder(InsertStart);

            for (int i = 0; i < party; i++)
            {
                var mainList = GetIsMain();

                foreach (var isMain in mainList)
                {
                    sb.Append(GetRow(_startId + i,
                                     ProfessionId(),
                                     isMain));
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

        private List<string> GetIsMain()
        {
            var g = _rand.Next(0, 10);

            int count;
            if (g >= 0 && g <= 6)
                count = 1;
            else if (g == 7 || g == 8)
                count = 2;
            else
                count = 3;

            var mainList = new List<string> { "TRUE" };
            for (int i = 1; i < count; i++)
                mainList.Add("FALSE");

            return mainList;
        }

        private int ProfessionId()
        {
            return _rand.Next(0, Helper.ProfessionsCount);
        }

        #endregion
    }
}
