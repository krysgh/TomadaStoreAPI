namespace TomadaStore.Models.Models
{
    public class Customer
    {
        public int Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string? PhoneNumber { get; private set; }

        public bool Status { get; private set; }

        public Customer(string firstName, string lastName, string email)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Status = true;
        }

        public Customer(string firstName, string lastName, 
                        string email, string? phoneNumber) 
                        : this(firstName, lastName, email)
        {
            this.PhoneNumber = phoneNumber;
        }
    }
}
