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
//using Sikiro.Dapper.Extension.Core.SetQ;
//using Sikiro.Dapper.Extension.Core.SetC;

namespace Kogel.Utility.Tool
{
    public enum Providers
    {
        SqlClient,
        OracleClient,
        // ReSharper disable once InconsistentNaming
        SQLite
    }
    public class Resource
    {
        /// <summary>
        /// 项目资源路径
        /// </summary>
        public static string url { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string connectionString { get; set; }
    }
    public class DapperHelper : IDbConnection, IDisposable
    {
        public IDbConnection Connection { get; }
        public string ConnectionString { get => Resource.connectionString; set => Connection.ConnectionString = value; }
        public int ConnectionTimeout => Connection.ConnectionTimeout;
        public string Database => Connection.Database;
        public ConnectionState State => Connection.State;
        /// <summary>
        /// Dapper帮助类创建
        /// </summary>
        /// <param name="providers">连接数据库对象</param>
        public DapperHelper(Providers providers = Providers.SqlClient)
        {
            switch (providers)
            {
                case Providers.SqlClient:
                    Connection = new SqlConnection(ConnectionString);
                    break;
                case Providers.OracleClient:
                    // Connection = new OracleConnection(connectionString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(providers), providers, null);
            }
        }
        public DapperHelper(string connectionString, Providers providers = Providers.SqlClient)
        {
            switch (providers)
            {
                case Providers.SqlClient:
                    Connection = new SqlConnection(connectionString);
                    break;
                case Providers.OracleClient:
                    // Connection = new OracleConnection(connectionString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(providers), providers, null);
            }
        }
        /// <summary>
        /// 对象释放
        /// </summary>
        public void Dispose()
        {
            Connection.Dispose();
        }
        #region 通用方法
        public IDbTransaction BeginTransaction() => Connection.BeginTransaction();
        public IDbTransaction BeginTransaction(System.Data.IsolationLevel il) => Connection.BeginTransaction(il);
        public void ChangeDatabase(string databaseName) => Connection.ChangeDatabase(databaseName);
        public void Close() => Connection.Close();
        public IDbCommand CreateCommand() => Connection.CreateCommand();
        /// <summary>
        /// 执行sql（返回影响行数）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute(string sql, object param = null, CommandType commandType = CommandType.Text) => Connection.Execute(sql, param, null, null, commandType);
        /// <summary>
        /// 执行sql（返回DataReader对象）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string sql, object param = null) => Connection.ExecuteReader(sql, param);
        /// <summary>
        /// 执行sql（返回第一行第一列的执行结果）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, object param = null, CommandType commandType = CommandType.Text) => Connection.ExecuteScalar(sql, param, null, null, commandType);
        public SqlMapper.GridReader QueryMultiple(string sql, object param = null) => Connection.QueryMultiple(sql, param);
        /// <summary>
        /// 查询sql（返回一个匿名类型的集合）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> Query(string sql, object param = null)
        {
            return Connection.Query(sql, param);
        }
        /// <summary>
        ///  查询sql（返回一个匿名类型）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public dynamic QueryFirst(string sql, object param = null) => Connection.QueryFirst(sql, param);
        public dynamic QueryFirstOrDefault(string sql, object param = null) => Connection.QueryFirstOrDefault(sql, param);
        public dynamic QuerySingle(string sql, object param = null) => Connection.QuerySingle(sql, param);
        public dynamic QuerySingleOrDefaul(string sql, object param = null) => Connection.QuerySingleOrDefault(sql, param);
        /// <summary>
        ///  查询sql（返回一个对象的集合）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null) => Connection.Query<T>(sql, param);
        public T QueryFirst<T>(string sql, object param = null) => Connection.QueryFirst<T>(sql, param);
        public T QueryFirstOrDefault<T>(string sql, object param = null) => Connection.QueryFirstOrDefault<T>(sql, param);
        public T QuerySingle<T>(string sql, object param = null) => Connection.QuerySingle<T>(sql, param);
        public T QuerySingleOrDefault<T>(string sql, object param = null) => Connection.QuerySingleOrDefault<T>(sql, param);
        public void Open()
        {
            Connection.Open();
        }
        #endregion

    }
}
