using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using LMSBackOfficeDAL.Model;
using LMSBackofficeDAL;
using System.Web.Helpers;
using System.Xml.Linq;

namespace LMSBackOfficeDAL
{
    public static class SupportTickets_DataAccess
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
        public static DataTable GetAllTickets(string memberId, string searchString = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_GetAllTickets", connection))
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

        public static DataTable GetTicketTypes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_GetAllTicketTypes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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

        public static int AddTicket(string memberId,string username, string email,string title, string ticketType,
                                     string Description, string priority)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_AddTicket", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    Random rand = new Random();
                    int randomNumber = rand.Next(100000, 999999); // Adjust range as needed
                    string ticketCode = "TK_" + randomNumber.ToString("D6");

                    command.Parameters.Add("@IN_Member_ID", SqlDbType.NVarChar).Value = memberId;
                    command.Parameters.Add("@IN_Title", SqlDbType.NVarChar).Value = title;
                    command.Parameters.Add("@IN_TicketType", SqlDbType.NVarChar).Value = ticketType;
                    command.Parameters.Add("@IN_TicketCode", SqlDbType.NVarChar).Value = ticketCode;
                    command.Parameters.Add("@IN_TicketDescription", SqlDbType.NVarChar).Value = Description;
                    command.Parameters.Add("@IN_TicketPriority", SqlDbType.NVarChar).Value = priority;
                    command.Parameters.Add("@IN_TicketStatus", SqlDbType.NVarChar).Value = TicketStatus.Pending;
                    command.Parameters.Add("@IN_TicketOwner", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("@IN_TicketIsAttachment", SqlDbType.NVarChar).Value = false;
                    SqlParameter outParameter = command.Parameters.Add("@OUT_TicketID", SqlDbType.NVarChar, 250); // Assuming 36 is the maximum length of a GUID represented as a string
                    outParameter.Direction = ParameterDirection.Output;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        if (outParameter.Value != DBNull.Value && outParameter.Value != null)
                        {
                            //UtilMethods.SendEmail(name,"support@tradiix.com", phone);
                            var ticketId = outParameter.Value.ToString();
                        }
                        SendEmail(email, username, ticketCode, ticketType);
                        return 1;

                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        WriteLog.LogError(ex);
                        return 0;
                    }
                }
            }
        }

        public static void SendEmail(string email, string sender, string ticketCode, string title)
        {
            var body = $"<p>Dear {sender}</p>, <p>We receive your query and your ticket no is {ticketCode}. We will resolve this issue as soon as we possible</p>";
            body += "<p>Regards,</p><p>Tradixx</p>";

            UtilMethods.SendEmail(email, title, body);
        }
    }
}
