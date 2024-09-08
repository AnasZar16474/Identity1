namespace Identity1.Models.ViewModel
{
    public class DisplayUsersViewModel
    {
        public string id { get; set; }
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string City { get; set; } = null!;

        public override string ToString()
        {
            return $"...{Name}...{Gender}...{City}";
        }
    }
}
