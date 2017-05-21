using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ConsoleApplication
{
    public class DAO
    {
        public DataSet Ds = new DataSet();
        private SqlConnection _conn = new SqlConnection(); 
        private SqlDataAdapter _adp = new SqlDataAdapter();
        
        private string connectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost\\SQLEXPRESS";
            builder.NetworkLibrary = "dbmssocn";
            builder.UserID = "sa";
            builder.InitialCatalog = "TEST";
            builder.Password = "NA";
            builder.ConnectTimeout = 10;
            builder.IntegratedSecurity = false;
            return builder.ConnectionString;
        }

        public void connect()
        {
            
        }
        public void QueryData()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            _conn.ConnectionString = connectionString();
            _conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "SELECT * FROM TEST.dbo.Table1";
            adapter.SelectCommand = cmd;
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
            adapter.FillSchema(Ds, SchemaType.Mapped);
            adapter.Fill(Ds);
            DataTable table = Ds.Tables[0];
            DataRow row = table.NewRow();
            row[0] = 100;
            row[1] = "There is no spoon";
            row[2] = "Taiwan Hello World";
//            table.Rows.Add(row);
            //table.Rows[2].Delete();
            //Ds.Tables[0].Rows[0][0] = 50;
            //table.AcceptChanges();
            table.Rows[2][2] = "Modified Value";
            adapter.Update(Ds);
           
            Console.WriteLine(cmdBuilder.GetUpdateCommand().CommandText);
            
            Console.WriteLine(cmdBuilder.GetDeleteCommand().CommandText);
            
            SqlCommand ins = cmdBuilder.GetInsertCommand();
            Console.WriteLine(ins.CommandText);
            foreach (SqlParameter parameter in ins.Parameters)
            {
                Console.Write("{0} => {1}\t",parameter.TypeName,parameter.SqlValue);
            }
            
            _conn.Close();
        }

        public void PrintTable()
        {
            Console.WriteLine(Ds.Tables[0].TableName);
            foreach(DataColumn col in Ds.Tables[0].Columns)
                Console.Write("\t{0}\t",col.ColumnName);
            foreach (DataRow row in Ds.Tables[0].Rows)
            {
                Console.Write("\n{\t");
                foreach (object val in row.ItemArray)
                {
                    Console.Write("{0}\t",val.ToString());
                }
                Console.Write("}");
            }
        }
    }
}