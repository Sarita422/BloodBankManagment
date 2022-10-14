using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class RegistrationModelManager
    {
        public RegistrationModel GetAllStateGender()
        {
            RegistrationModel oRegistrationModel = new RegistrationModel();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using(SqlConnection cn=new SqlConnection(scn))
            {
                using(SqlCommand cmd=new SqlCommand("SpGetAllStatesGender", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            oRegistrationModel.StateList.Add(dr["State"].ToString());
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            oRegistrationModel.GenderList.Add(dr["Gender"].ToString());
                        }
                        dr.Close();
                    }
                    catch(SqlException ex)
                    {
                        string s = ex.Message;
                    }
                    finally
                    {
                        if (cn.State == System.Data.ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return oRegistrationModel;
        }
        public RegistrationModel GetAllStates()
        {
            RegistrationModel oRegistrationModel = new RegistrationModel();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetAllStates", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            oRegistrationModel.StateList.Add(dr["State"].ToString());
                        }
                        dr.Close();
                    }
                    catch (SqlException ex)
                    {
                        string s = ex.Message;
                    }
                    finally
                    {
                        if (cn.State == System.Data.ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return oRegistrationModel;
        }
        public List<string> GetAllCities(string state)
        {
            List<string> lstCity = new List<string>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllCities", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@state", SqlDbType.VarChar, 40).Value = state;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            lstCity.Add(dr["City"].ToString());
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
            return lstCity;
        }
        public void InsertRegistration(RegistrationModel oRegistrationModel)
        {
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpInsertRegistration", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = oRegistrationModel.Name;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 40).Value = oRegistrationModel.Email;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar, 40).Value = oRegistrationModel.Password;
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar, 40).Value = oRegistrationModel.Gender ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@age", SqlDbType.Int).Value = oRegistrationModel.Age ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@weight", SqlDbType.Int).Value = oRegistrationModel.Weight ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@register_desgn", SqlDbType.VarChar, 40).Value = oRegistrationModel.RegDesignation ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar, 40).Value = oRegistrationModel.City;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar, 1000).Value = oRegistrationModel.Address;
                    cmd.Parameters.Add("@mobile", SqlDbType.VarChar, 40).Value = oRegistrationModel.Mobile;
                    cmd.Parameters.Add("@office", SqlDbType.VarChar,30).Value = oRegistrationModel.OfficeNum ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@ext", SqlDbType.VarChar, 10).Value = oRegistrationModel.Ext ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@role", SqlDbType.VarChar, 20).Value = oRegistrationModel.Role;
                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
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
        }
        public UnregisteredUserModel GetAllStatesBloodDetails()
        {
            UnregisteredUserModel oUnregisteredUserModel = new UnregisteredUserModel();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetAllStatesCitiesBloodDetails", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            oUnregisteredUserModel.StateList.Add(dr["State"].ToString());
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            oUnregisteredUserModel.BgList.Add(dr["Blood_Group"].ToString());
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            oUnregisteredUserModel.BloodProductList.Add(dr["Product_Type"].ToString());
                        }
                        dr.Close();
                    }
                    catch (SqlException ex)
                    {
                        string s = ex.Message;
                    }
                    finally
                    {
                        if (cn.State == System.Data.ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return oUnregisteredUserModel;
        }
        public int GetCostOfBlood(string bg,string product)
        {
            int cost=0;
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetCost", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@bg", SqlDbType.VarChar,40).Value = bg;
                    cmd.Parameters.Add("@product", SqlDbType.VarChar,40).Value = product;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            cost = Convert.ToInt32(dr["Cost"]);
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
                return cost;
        }
        public void UnregisteredUserBloodRequest(UnregisteredUserModel oUnregisteredUserModel)
        {
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpInsertUnRegisteredUserRequest", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pname", SqlDbType.VarChar, 40).Value = oUnregisteredUserModel.PatientName;
                    cmd.Parameters.Add("@bg", SqlDbType.VarChar, 20).Value = oUnregisteredUserModel.Bg;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar, 30).Value = oUnregisteredUserModel.City;
                    cmd.Parameters.Add("@hname", SqlDbType.VarChar, 500).Value = oUnregisteredUserModel.HospitalName;
                    cmd.Parameters.Add("@amount_of_blood", SqlDbType.Int).Value = oUnregisteredUserModel.AmountOfBlood;
                    cmd.Parameters.Add("@cname", SqlDbType.VarChar,40).Value = oUnregisteredUserModel.ContactName;
                    cmd.Parameters.Add("@cnumber", SqlDbType.VarChar, 40).Value = oUnregisteredUserModel.Mobile;
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = oUnregisteredUserModel.Date;
                    cmd.Parameters.Add("@product", SqlDbType.VarChar,40).Value = oUnregisteredUserModel.BloodProduct;
                    cmd.Parameters.Add("@quotation", SqlDbType.Int).Value = oUnregisteredUserModel.YourQuotation;
                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
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
        }
        public List<StockAvailabilityModel> GetBloodStockAvailability()
        {
            List<StockAvailabilityModel> lstStockAvailabilityModel = new List<StockAvailabilityModel>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SPGetStockAvailibility", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            StockAvailabilityModel stockAvailabilityModel = new StockAvailabilityModel()
                            {
                                Bg = dr["Blood_Group"].ToString(),
                                WholeBlood = (dr["Whole_Blood"]==DBNull.Value)? 0 : Convert.ToInt32(dr["Whole_Blood"]),
                                PackedCells = (dr["Packed_Cells"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Packed_Cells"]),
                                FrozenPlasma = (dr["Frozen_Plasma"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Frozen_Plasma"]),
                                Platelets = (dr["Platelets"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Platelets"])
                            };
                            lstStockAvailabilityModel.Add(stockAvailabilityModel);
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
            return lstStockAvailabilityModel;
        }
        public List<BloodCost> GetBloodCosts()
        {
            List<BloodCost> lstBloodCosts = new List<BloodCost>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetCostOfBlood", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            BloodCost bloodCost = new BloodCost
                            {
                                Bg = dr["Blood_Group"].ToString(),
                                ProductType = dr["Product_Type"].ToString(),
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Units = dr["Units"].ToString(),
                                Cost = Convert.ToInt32(dr["Cost"])  
                            };
                            lstBloodCosts.Add(bloodCost);
                        }
                        dr.Close();
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
            return lstBloodCosts;
        }
        
    }
}