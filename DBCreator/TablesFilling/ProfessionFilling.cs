using System.Text;

namespace DBCreator.TablesFilling
{
    internal class ProfessionFilling : TableFilling
    {
        public ProfessionFilling() : base("Profession", "('{0}'),") { }

        internal override string GetSql()
        {
            var sb = new StringBuilder(InsertStart);
            foreach (var profession in Helper.Professions)
                sb.Append(GetRow(profession));
            sb.Replace(',', ';', sb.Length - 1, 1);
            Helper.ProfessionsCount
            return sb.ToString();
        }        
    }
}