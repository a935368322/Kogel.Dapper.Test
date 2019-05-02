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
    public enum Providers
    {
        SqlClient,
        OracleClient,
        // ReSharper disable once InconsistentNaming
        SQLite
    }
    public class DapperHelper : IDbConnection, IDisposable
    {
        public IDbConnection Connection { get; }
        public string ConnectionString { get; set; }
        public int ConnectionTimeout => Connection.ConnectionTimeout;
        public string Database => Connection.Database;
        public ConnectionState State => Connection.State;
        /// <summary>
        /// Dapper帮助类创建
        /// </summary>
        /// <param name="providers">连接数据库对象</param>
        public DapperHelper(Providers providers = Providers.SqlClient)
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
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
        ///  执行sql（返回Dataset对象）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string sql, string tableName = "table1", object param = null)
        {
            var dataSet = DataReaderToDataSet(Connection.ExecuteReader(sql, param));
            dataSet.Tables[0].TableName = tableName;
            return dataSet;
        }
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
        /// <summary>
        /// 异步执行，返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<int> ExecuteAsync(string sql, object param = null) => Connection.ExecuteAsync(sql, param);
        public Task<IDataReader> ExecuteReaderAsync(string sql, object param = null) => Connection.ExecuteReaderAsync(sql, param);
        public Task<object> ExecuteScalarAsync(string sql, object param = null) => Connection.ExecuteScalarAsync(sql, param);
        public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null) => Connection.QueryMultipleAsync(sql, param);
        /// <summary>
        /// 异步执行（返回一个匿名类型的集合）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null) => Connection.QueryAsync(sql, param);
        public Task<dynamic> QueryFirstAsync(string sql, object param = null) => Connection.QueryFirstAsync(sql, param);
        public Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null) => Connection.QueryFirstOrDefaultAsync(sql, param);
        public Task<dynamic> QuerySingleAsync(string sql, object param = null) => Connection.QuerySingleAsync(sql, param);
        public Task<dynamic> QuerySingleOrDefaultAsync(string sql, object param = null) => Connection.QuerySingleOrDefaultAsync(sql, param);
        /// <summary>
        /// 异步执行（返回一个对象的集合）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null) => Connection.QueryAsync<T>(sql, param);
        public Task<T> QueryFirstAsync<T>(string sql, object param = null) => Connection.QueryFirstAsync<T>(sql, param);
        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null) => Connection.QueryFirstOrDefaultAsync<T>(sql, param);
        public Task<T> QuerySingleAsync<T>(string sql, object param = null) => Connection.QuerySingleAsync<T>(sql, param);
        public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null) => Connection.QuerySingleOrDefaultAsync<T>(sql, param);

        #endregion

        #region 参数拼接
        /// <summary>
        /// 获取类名字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ClassName<T>() => typeof(T).ToString().Split('.').Last();

        /// <summary>
        /// 属性名称拼接并附加连接符
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="param">要拼接的动态类型</param>
        /// <param name="isParam">是否为参数,即是否增加前缀'@'</param>
        /// <returns></returns>
        public static string Joint(string separator, object param, bool isParam = false)
        {
            var prefix = isParam ? "@" : string.Empty;
            var propertys = param.GetType().GetProperties().Select(t => $"{prefix}{t.Name}").ToArray();
            var joint = new StringBuilder();
            for (var i = 0; i < propertys.Length; i++)
            {
                joint.Append(i != 0 ? $"{separator}{propertys[i]}" : propertys[i]);
            }
            return joint.ToString();
        }

        /// <summary>
        /// 以"param=@param"格式拼接属性名称并附加连接符
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="param">要拼接的动态类型</param>
        /// <returns></returns>
        public static string ParamJoint(string separator, object param)
        {
            var propertys = param.GetType().GetProperties().Where(t => t.GetValue(param) != null).Select(t => t.Name).Select(t => $"{t}=@{t}").ToArray();
            var joint = new StringBuilder();
            for (var i = 0; i < propertys.Length; i++)
            {
                joint.Append(i != 0 ? $"{separator}{propertys[i]}" : propertys[i]);
            }
            return joint.ToString();
        }

        /// <summary>
        /// 将参数名和参数值拼接并附加连接符,用于where语句拼接
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string ValueJoint(string separator, object param)
        {
            var joint = new StringBuilder();
            var count = 0;
            foreach (var item in param.GetType().GetProperties())
            {
                var value = item.GetValue(param, null);
                if (value == null) continue;
                var slice = $"{item.Name}=\'{value}\'";
                joint.Append(count != 0 ? $"{separator}{slice}" : slice);
                count++;
            }
            return joint.ToString();
        }

        #endregion

        #region 语句拼接
        public static string CompileInsert<T>(object param)
        {
            return $"insert into {ClassName<T>()}({Joint(",", param)}) values ({Joint(",", param, true)})";
        }

        public static string CompileDelete<T>(object param)
        {
            return $"delete from {ClassName<T>()} where {ParamJoint(" and ", param)}";
        }

        public static string CompileUpdate<T>(object setParam, object whereParam)
        {
            return $"update {ClassName<T>()} set {ValueJoint(",", setParam)} where {ValueJoint(" and ", whereParam)}";
        }

        public static string CompileSelect<T>(object param)
        {
            return $"select {Joint(",", param)} from {ClassName<T>()}";
        }

        #endregion

        #region 便捷查询
        public T GetQuery<T>(DapperHelper conn, dynamic param)
        {
            return conn.QueryFirstOrDefault<T>($"select * from {ClassName<T>()} where {ParamJoint(" and ", param)}", param);
        }

        #endregion

        #region 自定义方法
        /// <summary>
        /// DataReader转DataSet
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public DataSet DataReaderToDataSet(IDataReader reader)
        {
            DataSet dataSet = new DataSet();
            do
            {
                // Create new data table
                DataTable schemaTable = reader.GetSchemaTable();
                DataTable dataTable = new DataTable();
                if (schemaTable != null)
                {
                    // A query returning records was executed 
                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        DataRow dataRow = schemaTable.Rows[i];
                        // Create a column name that is unique in the data table 
                        string columnName = (string)dataRow["ColumnName"]; //+ " // Add the column definition to the data table 
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }
                    dataSet.Tables.Add(dataTable);
                    // Fill the data table we just created
                    while (reader.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dataRow[i] = reader.GetValue(i);
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
                else
                {
                    // No records were returned
                    DataColumn column = new DataColumn("RowsAffected");
                    dataTable.Columns.Add(column);
                    dataSet.Tables.Add(dataTable);
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = reader.RecordsAffected;
                    dataTable.Rows.Add(dataRow);
                }
            }
            while (reader.NextResult());
            return dataSet;
        }


        private List<string> GetFieldArr<T>(T model, ref SqlParameter[] sp, string[] excludeFiled = null)
        {
            List<string> fileIdArr = new List<string>();
            PropertyInfo[] PropertyList = model.GetType().GetProperties();
            sp = new SqlParameter[PropertyList.Where(x => x.GetValue(model, null) != null).Count()];
            int count = 0;
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(model, null);
                if (value != null)
                {   //字段类型
                    var typeName = item.PropertyType.FullName.ToString();
                    if (typeName.Contains("System.Int32") || typeName.Contains("System.Double") || typeName.Contains("System.Decimal"))
                    {
                        fileIdArr.Add(name);
                    }
                    else if (typeName.Contains("System.String"))
                    {
                        if (value.ToString() != "")
                        {
                            fileIdArr.Add(name);
                        }
                    }
                    else if (typeName.Contains("System.DateTime"))
                    {
                        var time = Convert.ToDateTime(value);
                        if (time != null && time != DateTime.MinValue)
                        {
                            fileIdArr.Add(name);
                        }
                    }
                    sp[count++] = new SqlParameter("@" + name, value);
                }
            }
            //筛选掉不需要的字段
            if (excludeFiled != null)
            {
                foreach (var filed in excludeFiled)
                {
                    fileIdArr = fileIdArr.Where(x => x != filed).ToList();
                }
            }
            return fileIdArr;
        }
        /// <summary>
        /// 获得新增语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="sp"></param>
        /// <param name="excludeFiled">需要排除的字段</param>
        /// <returns></returns>
        public virtual string GetInsertStr<T>(T model, ref SqlParameter[] sp, string[] excludeFiled = null)
        {
            var fileIdArr = GetFieldArr(model, ref sp, excludeFiled);
            string sql = "Insert into " + (model.ToString().IndexOf(".") != -1 ? model.ToString().Substring(model.ToString().LastIndexOf(".") + 1) : "")
                + "(" + string.Join(",", fileIdArr) + ")VALUES(" + string.Join(",", fileIdArr.Select(x => "@" + x)) + ")";
            return sql;
        }
        /// <summary>
        /// 获得修改语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="sp"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="excludeFiled">需要排除的字段</param>
        /// <returns></returns>
        public virtual string GetUpdateStr<T>(T model, ref SqlParameter[] sp, string sqlWhere, string[] excludeFiled = null)
        {
            var fileIdArr = GetFieldArr(model, ref sp, excludeFiled);
            string sql = "update " + (model.ToString().IndexOf(".") != -1 ? model.ToString().Substring(model.ToString().LastIndexOf(".") + 1) : "")
                + " set " + string.Join(",", fileIdArr.Select(x => x + "=@" + x)) + " Where 1=1 " + sqlWhere;
            return sql;
        }

        #region list to datatable
        public static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }
        /// <summary>
        /// 改变列的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="ColumnName"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DataTable UpdateColumnType(DataTable dt, string ColumnName, Type entity)
        {
            DataTable table = dt.Clone();
            if (table.Columns.Contains(ColumnName))
            {
                table.Columns[ColumnName].DataType = entity;

                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = table.NewRow();
                    for (var i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName != ColumnName)
                        {
                            newRow[dt.Columns[i].ColumnName] = row[dt.Columns[i]];
                        }
                        else
                        {
                            newRow[i] = Convert.ChangeType(row[dt.Columns[i]], entity);
                        }
                    }
                    table.Rows.Add(newRow);
                }
                return table;
            }
            else
            {
                return dt;
            }
        }

        public void Open()
        {
            Connection.Open();
        }
        #endregion
        #endregion
    }
}
