using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;

namespace SoapService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        //  public string HelloWorld()
        //  {
        //      return "Hello World";
        //  }
        public string getLocation()
        {
            return "39.034411,-94.572049";
        }

        [WebMethod]
        public string GetAddress1()
        {
            return "Address 1 = 15090 W 119th St, Olathe, KS 66062";
        }

        [WebMethod]
        public string GetAddress2()
        {
            return "Address 2 = 6750 W 95th St, Overland Park, KS 66212";
        }

        [WebMethod]
        public List<Review> queryTable()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbReview"].ConnectionString);
            conn.Open();

            string age = "";
            string name = "";
            string city = "";
            string review = "";
            var reviews = new List<Review>();

            SqlCommand cmd = new SqlCommand("Select * From [Reviews Table]", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var reviewItem = new Review();
                reviewItem.Age = "" + reader["age"];
                reviewItem.Name = "" + reader["name"].ToString();
                reviewItem.City = "" + reader["city"].ToString();
                reviewItem.ReviewText = "" + reader["reviews"].ToString();

                reviews.Add(reviewItem);
            }
            reader.Close();
            conn.Close();
            //return "info:" + name + "," + age + "," + city + "," + review;
            return reviews;
        }

        [WebMethod]
        public string InsertReview(string name, string age, string city, string reviewText)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbReview"].ConnectionString);
            conn.Open();

            //Declare the sql command
            SqlCommand cmd = new SqlCommand
                ("Insert into [Reviews Table](name,age,city,reviews)values('" + name + "','" + age + "','" + city + "','" + reviewText + "')", conn);

            //Execute the insert query
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            //close the connection
            conn.Close();
            
            return "Review successfully added";
        }

        [WebMethod]
        public string RemoveReview(string reviewName)
        {
            //Declare Connection by passing the connection string from the web config file
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbReview"].ConnectionString);

            //Open the connection
            conn.Open();

            //Declare the sql command
            SqlCommand cmd = new SqlCommand("delete from [Reviews Table] where name= '" + reviewName + "'", conn);

            //Execute the insert query
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            //close the connection
            conn.Close();

            return "Review successfully deleted";
        }


        [WebMethod]
        public string getCityNotes(string City)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["neighbourhoodNotes"].ConnectionString);
            conn.Open();

            string cityNotes = "";
            
            SqlCommand cmd = new SqlCommand("Select * From [CityNotes] where cityname = @city", conn);
            SqlParameter cityParam = new SqlParameter("city", City);
            cmd.Parameters.Add(cityParam);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cityNotes += reader["citynotes"];
            }
            reader.Close();
            conn.Close();
            //return "info:" + name + "," + age + "," + city + "," + review;
            return cityNotes;
        }
    }

    public class Review
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string City { get; set; }
        public string ReviewText { get; set; }
    }

}
