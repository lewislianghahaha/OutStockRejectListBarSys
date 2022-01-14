using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutStockRejectListBarSys
{
    public class Generate
    {
        SqlList sqlList=new SqlList();

        /// <summary>
        /// 反审核使用
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public string Reject(string orderno)
        {
            var result = "Finish";

            try
            {
                Generdt(sqlList.Reject(orderno), 1);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 按照指定的SQL语句执行记录(反审核时使用)
        ///  <param name="conid">0:连接K3数据库,1:连接条码库</param>
        /// </summary>
        private void Generdt(string sqlscript, int conid)
        {
            using (var sql = GetCloudConn(conid))
            {
                sql.Open();
                var sqlCommand = new SqlCommand(sqlscript, sql);
                sqlCommand.ExecuteNonQuery();
                sql.Close();
            }
        }

        /// <summary>
        /// 获取连接返回信息
        /// <param name="conid">0:连接K3数据库,1:连接条码库</param>
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetCloudConn(int conid)
        {
            var sqlcon = new SqlConnection(GetConnectionString(conid));
            return sqlcon;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="conid">0:连接K3数据库,1:连接条码库</param>
        /// <returns></returns>
        private string GetConnectionString(int conid)
        {
            var strcon = string.Empty;

            if (conid == 0)
            {
                strcon = @"Data Source='192.168.1.228';Initial Catalog='AIS20211022091225';Persist Security Info=True;User ID='sa'; Password='kingdee';
                       Pooling=true;Max Pool Size=40000;Min Pool Size=0";
            }
            else
            {
                strcon = @"Data Source='172.16.4.249';Initial Catalog='RTIM_YATU';Persist Security Info=True;User ID='sa'; Password='Yatu8888';
                       Pooling=true;Max Pool Size=40000;Min Pool Size=0";
            }

            return strcon;
        }
    }
}
