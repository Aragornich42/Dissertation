namespace DBCreator.TablesFilling
{
    public abstract class TableFilling
    {
        // Пока что хардкод схемы
        private string _insertInto = "INSERT INTO public.";
        private string _values = " VALUES ";
        private string _table = "";
        private string _rowPattern = "(DEFAULT,'{0}', {1}),";

        protected string InsertStart
        {
            get { return _insertInto + _table + _values; }
        }

        public TableFilling(string table, string rowPattern)
        {
            _table = table;
            _rowPattern = rowPattern;
        }

        /// <summary>
        /// Цельный INSERT INTO
        /// </summary>
        internal abstract string GetSql();

        protected string GetRow(params object[] args)
        {
            return string.Format(_rowPattern, args);
        }
    }
}
