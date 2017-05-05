// <copyright file="AccessEx.cs" company="zondy">
//		Copyright (c) Zondy. All rights reserved.
// </copyright>
// <author>Administrator</author>
// <date>2017/4/28 21:59:43</date>
// <summary>文件功能描述</summary>
// <modify>
//		修改人:		
//		修改时间:	
//		修改描述:	
//		版本: 1.0	
// </modify>

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;

namespace UtilsHelper.SqlHelper
{
    public class AccessHelp
    {
        /// <summary>
        /// access数据库只支持32位，如果当前用的64位程序，用access会报错
        /// </summary>
        private const string ConnectionAcc03 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};User Id=;Password=;";
        private const string ConnectionAcc07 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};User Id=;Password=;";
        private const string DefaultSuffix = ".accdb";

        private readonly OleDbConnection _oleDbConnection;

        public AccessHelp(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var cs = DefaultSuffix.Equals(Path.GetExtension(filePath.ToLower())) ? ConnectionAcc07 : ConnectionAcc03;
                _oleDbConnection = new OleDbConnection(string.Format(cs, filePath));
            }
        }

        public DbConnection DbConn
        {
            get
            {
                if (_oleDbConnection.State == ConnectionState.Closed)
                {
                    _oleDbConnection.Open();
                }
                return _oleDbConnection;
            }
        }

        public int ExecuteNonQuery(string comText, params OleDbParameter[] param)
        {
            using (OleDbCommand cmd = new OleDbCommand(comText, _oleDbConnection))
            {
                if (param != null && param.Length != 0)
                {
                    cmd.Parameters.AddRange(param);
                }
                if (_oleDbConnection.State == ConnectionState.Closed)
                {
                    _oleDbConnection.Open();
                }
                return cmd.ExecuteNonQuery();
            }
        }

        public int ExecuteNonQuery(string sql)
        {
            using (OleDbCommand com = new OleDbCommand(sql, _oleDbConnection))
            {
                return com.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 执行SQL语句并返回受影响的行数
        /// </summary>
        public int ExecuteNonQuery(string sql, OleDbParameter par)
        {
            using (OleDbCommand com = new OleDbCommand(sql, _oleDbConnection))
            {
                com.Parameters.Add(par);
                return com.ExecuteNonQuery();
            }
        }

        public int ExecuteNonQuery(string sql, List<OleDbParameter> array)
        {

            OleDbCommand com = new OleDbCommand(sql, _oleDbConnection);
            SetParametersArray(ref com, array);
            return com.ExecuteNonQuery();
        }


        public OleDbDataReader ExecuteReader(string sql)
        {
            using (OleDbCommand com = new OleDbCommand(sql, _oleDbConnection))
            {
                OleDbDataReader reader = com.ExecuteReader();
                return reader;
            }
        }
        public OleDbDataReader ExecuteReader(string sql, OleDbParameter par)
        {
            using (OleDbCommand com = new OleDbCommand(sql, _oleDbConnection))
            {
                com.Parameters.Add(par);
                OleDbDataReader reader = com.ExecuteReader();
                return reader;
            }
        }

        public OleDbDataReader ExecuteReader(string sql, List<OleDbParameter> array)
        {
            OleDbCommand com = new OleDbCommand(sql, _oleDbConnection);
            SetParametersArray(ref com, array);
            OleDbDataReader reader = com.ExecuteReader();
            return reader;
        }


        public object ExecuteScalar(string sql)
        {
            using (OleDbCommand com = new OleDbCommand(sql, _oleDbConnection))
            {
                return com.ExecuteScalar();
            }
        }


        public object ExecuteScalar(string sql, OleDbParameter par)
        {
            using (OleDbCommand com = new OleDbCommand(sql, _oleDbConnection))
            {
                com.Parameters.Add(par);
                return com.ExecuteScalar();
            }
        }

        #region 执行Insert语句,并返回新添加的记录ID
        /// <summary>
        /// 执行Insert语句,并返回新添加的记录ID
        /// </summary>
        /// <returns></returns>
        public object ExecuteNonQueryAndGetIdentity(string sql)
        {
            using (OleDbCommand com = new OleDbCommand(sql, _oleDbConnection))
            {
                if (com.ExecuteNonQuery() >= 1)
                {
                    com.CommandText = "select @@identity";
                    return com.ExecuteScalar();
                }
            }
            return null;
        }

        /// <summary>
        /// 执行Insert语句,并返回新添加的记录ID
        /// </summary>
        public object ExecuteNonQueryAndGetIdentity(string sql, OleDbParameter par)
        {
            using (OleDbCommand com = new OleDbCommand(sql, _oleDbConnection))
            {
                com.Parameters.Add(par);
                if (com.ExecuteNonQuery() >= 1)
                {
                    com.CommandText = "select @@identity";
                    return com.ExecuteScalar();
                }
            }
            return null;
        }

        /// <summary>
        /// 执行Insert语句,并返回新添加的记录ID
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public object ExecuteNonQueryAndGetIdentity(string sql, List<OleDbParameter> array)
        {
            OleDbCommand com = new OleDbCommand(sql, _oleDbConnection);
            SetParametersArray(ref com, array);
            if (com.ExecuteNonQuery() >= 1)
            {
                com.CommandText = "select @@identity";
                return com.ExecuteScalar();
            }
            return null;
        }
        #endregion

        #region 返回DataSet
        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            using (OleDbDataAdapter adpter = new OleDbDataAdapter(sql, _oleDbConnection))
            {
                DataSet ds = new DataSet();
                adpter.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="par"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, OleDbParameter par)
        {
            using (OleDbCommand com = new OleDbCommand(sql, _oleDbConnection))
            {
                com.Parameters.Add(par);
                using (OleDbDataAdapter adpter = new OleDbDataAdapter(com))
                {
                    DataSet ds = new DataSet();
                    adpter.Fill(ds);
                    return ds;
                }
            }
        }

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, List<OleDbParameter> array)
        {
            OleDbCommand com = new OleDbCommand(sql, _oleDbConnection);
            SetParametersArray(ref com, array);
            using (OleDbDataAdapter adpter = new OleDbDataAdapter(com))
            {
                DataSet ds = new DataSet();
                adpter.Fill(ds);
                return ds;
            }
        }
        #endregion

        /// <summary>
        /// 填充
        /// </summary>
        /// <param name="com"></param>
        /// <param name="array"></param>
        void SetParametersArray(ref OleDbCommand com, List<OleDbParameter> array)
        {
            foreach (OleDbParameter item in array)
            {
                com.Parameters.Add(item);
            }
        }
    }
}
