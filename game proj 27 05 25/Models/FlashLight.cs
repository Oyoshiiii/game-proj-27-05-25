namespace game_proj_27_05_25.Models
{
    public class FlashLight
    {
        public bool WasFound { get; set; }
        public int Power { get; set; }
        //конструктор для начала игры или сброса игрового прогресса
        public FlashLight()
        {
            WasFound = false;
            Power = 10;
        }
        //конструктор для продолжения игры, данные берутся из сессии
        public FlashLight(bool WasFound, int Power)
        {
            this.WasFound = WasFound;
            this.Power = Power;
        }
    }
}
