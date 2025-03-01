namespace portfolio_project_web_api.DAL.Entities
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool isActive { get; set; } = true;
    }
}
