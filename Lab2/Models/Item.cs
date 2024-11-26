namespace Lab2.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public Item(string value)
        { 
            Value = value;
        }
    }
}