using System;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Core.List.PlugIn;
using Kingdee.BOS.Core.Msg;

namespace OutStockRejectListBarSys
{
    public class ButtonEvents : AbstractListPlugIn
    {
        Generate generate = new Generate();

        public override void BarItemClick(BarItemClickEventArgs e)
        {
            //定义主键变量(用与收集所选中的行主键值)
            var flistid = string.Empty;
            //中转判断值
            var tempstring = string.Empty;
            //返回信息记录
            var mesage = string.Empty;

            base.BarItemClick(e);

            //销售出库单 - 反审核时执行
            if (e.BarItemKey == "tbReject")
            {
                //获取列表上通过复选框勾选的记录
                var selectedrows = this.ListView.SelectedRowsInfo;
                //判断需要有选择记录时才继续
                if(selectedrows.Count>0)
                {
                    //通过循环将选中行的主键进行收集(注:去除重复的选项,只保留不重复的主键记录)
                    foreach (var row in selectedrows)
                    {
                        if (string.IsNullOrEmpty(flistid))
                        {
                            flistid = "'" + Convert.ToString(row.BillNo) + "'";
                            tempstring = Convert.ToString(row.BillNo);
                        }
                        else
                        {
                            if (tempstring != Convert.ToString(row.BillNo))
                            {
                                flistid += "," + "'" + Convert.ToString(row.BillNo) + "'";
                                tempstring = Convert.ToString(row.BillNo);
                            }    
                        }
                    }

                    //根据所获取的单号进行更新
                    var result = generate.Reject(flistid);
                    mesage = result == "Finish" ? "所选销售出库单,反审核成功" : $"出库数据与条码系统交互操作异常,原因:'{result}'";
                    View.ShowMessage(mesage);
                }
            }
        }
    }
}
