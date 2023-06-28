namespace ODataBookStore.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }

        public virtual IEnumerable<User>? Users { get; set; }
        public virtual IEnumerable<Account>? Accounts { get; set; }
    }
}
