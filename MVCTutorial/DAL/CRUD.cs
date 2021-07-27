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
    public class CRUD : ICrud<Hospital>
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalContext"].ToString());

        public Hospital Fetch(int id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter($"select * from hospitals where id='{id}'", con);
            var data = new DataTable();
            adapter.Fill(data);
            var hospital = new Hospital() { 
                Id = id,  
                Name = data.Rows[0]["name"].ToString(),
                Address = data.Rows[0]["address"].ToString(),
                Mobile = data.Rows[0]["mobile"].ToString(),
            };

            return hospital;
        }

        public List<Hospital> FetchAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from hospitals", con);
            DataTable data = new DataTable();
            adapter.Fill(data);

            var hospitals = new List<Hospital>();

            foreach (DataRow drow in data.Rows)
            {
                var hospital = new Hospital()
                {
                    Id = int.Parse(drow["id"].ToString()),
                    Name = drow["name"].ToString(),
                    Address = drow["address"].ToString(),
                    Mobile = drow["mobile"].ToString()
                };
                hospitals.Add(hospital);
            }

            return hospitals;
        }

        public string Create(Hospital hospital)
        {
            string message = "Successful Insertion Of Record!";
            var command = new SqlCommand($"insert into hospitals(name, address, mobile) values('{hospital.Name}', '{hospital.Address}', '{hospital.Mobile}')", con);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            return message;
        }

        public string Update(Hospital hospital)
        {
            var message = "Update successful";
            var commad = new SqlCommand($"update hospitals set name='{hospital.Name}', address='{hospital.Address}', mobile='{hospital.Mobile}' where id='{hospital.Id}'", con);
            try
            {
                con.Open();
                commad.ExecuteNonQuery();
                return message;
            }
            catch (Exception ex)
            {
                message = "Error to report: " + ex.Message;
                return message;
            }
            finally
            {
                con.Close();
            }
        }

        public string Delete(int id)
        {
            var message = "Successfully deleted";
            var command = new SqlCommand($"delete from hospitals where id='{id}'", con);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                return message;
            }
            catch (Exception ex)
            {
                message = "Error to report: " + ex.Message;
                return message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}