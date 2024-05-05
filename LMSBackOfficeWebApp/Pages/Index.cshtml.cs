using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LMSBackOfficeAPI;
using System.Data;
using System.Web.Helpers;
using LMSBackOfficeDAL;



namespace LMSBackOfficeWebApp.Pages
{
    public class IndexModel : PageModel
    {
        LMSBackOfficeAPI.Controllers.BonusTypesController objBTController;
        private readonly ILogger<IndexModel> _logger;
        
        public DataTable dtBonusTypes { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //dtBonusTypes = LMSBackOfficeAPI.Controllers.BonusTypesController.GetBonusTypesRequests();
            //dtBonusTypes = objBTController.GetBonusTypesRequests();
            //dtBonusTypes = LMSBackOfficeDAL.BonusTypes_DataAccess.GetAllBonusTypes();
            //if (dtBonusTypes.Rows.Count >0)
            //{
            //    //WebGrid wGrid = new WebGrid();
            //    // wGrid.GetHtml(dtBonusTypes);                
            //}
        }
    }
}