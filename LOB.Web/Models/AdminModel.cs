namespace LOB.Web.Models
{
    public class AdminModel:DataModel
    {
        public bool IsRunService { get; set; }
        public bool IsGenerateOrders { get; set; }
        public bool IsGeneratePicks { get; set; }
        public bool IsGeneratePacks { get; set; }
        public bool IsGenerateRoutes { get; set; }
    }
}