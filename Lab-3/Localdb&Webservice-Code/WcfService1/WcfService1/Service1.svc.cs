﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
       // [WebInvoke(Method = "GET",ResponseFormat = WebMessageFormat.Json,UriTemplate = "data/{id}")]

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "data/{userid}/{password}")]


     /*   public Person GetData2(string id)
        {
            // lookup person with the requested id 
            return new Person()
            {
                Id = Convert.ToInt32(id),
                
                
                Name = "Student"+id
                
            };
        }
     */
        public string IsValidUser(string userid, string password)
        {
            if (userid == "root" && password == "root")
            {
                return "Megha Sharma";
            }
            else
            {
                return "false";
            }

        }



        

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

       
        public string GetDateTime()
        {
            return DateTime.Now.ToString();
        }
    }


    public class Person
    {

        public int Id { get; set; }

        public string Name { get; set; }

      }
}
