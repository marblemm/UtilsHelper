using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace UtilsHelper.SqlHelper
{
    public class MysqlHelp
    {
        private const string ConnectionStr = "Server={0};Port={1};Database={2};Uid={3};Pwd={4};";
        private const string DefaultPort = "3306";
        private readonly string _connStr = "3306";
        private const int PoolSize = 20;

        private readonly BlockingCollection<DbConnection>    _dbConnectionsPool = new BlockingCollection<DbConnection>(PoolSize);

        protected DbConnection LeaseOne()
        {
            return _dbConnectionsPool.Take();
        }

        protected void Return(DbConnection dbConn)
        {
            _dbConnectionsPool.Add(dbConn);
        }
        public MysqlHelp(string server, string database, string uid, string pwd, string port = DefaultPort)
        {
            if (!string.IsNullOrEmpty(server))
            {
                _connStr = string.Format(ConnectionStr, server, port, database, uid, pwd);
            }
        }

        protected DbConnection DbConn
        {
            get { return new MySqlConnection(_connStr); }
        }

        protected DbDataAdapter DbAdapter
        {
            get { return new MySqlDataAdapter(); }
        }

        private const string GetAllDatabasesSql = "SELECT SCHEMA_NAME FROM information_schema.SCHEMATA";

        public static ReadOnlyCollection<string> EnumDbs(string ip, string port, string user, string pwd)
        {
            List<string> dbs = new List<string>();
            MySqlConnection connection = new MySqlConnection();
            // Try to open it
            try
            {
                var testString = string.Format(ConnectionStr, ip, port, string.Empty, user, pwd);
                connection.ConnectionString = testString;
                connection.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(GetAllDatabasesSql, connection);

                DataTable dt = new DataTable();
                if (dataAdapter.Fill(dt) > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        dbs.Add(Convert.ToString(dataRow[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                connection.Dispose();
            }

            return new ReadOnlyCollection<string>(dbs);
        }

        public static bool Check(IWin32Window owner, string ip, string port, string db, string user, string pwd)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection();
                var testString = string.Format(ConnectionStr, ip, port, db, user, pwd);
                connection.ConnectionString = testString;
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(owner, ee.Message);
                return false;
            }
        }

        public bool BulkCopy(DataTable table)
        {
            var dbConn = LeaseOne();

            try
            {
                if (table.Rows.Count == 0) { return false; }

                string tmpPath = Path.GetTempFileName();
                string csv = DataTableToCsv(table);
                // 生成无BOM的UTF-8文件
                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(tmpPath, csv, utf8);
                var tran = dbConn.BeginTransaction();
                MySqlBulkLoader bulk = new MySqlBulkLoader(dbConn as MySqlConnection)
                {
                    FieldTerminator = ",",
                    //FieldQuotationCharacter = '"',
                    //EscapeCharacter = '"',
                    LineTerminator = "\r\n",
                    FileName = tmpPath,
                    NumberOfLinesToSkip = 0,
                    TableName = table.TableName
                };

                //bulk.Columns.AddRange(table.Columns.Cast<DataColumn>().Select(colum => colum.ColumnName).ToArray());
                int count = bulk.Load();
                Console.WriteLine(count + " lines uploaded.");
                tran.Commit();
#if !DEBUG
                File.Delete(tmpPath);
#endif
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"BulkCopy::{table.TableName}:{ex.Message}");
            }
            finally
            {
                Return(dbConn);
            }

            return true;
        }

        private static string DataTableToCsv(DataTable table)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。  
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。  
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。  
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0)
                    {
                        sb.Append(",");
                    }

                    object obj = row[colum];
                    string value = Convert.ToString(obj);
                    if (DBNull.Value.Equals(obj) || string.IsNullOrEmpty(value))
                    {
                        value = "\\N";
                    }
                    else
                    {
                        if (colum.DataType == typeof(DateTime))
                        {
                            value = ((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }

                    if (value.Contains(","))
                    {
                        sb.Append("\"" + value.Replace("\"", "\"\"") + "\"");
                    }
                    else
                    {
                        sb.Append(value);
                    }
                }

                sb.AppendLine();
            }


            return sb.ToString();
        }
    }
}