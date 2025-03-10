using ClassTests.Models.Enums;

namespace ClassTests.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public string FirstNameApplication {  get; set; }

        public string LastNameApplication { get; set; }

        public string DiscordNameApplication { get; set; }

        public ApplicationStatus Status { get; set; }

        public ApplicationUser User { get; set; }

    }
}
