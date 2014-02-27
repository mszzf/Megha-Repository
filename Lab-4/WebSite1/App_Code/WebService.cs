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
    public string HelloWorld()
    {
        return "Hello World";
    } 


    [WebMethod]
    public string QueryTable(string name)
    {

        //Declare Connection by passing the connection string from the web config file
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);

        //Open the connection
        conn.Open();

        int age = 0;
        string ageString = "";
        string empName = "";
        string ssn = "";
        SqlCommand cmd = new SqlCommand("Select * From Employee where Name='" + name + "'", conn);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            ageString = "Age " + reader["Age"];
            empName = "Name " + reader["Name"].ToString();
            ssn = "SSN " + reader["SSN"].ToString();
        }
        reader.Close();
        //close the connection
        conn.Close();

        return "info: " + empName + ", " + ageString + ", " + ssn;

    }

    [WebMethod]
    public string InsertTable(string name, int age, string ssn)
    {
        //Declare Connection by passing the connection string from the web config file
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);

        //Open the connection
        conn.Open();

        //Declare the sql command
        SqlCommand cmd = new SqlCommand("Insert into Employee (Name,Age,SSN) values('" + name + "','" + age + "','" + ssn + "')", conn);

        //Execute the insert query
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        //close the connection
        conn.Close();

        return "Success insert";

    }

    [WebMethod]
    public string DeleteTable(string name)
    {
        //Declare Connection by passing the connection string from the web config file
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);

        //Open the connection
        conn.Open();

        //Declare the sql command
        SqlCommand cmd = new SqlCommand("Delete From Employee Where Name= '" + name + "'", conn);

        //Execute the insert query
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        //close the connection
        conn.Close();

        return "Success delete";
    }

}
