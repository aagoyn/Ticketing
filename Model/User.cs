using Ticketing.Model;

public class User
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string KtpNumber { get; set; }
    public List<Ticket> PurchasedTickets { get; set; } = new List<Ticket>();


    public User()
    {
    }

    // Konstruktor dengan parameter
    public User(string name, string phoneNumber, string ktpNumber)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        KtpNumber = ktpNumber;
    }
}
