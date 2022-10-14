using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class DonateBloodRequestModelManager
    {
        public DonateBloodRequestModel GetBloodDetails()
        {
            DonateBloodRequestModel oDonateBloodRequestModel = new DonateBloodRequestModel();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using(SqlConnection cn=new SqlConnection(scn))
            {
                using(SqlCommand cmd=new SqlCommand("SpGetAllBloodGroups", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            oDonateBloodRequestModel.BgList.Add(dr["Blood_Group"].ToString());
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
            return oDonateBloodRequestModel;
        }
        public int GetCostOfBlood(string bg)
        {
            int cost = 0;
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using(SqlConnection cn=new SqlConnection(scn))
            {
                using(SqlCommand cmd=new SqlCommand("SpGetBloodCost", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@bg", SqlDbType.VarChar, 40).Value = bg;
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
                        if (cn.State == System.Data.ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                }
            }
            return cost;
        }
        public void InsertBloodDonateRequest(DonateBloodRequestModel oDonateBloodRequestModel)
        {
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpInsertDonateBloodRequest", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@reg_id", SqlDbType.Int).Value = oDonateBloodRequestModel.Rid;
                    cmd.Parameters.Add("@bg", SqlDbType.VarChar,20).Value = oDonateBloodRequestModel.Bg;
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = oDonateBloodRequestModel.Date;
                    cmd.Parameters.Add("@AnyIll", SqlDbType.Bit).Value = (oDonateBloodRequestModel.AnyIlln==true)?1:0;
                    cmd.Parameters.Add("@IllDesc", SqlDbType.VarChar,500).Value = oDonateBloodRequestModel.IllnDesc??(object)DBNull.Value;
                    cmd.Parameters.Add("@amount_of_blood", SqlDbType.Int).Value = oDonateBloodRequestModel.AmountOfBlood;
                    cmd.Parameters.Add("@quotation", SqlDbType.Int).Value = oDonateBloodRequestModel.YourQuotation;
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
        public BloodRequestModel GetAllBloodProductDetails()
        {
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using(SqlConnection cn=new SqlConnection(scn))
            {
                using(SqlCommand cmd=new SqlCommand("SpGetAllBloodDetails", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        BloodRequestModel bloodRequestModel = new BloodRequestModel();
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            bloodRequestModel.BgList.Add(dr["Blood_Group"].ToString());
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            bloodRequestModel.BloodProductList.Add(dr["Product_Type"].ToString());
                        }
                        dr.Close();
                        return bloodRequestModel;
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
            return null;
        }
        public void InsertBloodRequest(BloodRequestModel bloodRequestModel)
        {
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpInsertBloodRequest", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@r_id", SqlDbType.Int).Value = bloodRequestModel.Rid;
                    cmd.Parameters.Add("@bg", SqlDbType.VarChar, 20).Value = bloodRequestModel.Bg;
                    cmd.Parameters.Add("@product", SqlDbType.VarChar, 20).Value = bloodRequestModel.BloodProduct;
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = bloodRequestModel.Date;
                    cmd.Parameters.Add("@amount_of_blood", SqlDbType.Int).Value = bloodRequestModel.AmountOfBlood;
                    cmd.Parameters.Add("@quotation", SqlDbType.Int).Value = bloodRequestModel.YourQuotation;
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
        public List<DonorHospitalDetailsModel> GetDOnorHospitalsDetails()
        {
            List<DonorHospitalDetailsModel> lstDonorHospitalDetails = new List<DonorHospitalDetailsModel>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetAllDonorsHospitals", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            DonorHospitalDetailsModel donorHospitalDetailsModel = new DonorHospitalDetailsModel
                            {
                                Rid = Convert.ToInt32(dr["User_Id"]),
                                Name = dr["Name"].ToString(),
                                Role = dr["User_Type"].ToString()
                            };
                            lstDonorHospitalDetails.Add(donorHospitalDetailsModel);
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
            return lstDonorHospitalDetails;
        }
        public DonorHospitalDetailsModel GetDetails(int id)
        {
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using(SqlConnection cn=new SqlConnection(scn))
            {
                using(SqlCommand cmd=new SqlCommand("SpDonorsHospitals", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    try
                    {
                        DonorHospitalDetailsModel oDonorHospitalDetailsModel = new DonorHospitalDetailsModel();
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            oDonorHospitalDetailsModel.Name = dr["Name"].ToString();
                            oDonorHospitalDetailsModel.Email = dr["Email_Id"].ToString();
                            oDonorHospitalDetailsModel.Mobile = dr["Mobile"].ToString();
                            oDonorHospitalDetailsModel.City = dr["City"].ToString();
                            oDonorHospitalDetailsModel.Address = dr["Address"].ToString();
                        }
                        dr.Close();
                        return oDonorHospitalDetailsModel;
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
            return null;
        }
    }
}