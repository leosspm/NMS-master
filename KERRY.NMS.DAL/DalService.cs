using System;
using System.Collections.Generic;
using System.Text;

namespace KERRY.NMS.DAL
{
    public interface IDalService
    {
        IDBContext NewConnection(bool isTransaction = false, bool isReadOnly = false, bool isMultipleResult = false);
    }

    public sealed class DalService : IDalService
    {
        private string connectionString = "";
        private DBContext dbContext = null;

        public DalService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDBContext NewConnection(bool isTransaction = false, bool isReadOnly = false, bool isMultipleResult = false)
        {
            dbContext = new DBContext(connectionString, isTransaction, isReadOnly, isMultipleResult);
            return dbContext;
        }
    }
}
