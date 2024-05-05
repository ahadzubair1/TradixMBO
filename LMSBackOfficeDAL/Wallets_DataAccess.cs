using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LMSBackOfficeDAL
{
    public static class Wallets_DataAccess
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
        public static void CreateMemberWallets(string memberId)
        {
            bool networkSuccess = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_CreateMemberWallets", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters
                    command.Parameters.Add("@IN_Member_ID", SqlDbType.NVarChar).Value = memberId;
                    
                    try
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            networkSuccess = Convert.ToBoolean(result);
                        }

                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
        public static decimal FetchCdtWalletBalanceForUsername(string username)
        {
            decimal balanceAmount = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[USP_FetchCdtWalletBalance]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IN_Username", SqlDbType.NVarChar).Value = username;

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            balanceAmount = Convert.ToDecimal(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return balanceAmount;
        }

        public static decimal FetchBonusWalletBalanceForUsername(string username)
        {
            decimal balanceAmount = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[USP_FetchBonusWalletBalance]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IN_Username", SqlDbType.NVarChar).Value = username;

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            balanceAmount = Convert.ToDecimal(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return balanceAmount;
        }

    }
}
