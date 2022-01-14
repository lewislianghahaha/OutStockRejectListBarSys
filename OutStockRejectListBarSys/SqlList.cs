namespace OutStockRejectListBarSys
{
    public class SqlList
    {
        private string _result;

        /// <summary>
        /// 反审核使用
        /// 作用:将条码库.[T_K3SalesOut]对应的'FRemarkid' 'Flastop_time'进行更新
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public string Reject(string orderno)
        {
            _result = $@"
                            UPDATE T_K3SalesOut SET FRemarkid=1,Flastop_time=GETDATE()
                            WHERE doc_no in ({orderno})
                        ";

            return _result;
        }
    }
}
