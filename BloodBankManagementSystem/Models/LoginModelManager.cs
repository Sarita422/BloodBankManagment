using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class LoginModelManager
    {
        public LoginModel UserAuth(LoginModel oLoginModel)
        {
            LoginModel loginModel = new LoginModel();
            string scn= ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpValidateUser", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmailMobile", SqlDbType.VarChar, 40).Value = oLoginModel.EmailMob;
                    cmd.Parameters.Add("@pwd", SqlDbType.VarChar, 40).Value = oLoginModel.Password;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if(dr["Name"] != DBNull.Value &&  dr["User_Type"]!= DBNull.Value)
                            {
                                loginModel.Role = Convert.ToString(dr["User_Type"]);
                                loginModel.Name = Convert.ToString(dr["Name"]);
                                loginModel.Rid = Convert.ToInt32(dr["User_Id"]);
                                loginModel.Isvalid = 1;
                            }
                            else
                            {
                                loginModel.Isvalid = 0;
                            }
                        }
                        dr.Close();
                    }
                    catch(SqlException ex)
                    {
                        string s = ex.Message;
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
           return loginModel;
        }
    }
}