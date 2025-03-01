namespace portfolio_project_web_api.DAL.Entities
{
    public class Skill
    {
        public int SkillId{ get; set; }
        public string Title{ get; set; }
        public int Value { get; set; }
        public bool isActive { get; set; } = true;
    }
}
