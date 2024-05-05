using ClosedXML.Excel;
using LMSBackOfficeDAL;
using LMSBackOfficeWebApplication.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMSBackOfficeWebApplication
{
    public partial class Tickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                string userName = Session["Username"].ToString();
                var member = Members_DataAccess.GetMemberInfo(userName);
                GetTicketTypes();
                GetDataIngridView(member.Id);
            }
        }

        public void GetDataIngridView(string memberId)
        {         
            var tickets = SupportTickets_DataAccess.GetAllTickets(memberId);
            gv_tickets.DataSource = tickets;
            gv_tickets.DataBind();
        }

        public void GetTicketTypes()
        {
            var ticketTypes = SupportTickets_DataAccess.GetTicketTypes();
            ddlTicketType.DataSource = ticketTypes;
            ddlTicketType.DataValueField = "Ticket_Type";
            ddlTicketType.DataTextField = "Ticket_Type";
            ddlTicketType.DataBind();
        }

        [WebMethod]
        public static string SaveTicket(TicketModel model)
        {
            string userName = HttpContext.Current.Session["Username"].ToString();
            var member = Members_DataAccess.GetMemberInfo(userName);
            var response = SupportTickets_DataAccess.AddTicket(member.Id, member.MemberFullName, member.Email, model.TicketTitle, model.TicketType, model.Description, model.Priority);
            if(response == 1)
            {
                return "success";
            }
            else
            {
                return "Error";
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {           
            if(txtKeyword.Text != null || txtKeyword.Text != "")
            {
                var searchText = txtKeyword.Text;
                string userName = Session["Username"].ToString();
                var member = Members_DataAccess.GetMemberInfo(userName);
                var tickets = SupportTickets_DataAccess.GetAllTickets(member.Id, searchText);
                gv_tickets.DataSource = tickets;
                gv_tickets.DataBind();
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            string userName = Session["Username"].ToString();
            var member = Members_DataAccess.GetMemberInfo(userName);
            var dbtickets = SupportTickets_DataAccess.GetAllTickets(member.Id);

            List<TicketVM> tickets = DataTableHelper.ConvertDataTableToList<TicketVM>(dbtickets);
            DataTable dt = DataTableHelper.ToDataTable(tickets);
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt, "Tickets");
                ws.Columns().AdjustToContents();
                ws.Table(0).ShowAutoFilter = false;
                ws.Table(0).Theme = XLTableTheme.None;
                ws.Rows(1, 1).Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                string FileName = "Tickets" + DateTime.Now + ".xlsx";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename="+ FileName);
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