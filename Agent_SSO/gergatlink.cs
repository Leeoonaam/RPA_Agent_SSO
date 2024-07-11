using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Agent_SSO
{
    class gergatlink
    {
        
        public SqlConnection abre_cn()
        {
            try
            {
                string sconn = "";

                //producao
                sconn = "Data Source=172.28.2.175;Initial Catalog=STTGAT_SSO;Persist Security Info=True;User ID=sa;Password=55gatlbd_110515";

                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = sconn;
                cn.Open();

                return cn;
            }
            catch (Exception err)
            {
                throw;
                return null;
            }
        }


        


    }
}
