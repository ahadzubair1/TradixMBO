using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Net.Http;
using System.Collections.Generic;
using System.Web.Script.Serialization;


namespace LMSBackOfficeDAL
{
	public class Countries_DataAccess
	{

        private static string connectionString = ConfigurationManager.ConnectionStrings["LMSBackOfficeConnectionString"].ConnectionString;
        public static List<Country> GetAllCountries()
        {
            List<Country> allCountriesDB = new List<Country>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SelectAllCountries", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Country country = new Country
                                {
                                    CountryID = reader["Country_ID"].ToString(),
                                    CountryName = reader["Country_Name"].ToString()
                                };
                                allCountriesDB.Add(country);
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

            return allCountriesDB;
        }

        // Nested class for country representation
        public class Country
        {
            public string CountryID { get; set; }
            public string CountryName { get; set; }
        }

    }
}
