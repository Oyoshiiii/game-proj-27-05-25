namespace game_proj_27_05_25.Models
{
    public class Photo
    {
        public bool WasFound { get; set; }
        public bool WasUsed { get; set; }
        //конструктор для начала игры или сброса игрового прогресса
        public Photo()
        {
            WasFound = false;
            WasUsed = false;
        }
    }
}
