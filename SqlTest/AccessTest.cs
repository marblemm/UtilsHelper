using System;
using System.Data.OleDb;
using Dapper;
using UtilsHelper.SqlHelper;

namespace SqlTest
{
    public class AccessTest
    {
        public static void Test()
        {
            AccessHelp conn = new AccessHelp(@"..\..\..\Doc\test.accdb");


            //by ado
            var dbConn = conn.DbConn;
            string sql = "insert into UserTable(UserId, UserName) values(@UserId, @UserName)";

            OleDbParameter[] dbParams = {
                new OleDbParameter("@UserId", Guid.NewGuid().ToString()),
                new OleDbParameter("@UserName", "bb"),
            };
            conn.ExecuteNonQuery(sql, dbParams);


            //by dapper
            dbConn.Execute(sql, new { UserId = Guid.NewGuid().ToString(), UserName = "aa" });
        }



    }
}