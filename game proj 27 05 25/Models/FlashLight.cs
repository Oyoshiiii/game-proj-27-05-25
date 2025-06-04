namespace game_proj_27_05_25.Models
{
    public class FlashLight
    {
        public bool WasFound { get; set; }
        //public int Power { get; set; }
        //конструктор для начала игры или сброса игрового прогресса
        public FlashLight()
        {
            WasFound = false;
            //Power = 3;
        }
    }
}
