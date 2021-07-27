using MVCTutorial.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCTutorial.DAL
{
    public class Repository<T> where T : new()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalContext"].ToString());

        public List<T> FetchAll(string table)
        {
            var adapter = new SqlDataAdapter($"select * from {table}", con);
            var data = new DataTable();
            adapter.Fill(data);
            var results = new List<T>();

            foreach (DataRow dataRow in data.Rows)
            {
                var patient = new T()
                {
                    Id = int.Parse(dataRow["id"].ToString()),
                    Name = dataRow["name"].ToString(),
                    Mobile = dataRow["mobile"].ToString(),
                    Ailment = dataRow["ailment"].ToString()
                };
                results.Add(patient);
            }
        }

        public T Fetch()
        {
            var adapter = new SqlDataAdapter("select * from patients", con);
        }

        public string Create()
        {
            throw new NotImplementedException();
        }

        public string Delete()
        {
            throw new NotImplementedException();
        }

        public string Update()
        {
            throw new NotImplementedException();
        }
    }
}