using Dapper;
using Slapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KERRY.NMS.DAL.Respository
{
    internal class RepositoryBase
    {
        private IDbConnection connection = null;
        private IDbTransaction transaction = null;

        public RepositoryBase(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string procedure, DynamicParameters dynamicParameters = null)
        {
            Type type = typeof(T);
            if (IsPrimitiveType(type))
            {
                return await QueryPrimitiveAsync<T>(procedure, dynamicParameters);
            }
            return await QueryDynamicAsync<T>(procedure, dynamicParameters);
        }

        protected async Task<T> QuerySingleOrDefaultAsync<T>(string procedure, DynamicParameters dynamicParameters = null)
        {
            Type type = typeof(T);
            if (IsPrimitiveType(type))
            {
                return await QuerySingleOrDefaultPrimitiveAsync<T>(procedure, dynamicParameters);
            }
            return await QuerySingleOrDefaultDynamicAsync<T>(procedure, dynamicParameters);
        }

        protected async Task<bool> ExecuteAsync(string procedure, DynamicParameters dynamicParameters = null)
        {
            await connection.ExecuteAsync(sql: procedure, param: dynamicParameters, commandType: CommandType.StoredProcedure, transaction: transaction);
            return true;
        }

        private async Task<IEnumerable<T>> QueryPrimitiveAsync<T>(string procedure, DynamicParameters dynamicParameters)
        {
            return await connection.QueryAsync<T>(sql: procedure, transaction: transaction, commandType: CommandType.StoredProcedure, param: dynamicParameters);
        }

        private async Task<IEnumerable<T>> QueryDynamicAsync<T>(string procedure, DynamicParameters dynamicParameters)
        {
            var result = await connection.QueryAsync<dynamic>(sql: procedure, transaction: transaction, commandType: CommandType.StoredProcedure, param: dynamicParameters);
            return result == null ? null : Slapper.AutoMapper.MapDynamic<T>(result, false) as IEnumerable<T>;
        }

        private async Task<T> QuerySingleOrDefaultPrimitiveAsync<T>(string procedure, DynamicParameters dynamicParameters)
        {
            return await connection.QuerySingleOrDefaultAsync<T>(sql: procedure, transaction: transaction, commandType: CommandType.StoredProcedure, param: dynamicParameters);
        }

        private async Task<T> QuerySingleOrDefaultDynamicAsync<T>(string procedure, DynamicParameters dynamicParameters)
        {
            var result = await connection.QuerySingleOrDefaultAsync<dynamic>(sql: procedure, transaction: transaction, commandType: CommandType.StoredProcedure, param: dynamicParameters);
            return result == null ? null : Slapper.AutoMapper.MapDynamic<T>(result, false);
        }

        private bool IsPrimitiveType(Type type)
        {
            Type[] types = new Type[]
            {
                typeof (Enum), typeof (String), typeof (Char), typeof (Guid),
                typeof (Boolean), typeof (Byte), typeof (Int16), typeof (Int32),
                typeof (Int64), typeof (Single), typeof (Double), typeof (Decimal),
                typeof (SByte), typeof (UInt16), typeof (UInt32), typeof (UInt64),
                typeof (DateTime), typeof (DateTimeOffset), typeof (TimeSpan),
                typeof(int?), typeof(float?), typeof(double?), typeof(bool?)
            };
            return types.Any(x => x == type);
        }
    }
}
