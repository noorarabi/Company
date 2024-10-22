namespace Company.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
        public string MenuUrl { get; set; }
        public int ParentId { get; set; }
    }
}
