namespace portfolio_project_web_api.DAL.Entities
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        public string Head { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public bool isActive { get; set; } = true;

    }
}
