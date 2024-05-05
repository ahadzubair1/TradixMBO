using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Helpers;
using System.Xml.Linq;
using LMSBackofficeDAL;
using LMSBackOfficeDAL.Model;

namespace LMSBackOfficeDAL
{
    public class Members_DataAccess
    {
        /* MAJOR API REQUIRED INSIDE CHECKOUT SYSTEM FOR
		  IN-ORDER, 
		  OUT-ORDER, 
		  OUT-TRANSACTION, 
		  ADDRESS-SYNCING, 
		  REBATE, BALANCE-HISTORY 
		  CALL-BACK (timer is callout/callback)
		  Utility-Class for Redis
		  From Redis to Method till MQ
		 */

        /// <summary>
        /// METHOD TO GET ALL THE MEMBERS INORDERS REQUESTS
        /// </summary>
        /// <returns></returns>
        //public static DataTable GetAllMembers()
        //{
        //	DataSet dsInOrders = null;
        //	try
        //	{
        //    	//var Constring = new System.Configuration.ConfigurationManager.ConnectionStrings["MerchantCheckOutConnectionString"].ConnectionString;
        //		//var  Constring = new SqlConnection(ConfigurationSettings.AppSettings["MerchantCheckOutConnectionString"]);
        //		//var Constring = new SqlConnection(ConfigurationManager.ConnectionStrings["MerchantCheckOutConnectionString"].ConnectionString);
        //		SqlConnection Connection = new SqlConnection("Data Source=15.184.218.35;Initial Catalog=OTC_TradingSystem;Persist Security Info=True;User ID=sa;Password=TC0qd8UiEqwP*xWB;Connect Timeout=30000");
        //		Connection.Open();
        //		SqlDataAdapter DataAdapter = new SqlDataAdapter("USP_FetchInOrders", Connection);

        //		//Set the command type as StoredProcedure.
        //		DataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //		DataAdapter.SelectCommand.CommandTimeout = 0;

        //		//Create a new DataSet to hold the records.
        //		dsInOrders = new DataSet();

        //		//Fill the DataSet 
        //		DataAdapter.Fill(dsInOrders, "dtInOrders");

        //		//Dispose of the DataAdapter.
        //		DataAdapter.Dispose();
        //		//Close the connection.
        //		Connection.Close();

        //		return dsInOrders.Tables["dtInOrders"];
        //	}
        //	catch (Exception ex)
        //	{
        //		dsInOrders.Dispose();
        //		throw new Exception("Error Occrred During IN-Order :   " + ex.Message);
        //	}
        //}

        private static string connectionString = ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
        public static string AddMember(string name, string username, string email, string password, string referredByParentId, int position, string phone, string country, string currentDomainUrl)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_AddMember", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters
                    var hashPassword = HashUtility.ComputeSHA512Hash(password);
                    Random rand = new Random();
                    int randomNumber = rand.Next(100000, 999999); // Adjust range as needed
                    string memberCode = "MEM_" + randomNumber.ToString("D6");
                    command.Parameters.Add("@IN_Member_FullName", SqlDbType.NVarChar).Value = name;
                    command.Parameters.Add("@IN_Member_Email", SqlDbType.NVarChar).Value = email;
                    command.Parameters.Add("@IN_Member_Password", SqlDbType.NVarChar).Value = hashPassword;
                    command.Parameters.Add("@IN_Member_Code", SqlDbType.NVarChar).Value = memberCode;
                    command.Parameters.Add("@IN_Member_Mobile", SqlDbType.NVarChar).Value = phone;
                    command.Parameters.Add("@IN_Member_CountryOfOrigin", SqlDbType.NVarChar).Value = country;
                    command.Parameters.Add("@IN_Member_UserName", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("@IN_Member_RefferredBy", SqlDbType.NVarChar).Value = referredByParentId;
                    SqlParameter outParameter = command.Parameters.Add("@OUT_Member_ID", SqlDbType.NVarChar, 36); // Assuming 36 is the maximum length of a GUID represented as a string
                    outParameter.Direction = ParameterDirection.Output;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        if (outParameter.Value != DBNull.Value && outParameter.Value != null)
                        {

                            UtilMethods.SendEmailNew(email, name, memberCode, currentDomainUrl);
                            var memberId = outParameter.Value.ToString();
                            string codeLeft = GenerateRandomAlphaNumericString(18)+"_L";
                            string codeRight = GenerateRandomAlphaNumericString(18)+"_R";
                            ReferralCodes_DataAccess.AddMemberReferralCodes(memberId, 1, codeLeft);
                            ReferralCodes_DataAccess.AddMemberReferralCodes(memberId, 2, codeRight);
                            Network_DataAccess.AddMemberNetwork(memberId, referredByParentId, position);
                            Wallets_DataAccess.CreateMemberWallets(memberId);
                        }
                        return "Success";

                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                        return ex.Message;
                    }
                }
            }
        }
        private static Random random = new Random();
        private static string GenerateRandomAlphaNumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }
            return stringBuilder.ToString();
        }
        public static bool CheckUsernameExists(string username)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_CheckUsernameExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;
                    command.Parameters.Add("@exists", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Retrieve the output parameter value
                    exists = Convert.ToBoolean(command.Parameters["@exists"].Value);
                }
            }

            return exists;
        }

        public static bool CheckEmailExists(string email)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_CheckEmailExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = email;
                    command.Parameters.Add("@exists", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Retrieve the output parameter value
                    exists = Convert.ToBoolean(command.Parameters["@exists"].Value);
                }
            }

            return exists;
        }

        public static bool ActivateMember(string memCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("USP_UpdateMemberStatus", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IN_MemCode", memCode);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    // Log or throw the exception as needed
                    // Example: throw;
                    return false;
                }
            }
        }

        public static string AddMembershipPurchase(string MemberID, string MembershipID, string MembershipName, decimal amount, decimal activation_fee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_AddMembershipPurchase", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IN_Member_ID", SqlDbType.NVarChar).Value = MemberID;
                    command.Parameters.Add("@IN_Membership_ID", SqlDbType.NVarChar).Value = MembershipID;
                    command.Parameters.Add("@IN_Membership_Name", SqlDbType.NVarChar).Value = MembershipName;
                    command.Parameters.Add("@IN_Membership_Amount", SqlDbType.NVarChar).Value = amount;
                    command.Parameters.Add("@IN_Membership_ActivationFee", SqlDbType.NVarChar).Value = activation_fee;
                    SqlParameter outParameter = command.Parameters.Add("@OUT_Member_ID", SqlDbType.NVarChar, 36); // Assuming 36 is the maximum length of a GUID represented as a string
                    outParameter.Direction = ParameterDirection.Output;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        return "Success";


                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                        return ex.Message;
                    }
                }
            }
        }

        public static DataTable GetMemberDetailsByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("[USP_GetMemberByEmail]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IN_Email", SqlDbType.NVarChar).Value = email;

                    try
                    {
                        connection.Open();
                        DataTable resultTable = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(resultTable);
                        }
                        return resultTable;
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                        return null; // Or throw an exception
                    }
                }
            }
        }

        public static bool UpdateMemberPassword(string memCode,string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var hashPassword = HashUtility.ComputeSHA512Hash(password);
                SqlCommand command = new SqlCommand("[USP_UpdateMemberPassword]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IN_MemCode", SqlDbType.NVarChar, 50).Value = memCode;
                command.Parameters.Add("@IN_Password", SqlDbType.NVarChar, 250).Value = hashPassword;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    // Log or throw the exception as needed
                    // Example: throw;
                    return false;
                }
            }
        }


        public static MemberInfo GetMemberInfo(string userName)
        {
            MemberInfo member = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_GetMemberByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.Parameters.Add("@IN_UserName", SqlDbType.NVarChar).Value = userName;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                member = new MemberInfo
                                {
                                    Id = reader["Member_ID"].ToString(),
                                    MemberFullName = reader["Member_FullName"].ToString(),
                                    UserName = reader["Member_UserName"].ToString(),
                                    MemberCode = reader["Member_Code"].ToString(),
                                    Email = reader["Member_Email"].ToString(),
                                    MemberAddress = reader["Wallet_Address"].ToString(),
                                    MemberWalletBalance = reader["Wallet_Balance"].ToString(),
                                    MemberWalletCurrency = reader["Wallet_Currency"].ToString(),
                                    Sponsor = reader["Sponsor"].ToString(),
                                    Country = reader["MemberCountry"].ToString(),
                                    MembershipName= reader["Membership_Name"].ToString(),
                                    MemberRank= reader["Member_RankDesc"].ToString(),
                                    CountryId = reader["CountryID"].ToString(),
                                    CreatedDate= reader["Created_Date"].ToString()
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception properly
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return member;
        }

        public static DataTable GetReferrelsByMemberId(string memberId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_GetReferrelsByMemberId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@IN_Member_ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(memberId);
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
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return dt;
        }

        public static bool IsMembershipExpired(string memberId)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_CheckMembershipExpired", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add("@MemberID", SqlDbType.NVarChar, 50).Value = memberId;
                    command.Parameters.Add("@Expired", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Retrieve the output parameter value
                    exists = Convert.ToBoolean(command.Parameters["@Expired"].Value);
                }
            }

            return exists;
        }

        public static DataTable GetAllTreeMembersByMemberId(string memberId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetNodesRecursively_tradiix", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@GenesisNode", SqlDbType.NVarChar).Value = (memberId);
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
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return dt;
        }

    }
}
