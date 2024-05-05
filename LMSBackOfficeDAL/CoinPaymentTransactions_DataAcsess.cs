using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LMSBackOfficeDAL
{
    public class CoinPaymentTransactions_DataAcsess
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;

        public static string AddCoinPaymentTransactions(string memberId,string transactionCode,string cpTransactionId, string transactionType,
                                                        string fromCurrency, string toCurrency, string memberAddress,  string status, decimal amount,
                                                        string redirectUrl, string successRedirectUrl, bool isCompleted)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_AddCoinPaymentTransaction", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@IN_Member_ID", SqlDbType.NVarChar).Value = memberId;
                    command.Parameters.Add("@IN_Transaction_Code", SqlDbType.NVarChar).Value = transactionCode;
                    command.Parameters.Add("@IN_Transaction_Type", SqlDbType.NVarChar).Value = transactionType;
                    command.Parameters.Add("@IN_Transaction_Amount", SqlDbType.Decimal).Value = amount;
                    command.Parameters.Add("@IN_Transaction_SenderAddress", SqlDbType.NVarChar).Value = memberAddress;
                    command.Parameters.Add("@IN_Transaction_Currency", SqlDbType.NVarChar).Value = fromCurrency;
                    command.Parameters.Add("@IN_Transaction_ToCurrency", SqlDbType.NVarChar).Value = toCurrency;
                    command.Parameters.Add("@IN_Transaction_Status", SqlDbType.NVarChar).Value = status;
                    command.Parameters.Add("@IN_Private_Key", SqlDbType.NVarChar).Value = Configurations.PrivateKey;
                    command.Parameters.Add("@IN_Public_Key", SqlDbType.NVarChar).Value = Configurations.PublicKey;
                    command.Parameters.Add("@Is_ProcessCompleted", SqlDbType.Bit).Value = isCompleted;
                   

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                       
                        return "Success";

                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        WriteLog.LogError(ex);
                        return ex.Message;
                    }
                }
            }
        }


        public static void UpdateCoinPaymentTransaction(string transactionId, string transactionOrderId, string transactionHash, int statusCode, string status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_UpdateCoinPaymentTransaction", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        var code = Transactions_DataAcsess.GetTransactionCode(transactionOrderId);
                        command.Parameters.Add("@TransactionCode", SqlDbType.NVarChar).Value = code;
                        command.Parameters.Add("@CPTransactionID", SqlDbType.NVarChar).Value = transactionId;
                       // command.Parameters.Add("@IN_TransactionHash", SqlDbType.NVarChar).Value = transactionHash;
                        command.Parameters.Add("@IN_TransactionStatusCode", SqlDbType.NVarChar).Value = statusCode.ToString();
                        command.Parameters.Add("@IN_Status", SqlDbType.NVarChar).Value = status;
                        connection.Open();

                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception properly
                        WriteLog.LogError(ex);
                    }
                }
            }
        }
    }
}
