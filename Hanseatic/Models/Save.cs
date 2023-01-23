namespace Hanseatic.Models
{
    public class Save
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Account? Account { get; set; } = null!;
        public int AccountId { get; set; }
    }
}
