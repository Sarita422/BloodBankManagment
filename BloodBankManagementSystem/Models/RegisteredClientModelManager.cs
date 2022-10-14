using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class RegisteredClientModelManager
    {
        public List<RegisteredClientModel> GetAllRegisteredClientDetails()
        {
            List<RegisteredClientModel> lstRegisteredClients = new List<RegisteredClientModel>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using(SqlConnection cn=new SqlConnection(scn))
            {
                using(SqlCommand cmd=new SqlCommand("SpGetAllRegisteredClients", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            RegisteredClientModel oRegisteredClientModel = new RegisteredClientModel
                            {
                                Rid = Convert.ToInt32(dr["User_Id"]),
                                Name = dr["Name"].ToString(),
                                Email = dr["Email_Id"].ToString(),
                                Password = dr["Password"].ToString(),
                                Address = dr["Address"].ToString(),
                                Mobile = dr["Mobile"].ToString(),
                            };
                            lstRegisteredClients.Add(oRegisteredClientModel);
                        }
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
            return lstRegisteredClients;
        }
        public void UpdateClientDetails(RegisteredClientModel oRegisteredClientModel)
        {
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateRegisteredClient", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.Int).Value = oRegisteredClientModel.Rid;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 40).Value = oRegisteredClientModel.Name;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 40).Value = oRegisteredClientModel.Email;
                    cmd.Parameters.Add("@pwd", SqlDbType.VarChar, 40).Value = oRegisteredClientModel.Password;
                    cmd.Parameters.Add("@addr", SqlDbType.VarChar, 1000).Value = oRegisteredClientModel.Address;
                    cmd.Parameters.Add("@mbl", SqlDbType.VarChar, 30).Value = oRegisteredClientModel.Mobile;
                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
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
        }
    }
}