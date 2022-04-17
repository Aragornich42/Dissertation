using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;

namespace DBCreator.TablesFilling
{

    /*
     EmployeeId      //foreign
     FIO
     DegreeOfKinship
     Phone
     */

    internal class RelativeFilling : TableFilling
    {
        private Random _rand = Helper.Rand;
        private long _startId = 1;
        private int _degreesLen = _degreesOfKinship.Length;

        public RelativeFilling() : base("Relative", "(DEFAULT,{0},'{1}','{2}','{3}'),") { }

        internal override string GetSql()
        {
            var party = Helper.Party;
            var sb = new StringBuilder(InsertStart);

            for (int i = 0; i < party; i++)
            {
                var degree = DegreeOfKinship();
                var fullName = new Name("ru").FullName(GetGender(degree));

                sb.Append(GetRow(_startId + i,
                                 fullName,
                                 degree,
                                 Helper.GetPhone()));                
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

        private bool IsEven(int num)
        {
            return (num % 2) == 0;
        }

        private Name.Gender GetGender(string degree)
        {
            var i = Array.IndexOf(_degreesOfKinship, degree);
            switch (IsEven(i))
            {
                case true:
                    return Name.Gender.Male;
                case false:
                    return Name.Gender.Female;
            }
        }

        private string DegreeOfKinship()
        {
            var i = _rand.Next(0, _degreesLen);
            return _degreesOfKinship[i];
        }
        
        #endregion

        #region Данные

        private static string[] _degreesOfKinship = new string[]
        {
            "Муж",
            "Жена",
            "Отец",
            "Мать",
            "Сын",
            "Дочь",
            "Дедушка",
            "Бабушка",
            "Внук",
            "Внучка",
            "Брат",
            "Сестра",
            "Отчим",
            "Мачеха",
            "Пасынок",
            "Падчерица",
            "Тесть",
            "Теща",
            "Свекор",
            "Свекровь",
            "Зять",
            "Невестка (сноха)"
        };

        #endregion
    }
}
