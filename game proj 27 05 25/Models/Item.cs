namespace game_proj_27_05_25.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public int X { get; set; }
        public int Y { get; set; }
        public InteractionType Interaction { get; set; } 
        public bool IsInteracted { get; set; }
    }

    public enum InteractionType
    {
        SimpleAction, //просто кликабельные объекты
        PickupDialog  //объекты с диалоговым окном, которые можно поднять
    }
}
