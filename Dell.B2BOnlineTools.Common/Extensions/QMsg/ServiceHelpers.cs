using Dell.B2BOnlineTools.Common.Models.QMsg;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Dell.B2BOnlineTools.Common.Extensions.QMsg
{
    public static partial class QMsgHelper
    {
        public static Dictionary<ushort, ushort> MapArgs(this List<Parameter> parameterList, List<ArgInfo> argInfoList, bool strict = false)
        {
            Dictionary<ushort, ushort> paramArgMap = new Dictionary<ushort, ushort>();
            if (parameterList != null)
            {
                //Mapped: Parameter Index <- Argumet Index            
                parameterList.ForEach(p =>
                {
                    ArgInfo arg = null;
                    if (strict)
                    {
                        arg = argInfoList.Where(a =>
                           string.Compare(a.Name, p.Name, StringComparison.InvariantCultureIgnoreCase) == 0
                           && Type.GetType(p.DataType) == Type.GetType(a.DataType)
                        ).FirstOrDefault();
                    }
                    else
                    {
                        arg = argInfoList.Where(a =>
                            string.Compare(a.Name, p.Name, StringComparison.InvariantCultureIgnoreCase) == 0
                        ).FirstOrDefault();
                    }
                    if (arg != null)
                    {
                        paramArgMap[p.Index] = arg.Index;
                    }
                });
            }
            return paramArgMap;
        }

        public static Dictionary<string, dynamic> MapValues(this List<Parameter> parameterList, object[] array,
            List<ArgInfo> argInfoList, Dictionary<ushort,ushort> mapArgs, string parameterType, Dictionary<string, object> paramWithValues = null)
        {
            if (array.Count() == 0 && paramWithValues == null) return null;
            Dictionary<string, dynamic> paramValues = new Dictionary<string, dynamic>();
            var serviceParameters = parameterList.Where(x => string.CompareOrdinal(x.Type, parameterType) == 0).OrderBy(x => x.Index).ToList();                 
            serviceParameters.ForEach(p =>
            {
                if (paramWithValues != null && paramWithValues.ContainsKey(p.Name))
                    paramValues[p.Name] = p.GetValue(paramWithValues[p.Name]);
                else if (mapArgs != null && mapArgs.ContainsKey(p.Index))
                    paramValues[p.Name] = p.GetValue(array[mapArgs[p.Index]], argInfoList[mapArgs[p.Index]]);
                else
                    paramValues[p.Name] = p.GetValue();
            });

            /*var result = paramValues.OrderBy(x => x.Key).Select(x => x.Value).ToArray();
            return result;*/
            return paramValues;
        }
        public static dynamic GetValue(this Parameter parameter, object paramValue = null)
        {
            if (paramValue != null)
                return Convert.ChangeType(paramValue, Type.GetType(parameter.DataType));
            else if (parameter.IsValueEncrypted && parameter.DefaultValue != null)
                return Convert.ChangeType(Convert.ToString(parameter.DefaultValue).Decrypt(), Type.GetType(parameter.DataType));
            else if (parameter.DefaultValue != null)
                return Convert.ChangeType(parameter.DefaultValue, Type.GetType(parameter.DataType));
            return null;
        }
        public static dynamic GetValue(this Parameter parameter, object value, ArgInfo argInfo)
        {
            if (argInfo != null && argInfo.IsValueEncrypted && value != null)
                return Convert.ChangeType(Convert.ToString(value).Decrypt(), Type.GetType(parameter.DataType));
            else if(value != null)
                return Convert.ChangeType(value, Type.GetType(parameter.DataType));
            return null;
        }
        
        public static SqlParameter[] MapValues(this List<Parameter> parameterList, object[] array, 
            List<ArgInfo> argInfoList, Dictionary<ushort, ushort> mapArgs, Dictionary<string, object> paramWithValues = null)
        {
            if (array.Count() == 0 && paramWithValues == null) return null;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            var sqlParameters = parameterList.Where(p =>
                p.Type == Constants.QMsg.Parameter.Type.In || p.Type == Constants.QMsg.Parameter.Type.InOut
                || p.Type == Constants.QMsg.Parameter.Type.Out || p.Type == Constants.QMsg.Parameter.Type.ReturnValue).OrderBy(x => x.Index).ToList();
            sqlParameters.ForEach(p =>
            {
                if (paramWithValues != null && paramWithValues.ContainsKey(p.Name))
                    parameterCollection.Add(p.GetSqlParameter(paramWithValues[p.Name]));
                else if (mapArgs != null && mapArgs.ContainsKey(p.Index))
                    parameterCollection.Add(p.GetSqlParameter(array[mapArgs[p.Index]], argInfoList[mapArgs[p.Index]]));
                else
                    parameterCollection.Add(p.GetSqlParameter());
            });
            return parameterCollection.ToArray();
        }
        public static SqlParameter GetSqlParameter(this Parameter parameter, object paramValue = null)
        {
            SqlParameter param = new SqlParameter()
            {
                ParameterName = parameter.Name,
                SqlDbType = Type.GetType(parameter.DataType).ToSqlDbType()
            };
            if (paramValue != null)
                param.SqlValue = paramValue;
            else if (parameter.IsValueEncrypted && parameter.DefaultValue != null)
                param.SqlValue = Convert.ChangeType(Convert.ToString(parameter.DefaultValue).Decrypt(), Type.GetType(parameter.DataType));
            else if(parameter.DefaultValue != null)
                param.SqlValue = parameter.DefaultValue;
            return param;
        }
        public static SqlParameter GetSqlParameter(this Parameter parameter, object value, ArgInfo argInfo)
        {
            var dbType = Type.GetType(parameter.DataType).ToSqlDbType();
            var param = new SqlParameter()
            {
                ParameterName = parameter.Name,
                SqlDbType = dbType               
            };
            if (argInfo != null && argInfo.IsValueEncrypted && value != null)
                param.SqlValue = Convert.ChangeType(Convert.ToString(value).Decrypt(), Type.GetType(parameter.DataType));
            else if (value != null)
                param.SqlValue = value;
            return param;
        }
        

    }//End of Class
}
