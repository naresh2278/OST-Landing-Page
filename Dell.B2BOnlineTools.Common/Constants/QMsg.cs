
namespace Dell.B2BOnlineTools.Common
{
    public partial class Constants
    {
        public class QMsg
        {
            public class Service
            {
                public class Type
                {
                    public const string URL = "url";
                    public const string ADO = "ado";
                    public const string Console = "console";
                }                
            }
            public class Ado
            {
                public class Type
                {
                    public const string MySql = "mysql";
                    public const string MsSql = "mssql";
                }
                public class CommandType
                {
                    public const string StoredProcedure = "storedprocedure";
                    public const string Text = "text";
                    public const string TableDirect = "tabledirect";
                }
                public class Execute
                {
                    public const string NonQuery = "nonquery";
                    public const string Scalar = "scalar";
                    public const string Reader = "reader";
                }
            }
            public class Url
            {
                public class Type
                {
                    public const string WebApi = "webapi";
                    public const string OdataApi = "odataapi";
                    public const string WCF = "wcf";
                }
                public class Method
                {
                    public const string GET = "get";
                    public const string POST = "post";
                    public const string PUT = "put";
                    public const string DELETE = "delete";
                }
                public class DataType
                {
                    public const string JSON = "application/json";
                    public const string XML = "application/xml";
                }
            }
            public class Parameter
            {
                /*public const string Index = "index";
                public const string Name = "name";
                public const string Type = "type";
                public const string RegExp = "regexp";
                public const string Location = "location";*/

                public class Type
                {
                    public const string Url = "url";
                    public const string Body = "body";
                    public const string Default = "default";
                    public const string None = null;
                    public const string In = "in";
                    public const string Out = "out";
                    public const string InOut = "inout";
                    public const string ReturnValue = "returnvalue";
                }
            }
            public class Channel
            {
                public class Type
                {
                    public const string RabbitMQ = "rabbitmq";
                    public const string NsQ = "nsq";
                }
            }

            public class Operation
            {
                public class Type
                {
                    public const string Batch = "batch";
                }
            }

        }//End of QMsg
    }//End of Constants
}
