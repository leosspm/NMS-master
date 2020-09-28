using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace KERRY.NMS.DAL
{
    public interface IDBContext : IDisposable
    {
        void Commit();

        void Rollback();
        IUnitOfWork UnitOfWork { get; }
    }

    internal sealed class DBContext : IDBContext
    {
        private const string multipeResult = "MultipleActiveResultSets=True";
        private const string readOnly = "ApplicationIntent=ReadOnly";

        private IDbConnection connection = null;
        private IDbTransaction transaction = null;
        private IUnitOfWork unitOfWork = null;


        public IUnitOfWork UnitOfWork => unitOfWork;

        public DBContext(string connectionString, bool isTransaction, bool isReadOnly = false, bool isMultipleResult = false)
        {
            try
            {
                if (isReadOnly)
                {
                    connectionString += ";" + readOnly;
                }
                if (isMultipleResult)
                {
                    connectionString += ";" + multipeResult;
                }
                connection = new SqlConnection(connectionString);
                connection.Open();
                if (isTransaction)
                {
                    transaction = connection.BeginTransaction();
                }
                unitOfWork = new UnitOfWork(connection, transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
