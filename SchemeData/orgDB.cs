using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.Windows.Forms;
using System.Web;
using System.Net.NetworkInformation;

namespace SchemeData
{
    class orgDB
    {
        public orgDB()
        {
            odbconn = new OdbcConnection("Driver={MySQL ODBC 3.51 Driver};Server=154.0.172.180;Port=3306;Database=seadsenp_orgData; User=seadsenp_emp;Password=dev101;Option=3");
            odbcmd = new OdbcCommand();
            odbcmd = odbconn.CreateCommand();
            odbcDA = new OdbcDataAdapter();
        }
        OdbcConnection odbconn;
        OdbcCommand odbcmd;
        OdbcDataAdapter odbcDA;
        public DataSet retDB(String cmdText, String mem)
        {
            DataSet dsTemp;
            odbcmd.CommandType = CommandType.Text;
            odbcmd.CommandText = cmdText;
            odbcDA.SelectCommand = odbcmd;                         
            dsTemp = new DataSet();
            dsTemp.Clear();
            odbcDA.Fill(dsTemp, mem);
            return dsTemp;
        }

        public void altDB(String cmdText)
        {
            //int noR = 0;
            odbcmd.CommandType = CommandType.Text;
            odbcmd.CommandText = cmdText;         
            if (odbcmd.Connection.State == ConnectionState.Closed)
            {
                odbcmd.Connection.Open();
            }
            odbcmd.ExecuteNonQuery();
        }

        public bool CheckForInternetConnection()
        {
            try
            {
                //DataSet dsTemp;
                //string cmdText, mem;
                //cmdText = "SHOW TABLES LIKE 'Users'";
                //mem = "ConnectionTest";
                //odbcmd.CommandType = CommandType.Text;
                //odbcmd.CommandText = cmdText;
                //odbcDA.SelectCommand = odbcmd;
                //dsTemp = new DataSet();
                //odbcDA.Fill(dsTemp, mem);
                //return true;

                string nameOrAddress = "8.8.4.4";
                bool pingable = false;
                Ping pinger = new Ping();
                try
                {
                    PingReply reply = pinger.Send(nameOrAddress);
                    pingable = reply.Status == IPStatus.Success;
                }
                catch (PingException)
                {
                    // Discard PingExceptions and return false;
                    return false;
                }
                return pingable;
            }
            catch
            {
                return false;
            }
        }

    }
}
