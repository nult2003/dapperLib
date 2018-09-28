//using Logs;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Text;

namespace DALLib
{
    public static class DapperExtension
    {
        //private static ClassLoggerHelper logger = new ClassLoggerHelper("SQL");

        private static string FormatParametersForLogs(object param, bool isExecute = false)
        {
            var sb = new StringBuilder();

            if (param != null)
            {
                sb.Append(isExecute ? "Execute:" : "Query:");
                var p = param as DynamicParameters;
                if (p != null)
                {
                    foreach (var name in p.ParameterNames)
                    {
                        if (name == DynamicParametersExtensions.RETURN_VALUE)
                        {
                            continue;
                        }

                        var pValue = p.Get<dynamic>(name);

                        if (pValue is string)
                        {
                            pValue = ((string)pValue).Replace("}", "}}").Replace("{", "{{");
                        }


                        sb.AppendFormat("@{0}={1} ", name, pValue == null ? "null" : pValue.ToString());
                    }
                }
            }
            return sb.ToString();
        }

        //
        // Summary:
        //     Executes a query, returning the data typed as per T
        //
        // Returns:
        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column in assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, string sql, bool log, object param = null,
            IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            //using (logger.CreateMethodLogger(FormatParametersForLogs(param)))
            //{
            var retValue = cnn.Query<T>(sql,
                                        param,
                                        transaction,
                                        buffered,
                                        commandTimeout ?? cnn.ConnectionTimeout,
                                        commandType);
            return retValue;
            //}
        }

        //
        // Summary:
        //     Execute parameterized SQL
        //
        // Returns:
        //     Number of rows affected
        public static int Execute(this IDbConnection cnn, string sql, bool log, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            //using (logger.CreateMethodLogger(FormatParametersForLogs(param, true)))
            //{
            var retValue = cnn.Execute(sql,
                                        param,
                                        transaction,
                                        commandTimeout ?? cnn.ConnectionTimeout,
                                        commandType);
            return retValue;
            //}
        }
    }
}
