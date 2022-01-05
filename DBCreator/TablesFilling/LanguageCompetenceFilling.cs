using System.Text;

namespace DBCreator.TablesFilling
{
    internal class LanguageCompetenceFilling : TableFilling
    {
        public LanguageCompetenceFilling() : base("LanguageCompetence", "('{0}'),") { }

        internal override string GetSql()
        {
            var sb = new StringBuilder(InsertStart);
            foreach (var competence in _competences)
                sb.Append(GetRow(competence));
            sb.Replace(',', ';', sb.Length - 1, 1);
            return sb.ToString();
        }

        #region Данные
        private static string[] _competences = new string[]
        {
            "Читает и переводит со словарем",
            "Читает и может объясняться",
            "Владеет свободно",
        };
        #endregion
    }
}
