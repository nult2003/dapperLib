using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Slapper;
using System;
using System.Data.SqlClient;
using System.Runtime.Caching;

namespace DALLib
{
    public abstract class DALDapperBase<T, TU> : DALBase
    {

        private static readonly Dictionary<Type, List<string>> TypeCacheDictionary =
               new Dictionary<Type, List<string>>
               {

               };


        public DALDapperBase(SqlConnection connection = null) : base(connection)
        {

        }


        /**
        * Used to initialize any static data, or to perform a particular action that needs performed once only. 
        * It is called automatically before the first instance is created or any static members are referenced.
        **/
        static DALDapperBase()
        {
            TypeCacheDictionary.Add(typeof(DashboardRequest), new List<string>() { "QuoteRequestId" });
            foreach (var typeCache in TypeCacheDictionary)
                AutoMapper.Configuration.AddIdentifiers(typeCache.Key, typeCache.Value);
        }


        public virtual TDP CreateParameters<TDP>(object[] values, string[] sufs = null, TDP parameters = null,
           int indexOutput = -1)
           where TDP : DynamicParameters, new()
        {
            var p = parameters ?? new TDP();
            var className = GetType().Name;
            if (sufs == null)
                sufs = new[] { "Id" };
            if (indexOutput >= sufs.Length)
                throw new ArgumentOutOfRangeException(nameof(indexOutput), indexOutput, $"Outside of Array");
            // Output par défaut -1, il peut-être modifié via le indexOutput (-1 pour aucun output).
            for (var i = 0; (i < sufs.Length) && (i < values.Length); ++i)
                if (i == indexOutput)
                    p.Add($"@{sufs[i]}", DbType.Int32, size: 4,
                        direction: ParameterDirection.Output);
                else
                    p.Add($"@{sufs[i]}", values[i]);

            return p;
        }

        public virtual T QueryDbCommandSpecific(string procStockName, SqlMapper.IDynamicParameters parameters)
        {
            var elements = QueryDbCommand(procStockName, parameters);

            return elements != null && elements.Count == 1 ? elements.SingleOrDefault() : default(T);
        }

        public virtual TC QueryDbCommandSpecific<TC>(string procStockName, SqlMapper.IDynamicParameters parameters = null)
        {
            var elements = QueryDbCommand<TC>(procStockName, parameters);

            return elements != null && elements.Count == 1 ? elements.SingleOrDefault() : default(TC);
        }

        public virtual List<TC> QueryDbCommand<TC>(string procStockName, SqlMapper.IDynamicParameters parameters = null)
        {
            var retEnumerable = Connection.Query<TC>(procStockName, true,
                parameters, commandType: CommandType.StoredProcedure, transaction: Transaction);

            return retEnumerable != null ? retEnumerable.ToList() : null;
        }

        public virtual List<T> QueryDbCommandRaw(string rawQuery)
        {
            var query = Connection.Query<dynamic>(rawQuery);

            if (query != null)
            {
                var dynamicMapped = AutoMapper.MapDynamic<T>(query, keepCache: false);
                return dynamicMapped != null ? dynamicMapped.ToList() : null;
            }
            return null;
        }

        public virtual List<T> QueryDbCommand(string procStockName, SqlMapper.IDynamicParameters parameters = null)
        {
            var query = Connection.Query<dynamic>(procStockName, true, parameters,
                commandType: CommandType.StoredProcedure, transaction: Transaction);

            if (query != null)
            {
                var dynamicMapped = AutoMapper.MapDynamic<T>(query, keepCache: false);
                return dynamicMapped != null ? dynamicMapped.ToList() : null;
            }

            return null;
        }


        public virtual R ExecuteDbCommand<R>(string procStockname, DynamicParameters parameters, string outputParam, SqlTransaction transaction = null)
        {
            Connection.Execute(procStockname, true, parameters,
               commandType: CommandType.StoredProcedure, transaction: transaction ?? Transaction);
            var tDynamics = parameters;
            return tDynamics != null ? tDynamics.Get<R>(outputParam) : default(R);
        }

        public virtual void ExecuteDbCommand(string procStockname, SqlMapper.IDynamicParameters parameters, SqlTransaction transaction = null)
        {
            Connection.Execute(procStockname, true, parameters,
                commandType: CommandType.StoredProcedure, transaction: transaction ?? Transaction);
        }

        public virtual TU ExecuteDbCommand(string procStockname, DynamicParameters parameters, string idValue, SqlTransaction transaction = null)
        {
            ExecuteDbCommand(procStockname, parameters, transaction: transaction ?? Transaction);

            var tDynamics = parameters;
            return tDynamics != null ? tDynamics.Get<TU>(idValue) : default(TU);
        }
    }

}
