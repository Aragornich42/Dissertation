using System;
using System.Text;
using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;

namespace DBCreator.TablesFilling
{

    /*
     INN
     SNILS
     Position
     Surname
     Name
     Patronymic
     Sex
     Bithday
     Nationality
     RegAddress
     FactAddress
     InMarriage
     Phone
     Additional
    */

    internal class EmployeeFilling : TableFilling
    {
        private Random _rand = Helper.Rand;
        private int _profLen = Helper.Professions.Length;
        private int _nationLen = _nationalities.Length;
        private int _marrLen = _marriage.Length;

        public EmployeeFilling() : base("Employee", 
            "('{0}','{1}','{2}','{3}','{4}',{5},'{6}',{7},'{8}','{9}',{10},'{11}','{12}',{13}),") { }

        internal override string GetSql()
        {
            var party = Helper.Party;
            var sb = new StringBuilder(InsertStart);
            for(int i = 0; i < party; i++)
            {                
                var sex = GenerateSex();
                var name = new Faker<Name>("ru").Generate();
                var person = new Faker<Person>("ru").Generate();
                var address = new Faker<Address>("ru").Generate();

                sb.Append(GetRow(Inn(),
                                 Snils(),
                                 Position(),
                                 name.LastName(sex),
                                 name.FirstName(sex),
                                 Patronymic(),
                                 Sex(sex),
                                 person.DateOfBirth,
                                 Nationality(),
                                 address.FullAddress(),
                                 FactAddress(),
                                 InMarriage(),
                                 Phone(),
                                 Additional()));
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
                scripts.Add(GetSql());
            return scripts;
        }

        #region Приватные методы

        private string Inn()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 12; i++)
                sb.Append(Helper.GetDigit());
            return sb.ToString();
        }

        private string Snils()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    sb.Append(Helper.GetDigit());
                if (i != 2)
                    sb.Append('-');
                else
                    sb.Append(' ');
            }
            for (int i = 0; i < 2; i++)
                sb.Append(Helper.GetDigit());

            return sb.ToString();
        }

        private string Position()
        {
            var i = _rand.Next(0, _profLen);
            return Helper.Professions[i];
        }

        private string Patronymic()
        {
            if (_rand.Next(0, 1000000) == 999999)
                return "NULL";

            var sb = new StringBuilder('\'');
            sb.Append(Helper.GetUpperLetter());
            for (int i = 0; i < _rand.Next(6, 31); i++)
                sb.Append(Helper.GetLowerLetter());
            sb.Append('\'');
            return sb.ToString();
        }

        private Name.Gender GenerateSex()
        {
            var i = _rand.Next(0, 2);
            return (Name.Gender)i;
        }

        private string Sex(Name.Gender gender)
        {
            switch (gender)
            {
                case Name.Gender.Male:
                    return "М";
                case Name.Gender.Female:
                    return "Ж";
            }
            throw new Exception("Третьего не дано!");
        }

        private string Nationality()
        {
            var i = _rand.Next(0, _nationLen);
            return _nationalities[i];
        }

        private string FactAddress()
        {
            if (_rand.Next(0, 5) == 4)
            {
                var address = new Faker<Address>("ru").Generate().FullAddress();
                return $"'{address}'";
            }
            return "NULL";
        }

        private string InMarriage()
        {
            var i = _rand.Next(0, _marrLen);
            return _marriage[i];
        }

        private string Phone()
        {
            var sb = new StringBuilder("+7");
            for (int i = 0; i < 10; i++)
                sb.Append(Helper.GetDigit());
            return sb.ToString();
        }

        private string Additional()
        {
            if (_rand.Next(0, 5) == 4)
            {
                var lorem = new Faker<Lorem>().Generate().Sentences(_rand.Next(3, 101));
                return $"'{lorem}'";
            }
            return "NULL";
        }

        #endregion

        #region Данные

        private static string[] _nationalities = new string[]
        {
            "Русские",
            "Поморы",
            "Абазины",
            "Абхазы",
            "Аварцы",
            "Андийцы",
            "Арчинцы",
            "Ахвахцы",
            "Багулалы",
            "Бежтинцы",
            "Ботлихцы",
            "Гинухцы",
            "Годоберинцы",
            "Гунзибцы",
            "Дидойцы",
            "Каратинцы",
            "Тиндалы",
            "Хваршины",
            "Чамалалы",
            "Агулы",
            "Адыгейцы",
            "Азербайджанцы",
            "Алеуты",
            "Алтайцы",
            "Теленгиты",
            "Тубалары",
            "Челканцы",
            "Американцы",
            "Арабы",
            "Арабы среднеазиатские",
            "Армяне",
            "Черкесогаи",
            "Ассирийцы",
            "Афганцы",
            "Балкарцы",
            "Башкиры",
            "Белорусы",
            "Бесермяне",
            "Болгары",
            "Боснийцы",
            "Британцы",
            "Буряты",
            "Венгры",
            "Вепсы",
            "Водь",
            "Вьетнамцы",
            "Гагаузы",
            "Горские евреи",
            "Греки",
            "Греки-урумы",
            "Грузинские евреи",
            "Грузины",
            "Аджарцы",
            "Ингилойцы",
            "Лазы",
            "Мегрелы",
            "Сваны",
            "Даргинцы",
            "Кайтагцы",
            "Кубачинцы",
            "Долганы",
            "Дунгане",
            "Евреи",
            "Езиды",
            "Ижорцы",
            "Ингуши",
            "Индийцы",
            "Испанцы",
            "Итальянцы",
            "Ительмены",
            "Кабардинцы",
            "Казахи",
            "Калмыки",
            "Камчадалы",
            "Караимы",
            "Каракалпаки",
            "Карачаевцы",
            "Карелы",
            "Кереки",
            "Кеты",
            "Юги",
            "Киргизы",
            "Китайцы",
            "Коми",
            "Коми-ижемцы",
            "Коми-пермяки",
            "Корейцы",
            "Коряки",
            "Алюторцы",
            "Крымские татары",
            "Крымчаки",
            "Кубинцы",
            "Кумандинцы",
            "Кумыки",
            "Курды",
            "Курманч",
            "Лакцы",
            "Латыши",
            "Латгальцы",
            "Лезгины",
            "Литовцы",
            "Македонцы",
            "Манси",
            "Марийцы",
            "Горные марийцы",
            "Лугово-восточные марийцы",
            "Молдаване",
            "Монголы",
            "Мордва",
            "Мордва-мокша",
            "Мордва-эрзя",
            "Нагайбаки",
            "Нанайцы",
            "Нганасаны",
            "Негидальцы",
            "Немцы",
            "Меннониты",
            "Ненцы",
            "Нивхи",
            "Ногайцы",
            "Карагаши",
            "Орочи",
            "Осетины",
            "Осетины-дигорцы",
            "Осетины-иронцы",
            "Пакистанцы",
            "Памирцы",
            "Персы",
            "Поляки",
            "Румыны",
            "Русины",
            "Рутульцы",
            "Саамы",
            "Селькупы",
            "Сербы",
            "Словаки",
            "Словенцы",
            "Сойоты",
            "Среднеазиатские евреи",
            "Табасараны",
            "Таджики",
            "Тазы",
            "Талыши",
            "Татары",
            "Астраханские татары",
            "Кряшены",
            "Мишари",
            "Сибирские татары",
            "Таты",
            "Телеуты",
            "Тофалары (тофа)",
            "Тувинцы",
            "Тувинцы-тоджинцы",
            "Турки",
            "Турки-месхетинцы",
            "Туркмены",
            "Удины",
            "Удмурты",
            "Удэгейцы",
            "Узбеки",
            "Уйгуры",
            "Уйльта (ороки)",
            "Украинцы",
            "Ульчи",
            "Финны",
            "Финны-ингерманландцы",
            "Французы",
            "Хакасы",
            "Ханты",
            "Хемшилы",
            "Хорваты",
            "Цахуры",
            "Цыгане",
            "Цыгане среднеазиатские",
            "Черкесы",
            "Черногорцы",
            "Чехи",
            "Чеченцы",
            "Чеченцы-аккинцы",
            "Чуванцы",
            "Чуваши",
            "Чукчи",
            "Чулымцы",
            "Шапсуги",
            "Шорцы",
            "Эвенки",
            "Эвены (ламуты)",
            "Энцы",
            "Эскимосы",
            "Эстонцы",
            "Сету (сето)",
            "Юкагиры",
            "Якуты (саха)",
            "Японцы",
            "Австралийцы",
            "Австрийцы",
            "Айны",
            "Албанцы",
            "Ангольцы",
            "Аргентинцы",
            "Бангладешцы",
            "Баски",
            "Бельгийцы",
            "Бенинцы",
            "Берберы",
            "Бирманцы",
            "Бисау-гвинейцы",
            "Боливийцы",
            "Ботсванцы",
            "Бразильцы",
            "Булгары",
            "Буркинабцы",
            "Бурундийцы",
            "Венесуэльцы",
            "Габонцы",
            "Гаитяне",
            "Гамбийцы",
            "Ганцы",
            "Гватемальцы",
            "Гвинейцы",
            "Голландцы",
            "Гондурасцы",
            "Дагестанцы",
            "Датчане",
            "Дауры",
            "Джекцы",
            "Джибутинцы",
            "Доминиканцы",
            "Замбийцы",
            "Зимбабвийцы",
            "Израильтяне",
            "Индонезийцы",
            "Ирландцы",
            "Исландцы",
            "Кабоверденцы",
            "Камасинцы",
            "Камбоджийцы",
            "Камерунцы",
            "Канадцы",
            "Кенийцы",
            "Киприоты",
            "Кистины",
            "Колумбийцы",
            "Коморцы",
            "Конголезцы",
            "Костариканцы",
            "Котдивуарцы",
            "Ланкийцы",
            "Лаосцы",
            "Лесотцы",
            "Либерийцы",
            "Лихтенштейнцы",
            "Люксембуржцы",
            "Маврикийцы",
            "Малавийцы",
            "Малагасийцы",
            "Малайцы",
            "Малийцы",
            "Мальдивцы",
            "Маньчжуры",
            "Мексиканцы",
            "Мозамбикцы",
            "Монегаски",
            "Намибийцы",
            "Непальцы",
            "Нигерийцы",
            "Нигерцы",
            "Никарагуанцы",
            "Новогвинейцы",
            "Новозеландцы",
            "Норвежцы",
            "Панамцы",
            "Парагвайцы",
            "Перуанцы",
            "Полинезийцы",
            "Португальцы",
            "Пуэрториканцы",
            "Россияне",
            "Руандийцы",
            "Сальвадорцы",
            "Сантомийцы",
            "Сенегальцы",
            "Сибо",
            "Сомалийцы",
            "Суданцы",
            "Сьерралеонцы",
            "Таиландцы",
            "Танзанийцы",
            "Тоголезцы",
            "Тонганцы",
            "Тюрки",
            "Угандийцы",
            "Уругвайцы",
            "Филиппинцы",
            "Чадцы",
            "Чилийцы",
            "Шведы",
            "Швейцарцы",
            "Эквадорцы",
            "Экваторианцы",
            "Эритрейцы",
            "Эфиопы",
            "Югославы",
            "Южноафриканцы",
            "Ямайцы",
            "Прочие национальности"
        };

        private static string[] _marriage = new string[]
        {
            "Никогда не состоял(а) в браке",
            "Состоит в зарегистрированном браке",
            "Состоит в незарегистрированном браке",
            "Вдовец (вдова)",
            "Разведен(а) официально (развод зарегистрирован)",
            "Разошелся(лась)"
        };

        #endregion
    }
}