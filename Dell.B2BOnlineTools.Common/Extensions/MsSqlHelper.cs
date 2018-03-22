using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dell.B2BOnlineTools.Common.Extensions
{
    public static class MsSqlHelper
    {
        public static SqlDbType ToSqlDbType(this Type type)
        {
            Dictionary<Type, SqlDbType> ClrToSqlDbType = new Dictionary<Type, SqlDbType>()
            {
                {typeof (Boolean), SqlDbType.Bit},
                {typeof (Boolean?), SqlDbType.Bit},
                {typeof (Byte), SqlDbType.TinyInt},
                {typeof (Byte?), SqlDbType.TinyInt},
                {typeof (String), SqlDbType.NVarChar},
                {typeof (DateTime), SqlDbType.DateTime},
                {typeof (DateTime?), SqlDbType.DateTime},
                {typeof (Int16), SqlDbType.SmallInt},
                {typeof (Int16?), SqlDbType.SmallInt},
                {typeof (Int32), SqlDbType.Int},
                {typeof (Int32?), SqlDbType.Int},
                {typeof (Int64), SqlDbType.BigInt},
                {typeof (Int64?), SqlDbType.BigInt},
                {typeof (Decimal), SqlDbType.Decimal},
                {typeof (Decimal?), SqlDbType.Decimal},
                {typeof (Double), SqlDbType.Float},
                {typeof (Double?), SqlDbType.Float},
                {typeof (Single), SqlDbType.Real},
                {typeof (Single?), SqlDbType.Real},
                {typeof (TimeSpan), SqlDbType.Time},
                {typeof (Guid), SqlDbType.UniqueIdentifier},
                {typeof (Guid?), SqlDbType.UniqueIdentifier},
                {typeof (Byte[]), SqlDbType.Binary},
                {typeof (Byte?[]), SqlDbType.Binary},
                {typeof (Char[]), SqlDbType.Char},
                {typeof (Char?[]), SqlDbType.Char},
                {typeof(DataTable), SqlDbType.Structured }
            };
            return ClrToSqlDbType[type];
        }
        public static Type ToClrType(this SqlDbType sqlDbType)
        {
            Dictionary<SqlDbType, Type> SqlDbTypeToClr = new Dictionary<SqlDbType, Type>()
            {
                {SqlDbType.Bit, typeof (Boolean)},
                {SqlDbType.Bit, typeof (Boolean?)},
                {SqlDbType.TinyInt, typeof (Byte)},
                {SqlDbType.TinyInt, typeof (Byte?)},
                {SqlDbType.NVarChar, typeof (String)},
                {SqlDbType.DateTime, typeof (DateTime)},
                {SqlDbType.DateTime, typeof (DateTime?)},
                {SqlDbType.SmallInt, typeof (Int16)},
                {SqlDbType.SmallInt, typeof (Int16?)},
                {SqlDbType.Int, typeof (Int32)},
                {SqlDbType.Int, typeof (Int32?)},
                {SqlDbType.BigInt, typeof (Int64)},
                {SqlDbType.BigInt, typeof (Int64?)},
                {SqlDbType.Decimal, typeof (Decimal)},
                {SqlDbType.Decimal, typeof (Decimal?)},
                {SqlDbType.Float, typeof (Double)},
                {SqlDbType.Float, typeof (Double?)},
                {SqlDbType.Real, typeof (Single)},
                {SqlDbType.Real, typeof (Single?)},
                {SqlDbType.Time, typeof (TimeSpan)},
                {SqlDbType.UniqueIdentifier, typeof (Guid)},
                {SqlDbType.UniqueIdentifier, typeof (Guid?)},
                {SqlDbType.Binary, typeof (Byte[])},
                {SqlDbType.Binary, typeof (Byte?[])},
                {SqlDbType.Char, typeof (Char[])},
                {SqlDbType.Char, typeof (Char?[])},
                {SqlDbType.Structured, typeof(DataTable) }
            };
            return SqlDbTypeToClr[sqlDbType];
        }

        public static List<DataTable> GetDataTables(this SqlDataReader reader)
        {
            List<DataTable> dataTables = new List<DataTable>();
            do
            {
                DataTable dt = new DataTable(reader.GetSchemaTable().TableName);
                DataTable dtSchema = reader.GetSchemaTable();
                foreach (DataRow row in dtSchema.Rows)
                {
                    DataColumn column = new DataColumn(Convert.ToString(row["ColumnName"]),
                                                       (Type)(row["DataType"]));
                    column.Unique = Convert.ToBoolean(row["IsUnique"]);
                    column.AllowDBNull = Convert.ToBoolean(row["AllowDBNull"]);
                    column.AutoIncrement = Convert.ToBoolean(row["IsAutoIncrement"]);
                    dt.Columns.Add(column);
                }
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    dt.Rows.Add(values);
                }
                dataTables.Add(dt);
            } while (reader.NextResult());
            return dataTables;
        }
    }
}
