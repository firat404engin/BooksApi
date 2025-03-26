namespace BooksApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; } //burdaki soru işareti Null olabiliri ifade eder.

        public List<RentedBook>? RentedBooks { get; set; }
    }
}
