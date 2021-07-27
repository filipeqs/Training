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
    public class DoctorsCrud : ICrud<Doctor>
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalContext"].ToString());
        public List<Doctor> FetchAll()
        {
            var adapter = new SqlDataAdapter("select * from doctors", con);
            var data = new DataTable();
            adapter.Fill(data);

            var doctors = new List<Doctor>();
            foreach (DataRow dataRow in data.Rows)
            {
                var doctor = new Doctor()
                {
                    Id = int.Parse(dataRow["id"].ToString()),
                    Name = dataRow["name"].ToString(),
                    Speciality = dataRow["speciality"].ToString()
                };

                doctors.Add(doctor);
            }

            return doctors;
        }

        public Doctor Fetch(int id)
        {
            var adapter = new SqlDataAdapter($"select * from doctors where id='{id}'", con);
            var data = new DataTable();
            adapter.Fill(data);

            var doctor = new Doctor()
            {
                Id = int.Parse(data.Rows[0]["id"].ToString()),
                Name = data.Rows[0]["name"].ToString(),
                Speciality = data.Rows[0]["speciality"].ToString()
            };

            return doctor;
        }

        public string Create(Doctor doctor)
        {
            var message = "Succefully created";
            var command = new SqlCommand($"insert into doctors(name, speciality) values('{doctor.Name}', '{doctor.Speciality}')", con);
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

        public string Update(Doctor doctor)
        {
            var message = "Updated succefully";
            var command = new SqlCommand($"update doctors set name='{doctor.Name}', speciality='{doctor.Speciality}' where id='{doctor.Id}'", con);
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
            var command = new SqlCommand($"delete from doctors where id={id}", con);
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