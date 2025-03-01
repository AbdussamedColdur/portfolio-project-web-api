namespace portfolio_project_web_api.DAL.Entities
{
    public class Feature
    {
        public int FeatureId{ get; set; }
        public string Title{ get; set; }
        public string Description{ get; set; }
        public bool isActive { get; set; } = true;
    }
}
