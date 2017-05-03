using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;

namespace UtilsHelper.SqlHelper
{
    public class SqLiteHelp
    {
        private readonly SQLiteConnection _connection;
        private SQLiteCommand _command;
        private const string ConnectionString = "data source = {0}; Pooling = true; FailIfMissing = false";
        public SqLiteHelp()
        {
            //Form1.Check_DB
            //connStr = System.Configuration.ConfigurationSettings.AppSettings["connStr"].ToString();
            //connStr=ConfigurationManager.ConnectionStrings["conStr"].ToString();
            //try
            //{
            //    var connStr = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\aa.db";
            //}
            //catch (Exception)
            //{
            //    //MessageBox.Show(ex.Message.ToString());
            //    //Application.Exit();
            //}
            _connection = new SQLiteConnection(string.Format(ConnectionString, "aa.db"));
        }

        public SqLiteHelp(string filePath)
        {
            _connection = new SQLiteConnection(string.Format(ConnectionString, filePath));
        }

        public IDbConnection DbConnection { get { return _connection; } }

        public string DbConnectionString { get { return _connection.ConnectionString; } }

        public void OpenConn()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public void CloseConn()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        #region 执行SQL语句，返回数据到DataSet中
        /// <summary>
        /// 执行SQL语句，返回数据到DataSet中
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dataSetName">自定义返回的DataSet表名</param>
        /// <returns>返回DataSet</returns>
        public DataSet ReturnDataSet(string sql, string dataSetName)
        {
            DataSet dataSet = new DataSet();
            OpenConn();
            SQLiteDataAdapter sqLiteDa = new SQLiteDataAdapter(sql, _connection);
            sqLiteDa.Fill(dataSet, dataSetName);
            CloseConn();
            return dataSet;
        }
        #endregion

        #region 执行Sql语句,返回带分页功能的dataset

        public DataSet ReturnDataSet(string sql, int pageSize, int currPageIndex, string dataSetName)
        {
            DataSet dataSet = new DataSet();
            OpenConn();
            SQLiteDataAdapter sqLiteDa = new SQLiteDataAdapter(sql, _connection);
            sqLiteDa.Fill(dataSet, pageSize * (currPageIndex - 1), pageSize, dataSetName);
            //   CloseConn();
            return dataSet;
        }
        #endregion

        #region 执行SQL语句，返回 DataReader,用之前一定要先.read()打开,然后才能读到数据
        /// <summary>
        /// 执行SQL语句，返回 DataReader,用之前一定要先.read()打开,然后才能读到数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回一个OracleDataReader</returns>
        public SQLiteDataReader ReturnDataReader(string sql)
        {
            OpenConn();
            _command = new SQLiteCommand(sql, _connection);
            //return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            SQLiteDataReader dataReader = _command.ExecuteReader();
            //CloseConn();
            return dataReader;
        }

        public SQLiteDataReader ReturnDataReader(string sql, ref int i)
        {
            OpenConn();
            _command = new SQLiteCommand(sql, _connection);
            //return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            i = 3;
            SQLiteDataReader dataReader = _command.ExecuteReader();
            //CloseConn();
            return dataReader;
        }
        #endregion

        #region 执行SQL语句，返回记录总数数
        /// <summary>
        /// 执行SQL语句，返回记录总数数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回记录总条数</returns>
        public int GetRecordCount(string sql)
        {
            int recordCount = 0;
            OpenConn();
            _command = new SQLiteCommand(sql, _connection);
            SQLiteDataReader dataReader = _command.ExecuteReader();
            while (dataReader.Read())
            {
                recordCount++;
            }
            dataReader.Close();
            //   CloseConn();
            return recordCount;
        }
        #endregion

        #region 取当前序列,条件为seq.nextval或seq.currval

        /// <summary>
        /// 取当前序列
        /// </summary>
        /// <param name="seqstr"></param>
        /// <returns></returns>
        public decimal GetSeq(string seqstr)
        {
            decimal seqnum = 0;
            string sql = "select " + seqstr + " from dual";
            OpenConn();
            _command = new SQLiteCommand(sql, _connection);
            SQLiteDataReader dataReader = _command.ExecuteReader();
            if (dataReader.Read())
            {
                seqnum = decimal.Parse(dataReader[0].ToString());
            }
            dataReader.Close();
            //   CloseConn();
            return seqnum;
        }
        #endregion

        #region 执行SQL语句,返回所影响的行数
        /// <summary>
        /// 执行SQL语句,返回所影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql)
        {
            int cmd = 0;
            OpenConn();
            _command = new SQLiteCommand(sql, _connection);
            try
            {
                cmd = _command.ExecuteNonQuery();
            }
            catch
            {
                // ignored
            }
            return cmd;
        }
        #endregion

        #region 执行SQL语句,对于 UPDATE、INSERT 和 DELETE 语句，返回值为该命令所影响的行数。对于其他所有类型的语句，返回值为 -1

        /// <summary>
        /// 对于 UPDATE、INSERT 和 DELETE 语句，返回值为该命令所影响的行数。对于其他所有类型的语句，返回值为 -1
        /// </summary>
        /// <param name="sql">UPDATE、INSERT 和 DELETE 语句</param>
        public int ExecuteNonQuery(string sql)
        {
            OpenConn();
            _command = new SQLiteCommand(sql, _connection);
            int rv;
            SQLiteTransaction sqliteTransaction = null;
            try
            {
                sqliteTransaction = _connection.BeginTransaction();
                _command.Transaction = sqliteTransaction;
                rv = _command.ExecuteNonQuery();
                sqliteTransaction.Commit();
            }
            catch (Exception)
            {
                if (sqliteTransaction != null) sqliteTransaction.Rollback();
                throw;
            }
            return rv;
        }

        /// <summary>
        /// 程序异常写入到数据库LOG
        /// </summary>
        /// <param name="syTs">信息题头</param>
        /// <param name="syError">错误信息</param>
        /// <param name="syForm">窗体</param>
        /// <param name="syFormnaMe">模块名</param>
        public void ExecuteNonQuery(string syTs, string syError, string syForm, string syFormnaMe)
        {
            OpenConn();
            string strSql = "insert into TB_SY_ERROR (SY_TS,SY_ERROR,SY_FORM,SY_FORMNAMe,SY_DATE,SY_TYPE) "
                + "VALUES('" + syTs + "','" + syError + "','" + syForm + "','" + syFormnaMe + "',sysdate,6)";
            _command = new SQLiteCommand(strSql, _connection);
            SQLiteTransaction sqliteTransaction = null;
            try
            {
                sqliteTransaction = _connection.BeginTransaction();
                _command.Transaction = sqliteTransaction;
                _command.ExecuteNonQuery();
                sqliteTransaction.Commit();
            }
            catch //(Exception ex)
            {
                if (sqliteTransaction != null) sqliteTransaction.Rollback();
                //throw ex;
            }
        }
        #endregion

        #region 执行查询，并将查询返回的结果集中第一行的第一列作为 .NET Framework 数据类型返回。忽略额外的列或行。
        /// <summary>
        /// 执行查询，并将查询返回的结果集中第一行的第一列作为 .NET Framework 数据类型返回。忽略额外的列或行。
        /// </summary>
        /// <param name="sql">SELECT 语句</param>
        /// <returns>.NET Framework 数据类型形式的结果集第一行的第一列；如果结果集为空或结果为 REF CURSOR，则为空引用</returns>
        public object ExecuteScalar(string sql)
        {
            OpenConn();
            _command = new SQLiteCommand(sql, _connection);
            return _command.ExecuteScalar();
        }
        #endregion

        #region 执行单Sql语句查询，并将查询返回的结果作为一个数据集返回
        /// <summary>
        ///  执行单Sql语句查询，并将查询返回的结果作为一个数据集返回
        /// </summary>
        /// <returns>数据集 DataSet</returns>
        public DataSet RetriveDataSet(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new Exception("抱歉，SQL 语句为空...");
            }
            OpenConn();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(sql, _connection))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        #endregion

        #region 执行Sql数组语句查询，并将查询返回的结果作为一个数据集返回
        /// <summary>
        /// 执行Sql数组语句查询，并将查询返回的结果作为一个数据集返回
        /// </summary>
        /// <returns>数据集 DataSet</returns>
        public DataSet RetriveDataSet(string[] sqls, params string[] tableNames)
        {
            if (sqls == null || sqls.Length == 0)
            {
                throw new Exception("抱歉，SQL 语句为空...");
            }
            var sqlLength = sqls.Length;
            OpenConn();
            DataSet ds = new DataSet();
            int tableNameLength = tableNames.Length;
            for (int i = 0; i < sqlLength; i++)
            {
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(sqls[i], _connection))
                {
                    if (i < tableNameLength)
                        da.Fill(ds, tableNames[i]);
                    else
                        da.Fill(ds, "table" + i);
                }
            }
            return ds;
        }

        public DataSet RetriveDataSet(string[,] sqlTableNames)
        {
            if (sqlTableNames.GetLength(0) != 2)
            {
                throw new Exception("抱歉，SQL语句-表名 参数必须为二维数组...");
            }
            int length = sqlTableNames.GetLength(1);
            if (length <= 0)
            {
                throw new Exception("抱歉，SQL语句-表名 为空...");
            }

            string[] sqls = new string[length];
            string[] tableNames = new string[length];
            for (int i = 0; i < length; i++)
            {
                sqls[i] = sqlTableNames[i, 0];
                tableNames[i] = sqlTableNames[i, 1];
            }
            return RetriveDataSet(sqls, tableNames);
        }
        #endregion

        #region  更新数据集.

        /// <summary>
        /// 更新数据集. 
        /// 过程:客户层(dataSet.GetChanges()) -- 修改 --> 数据服务层(hasChangesDataSet.update()) -- 更新--> 数据层(hasChangesDataSet) ...
        ///  数据层(hasChangesDataSet) -- 新数据 --> 数据服务层 (hasChangesDataSet) -- 合并 -- > 客户层(dataSet.Merge(hasChangesDataSet))
        /// </summary>
        /// <returns></returns>
        public DataSet UpdateDataSet(string sql, DataSet hasChangesDataSet)
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new Exception("抱歉，SQL 语句为空...");
            }
            OpenConn();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(sql, _connection))
            {
                SQLiteCommandBuilder cb = new SQLiteCommandBuilder(da);
                da.Update(hasChangesDataSet);
                return hasChangesDataSet;
            }
        }
        #endregion

        #region 将一组 UPDATE、INSERT 和 DELETE 语句以事务执行
        /// <summary>
        /// 将一组 UPDATE、INSERT 和 DELETE 语句以事务执行
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns>是否执行成功</returns>
        public bool ExecuteTransaction(string[] sqls)
        {
            if (sqls == null || sqls.Length == 0)
            {
                throw new Exception("抱歉，SQL 语句为空...");
            }
            SQLiteTransaction sqliteTransaction = null;
            //OracleCommand command = new OracleCommand(sql, Connection);
            //OracleCommand command = null;
            try
            {
                OpenConn();
                _command = _connection.CreateCommand();
                sqliteTransaction = _connection.BeginTransaction();
                _command.Connection = _connection;
                _command.Transaction = sqliteTransaction;

                foreach (string t in sqls)
                {
                    _command.CommandText = t;
                    _command.ExecuteNonQuery();
                }
                sqliteTransaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (sqliteTransaction != null)
                {
                    sqliteTransaction.Rollback();
                }
                throw;
            }
        }

        public bool ExecuteTransaction(ArrayList sqls)
        {
            if (sqls == null || sqls.Count == 0)
            {
                throw new Exception("抱歉，SQL 语句为空...");
            }
            SQLiteTransaction sqliteTransaction = null;
            //OracleCommand command = new OracleCommand(sql, Connection);
            //OracleCommand command = null;
            try
            {
                OpenConn();
                _command = _connection.CreateCommand();
                sqliteTransaction = _connection.BeginTransaction();
                _command.Connection = _connection;
                _command.Transaction = sqliteTransaction;

                //                 for (int i = 0; i < sqls.Length; i++)
                //                 {
                //                     command.CommandText = sqls[i];
                //                     command.ExecuteNonQuery();
                //                 }
                foreach (string str in sqls)
                {
                    _command.CommandText = str;
                    _command.ExecuteNonQuery();
                }
                sqliteTransaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (sqliteTransaction != null)
                {
                    sqliteTransaction.Rollback();
                }
                throw;
            }
        }

        public bool ExecuteTransaction(ArrayList sqlfp, ArrayList sqlmx)
        {
            if (sqlfp == null || sqlfp.Count == 0)
            {
                throw new Exception("抱歉，SQL 语句为空...");
            }
            SQLiteTransaction sqliteTransaction = null;
            try
            {
                OpenConn();
                _command = _connection.CreateCommand();
                sqliteTransaction = _connection.BeginTransaction();
                _command.Connection = _connection;
                _command.Transaction = sqliteTransaction;
                foreach (string str in sqlfp)
                {
                    _command.CommandText = str;
                    _command.ExecuteNonQuery();
                }
                foreach (string str in sqlmx)
                {
                    _command.CommandText = str;
                    _command.ExecuteNonQuery();
                }
                sqliteTransaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (sqliteTransaction != null)
                {
                    sqliteTransaction.Rollback();
                }
                throw;
            }
        }
        #endregion

        #region 执行Sql数组语句查询，并将查询返回的结果作为一个数据读取器返回
        /// <summary>
        ///  执行Sql数组语句查询，并将查询返回的结果作为一个数据读取器返回
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>OracleDataReader</returns>
        public SQLiteDataReader RetriveDataReader(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new Exception("抱歉，SQL 语句为空...");
            }
            OpenConn();
            using (_command = new SQLiteCommand(sql, _connection))
            {
                SQLiteDataReader sqliteDataReader = _command.ExecuteReader(CommandBehavior.CloseConnection);
                return sqliteDataReader;
            }
        }
        #endregion

        #region 执行一个查询式的存贮过程,返回得到的数据集
        /// <summary>
        /// 执行一个查询式的存贮过程,返回得到的数据集
        /// </summary>
        /// <param name="proceName">存贮过程名称</param>
        /// <param name="myParams">所有属性值</param>
        /// <returns></returns>
        public DataSet ExecStoredProcedure(string proceName, object[] myParams)
        {
            if (string.IsNullOrEmpty(proceName))
            {
                throw new Exception("抱歉，存贮过程名称为空...");
            }
            DataSet ds = new DataSet();
            OpenConn();
            //OracleCommand oracleCommand = new OracleCommand(sql, Connection);
            //OracleCommand oracleCommand = null;
            _command = _connection.CreateCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = proceName;
            if (myParams != null)
            {
                foreach (object t in myParams)
                    _command.Parameters.Add((SQLiteParameter)t);
            }
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_command))
            {
                int returnValue = da.Fill(ds);
                if (returnValue < 0)
                {
                    //                         throw new Exception("存储过程执行错误:" + (returnValue >= -14 ?
                    //                             ((StoreProcReturn)returnValue).ToString() : "ErrCode:" + returnValue));
                }
                return ds;
            }
        }
        #endregion

        #region 执行一个非查询式的存贮过程
        /// <summary>
        /// 执行一个非查询式的存贮过程
        /// </summary>
        /// <param name="proceName">存贮过程名称</param>
        /// <param name="myParams">所有属性值</param>
        /// <returns>存储过程return值</returns>
        public int ExecNonQueryStoredProcedure(string proceName, ref object[] myParams)
        {
            if (string.IsNullOrEmpty(proceName))
            {
                throw new Exception("抱歉，存贮过程名称为空...");
            }
            OpenConn();
            //OracleCommand oracleCommand = new OracleCommand(sql, Connection);
            //OracleCommand oracleCommand = null;
            _command = _connection.CreateCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = proceName;
            if (myParams != null)
            {
                foreach (object t in myParams)
                {
                    _command.Parameters.Add((SQLiteParameter)t);
                }
            }
            int returnValue = _command.ExecuteNonQuery();
            if (returnValue < 0)
            {
                //                     throw new Exception("存储过程执行错误:" + (returnValue >= -14 ?
                //                         ((StoreProcReturn)returnValue).ToString() : "ErrCode:" + returnValue));
            }
            return returnValue;
        }
        #endregion

        public void Dispose()
        {
            if (_command != null)
                _command.Dispose();
            if (_connection != null)
                _connection.Dispose();
        }
    }
}
