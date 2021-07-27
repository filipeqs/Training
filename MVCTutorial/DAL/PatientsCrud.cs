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
    public class PatientsCrud : ICrud<Patient>
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalContext"].ToString());

        public List<Patient> FetchAll()
        {
            var adapter = new SqlDataAdapter("select * from patients", con);
            var data = new DataTable();
            adapter.Fill(data);

            var patients = new List<Patient>();
            foreach (DataRow dataRow in data.Rows)
            {
                var patient = new Patient() { 
                    Id = int.Parse(dataRow["id"].ToString()),
                    Name = dataRow["name"].ToString(),
                    Mobile = dataRow["mobile"].ToString(),
                    Ailment = dataRow["ailment"].ToString()
                };
                patients.Add(patient);
            }

            return patients;
        }

        public Patient Fetch(int id)
        {
            var adapter = new SqlDataAdapter($"select * from patients where id='{id}'", con);
            var data = new DataTable();
            adapter.Fill(data);
            var patient = new Patient()
            {
                Id = int.Parse(data.Rows[0]["id"].ToString()),
                Name = data.Rows[0]["name"].ToString(),
                Mobile = data.Rows[0]["mobile"].ToString(),
                Ailment = data.Rows[0]["ailment"].ToString()
            };

            return patient;
        }

        public string Create(Patient patient)
        {
            var message = "Succefully updated";
            var command = new SqlCommand($"insert into patients(name, mobile, ailment) values('{patient.Name}', '{patient.Mobile}', '{patient.Ailment}')", con);
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

        public string Update(Patient patient)
        {
            var message = "Updated updated";
            var command = new SqlCommand($"update patients set name='{patient.Name}', mobile='{patient.Mobile}', ailment='{patient.Ailment}' where id='{patient.Id}'", con);
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

        public string Delete(int id)
        {
            var message = "Deleted";
            var command = new SqlCommand($"delete from patients where id='{id}'", con);
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