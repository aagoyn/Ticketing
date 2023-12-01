using Ticketing.Model;

namespace Ticketing.View
{
    internal class HistoryView
    {
        public static void ShowInvoice(User user, List<Ticket> purchasedTickets)
        {
            Console.WriteLine("\n=== HISTORY ===");
            Console.WriteLine("\n---Detail Tiket---");
            Console.WriteLine("Nama: " + user.Name);
            Console.WriteLine("Nomor KTP: " + user.KtpNumber);
            Console.WriteLine("Nomor Telepon: " + user.PhoneNumber);
            Console.WriteLine();

            if (purchasedTickets.Count == 0)
            {
                Console.WriteLine("Tidak ada tiket yang dibeli dalam riwayat ini.");
            }
            else
            {
                Console.WriteLine("Riwayat pembelian tiket Anda:");

                foreach (var ticket in purchasedTickets)
                {
                    Console.WriteLine("\nID Tiket: " + ticket.TicketId);
                    Console.WriteLine("Jenis Tiket: " + ticket.Type);
                    Console.WriteLine("Dari: " + ticket.From);
                    Console.WriteLine("Menuju: " + ticket.To);
                    Console.WriteLine("Tanggal Keberangkatan: " + ticket.DepartureDate.ToString("dd-MM-yyyy"));
                    Console.WriteLine("Nomor Tempat Duduk: " + ticket.SeatNumber);
                    Console.WriteLine("Harga: " + ticket.Price);
                    Console.WriteLine("Tanggal Pembelian: " + ticket.PurchaseDate.ToString("dd-MM-yyyy HH:mm:ss"));
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\n1. Kembali ke Menu Utama");
            Console.WriteLine("2. Keluar dari Aplikasi");
            Console.Write("Pilih menu: ");

            string option = Console.ReadLine();
            Console.WriteLine();

            switch (option)
            {
                case "1":
                    // Kembali ke Menu Utama
                    Program.Main();
                    break;
                case "2":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
        }
    }
}
