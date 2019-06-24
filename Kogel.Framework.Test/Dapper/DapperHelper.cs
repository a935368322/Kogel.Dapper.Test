using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
//using Sikiro.Dapper.Extension.Core.SetQ;
//using Sikiro.Dapper.Extension.Core.SetC;

namespace Kogel.Utility.Tool
{

    public class DapperHelper : IDbConnection, IDisposable
    {
        private IDbConnection DbConnection;
        public DapperHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
            DbConnection = new SqlConnection(this.ConnectionString);
        }
        public string ConnectionString { get; set; }

        public int ConnectionTimeout { get; set; }

        public string Database { get; set; }

        public ConnectionState State { get; set; }

        public IDbTransaction BeginTransaction()
        {
          return  DbConnection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return DbConnection.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            DbConnection.Close();
        }

        public IDbCommand CreateCommand()
        {
            return DbConnection.CreateCommand();
        }

        public void Dispose()
        {
            DbConnection.Dispose();
        }

        public void Open()
        {
            DbConnection.Open();
        }
        #region 实现Kogel.Dapper.Extension.MsSql的执行对象
        /// <summary>
        /// ToList实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public  IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }
        /// <summary>
        /// Get实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public T QueryFirstOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }
        /// <summary>
        /// 增删改实现
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
        }
        #endregion
    }
}
