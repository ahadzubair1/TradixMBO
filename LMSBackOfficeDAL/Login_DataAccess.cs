using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Net.Http;
using LMSBackOfficeDAL;
using LMSBackofficeDAL;
using System.Web;


namespace LMSBackOfficeDAL
{

   

    public static class Login_DataAccess
    {
        static int retLogin = 0;



        public static int GetLoginResponse(string username, string emailAddress, string password, string userType)
        {


            return retLogin;
        }
        private static string connectionString = ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
        public static bool CheckLogin(string username, string password)
        {
            bool loginSuccess = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_CheckMemberLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    Guid newGuid = Guid.NewGuid();
                    // Add parameters
                    var hashPassword = HashUtility.ComputeSHA512Hash(password);
                    command.Parameters.Add("@IN_LoginUserName", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("@IN_LoginPassword", SqlDbType.NVarChar).Value = hashPassword;
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            loginSuccess = Convert.ToBoolean(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }
            return loginSuccess;
        }

        public static void AddLogin(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_AddLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    string userIpAddress = HttpContext.Current.Request.UserHostAddress;
                    Guid newGuid = Guid.NewGuid();
                    // Add parameters
                    var hashPassword = HashUtility.ComputeSHA512Hash(password);
                    command.Parameters.Add("@IN_LoginUserName", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("@IN_LoginPassword", SqlDbType.NVarChar).Value = hashPassword;
                    command.Parameters.Add("@IN_LoginIP", SqlDbType.NVarChar).Value = userIpAddress;
                    command.Parameters.Add("@IN_LoginSource", SqlDbType.NVarChar).Value = "Tradixx Backoffice";
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



        /// <summary>
        /// INSERTION OF VISITORS INFORMATION FROM ANY PAGE
        /// </summary>
        /// <returns></returns>
        public static string GetVisitorInfo()
        {
            string visitorResponse = string.Empty;

            int retVisitor = UtilMethods.GrabVisitorInfo();

            if (retVisitor==1)
            {
                //visitorResponse = "Visitor Response has been Successfully Saved";
                visitorResponse = "Success";
            }
            else
            {
                //visitorResponse = "Error Occurred while Saving the Visitor Response";
                visitorResponse = "Failure";
            }

            return visitorResponse;
        }
    }
}
