using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;

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
    public string GetRoute(string routeName)
    {

        //Declare Connection by passing the connection string from the web config file
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);

        //Open the connection
        conn.Open();

        List<Route> routes = new List<Route>();
        SqlCommand cmd = new SqlCommand("Select * From Places where routeName = '"+routeName+"' order by routeId, whenCreated", conn);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Place place = new Place();
            place.Id = (int)reader["placeId"];
            place.Name = reader["placeName"].ToString().Trim();
            place.Latitude = reader["placeLatitude"].ToString().Trim();
            place.Longitude = reader["placeLongitude"].ToString().Trim();
            place.ImageUrl = reader["imageURL"].ToString().Trim();
            place.AdditionalInfo = reader["additionalInfo"].ToString().Trim();
            place.WhenCreated = ((String)reader["whenCreated"]).Trim();

            Route route = new Route();
            route.Id = (int)reader["routeId"];
            route.Name = reader["routeName"].ToString().Trim();
            route.Price = (int)reader["routePrice"];
            route.Rating = (int)reader["routeRating"];

            if (routes.Contains(route))
            {
                route = routes[routes.IndexOf(route)];

            }
            else
            {
                routes.Add(route);
            }
            route.Places.Add(place);
        }
        reader.Close();
        //close the connection
        conn.Close();



        return JSONHelper.ToJSON(routes);

    }
    [WebMethod]
    public string GetRoutes()
    {

        //Declare Connection by passing the connection string from the web config file
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);

        //Open the connection
        conn.Open();

        List<Route> routes = new List<Route>();
        SqlCommand cmd = new SqlCommand("Select * From Places order by routeId, whenCreated", conn);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Place place = new Place();
            place.Id = (int)reader["placeId"];
            place.Name = reader["placeName"].ToString().Trim();
            place.Latitude = reader["placeLatitude"].ToString().Trim();
            place.Longitude = reader["placeLongitude"].ToString().Trim();
            place.ImageUrl = reader["imageURL"].ToString().Trim();
            place.AdditionalInfo = reader["additionalInfo"].ToString().Trim();
            place.WhenCreated = ((String)reader["whenCreated"]).Trim();

            Route route = new Route();
            route.Id = (int)reader["routeId"];
            route.Name = reader["routeName"].ToString().Trim();
            route.Price = (int)reader["routePrice"];
            route.Rating = (int)reader["routeRating"];

            if (routes.Contains(route))
            {
                route = routes[routes.IndexOf(route)];

            }
            else
            {
                routes.Add(route);
            }
            route.Places.Add(place);
        }
        reader.Close();
        //close the connection
        conn.Close();



        return JSONHelper.ToJSON(routes);

    }

   

    [WebMethod]
    public string insertPlaces(string routeName, int routeRating, int routePrice, string placeName, string latitude, string longitude, string imageUrl, string additionalInfo, string createdBy)
    {
        //Declare Connection by passing the connection string from the web config file
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);

        //Open the connection
        conn.Open();

        SqlCommand cmd = new SqlCommand("Select top 1 routeId From Places where routeName='"+routeName+"'", conn);
        SqlDataReader reader = cmd.ExecuteReader();


        int routeId = 0;
        while (reader.Read())
        {
            routeId = (int)reader["routeId"];
        }


        reader.Close();

        cmd.Dispose();

        if (routeId == 0)
        {
            cmd = new SqlCommand("Select max(routeId) as MaxId From Places ", conn);
            reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                routeId = (int)reader["MaxId"];
                routeId++;
            }


            reader.Close();

            cmd.Dispose(); 
        
        
        }

        var currentDate = DateTime.Now.Ticks;

        //Declare the sql command
        cmd = new SqlCommand("Insert into Places (placeName,placeLatitude, placeLongitude, routeId,routeName,routePrice,routeRating,imageURL,additionalInfo,whenCreated,createdBy)values('" + placeName + "','" + latitude + "','" + longitude + "','" + routeId + "','" + routeName + "','" + routePrice + "','" + routeRating + "','" + imageUrl + "','" + additionalInfo + "','" + currentDate + "','" + createdBy + "')", conn);

        //Execute the insert query
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        //close the connection
        conn.Close();

        return "Success insert";

    }




    [WebMethod]
    public string deleteTable(string name)
    {
        //Declare Connection by passing the connection string from the web config file
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString);

        //Open the connection
        conn.Open();

        //Declare the sql command
        SqlCommand cmd = new SqlCommand("Delete From Employee Where name= '" + name + "'", conn);

        //Execute the insert query
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        //close the connection
        conn.Close();

        return "Success delete";
    }
}

class Place
{
    public string Name { get; set; }
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string AdditionalInfo { get; set; }
    public String WhenCreated { get; set; }
    
}

class Route
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int Rating { get; set; }
    public List<Place> Places { get; set;}

    public Route()
    {

        Places = new List<Place>();
    }

    public override bool Equals(object obj)
    {
        var item = obj as Route;

        if (item == null)
        {
            return false;
        }

        return this.Id.Equals(item.Id);
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }



}

static class JSONHelper
{
    public static string ToJSON(this object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }

    public static string ToJSON(this object obj, int recursionDepth)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.RecursionLimit = recursionDepth;
        return serializer.Serialize(obj);
    }

}
