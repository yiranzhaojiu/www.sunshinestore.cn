using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Data
{
    public class AosTransaction
    {
        internal readonly string DataBase;
        internal readonly Guid Distinct = Guid.NewGuid();

        private readonly IDbTransaction Transaction; 
        private readonly IDbConnection Connection;  
        private int _count = 0;
        internal AosTransaction(IDbTransaction transaction)
        {
            this.Transaction = transaction;
            this.Connection = transaction.Connection;
            this.DataBase = this.Connection.Database; 
        }

        public void AddCount()
        {
            _count++;
        }

        public IDbConnection GetConnection()
        {
            return Connection;
        }

        public void Commit()
        {
            _count--;

            if (_count == 0)
            {
                //var database = Transaction.Connection.Database;

                Transaction.Commit();

                DbTransactionContext.RemoveTransaction(DataBase);

                if (!DbTransactionContext.HasTransaction(DataBase) && Connection != null)
                    DbUtility.CloseConnection(Connection); 
            }
        }

        public void Rollback()
        {
            // var database = Transaction.Connection.Database;
            _count--;
            if (_count == 0)
            {
                Transaction.Rollback();

                DbTransactionContext.RemoveTransaction(DataBase);

                if (!DbTransactionContext.HasTransaction(DataBase) && Connection != null)
                    DbUtility.CloseConnection(Connection);
                 
            }
        }

        public override int GetHashCode()
        {
            byte[] buffer = Distinct.ToByteArray();
            return BitConverter.ToInt32(buffer, 0);
        } 
    }
}
