using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Net.Http;
using System.Text;

namespace LMSBackOfficeDAL
{
	public class Network_DataAccess
	{
        private static string connectionString = ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
        public static bool AddMemberNetwork(string memberId, string referredByParentId,int Position)
        {
            bool networkSuccess = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_AddMemberNetwork", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters
                    command.Parameters.Add("@IN_Member_ID", SqlDbType.NVarChar).Value = memberId;
                    command.Parameters.Add("@IN_ReferredBy", SqlDbType.NVarChar).Value = referredByParentId;
                    command.Parameters.Add("@IN_Position", SqlDbType.NVarChar).Value = Position;

                    
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
                        return false;
                    }
                }
            }
            return networkSuccess;
        }


        public static DataTable GetNetworkDetails(string memberId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_GetNetworkDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IN_Member_ID", SqlDbType.NVarChar).Value = memberId;
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
        public static DataTable GetNetworkFarNode(string memberId)
        {
            DataTable networkDetails = GetNetworkDetails(memberId);
            if (networkDetails != null && networkDetails.Rows.Count > 0)
            {
                DataRow NetworkRow = networkDetails.Rows[0];
                string Network_ID = NetworkRow["Network_Id"].ToString();
                string Referred_By = NetworkRow["Referred_By"].ToString();
                string Position = NetworkRow["Position"].ToString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("USP_GetUserNetworkFarNode", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@In_Member_ID", SqlDbType.NVarChar).Value = Referred_By;
                        command.Parameters.Add("@Position", SqlDbType.Int).Value = Position;
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
                            return null;
                                 // Or throw an exception
                        }
                    }
                }
            }
            else
            {
                return null;
            }
                
        }


        public static DataTable GetFarNodeByMemberPosition(string memberId, Int32 position)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_GetUserNetworkFarNode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IN_Member_ID", SqlDbType.NVarChar).Value = memberId;
                    command.Parameters.Add("@Position", SqlDbType.Int).Value = position;
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
            public static void AssignNetworkParent(string memberId)
        {
            DataTable FarNodeDetails = GetNetworkFarNode(memberId);
            if (FarNodeDetails != null && FarNodeDetails.Rows.Count > 0)
            {
                DataRow FarNodeRow = FarNodeDetails.Rows[0];
                string ParentId = FarNodeRow["Member_ID"].ToString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("USP_AssignNetworkParent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@IN_Member_ID", SqlDbType.NVarChar).Value = memberId;
                        command.Parameters.Add("@IN_Parent_ID", SqlDbType.NVarChar).Value = ParentId;
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
            
        }

    }
}
