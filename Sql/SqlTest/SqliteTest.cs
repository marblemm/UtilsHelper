using System.Data.OleDb;
using Dapper;
using UtilsHelper.SqlHelper;


namespace SqlTest
{
    public class SqliteTest
    {
        public static  void Test()
        {
            //Ïà¶ÔÂ·¾¶
            SqLiteHelp help = new SqLiteHelp(@"..\..\..\Doc\aa.db");
            string sql = "select * from TableTest";
            var aa = help.DbConnection.Query(sql);


            string insertSql = "insert into TableTest(col1, col2) values(@col1, @col2)";

            //using dapper
            var i = help.DbConnection.Execute(insertSql, new { col1 = "aa", col2 = "bb" });


            //using helper
            var paramss = new OleDbParameter[] { new OleDbParameter("@col1", "aa"), new OleDbParameter("@col2", "bb") };
            SqLiteHelper.ExecuteNonQuery(help.DbConnection.ConnectionString, insertSql, new object[] {"cc", "cc"});

            var count = help.RetriveDataSet(sql);
        }
    }
}