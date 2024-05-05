using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.PeerToPeer;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing;
using LMSBackOfficeDAL; // Make sure to include the necessary namespace

namespace LMSBackOfficeWebApplication
{
    public partial class Networks : System.Web.UI.Page
    {
        protected DataTable referrelsTable { get; set; }
        protected string leftFarNodeId {  get; set; }
        protected string rightFarNodeId { get; set; }

        protected DataTable networkTreeTable
        {
            get { return ViewState["NetworkTreeTable"] as DataTable; }
            set { ViewState["NetworkTreeTable"] = value; }
        }



        protected DataTable allTreeMembers { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divSerachMessage.Visible = false;

                if (Session["Username"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                PopulateTreeandGrid();
                SetFarNode();
            }
        }

        private void PopulateTreeandGrid()
        {
            string memberIdParam = Convert.ToString(Request.QueryString["memberkey"]);


            if (!string.IsNullOrEmpty(memberIdParam))
            {


                BindGridView();
                networkTreeTable = NetworkTree_DataAccess.GetNetworkTree(memberIdParam);

                // Generate HTML only once during the initial load
                string generatedHtml = GenerateHTML(networkTreeTable, memberIdParam, txtSearch.Text.ToLower());
                litGeneratedHtml.Text = generatedHtml;
            }
            else
            {

                string userName = Session["Username"].ToString();
                var member = Members_DataAccess.GetMemberInfo(userName);

                BindGridView();
                networkTreeTable = NetworkTree_DataAccess.GetNetworkTree(member.Id);

                // Generate HTML only once during the initial load
                string generatedHtml = GenerateHTML(networkTreeTable, member.Id, txtSearch.Text.ToLower());
                litGeneratedHtml.Text = generatedHtml;
            }
        }
        public void PopulateFarNodes()
        {

            string userName = Session["Username"].ToString();
            var member = Members_DataAccess.GetMemberInfo(userName);
            //member.Id
        }


        public void SetFarNode()
        {
            string userName = Session["Username"].ToString();
            var member = Members_DataAccess.GetMemberInfo(userName);

            DataTable leftFarnode = Network_DataAccess.GetFarNodeByMemberPosition(member.Id, 1);
            if (leftFarnode != null && leftFarnode.Rows.Count > 0)
            {
                leftFarNodeId =Convert.ToString( leftFarnode.Rows[0]["MEMBER_ID"]);
            }

            DataTable rightFarnode = Network_DataAccess.GetFarNodeByMemberPosition(member.Id, 2);
            if (rightFarnode != null && rightFarnode.Rows.Count > 0)
            {
                rightFarNodeId = Convert.ToString(rightFarnode.Rows[0]["Member_Id"]);
            }
        }
        private string GenerateHTML(DataTable dataTable, string memberId, string searchName)
        {
            StringBuilder sb = new StringBuilder();


            //string userName = Session["Username"].ToString();
            var member = Members_DataAccess.GetMemberInfo(networkTreeTable.Rows[0]["Member_UserName"].ToString());
            // Get root nodes (Position = 0)
            DataRow[] rootNodes = dataTable.Select($"Member_Id = '{memberId}'");

            // Start the parent node
            sb.AppendLine("<ul class=\"parent-node\">");
            //sb.AppendLine("    <li>");


            string memberUserName = member.UserName;
            string position = "";
            string country = member.Country;// Add country data
            string sponsor = member.Sponsor;
            string rank = member.MemberRank;
            string membership = member.MembershipName;
            string cretedDate = member.CreatedDate;
            // Start the list item for the current node
            sb.AppendLine("<li>");
            // Generate HTML for the current node

            if (member.UserName.ToLower().Equals(searchName))
                sb.AppendLine($@"<a href=""/Networks?memberkey={memberId}"" class=""{membership.ToLower()} highlighted"">");
            else
                sb.AppendLine($@"<a href=""/Networks?memberkey={memberId}"" class=""{membership.ToLower()}"">");



            sb.AppendLine($@"
                        <img class=""user-rank"" src=""Content/images/user/avatar-2.jpg"" data-toggle=""tooltip"" data-placement=""top"" title=""elite"">
                        <img class=""user-avatar"" src=""Content/images/user/avatar-2.jpg"">

                        <span class=""user-name"">{memberUserName}</span>
                        <span class=""node-detail"">
                            <label>Sponsor: {sponsor}</label>
                            <label>Membership: {membership}</label>
                            <label>Rank: {rank}</label>
                            <label>Country: {country}</label>
 <label>Purchase Date: {cretedDate}</label>
                        </span>
                    </a>");






            sb.AppendLine("        <ul class=\"child-node\">");

            foreach (DataRow rootNode in rootNodes)
            {
                //string memberId = rootNode["Member_ID"].ToString();
                // Generate HTML for the root node and its children recursively
                sb.Append(GenerateNodeHtml(dataTable, memberId, 1, searchName));
            }
            sb.AppendLine("    </ul>");
            sb.AppendLine("</li>");
            sb.AppendLine("</ul>");


            return sb.ToString();
        }

        public void BindGridView()

        {

            string userName = Session["Username"].ToString();
            var member = Members_DataAccess.GetMemberInfo(userName);
            referrelsTable = Members_DataAccess.GetReferrelsByMemberId(member.Id);
            gvReferrelsTable.DataSource = referrelsTable;
            gvReferrelsTable.DataBind();




        }


        public void LoadAllMembers(string memberIdParam)
        {

            try
            {
                allTreeMembers = Members_DataAccess.GetAllTreeMembersByMemberId(memberIdParam);

            }
            catch
            {

            }


        }

        private string GenerateNodeHtml(DataTable dataTable, string parentId, int Level, string SearchName)
        {
            StringBuilder sb = new StringBuilder();

            // Get the current node and its children
            DataRow[] leftChildRows = dataTable.Select($"Parent_MemberId = '{parentId}' and Position=1");
            if (leftChildRows.Length > 0)
            {









                string memberUserName = leftChildRows[0]["Member_UserName"].ToString();

                var member = Members_DataAccess.GetMemberInfo(memberUserName);

                string country = member.Country;// Add country data
                string sponsor = member.Sponsor;
                string rank = member.MemberRank;
                string membership = member.MembershipName;
                string createdDate=member.CreatedDate;

                // Start the list item for the current node
                sb.AppendLine("<li>");
                // Generate HTML for the current node



                if (member.UserName.ToLower().Equals(SearchName))
                { sb.AppendLine($@"<a href=""/Networks?memberkey={member.Id}"" class=""{membership.ToLower()} highlighted"">"); }
                else
                { sb.AppendLine($@"<a href=""/Networks?memberkey={member.Id}"" class={membership.ToLower()}>"); }
                sb.AppendLine($@"
                        <img class=""user-rank"" src=""Content/images/user/avatar-2.jpg"" data-toggle=""tooltip"" data-placement=""top"" title=""elite"">
                        <img class=""user-avatar"" src=""Content/images/user/avatar-2.jpg"">
                        <span class=""user-name"">{memberUserName}</span>
                        <span class=""node-detail"">
                            <label>Sponsor: {sponsor}</label>
                            <label>Membership: {membership}</label>
                            <label>Rank: {rank}</label>
                            <label>Country: {country}</label>
                 <label>Purchase Date: {createdDate}</label>

                        </span>
                    </a>");
                // Check if there are child nodes for the current node
                string memberId = leftChildRows[0]["Member_ID"].ToString();
                if (Int32.Parse(leftChildRows[0]["Level"].ToString()) <= 3)
                {
                    string childHtml = GenerateNodeHtml(dataTable, memberId, Level + 1, SearchName);
                    if (!string.IsNullOrEmpty(childHtml))
                    {
                        // Start the child node
                        sb.AppendLine("<ul class=\"child-node\">");
                        sb.AppendLine(childHtml);
                        // End the child node
                        sb.AppendLine("</ul>");

                    }
                }

                // End the list item for the current node
                sb.AppendLine("</li>");


            }
            else
            {
                getDummyChilds(sb, Level);

            }

            DataRow[] RightChildRows = dataTable.Select($"Parent_MemberId = '{parentId}' and Position=2");
            if (RightChildRows.Length > 0)
            {


                string memberUserName = RightChildRows[0]["Member_UserName"].ToString();

                var member = Members_DataAccess.GetMemberInfo(memberUserName);

                string country = member.Country;// Add country data
                string sponsor = member.Sponsor;
                string rank = member.MemberRank;
                string membership = member.MembershipName;
                string createdDate=member.CreatedDate;

                // Start the list item for the current node
                sb.AppendLine("<li>");
                // Generate HTML for the current node


                if (member.UserName.ToLower().Equals(SearchName))
                {
                    sb.AppendLine($@"<a href=""/Networks?memberkey={member.Id}"" class=""{membership.ToLower()} highlighted"">");


                }
                else
                {
                    sb.AppendLine($@"<a href=""/Networks?memberkey={member.Id}"" class={membership.ToLower()}>");

                }
                sb.AppendLine($@"
                        <img class=""user-rank"" src=""Content/images/user/avatar-2.jpg"" data-toggle=""tooltip"" data-placement=""top"" title=""elite"">
                        <img class=""user-avatar"" src=""Content/images/user/avatar-2.jpg"">
                        <span class=""user-name"">{memberUserName}</span>
                        <span class=""node-detail"">
                            <label>Sponsor: {sponsor}</label>
                            <label>Membership: {membership}</label>
                            <label>Rank: {rank}</label>
                            <label>Country: {country}</label>
                 <label>Purchase Date: {createdDate}</label>
                        </span>
                    </a>");
                // Check if there are child nodes for the current node
                string memberId = RightChildRows[0]["Member_ID"].ToString();
                if (Int32.Parse(RightChildRows[0]["Level"].ToString()) <= 3)
                {
                    string childHtml = GenerateNodeHtml(dataTable, memberId, Level + 1, SearchName);
                    if (!string.IsNullOrEmpty(childHtml))
                    {
                        // Start the child node
                        sb.AppendLine("<ul class=\"child-node\">");
                        sb.AppendLine(childHtml);
                        // End the child node
                        sb.AppendLine("</ul>");

                    }
                }
                // End the list item for the current node
                sb.AppendLine("</li>");


            }
            else
            {

                getDummyChilds(sb, Level);

                //// Start the list item for the current node
                //sb.AppendLine("<li>");
                //// Generate HTML for the current node
                //sb.AppendLine("<a href=\"#\" class=\"elite\">");
                //sb.AppendLine($@"
                //        <img class=""user-rank"" src=""Content/images/user/avatar-2.jpg"" data-toggle=""tooltip"" data-placement=""top"" title=""elite"">
                //        <img class=""user-avatar"" src=""Content/images/user/avatar-2.jpg"">
                //        <span class=""user-name"">No Member</span>
                //        <span class=""node-detail"">
                //            <label>Username: No User</label>
                //            <label>Position: NA</label>
                //            <label>Country: NA</label>
                //        </span>
                //    </a>");


                //// End the list item for the current node
                //sb.AppendLine("</li>");


            }

            return sb.ToString();
        }

        private static void getDummyChilds(StringBuilder sb, int level)
        {


            // Start the list item for the current node
            sb.AppendLine("<li>");
            // Generate HTML for the current node
            dummychild(sb);
            if (level == 1)
            {
                sb.AppendLine("<ul >");
                sb.AppendLine("<li>");
                dummychild(sb);
                sb.AppendLine(" <ul >");
                sb.AppendLine("  <li>");
                dummychild(sb);
                sb.AppendLine("  </li>");
                sb.AppendLine("   <li>");
                dummychild(sb);
                sb.AppendLine("  </li>");
                sb.AppendLine(" </ul ");
                sb.AppendLine("</li>");
                sb.AppendLine("<li>");

                dummychild(sb);
                //
                sb.AppendLine(" <ul >");
                sb.AppendLine("  <li>");
                dummychild(sb);
                sb.AppendLine("  </li>");
                sb.AppendLine("   <li>");
                dummychild(sb);
                sb.AppendLine("  </li>");
                sb.AppendLine(" </ul ");
                //
                sb.AppendLine("</li>");
                sb.AppendLine("        </ul ");
            }

            if (level == 2)
            {
                sb.AppendLine("        <ul >");
                sb.AppendLine("<li>");
                // Generate HTML for the current node
                dummychild(sb);
                sb.AppendLine("</li>");
                sb.AppendLine("<li>");

                dummychild(sb);
                sb.AppendLine("</li>");
                sb.AppendLine("        </ul ");
            }


            // End the list item for the current node
            sb.AppendLine("</li>");
        }

        private static void dummychild(StringBuilder sb)
        {
            sb.AppendLine("<a href=\"#\" class=\"na\">");
            sb.AppendLine($@"
                        <img class=""user-rank"" src=""Content/images/user/avatar-2.jpg"" data-toggle=""tooltip"" data-placement=""top"" title=""elite"">
                        <img class=""user-avatar"" src=""Content/images/user/avatar-2.jpg"">
                        <span class=""user-name"">No Member</span>
                        <span class=""node-detail"">
                            <label>Username: No User</label>
                            <label>Position: NA</label>
                            <label>Country: NA</label>
                        </span>
                    </a>");
        }

        protected void gvReferrelsTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
      
            gvReferrelsTable.PageIndex = e.NewPageIndex;
            BindGridView(); //bindgridview will get the data source and bind it again
        }

        /*protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (txtSearch.Text == "")
            //{ }
            //else
            //{


            string userName = Session["Username"].ToString();
            var member = Members_DataAccess.GetMemberInfo(userName);

         //  LoadAllMembers(member.Id);
           DataTable dt=Members_DataAccess.GetAllTreeMembersByMemberId(member.Id);
            FindMemberId(dt, txtSearch.Text);

            //}
        }*/

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblSearchMessage.Text = "";
            divSerachMessage.Visible = false;

            string searchUsername = txtSearch.Text.Trim().ToLower();

            // Search for the username in the networkTreeTable
            int level = FindMemberLevel(networkTreeTable, searchUsername);
            string memId = FindMemberId(networkTreeTable, searchUsername);

            // If a matching memberId is found, redirect to the corresponding member's network page
            if (level == 0)
            {
                PopulateTreeandGrid();
                lblSearchMessage.Text = "Unable to find any user with user name: " + searchUsername;
                divSerachMessage.Visible = true;

                return;
            }
            if (level != 0 && level <= 4)
            {
                //Response.Redirect("/Networks?memberkey=" + memberId);

                string userName = Session["Username"].ToString();
                var member = Members_DataAccess.GetMemberInfo(userName);
                //networkTreeTable = NetworkTree_DataAccess.GetNetworkTree(member.Id);


                PopulateTreeandGrid();
                return;

                string generatedHtml = GenerateHTML(networkTreeTable, member.Id, txtSearch.Text.ToLower());
                litGeneratedHtml.Text = generatedHtml;
            }
            else
            {
                try
                {
                    string userName = Session["Username"].ToString();

                    if (txtSearch.Text.ToLower().Equals(userName.ToString()))//Case when user search logged in member i.e himself
                        Response.Redirect("/Networks");

                    var member = Members_DataAccess.GetMemberInfo(searchUsername);

                    // Load all members data only once during the initial page load
                    if (allTreeMembers == null)
                    {
                        LoadAllMembers(member.Id);
                    }
                    networkTreeTable = NetworkTree_DataAccess.GetNetworkTree(member.Id);
                    // Perform the search operation within the existing data
                    string memberId = FindMemberId(networkTreeTable, txtSearch.Text);

                    // Redirect to the found member's network page
                    if (!string.IsNullOrEmpty(memberId))
                    {
                        Response.Redirect("/Networks?memberkey=" + memberId);
                    }
                }
                catch
                {

                }




            }


            /* string userName = Session["Username"].ToString();
             var member = Members_DataAccess.GetMemberInfo(userName);
             networkTreeTable = NetworkTree_DataAccess.GetNetworkTree(member.Id);

             string generatedHtml = GenerateHTML(networkTreeTable, member.Id, txtSearch.Text.ToLower());
             litGeneratedHtml.Text = generatedHtml;*/
            /*

            try
            {
                string userName = Session["Username"].ToString();

                if (txtSearch.Text.ToLower().Equals(userName.ToString()))//Case when user search logged in member i.e himself
                    Response.Redirect("/Networks");

                var member = Members_DataAccess.GetMemberInfo(userName);

                // Load all members data only once during the initial page load
                if (allTreeMembers == null)
                {
                    LoadAllMembers(member.Id);
                }

                // Perform the search operation within the existing data
                string memberId = FindMemberId(allTreeMembers, txtSearch.Text);

                // Redirect to the found member's network page
                if (!string.IsNullOrEmpty(memberId))
                {
                    Response.Redirect("/Networks?memberkey=" + memberId);
                }
            }
            catch
            {

            }*/
        }
        public string FindMemberId(DataTable dataTable, string searchString)
        {
            string memberId = "";

            // Check if the DataTable and search string are valid
            if (dataTable != null && !string.IsNullOrEmpty(searchString))
            {
                // Perform a case-insensitive search for the username within the Member_UserName column of the networkTreeTable
                DataRow[] foundRows = dataTable.Select($"Member_UserName = '{searchString}'", "");

                // If a matching row is found, retrieve the corresponding Member_ID
                if (foundRows.Length > 0)
                {
                    memberId = foundRows[0]["Member_ID"].ToString();
                }
            }

            return memberId;
        }

        public int FindMemberLevel(DataTable dataTable, string searchString)
        {
            int level = 0;

            // Check if the DataTable and search string are valid
            if (dataTable != null && !string.IsNullOrEmpty(searchString))
            {
                // Perform a case-insensitive search for the username within the Member_UserName column of the networkTreeTable
                DataRow[] foundRows = dataTable.Select($"Member_UserName = '{searchString}'", "");

                // If a matching row is found, retrieve the corresponding Member_ID
                if (foundRows.Length > 0)
                {
                    level = Convert.ToInt32(foundRows[0]["Level"].ToString());
                }
            }

            return level;
        }

    }
}