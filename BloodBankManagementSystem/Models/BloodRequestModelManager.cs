using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class BloodRequestModelManager
    {
        public List<PendingBloodRequestModel> GetPendingBloodRequests()
        {
            List<PendingBloodRequestModel> lstPendingBloodRequestModels = new List<PendingBloodRequestModel>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using(SqlCommand cmd=new SqlCommand("SpGetAllBloodRequests", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            PendingBloodRequestModel oPendingBloodRequestModel = new PendingBloodRequestModel()
                            {
                                Rid = Convert.ToInt32(dr["User_Id"]),
                                Email = dr["Email_Id"].ToString(),
                                Name = (dr["Name"].ToString()),
                                Date = dr["Date"].ToString(),
                                Bg = dr["Blood_Group"].ToString(),
                                ProductType = dr["Product_Type"].ToString(),
                                AmountOfBlood = Convert.ToInt32(dr["Amount_Of_Blood"]),
                                YourQuotation = Convert.ToInt32(dr["Your_Quotation"]),
                                Mobile = dr["Mobile"].ToString(),
                                City = dr["City"].ToString(),
                                Address = dr["Address"].ToString()
                            };
                            lstPendingBloodRequestModels.Add(oPendingBloodRequestModel);
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
            return lstPendingBloodRequestModels;
        }
        public SearchValuesBloodRequest GetddlCityValues()
        {
            SearchValuesBloodRequest oSearchValuesBloodRequest = new SearchValuesBloodRequest();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetDetailsCityHospInd", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            oSearchValuesBloodRequest.HospIndList.Add(dr["Name"].ToString());
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            oSearchValuesBloodRequest.CityList.Add(dr["City"].ToString());
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
                return oSearchValuesBloodRequest;
        }
        public List<PendingBloodRequestModel> GetSearchedBloodRequests(SearchValuesBloodRequest oSearchValuesBloodRequest)
        {
            List<PendingBloodRequestModel> lstPendingBloodRequestModels = new List<PendingBloodRequestModel>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("GetSearchedBloodRequests", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@hospInd", SqlDbType.VarChar, 20).Value = (oSearchValuesBloodRequest.HospInd == "Individual") ? "acceptor" : oSearchValuesBloodRequest.HospInd;
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = oSearchValuesBloodRequest.Date;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar, 30).Value = oSearchValuesBloodRequest.City;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            PendingBloodRequestModel oPendingBloodRequestModel = new PendingBloodRequestModel()
                            {
                                Rid = Convert.ToInt32(dr["User_Id"]),
                                Email = dr["Email_Id"].ToString(),
                                Name = (dr["Name"].ToString()),
                                Date = dr["Date"].ToString(),
                                Bg = dr["Blood_Group"].ToString(),
                                ProductType = dr["Product_Type"].ToString(),
                                AmountOfBlood = Convert.ToInt32(dr["Amount_Of_Blood"]),
                                YourQuotation = Convert.ToInt32(dr["Your_Quotation"]),
                                Mobile = dr["Mobile"].ToString(),
                                City = dr["City"].ToString(),
                                Address = dr["Address"].ToString()
                            };
                            lstPendingBloodRequestModels.Add(oPendingBloodRequestModel);
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
            return lstPendingBloodRequestModels;
        }
        public List<PendingBloodRequestModel> GetPendingDonateRequests()
        {
            List<PendingBloodRequestModel> lstPendingBloodRequestModels = new List<PendingBloodRequestModel>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetAllBloodDonationRequest", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            PendingBloodRequestModel oPendingBloodRequestModel = new PendingBloodRequestModel()
                            {
                                Rid = Convert.ToInt32(dr["User_Id"]),
                                Email = dr["Email_Id"].ToString(),
                                Name = (dr["Name"].ToString()),
                                Date = dr["Date"].ToString(),
                                Bg = dr["Blood_Group"].ToString(),
                                IllnDesc = dr["Illness_Desc"].ToString(),
                                AmountOfBlood = Convert.ToInt32(dr["Amount_Of_Blood"]),
                                YourQuotation = Convert.ToInt32(dr["Your_Quotation"]),
                                Mobile = dr["Mobile"].ToString(),
                                City = dr["City"].ToString(),
                                Address = dr["Address"].ToString()
                            };
                            lstPendingBloodRequestModels.Add(oPendingBloodRequestModel);
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
            return lstPendingBloodRequestModels;
        }
        public List<PendingBloodRequestModel> GetSearchedDonateRequests(SearchValuesBloodRequest oSearchValuesBloodRequest)
        {
            List<PendingBloodRequestModel> lstPendingBloodRequestModels = new List<PendingBloodRequestModel>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpSearchedBloodDonationRequests", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@hospInd", SqlDbType.VarChar, 20).Value = (oSearchValuesBloodRequest.HospInd == "Individual") ? "donor" : oSearchValuesBloodRequest.HospInd;
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = oSearchValuesBloodRequest.Date;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar, 30).Value = oSearchValuesBloodRequest.City;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            PendingBloodRequestModel oPendingBloodRequestModel = new PendingBloodRequestModel()
                            {
                                Rid = Convert.ToInt32(dr["User_Id"]),
                                Email = dr["Email_Id"].ToString(),
                                Name = (dr["Name"].ToString()),
                                Date = dr["Date"].ToString(),
                                Bg = dr["Blood_Group"].ToString(),
                                IllnDesc = dr["Illness_Desc"].ToString(),
                                AmountOfBlood = Convert.ToInt32(dr["Amount_Of_Blood"]),
                                YourQuotation = Convert.ToInt32(dr["Your_Quotation"]),
                                Mobile = dr["Mobile"].ToString(),
                                City = dr["City"].ToString(),
                                Address = dr["Address"].ToString()
                            };
                            lstPendingBloodRequestModels.Add(oPendingBloodRequestModel);
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
            return lstPendingBloodRequestModels;
        }
        public void UpdateBloodCost(BloodCost oBloodCost)
        {
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpUpdateCostOfBloodProduct", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@bg", SqlDbType.VarChar, 20).Value = oBloodCost.Bg;
                    cmd.Parameters.Add("@product", SqlDbType.VarChar,20).Value = oBloodCost.ProductType;
                    cmd.Parameters.Add("@qty", SqlDbType.Int).Value = oBloodCost.Qty;
                    cmd.Parameters.Add("@unit", SqlDbType.VarChar, 30).Value = oBloodCost.Units??(object)DBNull.Value;
                    cmd.Parameters.Add("@cost", SqlDbType.Int).Value = oBloodCost.Cost;
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
        public void UpdateBloodStockAvailability(StockAvailabilityModel oStockAvailabilityModel)
        {
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpUpdateStockAvailibility", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@bg", SqlDbType.VarChar, 20).Value = oStockAvailabilityModel.Bg;
                    cmd.Parameters.Add("@whole", SqlDbType.Int).Value = oStockAvailabilityModel.WholeBlood;
                    cmd.Parameters.Add("@packed", SqlDbType.Int).Value = oStockAvailabilityModel.PackedCells;
                    cmd.Parameters.Add("@frozen", SqlDbType.Int).Value = oStockAvailabilityModel.FrozenPlasma;
                    cmd.Parameters.Add("@platelets", SqlDbType.Int).Value = oStockAvailabilityModel.Platelets;
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
        public List<UnregisteredUserModel> GetAllUnregisteredBloodRequests()
        {
            List<UnregisteredUserModel> lstUnregisteredUserRequests = new List<UnregisteredUserModel>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetAllUnregisteredRequest", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            UnregisteredUserModel oUnregisteredUserModel = new UnregisteredUserModel()
                            {
                                Pid = Convert.ToInt32(dr["P_Id"]),
                                PatientName = (dr["P_Name"]).ToString(),
                                Bg = (dr["Blood_Group"]).ToString(),
                                BloodProduct = (dr["Product_Type"]).ToString(),
                                City = dr["City"].ToString(),
                                HospitalName = dr["Hospital_Name"].ToString(),
                                AmountOfBlood = dr["Amount_Of_Blood"].ToString(),
                                ContactName = dr["Contact_Name"].ToString(),
                                Mobile = dr["Contact_Number"].ToString(),
                                Date = dr["Date"].ToString(),
                                YourQuotation = Convert.ToInt32(dr["Your_Quotation"])
                            };
                            lstUnregisteredUserRequests.Add(oUnregisteredUserModel);
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
                return lstUnregisteredUserRequests;
        }
        public UnregisteredSearchValuesModel GetAllCities()
        {
            UnregisteredSearchValuesModel unregisteredSearchValuesModel = new UnregisteredSearchValuesModel();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetAllCities", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            unregisteredSearchValuesModel.CityList.Add(dr["City"].ToString());
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
            return unregisteredSearchValuesModel;
        }
        public List<UnregisteredUserModel> GetAllUnregisteredSearchedRequests(UnregisteredSearchValuesModel oUnregisteredSearchValuesModel)
        {
            List<UnregisteredUserModel> lstUnregisteredUserRequests = new List<UnregisteredUserModel>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetUnregisteredSerachedRequests", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = oUnregisteredSearchValuesModel.Date;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar, 20).Value = oUnregisteredSearchValuesModel.City;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            UnregisteredUserModel oUnregisteredUserModel = new UnregisteredUserModel()
                            {
                                Pid = Convert.ToInt32(dr["P_Id"]),
                                PatientName = (dr["P_Name"]).ToString(),
                                Bg = (dr["Blood_Group"]).ToString(),
                                BloodProduct = (dr["Product_Type"]).ToString(),
                                City = dr["City"].ToString(),
                                HospitalName = dr["Hospital_Name"].ToString(),
                                AmountOfBlood = dr["Amount_Of_Blood"].ToString(),
                                ContactName = dr["Contact_Name"].ToString(),
                                Mobile = dr["Contact_Number"].ToString(),
                                Date = dr["Date"].ToString(),
                                YourQuotation = Convert.ToInt32(dr["Your_Quotation"])
                            };
                            lstUnregisteredUserRequests.Add(oUnregisteredUserModel);
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
            return lstUnregisteredUserRequests;
        }
        public BadgeValuesModel GetNumberOfRequests()
        {
            BadgeValuesModel badgeValuesModel = new BadgeValuesModel();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetNumberOfRequests", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            badgeValuesModel.BloodReq = Convert.ToInt32(dr["bloodreq"]);
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            badgeValuesModel.DonateReq = Convert.ToInt32(dr["donatereq"]);
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            badgeValuesModel.UnregReq = Convert.ToInt32(dr["unregreq"]);
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
            return badgeValuesModel;
        }
        public List<DataPoint> GetGraphDetails()
        {
            List<DataPoint> lstDataPoint = new List<DataPoint>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpGetNumberOfRequests", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            lstDataPoint.Add(new DataPoint("Blood Reqs", Convert.ToInt32(dr["bloodreq"])));
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            lstDataPoint.Add(new DataPoint("Donate Reqs", Convert.ToInt32(dr["donatereq"])));
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            lstDataPoint.Add(new DataPoint("Unreg Reqs", Convert.ToInt32(dr["unregreq"])));
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
            return lstDataPoint;
        }
        public List<DataPoint> GetStockData()
        {
            List<DataPoint> lstDataPoint2 = new List<DataPoint>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SpTotalofStock", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            lstDataPoint2.Add(new DataPoint(dr["Blood_Group"].ToString(), Convert.ToDouble(dr["Total"])));                            
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
            return lstDataPoint2;
        }

    }
}