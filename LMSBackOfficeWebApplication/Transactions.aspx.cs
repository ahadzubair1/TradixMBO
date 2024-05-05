using ClosedXML.Excel;
using LMSBackOfficeDAL;
using LMSBackOfficeWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMSBackOfficeWebApplication
{
    public partial class Transactions : System.Web.UI.Page
    {
        protected DataTable dt { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                string userName = Session["Username"].ToString();
                var member = Members_DataAccess.GetMemberInfo(userName);
                dt = Transactions_DataAcsess.GetAllTransaction(member.Id);

            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtKeyword.Text != null || txtKeyword.Text != "")
            {
                var searchText = txtKeyword.Text;
                string userName = Session["Username"].ToString();
                var member = Members_DataAccess.GetMemberInfo(userName);
                dt = Transactions_DataAcsess.GetAllTransaction(member.Id, searchText);
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            string userName = Session["Username"].ToString();
            var member = Members_DataAccess.GetMemberInfo(userName);
            var dbTransactions = Transactions_DataAcsess.GetAllTransaction(member.Id);
            List<TransactionVM> transactions = DataTableHelper.ConvertDataTableToList<TransactionVM>(dbTransactions);
            DataTable dt = DataTableHelper.ToDataTable(transactions);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt, "Transactions");
                ws.Columns().AdjustToContents();
                ws.Table(0).ShowAutoFilter = false;
                ws.Table(0).Theme = XLTableTheme.None;
                ws.Rows(1, 1).Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string FileName = "Transactions" + DateTime.Now + ".xlsx";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}