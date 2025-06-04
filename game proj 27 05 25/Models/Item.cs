namespace game_proj_27_05_25.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool WasFound { get; set; }
        public bool WasUsed { get; set; }
        public string Description { get; set; } = "";
    }
}
