namespace portfolio_project_web_api.DAL.Entities
{
    public class Portfolio
    {
        public int PortfolioId{ get; set; }
        public string Title{ get; set; }
        public string SubTitle{ get; set; }
        public string ImgUrl{ get; set; }
        public string Url{ get; set; }
        public string Description{ get; set; }
        public bool isActive { get; set; } = true;
    }
}
