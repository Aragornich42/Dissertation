using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreator.TablesFilling
{

    /*
     EmployeeId      //foreign
     LanguageId      //foreign
     LanguageCompId      //foreign
     */

    internal class LanguageKnowledgeFilling : TableFilling
    {
        private Random _rand = Helper.Rand;
        private long _startId = 1;

        public LanguageKnowledgeFilling() : base("LanguageKnowledge", "({0},{1},{2}),") { }

        internal override string GetSql()
        {
            var party = Helper.Party;
            var sb = new StringBuilder(InsertStart);

            for (int i = 0; i < party; i++)
            {
                var knowCount = KnowCount();
                if (knowCount == 0)
                    continue;
                
                for (int j = 0; j < knowCount; j++)
                {
                    sb.Append(GetRow(_startId + i,
                                     LanguageId(),
                                     LanguageCompId()));
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

        private int KnowCount()
        {
            var g = _rand.Next(0, 100);
            if (g >= 0 && g <= 4)
                return 0;
            else if (g >= 5 && g <= 79)
                return 1;
            else if (g >= 80 && g <= 89)
                return 2;
            else if (g >= 90 && g <= 96)
                return 3;
            else if (g == 97 || g == 98)
                return 4;
            else
                return 5;
        }

        private int LanguageId()
        {
            return _rand.Next(0, Helper.LanguageCount);
        }

        private int LanguageCompId()
        {
            return _rand.Next(0, Helper.LanguageCompCount);
        }

        #endregion
    }
}
