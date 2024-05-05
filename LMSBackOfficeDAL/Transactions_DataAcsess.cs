using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LMSBackOfficeDAL
{
    public class Transactions_DataAcsess
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;

        public static DataTable GetAllTransaction(string memberId, string searchString = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_GetAllTransactions", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@IN_Member_ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(memberId);
                    command.Parameters.Add("@IN_SearchText", SqlDbType.NVarChar).Value = searchString;
                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        WriteLog.LogError(ex);
                    }                 
                }
            }

            return dt;
        }

        public static string AddTransactions(string memberId, string orderId,
                                            string transactionType, string fromCurrency, string toCurrency, string memberAddress, string toAddress, 
                                            string status, decimal amount)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_AddTransaction", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    Random rand = new Random();
                    int randomNumber = rand.Next(100000, 999999); // Adjust range as needed
                    string transactionCode = "TR_" + randomNumber.ToString("D6");

                    command.Parameters.Add("@IN_Transaction_Code", SqlDbType.NVarChar).Value = transactionCode;
                    command.Parameters.Add("@IN_Member_ID", SqlDbType.NVarChar).Value = memberId;
                    command.Parameters.Add("@IN_Transaction_OrderID", SqlDbType.NVarChar).Value = orderId;
                    command.Parameters.Add("@IN_Transaction_Type", SqlDbType.NVarChar).Value = transactionType;
                    command.Parameters.Add("@IN_Transaction_Amount", SqlDbType.Decimal).Value = amount;
                    command.Parameters.Add("@IN_Transaction_Currency", SqlDbType.NVarChar).Value = fromCurrency;
                    command.Parameters.Add("@IN_Transaction_SenderAddress", SqlDbType.NVarChar).Value = memberAddress;
                    command.Parameters.Add("@IN_Transaction_ReceiverAddress", SqlDbType.NVarChar).Value = toAddress;                    
                    command.Parameters.Add("@IN_Transaction_Status", SqlDbType.NVarChar).Value = status;
                    SqlParameter outParameter = command.Parameters.Add("@OUT_TransactionID", SqlDbType.NVarChar, 250); // Assuming 36 is the maximum length of a GUID represented as a string
                    outParameter.Direction = ParameterDirection.Output;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        if (outParameter.Value != DBNull.Value && outParameter.Value != null)
                        {
                            //UtilMethods.SendEmail(name,"support@tradiix.com", phone);
                            var transactionId = outParameter.Value.ToString();
                           CoinPaymentTransactions_DataAcsess.AddCoinPaymentTransactions(memberId,transactionCode, null, transactionType,
                                                                    fromCurrency, toCurrency, memberAddress, status, amount, null, null, false);     
                        }
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



        public static string UpdateTransaction(string memberId, string transactionOrderId, string fee, string status)
        {
            string transactionCode = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_UpdateTransaction", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.Parameters.Add("@IN_MemberId", SqlDbType.UniqueIdentifier).Value = Guid.Parse(memberId);
                        command.Parameters.Add("@IN_TransactionOrderID", SqlDbType.NVarChar).Value = transactionOrderId;
                        command.Parameters.Add("@IN_Status", SqlDbType.NVarChar).Value = status;
                        command.Parameters.Add("@IN_Fee", SqlDbType.NVarChar).Value = fee;
                        SqlParameter outParameter = command.Parameters.Add("@OUT_TransactionCode", SqlDbType.NVarChar, 250); // Assuming 36 is the maximum length of a GUID represented as a string
                        outParameter.Direction = ParameterDirection.Output;


                        connection.Open();

                        command.ExecuteNonQuery();
                        if (outParameter.Value != DBNull.Value && outParameter.Value != null)
                        {
                            transactionCode = outParameter.Value.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteLog.LogError(ex);
                    }
                }
            }
            return transactionCode;
        }

        public static string GetTransactionCode(string transactionOrderId)
        {
            string transactionCode = string.Empty; ;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select Transaction_Code from Transactions where Transaction_OrderID='" + transactionOrderId + "'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;

                    try
                    {
                        connection.Open();
                        transactionCode = Convert.ToString(command.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        WriteLog.LogError(ex);
                    }
                }
            }

            return transactionCode;
        }

        public static bool CheckTopupAlreadyRequested(string memberId)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_CheckTopupAlreadyExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add("@IN_MemberId", SqlDbType.UniqueIdentifier, 50).Value = Guid.Parse(memberId);
                    command.Parameters.Add("@exists", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Retrieve the output parameter value
                    exists = Convert.ToBoolean(command.Parameters["@exists"].Value);
                }
            }

            return exists;
        }
    }
}
