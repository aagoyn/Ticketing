namespace Ticketing.View;

using Ticketing.Service;

class MainView
{
    private static User currentUser; // Make currentUser static

    public void ShowMainMenu()
    {
        Console.WriteLine("=== Ticket BRO ===");
        Console.WriteLine("1. Beli Tiket");
        Console.WriteLine("2. Histori Pembelian");
        Console.WriteLine("3. Keluar Aplikasi");

        Console.Write("Masukkan pilihan Anda: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                if (currentUser == null)
                {
                    currentUser = new User(); // Initialize only if null
                }
                TicketService ticketService = new TicketService();
                TicketView ticketView = new TicketView(ticketService);
                ticketView.ShowTicketOptions(currentUser); // Pass currentUser
                break;
            case "2":
                if (currentUser != null && currentUser.PurchasedTickets.Count > 0)
                {
                    HistoryView.ShowInvoice(currentUser, currentUser.PurchasedTickets);
                }
                else
                {
                    Console.WriteLine("Belum ada riwayat pembelian.");
                }
                break;
            case "3":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Pilihan tidak valid.");
                break;
        }
    }
}
