using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BloodBankManagementSystem.Models
{
    public class LastHistoryModelManager
    {
        public List<LastHistoryModel> GetAcceptorHistory(int rid)
        {
            List<LastHistoryModel> lstLastHistoryModel = new List<LastHistoryModel>();
            string scn = ConfigurationManager.ConnectionStrings["BloodBankCon"].ConnectionString;
            using(SqlConnection cn=new SqlConnection(scn))
            {
                using(SqlCommand cmd=new SqlCommand("SpGetAcceptorLastHistory", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("rid", SqlDbType.Int).Value = rid;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            LastHistoryModel lastHistoryModel = new LastHistoryModel
                            {
                                Bg = dr["Blood_Group"].ToString(),
                                BgProduct = dr["Product_Type"].ToString(),
                                Date = dr["Date"].ToString(),
                                Qty = Convert.ToInt32(dr["Amount_Of_Blood"]),
                                Quotation = Convert.ToInt32(dr["Your_Quotation"])
                            };
                            lstLastHistoryModel.Add(lastHistoryModel);
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
            return lstLastHistoryModel;
        }
    }
}