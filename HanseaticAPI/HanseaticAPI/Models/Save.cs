using System.Text.Json.Serialization;

namespace HanseaticAPI.Models
{
    public class Save
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [JsonIgnore]
        public Account? Account { get; set; } = null!;
        public int AccountId { get; set; }

    }
}
