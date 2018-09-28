using System;
using System.Data;
using System.Linq;
using Dapper;
using System.Collections.Generic;

namespace DALLib
{
    public static class DynamicParametersExtensions
    {
        internal const string RETURN_VALUE = "RETURN_VALUE";

        public static void SetDefaultReturnValue(this DynamicParameters param)
        {
            if (!param.ParameterNames.Contains(RETURN_VALUE))
            {
                param.Add($"@{RETURN_VALUE}", dbType: DbType.Int32, size: 4, direction: ParameterDirection.ReturnValue);
            }
        }

        public static bool GetIfSuccess(this DynamicParameters param)
        {
            if (param.ParameterNames.Contains(RETURN_VALUE))
                return param.Get<int>(RETURN_VALUE) == 0;
            throw new Exception(
                "You need to call method SetDefaultReturnValue() before executing your stored procedure to call GetIfSuccess().");
        }

        public static bool ExtractParameters<T>(this DynamicParameters param, T element)
        {
            var returnValue = false;
            if (element != null)
            {
                param.SetDefaultReturnValue();
                var elementType = element.GetType();
                var props = elementType.GetProperties();
                foreach (var prop in props)
                {
                    IgnoreExtractionAttribute attribute = null;
                    foreach (var attr in prop.GetCustomAttributes(true))
                    {
                        attribute = attr as IgnoreExtractionAttribute;
                        if (attribute != null)
                            break;
                    }

                    if (attribute == null)
                    {
                        var value = prop.GetValue(element, new object[] { });
                        if ((prop.Name == "Id" || prop.Name == $"{elementType.Name}Id") && object.Equals(value, prop.PropertyType.IsValueType ? Activator.CreateInstance(prop.PropertyType) : null))
                        {
                            param.Add($"@{prop.Name}", dbType: DbType.Int32, size: 4, direction: ParameterDirection.Output);
                            continue;
                        }

                        if (value != null)
                        {
                            param.Add($"@{prop.Name}", value);
                        }
                    }
                }
                returnValue = true;
            }
            return returnValue;
        }

        public static bool ExtractDataTable<T>(this DynamicParameters param, IEnumerable<T> elements)
        {
            var returnValue = false;

            if (elements != null && elements.Any())
            {
                List<int> a = new List<int>();
                DataTable dt = new DataTable();

                foreach (T element in elements)
                {
                    DataRow row = dt.NewRow();
                    var elementType = element.GetType();
                    var props = elementType.GetProperties();
                    foreach (var prop in props)
                    {
                        IgnoreExtractionAttribute attribute = null;
                        foreach (var attr in prop.GetCustomAttributes(true))
                        {
                            attribute = attr as IgnoreExtractionAttribute;
                            if (attribute != null)
                                break;
                        }

                        if (attribute == null)
                        {
                            if (!dt.Columns.Contains(prop.Name))
                                dt.Columns.Add(prop.Name, prop.PropertyType);
                            row[prop.Name] = prop.GetValue(element);
                        }
                    }
                    dt.Rows.Add(row);
                }
                param.Add("@table", dt.AsTableValuedParameter());
                returnValue = true;
            }

            return returnValue;
        }
    }
}
