namespace Ticketing.Model;

using Ticketing.Constant;

public class Ticket
{
    public string TicketId { get; set; }
    public TicketType Type { get; set; }
    public User User { get; set; }
    public string SeatNumber { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public DateTime DepartureDate { get; set; }
    public int Price { get; set; }
    public DateTime PurchaseDate { get; set; }

    // Const
    public Ticket(string ticketId, TicketType type, string seatNumber, string from, string to, DateTime departureDate, int price)
    {
        TicketId = ticketId;
        Type = type;
        SeatNumber = seatNumber;
        From = from;
        To = to;
        DepartureDate = departureDate;
        Price = price;
    }

}
