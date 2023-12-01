namespace Ticketing.Service;

using Ticketing.Constant;
using Ticketing.IService;
using Ticketing.Model;


public class TicketService : ITicketService
{
    private List<Ticket> tickets;

    public TicketService()
    {
        tickets = new List<Ticket>(); // Inisialisasi daftar tiket
        tickets.Add(new Ticket("B125", TicketType.Bus, "1A", "Bandung", "Jakarta", DateTime.Now.AddDays(1), 149000));
        tickets.Add(new Ticket("B413", TicketType.Bus, "4A", "Jogja", "Bandung", DateTime.Now.AddDays(1), 1490000));
        tickets.Add(new Ticket("B956", TicketType.Bus, "3A", "Jakarta", "Jogja", DateTime.Now.AddDays(2), 149000));
        tickets.Add(new Ticket("B965", TicketType.Bus, "6A", "Bekasi", "Jakarta", DateTime.Now.AddDays(2), 149000));
        tickets.Add(new Ticket("B653", TicketType.Bus, "2A", "Bandung", "Jakarta", DateTime.Now.AddDays(3), 149000));
        tickets.Add(new Ticket("B562", TicketType.Bus, "5A", "Jakarta", "Purwakarrta", DateTime.Now.AddDays(3), 149000));

        tickets.Add(new Ticket("T451", TicketType.Kereta, "2A", "Jakarta", "Bandung", DateTime.Now.AddDays(1), 162000));
        tickets.Add(new Ticket("T236", TicketType.Kereta, "3A", "Jakarta", "Jogja", DateTime.Now.AddDays(1), 117000));
        tickets.Add(new Ticket("T986", TicketType.Kereta, "3C", "Jogja", "Bandung", DateTime.Now.AddDays(1), 111000));
        tickets.Add(new Ticket("T653", TicketType.Kereta, "2C", "Bandung", "Malang", DateTime.Now.AddDays(1), 111000));
        tickets.Add(new Ticket("T045", TicketType.Kereta, "3A", "Jakarta", "Surabaya", DateTime.Now.AddDays(2), 165000));
        tickets.Add(new Ticket("T635", TicketType.Kereta, "3A", "Jakarta", "Purwakarrta", DateTime.Now.AddDays(2), 117000));
        tickets.Add(new Ticket("T652", TicketType.Kereta, "2B", "Bandung", "Jakarta", DateTime.Now.AddDays(3), 157000));
        tickets.Add(new Ticket("T458", TicketType.Kereta, "1A", "Bekasi", "Jakarta", DateTime.Now.AddDays(3), 157000));

        tickets.Add(new Ticket("P635", TicketType.Pesawat, "2A", "Jakarta", "Bandung", DateTime.Now.AddDays(1), 1600000));
        tickets.Add(new Ticket("P4781", TicketType.Pesawat, "1B", "Jakarta", "Bandung", DateTime.Now.AddDays(1), 1350000));
        tickets.Add(new Ticket("P659", TicketType.Pesawat, "3B", "Jakarta", "Bandung", DateTime.Now.AddDays(1), 1600000));
        tickets.Add(new Ticket("P302", TicketType.Pesawat, "3C", "Jakarta", "Bandung", DateTime.Now.AddDays(1), 1650000));
        tickets.Add(new Ticket("P515", TicketType.Pesawat, "1A", "Bandung", "Jakarta", DateTime.Now.AddDays(2), 125000));
        tickets.Add(new Ticket("P361", TicketType.Pesawat, "3A", "Bandung", "Jakarta", DateTime.Now.AddDays(2), 1980000));
        tickets.Add(new Ticket("P103", TicketType.Pesawat, "2B", "Bandung", "Jakarta", DateTime.Now.AddDays(2), 125000));
        tickets.Add(new Ticket("P451", TicketType.Pesawat, "2C", "Bandung", "Jakarta", DateTime.Now.AddDays(2), 1980000));
        tickets.Add(new Ticket("P6582", TicketType.Pesawat, "4A", "Jakarta", "Bandung", DateTime.Now.AddDays(3), 1350000));
        tickets.Add(new Ticket("P158", TicketType.Pesawat, "1C", "Bandung", "Jakarta", DateTime.Now.AddDays(3), 1490000));
    }


    public List<Ticket> GetAvailableTickets(TicketType type, string departure = "", string arrival = "")
    {
        List<Ticket> availableTickets = new List<Ticket>();
        foreach (var ticket in tickets)
        {
            if (ticket.Type == type)
            {
                if (string.IsNullOrEmpty(departure) && string.IsNullOrEmpty(arrival))
                {
                    availableTickets.Add(ticket);
                }
                else if (ticket.From == departure && ticket.To == arrival)
                {
                    availableTickets.Add(ticket);
                }
            }
        }
        return availableTickets;
    }

    public void PurchaseTicket(User user, Ticket ticket)
    {
        ticket.PurchaseDate = DateTime.Now;

        if (!user.PurchasedTickets.Contains(ticket))
        {
            user.PurchasedTickets.Add(ticket); // Add the ticket only if it's not already in the list
        }
    }

    public int GetNumberOfSeatsByTicketType(TicketType ticketType)
    {
        return ticketType switch
        {
            TicketType.Pesawat => 10,
            TicketType.Kereta => 8,
            TicketType.Bus => 6,
            _ => 0
        };
    }

    public List<string> GenerateAvailableSeats(int numberOfSeats)
    {
        List<string> seats = new List<string>();

        for (int i = 1; i <= numberOfSeats; i++)
        {
            char seatLetter = (char)('A' + (i - 1));
            seats.Add($"{i}{seatLetter}");
        }

        return seats;
    }

    public bool IsSeatAvailable(string seatNumber, DateTime departureDate)
    {
        foreach (var ticket in tickets)
        {
            if (ticket.SeatNumber == seatNumber && ticket.DepartureDate == departureDate)
            {
                return false;
            }
        }
        return true;
    }

    public Ticket GetTicketDetails(string ticketId)
    {
        foreach (var ticket in tickets)
        {
            if (ticket.TicketId == ticketId)
            {
                return ticket;
            }
        }
        return null; // Jika tidak ditemukan
    }

    public List<Ticket> GetPurchaseHistory(string ktpNumber)
    {
        List<Ticket> purchaseHistory = new List<Ticket>();
        foreach (var ticket in tickets)
        {
            if (ticket.User != null && ticket.User.KtpNumber == ktpNumber)
            {
                purchaseHistory.Add(ticket);
            }
        }
        return purchaseHistory;
    }


}
