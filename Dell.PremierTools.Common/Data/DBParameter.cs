namespace Dell.PremierTools.Common.Data
{
    public class DBParameter
    {
        public string Name { get; set; }
        public System.Data.DbType DataType { get; set; }
        public object Value { get; set; }

        /// <summary>
        /// Only In and Out values are supported.
        /// </summary>
        public System.Data.ParameterDirection Direction { get; set; }
        public int Size { get; set; }
    }

}
