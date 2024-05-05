using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Net.Http;

namespace LMSBackOfficeDAL
{
	public class DirectBonus_DataAccess
	{
        private static string connectionString = ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
        public static void InsertOrUpdateDirectBonus(string memberId,double amount)
        {
             using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 using (SqlCommand command = new SqlCommand("[USP_InsertOrUpdateDirectBonus]", connection))
                 {
                     command.CommandType = CommandType.StoredProcedure;
                     // Add parameters
                     decimal bonusAmount = (decimal)amount * 0.1m;
                     command.Parameters.Add("@IN_Member_ID", SqlDbType.NVarChar).Value = memberId;
                     command.Parameters.Add("@IN_Amount", SqlDbType.Decimal).Value = bonusAmount;
                    
                    // command.Parameters.Add("@IN_Member_ReferralCode", SqlDbType.NVarChar).Value = refCode;

                     try
                     {
                         connection.Open();
                         command.ExecuteNonQuery();
                     }
                     catch (Exception ex)
                     {
                         // Handle exception
                         Console.WriteLine("Error: " + ex.Message);
                     }
                 }
             }
        }

        public static decimal FetchDirectBonusForUsername(string username)
        {
            decimal bonusAmount = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[USP_FetchDirectBonusForUsername]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IN_Username", SqlDbType.NVarChar).Value = username;

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            bonusAmount = Convert.ToDecimal(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return bonusAmount;
        }







    }
}
