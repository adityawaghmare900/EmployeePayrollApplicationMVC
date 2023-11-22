
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace RepositoryLayer.Services
{
    public class EmployeeRepo: IEmployeeRepo
    {
        private readonly IConfiguration configuration;

        public EmployeeRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddEmployee(EmployeeModel employee)
        {
            try
            {
                if(employee != null)
                {
                    using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("EmployeePayroll")))
                    {
                        {
                            SqlCommand cmd = new SqlCommand("spAddEmployees", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@EMP_Name", employee.Name);
                            cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                            cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                            cmd.Parameters.AddWithValue("@Department", employee.Department);
                            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                            cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                            cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                        
                    }
                    return "Added Successfully";
                }
                else
                {
                    return "Failed" ;
                } 
            }
            catch(Exception ex)
            {
                throw ex;
            }
            }

        public List<EmployeeModel> GetAllEmployee()
        {
            List<EmployeeModel> empList = new List<EmployeeModel>();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("EmployeePayroll")))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", connection);
                cmd.CommandType= CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader sdr= cmd.ExecuteReader();
                while (sdr.Read())
                {
                    EmployeeModel employee = new EmployeeModel();
                    employee.EmployeeId = (Convert.ToInt32(sdr["EmployeeId"]));
                    employee.Name= (Convert.ToString(sdr["EMP_Name"]));
                    employee.ProfileImage = (Convert.ToString(sdr["ProfileImage"]));
                    employee.Gender = (Convert.ToString(sdr["Gender"]));
                    employee.Department = (Convert.ToString(sdr["Department"]));
                    employee.Salary = (Convert.ToInt32(sdr["salary"]));
                    employee.StartDate = (Convert.ToDateTime(sdr["StartDate"]));
                    employee.Notes = (Convert.ToString(sdr["Notes"]));
                    empList.Add(employee);
                }
                connection.Close();
            }
            return empList;
        }

        public string UpdateEmployee(EmployeeModel employee)
        {
            if (employee != null)
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("EmployeePayroll")))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@EMP_Name", employee.Name);
                    cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Update Successfully"; 
            }
            else
            {
                return "Failed Updation";
            }
        }

        public EmployeeModel GetEmployeeById(int id)
        {
            EmployeeModel employee = new EmployeeModel();

            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("EmployeePayroll")))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.Name = rdr["EMP_Name"].ToString();
                    employee.ProfileImage = rdr["ProfileImage"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToInt32(rdr["salary"]);
                    employee.StartDate = (Convert.ToDateTime(rdr["StartDate"]));
                    employee.Notes = rdr["Notes"].ToString();
                }
                con.Close();
            }
            return employee;
        }

        public string DeleteEmployee(int id)
        {

            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("EmployeePayroll")))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return "Employee Deleted successfully";
        }


        public EmployeeModel login(EmpLogin login)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("EmployeePayroll")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("uspUserlogin", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("EmployeeId", login.EmployeeId);
                    cmd.Parameters.AddWithValue("EMP_Name", login.Name);

                    EmployeeModel user = new EmployeeModel();
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        user.EmployeeId = rd.GetInt32(0);
                        user.Name = rd.GetString(1);
                        user.ProfileImage = rd.GetString(2);
                        user.Salary = rd.GetDouble(5);
                    }
                    return user;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public EmployeeModel GetDetailsByName(string name)
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();

                using (SqlConnection connection=new SqlConnection(configuration.GetConnectionString("EmployeePayroll")))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spGetEmployeeByName", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@EMP_Name",name );
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        employeeModel.EmployeeId=Convert.ToInt32((rd["EmployeeId"]));
                        employeeModel.Name = rd["EMP_Name"].ToString();
                        employeeModel.ProfileImage = rd["ProfileImage"].ToString();
                        employeeModel.Gender = rd["Gender"].ToString();
                        employeeModel.Department = rd["Department"].ToString();
                        employeeModel.Salary =Convert.ToInt32 (rd["Salary"]);
                        employeeModel.StartDate = Convert.ToDateTime(rd["StartDate"]);
                        employeeModel.Notes = rd["Notes"].ToString();
                    }
                    connection.Close();
                }
                return employeeModel;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public EmployeeModel AddOrUpdateEmployee(EmployeeModel employee)
        {
            if (employee != null)
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("EmployeePayroll")))
                {
                    SqlCommand cmd = new SqlCommand("spAddOrUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@EMP_Name", employee.Name);
                    cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return employee;
            }
            else
            {
                return null;
            }
        }
    }
}
