namespace Ticketing.IService;

using Ticketing.Model;
using Ticketing.Constant;


interface ITicketService
{
    List<Ticket> GetAvailableTickets(TicketType type, string departure, string arrival); //metode untuk mendapatkan daftar tiket yang tersedia berdasarkan jenis tiket
    void PurchaseTicket(User user, Ticket ticket); //metode untuk membeli tiket
    List<Ticket> GetPurchaseHistory(string ktpNumber); //metode untuk mendapatkan riwayat pembelian tiket berdasarkan nomor KTP
    int GetNumberOfSeatsByTicketType(TicketType ticketType); //metode untuk mendapatkan jumlah tempat duduk berdasarkan jenis tiket
    List<string> GenerateAvailableSeats(int numberOfSeats); //metode untuk menghasilkan daftar tempat duduk yang tersedia
    bool IsSeatAvailable(string seatNumber, DateTime departureDate); //metode untuk memeriksa ketersediaan tempat duduk
    Ticket GetTicketDetails(string ticketId); //metode untuk mendapatkan detail tiket berdasarkan ID tiket
}