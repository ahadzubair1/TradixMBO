using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;

namespace LMSBackOfficeDAL
{

	public static class ADOUtility
	{


        //string qry, sql = "";
        //public string connString = "";
        //Int32 userID;
        //int retLocationID = 0;
        //DataTable dt = new DataTable();
        //DataSet ds = new DataSet();
        //SqlDataReader dr;
        //SqlDataAdapter sda = new SqlDataAdapter();
        //SqlCommand objCommand = new SqlCommand();




        /// <summary>
        /// GLOBAL CONNECTION STRING METHOD
        /// </summary>
        /// <returns></returns>
        //public static string GetCoreDatabaseConnectionString()
        //{
        //    return System.Configuration.ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
        //}



        /// <summary>
        /// METHOD TO CONVERT DATATABLE TO CSV
        /// </summary>
        /// <param name="dtable"></param>
        /// <returns></returns>
        private static string DataTableToCSV(DataTable dtable)
        {
           
            StringBuilder sb = new StringBuilder();
            DataColumn colum;

            if (dtable != null || dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    for (int i = 0; i < dtable.Columns.Count; i++)
                    {
                        colum = dtable.Columns[i];
                        if (i != 0) sb.Append(",");
                        if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                        {
                            sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                        }
                        else sb.Append(row[colum].ToString());
                    }
                    sb.AppendLine();
                }

                return sb.ToString();
            }
            else
			{
                return "Invalid Input Supplied";
			}

        }

        /// <summary>
        /// METHOD TO RETURN ALL MERCHANT ADDRESSES
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllMerchantAddresses(Int16 PageIndex, Int16 PageSize, Int16 IsExcel, Int16 RecordCount)
        {
            DataTable dtAddressInfo = null;
            try
            {
                 DataSet dsAddressInfo = null;
                SqlConnection Connection = new SqlConnection("Data Source=15.184.218.35;Initial Catalog=OTC_TradingSystem;Persist Security Info=True;User ID=sa;Password=TC0qd8UiEqwP*xWB;Connect Timeout=30000");
                //string Constring = System.Configuration.ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
                // SqlConnection Connection = new SqlConnection(Constring);
                //SqlDataAdapter DataAdapter = new SqlDataAdapter("GetMerchantAddresses", Connection);

                SqlCommand objCommand = new SqlCommand("USP_GetMemberAddresses", Connection);
                objCommand.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                objCommand.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                objCommand.Parameters.Add(new SqlParameter("@Is_Excel", IsExcel));
                objCommand.Parameters.Add(new SqlParameter("@RecordCount", RecordCount));

                SqlDataAdapter DataAdapter = new SqlDataAdapter();
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the command type as StoredProcedure.
                DataAdapter.SelectCommand = objCommand;

                //Create a new DataSet to hold the records.
                dsAddressInfo = new DataSet();

                //Fill the DataSet 
                DataAdapter.Fill(dsAddressInfo, "dtAddressInfo");

                //Dispose of the DataAdapter.
                DataAdapter.Dispose();
                //Close the connection.
                Connection.Close();

                return dsAddressInfo.Tables["dtAddressInfo"];
            }
            catch (Exception ex)
            {
                dtAddressInfo.Dispose();
                throw new Exception("Error Occurred During Merchant Address Fetching :   " + ex.Message);
            }
        }


		public static DataTable GetPager(string selectList, string tableName, string whereStr, string orderExpression, int pageIndex, int pageSize)
		{
			int rows = 0;

    		SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            //string Constring = System.Configuration.ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
            //SqlConnection Connection = new SqlConnection("Data Source=15.184.218.35;Initial Catalog=OTC_TradingSystem;Persist Security Info=True;User ID=sa;Password=TC0qd8UiEqwP*xWB;Connect Timeout=30000");
            DataTable dtTbl = new DataTable();
            string sqlString = string.Empty;
            string sqlCount = string.Empty;

            string Constring = "Data Source=15.184.218.35;Initial Catalog=OTC_TradingSystem;Persist Security Info=True;User ID=sa;Password=TC0qd8UiEqwP*xWB;Connect Timeout=30000";
            using (SqlConnection objConn = new SqlConnection(Constring))
            {
             
                try
                {
                    MatchCollection mCol = Regex.Matches(selectList, @"top\s+\d{1,}", RegexOptions.IgnoreCase);
                    sqlString = string.Format("select {0} from {1} where {2}", selectList, tableName, whereStr);
                  
                    
                    if (!string.IsNullOrEmpty(orderExpression)) { sqlString += string.Format(" Order by {0}", orderExpression); }
                    if (mCol.Count > 0)
                    {
                        objCommand = new SqlCommand(sqlString, objConn);
                        objConn.Open();
                        objCommand.CommandType = CommandType.Text;
                        sda.SelectCommand = objCommand;
                        sda.Fill(dtTbl);
                        rows = dtTbl.Rows.Count;
                    }
                    else 
                    {
                        sqlCount = string.Format("select count(*) from {0} where {1} ", tableName, whereStr);
                        objConn.Open();
                        objCommand = new SqlCommand(sqlCount, objConn);
                        object obj = objCommand.ExecuteScalar();
                        //object obj = ExecuteScalar(sqlCount);
                        if (obj != null)
                        {
                            rows = Convert.ToInt32(obj);
                        }
                    }

                    
                   if (pageIndex >0 && pageSize > 0)
					{

                        sqlString = string.Empty;
                        sqlString = string.Format("select {0}, Count(*) Over() As TotalCount from {1} where {2} Order by {3} Offset ({4} -1) * {5} Rows Fetch Next {5} Rows Only", selectList, tableName, whereStr, orderExpression, pageIndex, pageSize);

                    }
                    SqlCommand objCmd = new SqlCommand(sqlString, objConn);
                    SqlDataAdapter dapter = new SqlDataAdapter();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandTimeout = 0;
                    dapter.SelectCommand = objCmd;
                    DataSet dsPager = new DataSet();
                   
                    dapter.Fill(dsPager,"dtPager");
                    // DataTable dt = ExecuteDataTable(sqlString, (pageIndex - 1) * pageSize, pageSize);
                    dtTbl = dsPager.Tables["dtPager"];
                    if (dtTbl.Rows.Count > 0)
                    {
                        rows = dtTbl.Rows.Count;
                        //recordCount = rows;
                    }
                     return dtTbl;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    objCommand.Dispose();
                    objConn.Close();
                }
                

            }
            //return dtTbl;
        }


        public static DataTable ExecuteDataTable(string connectionString, string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataSet(connectionString, CommandType.Text, commandText, parms).Tables[0];
        }


        public static DataSet ExecuteDataSet(string connectionString, string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataSet(connectionString, CommandType.Text, commandText, parms);
        }

        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return ExecuteDataSet(connection, commandType, commandText, parms);
            }
        }




        public static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataSet(connection, null, commandType, commandText, parms);
        }

    
        public static DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataSet(transaction.Connection, transaction, commandType, commandText, parms);
        }

  
        private static DataSet ExecuteDataSet(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            SqlCommand command = new SqlCommand();

            PrepareCommand(command, connection, transaction, commandType, commandText, parms);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            if (commandText.IndexOf("@") > 0)
            {
                commandText = commandText.ToLower();
                int index = commandText.IndexOf("where ");
                if (index < 0)
                {
                    index = commandText.IndexOf("\nwhere");
                }
                if (index > 0)
                {
                    ds.ExtendedProperties.Add("SQL", commandText.Substring(0, index - 1));  
                }
                else
                {
                    ds.ExtendedProperties.Add("SQL", commandText);  
                }
            }
            else
            {
                ds.ExtendedProperties.Add("SQL", commandText);  
            }

            foreach (DataTable dt in ds.Tables)
            {
                dt.ExtendedProperties.Add("SQL", ds.ExtendedProperties["SQL"]);
            }

            command.Parameters.Clear();
            return ds;
        }


        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] parms)
        {
            if (connection.State != ConnectionState.Open) connection.Open();

            command.Connection = connection;
            command.CommandTimeout = 0;
           
            command.CommandText = commandText;
           
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
           
            command.CommandType = commandType;
            if (parms != null && parms.Length > 0)
            {
               
                foreach (SqlParameter parameter in parms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                command.Parameters.AddRange(parms);
            }
        }


        public static object ExecuteScalar(string sql)
        {

            string Constring = "Data Source=15.184.218.35;Initial Catalog=OTC_TradingSystem;Persist Security Info=True;User ID=sa;Password=TC0qd8UiEqwP*xWB;Connect Timeout=30000";
            //SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString); 
            SqlConnection Connection = new SqlConnection(Constring);
            Connection.Open();
            SqlCommand cmd = new SqlCommand(sql, Connection);
            try
            {
                cmd.CommandTimeout = 600; //10 Mins
                return cmd.ExecuteScalar();
            }
            finally
            {
                //Connection.Close();
            }
        }


        //public static DataTable ExecuteQuery(SqlCommand command)
        //{
        //    SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString);
        //    try
        //    {
        //        DataSet dsCR = null;

        //        SqlDataAdapter DataAdapter = null;
        //        command.Connection = Connection;
        //        DataAdapter = new SqlDataAdapter(command);
        //        command.CommandTimeout = 600; //10 Mins
        //        //Create a new DataSet to hold the records.
        //        dsCR = new DataSet();

        //        //Fill the DataSet 
        //        DataAdapter.Fill(dsCR);

        //        //Dispose of the DataAdapter.
        //        DataAdapter.Dispose();
        //        //Close the connection.
        //        Connection.Close();

        //        if (dsCR.Tables.Count > 0 && dsCR.Tables[0] != null)
        //            return dsCR.Tables[0];
        //        return null;
        //    }
        //    finally
        //    {
        //        if (Connection.State != ConnectionState.Closed)
        //        {
        //            Connection.Close();
        //        }
        //    }
        //}





       
    }
}

