using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string queryTable(string user_name1)
    {

        //Declare Connection by passing the connection string from the web config file
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);

        //Open the connection
        conn.Open();

        //string user_name1 = "";
        string email = "";
        string password  = "";
        string address = "";
        string phonenumber = "";
        SqlCommand cmd = new SqlCommand("Select * From users where user_name1='" + user_name1 + "'", conn);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            user_name1 = user_name1 + ";" + reader["user_name1"].ToString();
            email = email + ";" + reader["email"].ToString();
            password = password + ";" + reader["password"].ToString();
            address = address + ";" + reader["address"].ToString();
            phonenumber = phonenumber + ";" + reader["phonenumber"];
        }
        reader.Close();
        //close the connection
        conn.Close();

        return "info: " + user_name1 + "," + email + "," + password + "," + address + "," + phonenumber;

    }

    [WebMethod]
    public string InsertTable(string user_name1, string email, string password, string address, string phonenumber)
    {
        //Declare Connection by passing the connection string from the web config file
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);

        //Open the connection
        conn.Open();

        //Declare the sql command
        SqlCommand cmd = new SqlCommand
            ("Insert into users(user_name1,email,password,address,phonenumber)values('" + user_name1 + "','" + email + "','" + password + "','" + address + "','" + phonenumber + "')", conn);

        //Execute the insert query
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        //close the connection
        conn.Close();

        return "Success insert";

    }

}
