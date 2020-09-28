using KERRY.NMS.CORE.Enums;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace KERRY.NMS.DAL.Respository
{
    public interface ISMSRepository
    {
        Task<bool> StoreSMSToORC(string messageContent, string mobileNumber, string recipient);
    }
    public class SMSRepository : ISMSRepository
    {
        public SMSRepository()
        {

        }

        public async Task<bool> StoreSMSToORC(string messageContent, string mobileNumber, string recipient)
        {
            string queryString = @"
                                INSERT INTO smsserver_out (id, type, recipient, text, create_date, encoding, so_dt, ad_user_id) 
                                VALUES (sms_seq.nextval, 'O', N'" + recipient + "', d_reports.remove_tone_marks(N'" + messageContent + @"'), 
                                        sysdate, 7, '" + mobileNumber + "', '1000000' )";


            using (OracleConnection con = new OracleConnection(ConnectionConfigurations.ConnectStringOracle))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = queryString;
                var result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Dispose();

                return result > 0;
            }
        }
    }
}
