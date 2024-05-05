using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Net.Http;
using LMSBackOfficeDAL;


namespace LMSBackOfficeDAL
{
	public static class Setup_DataAccess
	{
        private static string connectionString = ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
        /// <summary>
        /// METHOD TO GET ALL THE BONUS TYPES
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllBonusTypes()
		{
			DataSet dsBonusTypes = null;
			try
			{
                //var Constring = new System.Configuration.ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
                //var  Constring = new SqlConnection(ConfigurationSettings.AppSettings["LMSBackOfficeConnectionString"]);
                //var Constring = new SqlConnection(ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString);
                SqlConnection Connection = new SqlConnection(connectionString);
				Connection.Open();
				SqlDataAdapter DataAdapter = new SqlDataAdapter("USP_GetAllBonusTypes", Connection);

				//Set the command type as StoredProcedure.
				DataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
				DataAdapter.SelectCommand.CommandTimeout = 0;

                //Create a new DataSet to hold the records.
                dsBonusTypes = new DataSet();

				//Fill the DataSet 
				DataAdapter.Fill(dsBonusTypes, "dtBonusTypes");

				//Dispose of the DataAdapter.
				DataAdapter.Dispose();
				//Close the connection.
				Connection.Close();

				return dsBonusTypes.Tables["dtBonusTypes"];
			}
			catch (Exception ex)
			{
                dsBonusTypes.Dispose();
				throw new Exception("Error Occrred During BonusTypes-Fetching :   " + ex.Message);
			}
		}


		/// <summary>
		/// METHOD TO ADD THE INORDERS
		/// </summary>
		/// <param name="objInOrders"></param>
		/// <returns></returns>
		//public static bool AddInOrders(InOrders objInOrders)
		//{
		
		//    SqlConnection Connection = new SqlConnection("Data Source=15.184.218.35;Initial Catalog=OTC_TradingSystem;Persist Security Info=True;User ID=sa;Password=TC0qd8UiEqwP*xWB;Connect Timeout=30000");
		//	Connection.Open();
		//	SqlDataAdapter DataAdapter = new SqlDataAdapter("USP_InsertInOrders", Connection);


		//	using (SqlConnection objConn = new SqlConnection(connString))
		//	{
		//		objConn.Open();
		//		try
		//		{
		//			if (objLocationBO.LocationID != null)
		//			{
		//				objCommand = new SqlCommand("CRE_Update_Location", objConn);
		//				objCommand.CommandType = CommandType.StoredProcedure;
		//				objCommand.Parameters.Add(new SqlParameter("@LocationID", objLocationBO.LocationID));
		//			}
		//			else
		//			{
		//				objCommand = new SqlCommand("CRE_Insert_Location", objConn);
		//				objCommand.CommandType = CommandType.StoredProcedure;
		//				objCommand.Parameters.Add(new SqlParameter("@LocationCreationDate", objLocationBO.LocationCreationDate));
		//			}
		//			objCommand.Parameters.Add(new SqlParameter("@LocationName", objLocationBO.LocationName));
		//			objCommand.Parameters.Add(new SqlParameter("@LocationCityID", objLocationBO.LocationCityID));
		//			objCommand.Parameters.Add(new SqlParameter("@LocationTrecTransferFee", objLocationBO.LocationTrecTransferFee));
		//			objCommand.Parameters.Add(new SqlParameter("@LocationAnnualMembershipFee", objLocationBO.LocationAnnualMembershipFee));
		//			objCommand.Parameters.Add(new SqlParameter("@LocationHARTransferFee", objLocationBO.LocationHARTransferFee));
		//			objCommand.Parameters.Add(new SqlParameter("@LocationProcessingFee", objLocationBO.LocationProcessingFee));
		//			objCommand.Parameters.Add(new SqlParameter("@LocationTotalFee", objLocationBO.LocationTotalFee));
		//			objCommand.Parameters.Add(new SqlParameter("@LocationIsActive", objLocationBO.LocationIsActive));
		//			objCommand.Parameters.Add(new SqlParameter("@LocationUpdateDate", objLocationBO.LocationUpdateDate));

		//			retLocationID = Convert.ToInt32(objCommand.ExecuteNonQuery());

		//		}
		//		catch (Exception ex)
		//		{
		//			throw new Exception(ex.Message);
		//		}
		//		finally
		//		{
		//			objCommand.Dispose();
		//			objConn.Close();
		//		}
		//		if (retLocationID != 0)

		//			return retLocationID;
		//		else
		//			return -1;

		//	}


		//	//-------------- HERE---------//
		//	bool addresponse = false;
		//	try
		//	{

		//		SqlConnection Connection = new SqlConnection("Data Source=15.184.218.35;Initial Catalog=OTC_TradingSystem;Persist Security Info=True;User ID=sa;Password=TC0qd8UiEqwP*xWB;Connect Timeout=30000");
		//		Connection.Open();
		//		SqlDataAdapter DataAdapter = new SqlDataAdapter("USP_InsertInOrders", Connection);

		//		//Set the command type as StoredProcedure.
		//		DataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
		//		DataAdapter.SelectCommand.CommandTimeout = 0;

		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Inner_FlowNumber", SqlDbType.NVarChar));
		//		DataAdapter.SelectCommand.Parameters["@Inner_FlowNumber"].Value = objInOrders.Inner_FlowNumber;

		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Order_Quantity", SqlDbType.Decimal));
		//		DataAdapter.SelectCommand.Parameters["@Order_Quantity"].Value = objInOrders.Order_Quantity;

		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Order_USDTAmount", SqlDbType.Decimal));
		//		DataAdapter.SelectCommand.Parameters["@Order_USDTAmount"].Value = objInOrders.Order_USDTAmount;

		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Order_USDTAmount", SqlDbType.Decimal));
		//		DataAdapter.SelectCommand.Parameters["@Order_USDTAmount"].Value = objInOrders.Order_USDTAmount;

		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Order_SourceCurrency", SqlDbType.NVarChar));
		//		DataAdapter.SelectCommand.Parameters["@Order_SourceCurrency"].Value = objInOrders.Order_SourceCurrency;

		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Order_DestinationCurrency", SqlDbType.NVarChar));
		//		DataAdapter.SelectCommand.Parameters["@Order_DestinationCurrency"].Value = objInOrders.Order_DestinationCurrency;

		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Order_Expense", SqlDbType.Decimal));
		//		DataAdapter.SelectCommand.Parameters["@Order_Expense"].Value = objInOrders.Order_Expense;


		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Order_ExchangeRate", SqlDbType.Decimal));
		//		DataAdapter.SelectCommand.Parameters["@Order_ExchangeRate"].Value = objInOrders.Order_ExchangeRate;

		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Order_FinalUSDTAmount", SqlDbType.Decimal));
		//		DataAdapter.SelectCommand.Parameters["@Order_FinalUSDTAmount"].Value = objInOrders.Order_FinalUSDTAmount;

		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Order_Status", SqlDbType.NVarChar));
		//		DataAdapter.SelectCommand.Parameters["@Order_Status"].Value = objInOrders.Order_Status;

		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@IP_Address", SqlDbType.NVarChar));
		//		DataAdapter.SelectCommand.Parameters["@IP_Address"].Value = objInOrders.IP_Address;


		//		DataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@InOrder_Desc", SqlDbType.NVarChar));
		//		DataAdapter.SelectCommand.Parameters["@InOrder_Desc"].Value = objInOrders.InOrder_Desc;
		//		//Create a new DataSet to hold the records.
		//		retLocationID = Convert.ToInt32(objCommand.ExecuteNonQuery());


		//		//Dispose of the DataAdapter.
		//		DataAdapter.Dispose();
		//		//Close the connection.
		//		Connection.Close();
		//		addresponse = true;
		//		return addresponse;
		//		//return dsInOrders.Tables["dtInOrders"];
		//	}
		//	catch (Exception ex)
		//	{
		//		//dsInOrders.Dispose();
		//		throw new Exception("Error Occrred During IN-Order :   " + ex.Message);
		//	}

		//	return addresponse;
		//}




	}
}
