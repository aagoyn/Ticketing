﻿namespace Ticketing.View;using Ticketing.Constant;using Ticketing.Service;using Ticketing.Model;class TicketView{    private readonly TicketService ticketService;    private List<Ticket> selectedTickets;    private string voucherCode;    private double discount = 0;    public TicketView(TicketService service)    {        ticketService = service;        selectedTickets = new List<Ticket>();    }    public void ShowTicketOptions(User user)    {        Console.WriteLine("Pilih Jenis Tiket:");        Console.WriteLine("1. Bus");        Console.WriteLine("2. Kereta");        Console.WriteLine("3. Pesawat");        Console.Write("Masukkan pilihan Anda: ");        var choice = Console.ReadLine();        switch (choice)        {            case "1":                ShowSchedule(TicketType.Bus, user);                break;            case "2":                ShowSchedule(TicketType.Kereta, user);                break;            case "3":                ShowSchedule(TicketType.Pesawat, user);                break;            default:                Console.WriteLine("Pilihan tidak valid.");                break;        }    }    public void ShowSchedule(TicketType ticketType, User user)    {        var availableTickets = ticketService.GetAvailableTickets(ticketType);        Console.WriteLine("Jadwal Keberangkatan untuk " + ticketType + ":");        for (int i = 0; i < availableTickets.Count; i++)        {            var ticket = availableTickets[i];            Console.WriteLine((i + 1) + ". ID Tiket: " + ticket.TicketId + ", Dari: " + ticket.From + ", Menuju: " + ticket.To + ", Tanggal: " + ticket.DepartureDate.ToString("dd-MM-yyyy") + ", Harga: " + ticket.Price);        }        Console.Write("Pilih jadwal: ");        if (int.TryParse(Console.ReadLine(), out int selectedScheduleIndex) && selectedScheduleIndex >= 1 && selectedScheduleIndex <= availableTickets.Count)        {            var selectedTicket = availableTickets[selectedScheduleIndex - 1];            ChooseSeat(selectedTicket, user);        }        else        {            Console.WriteLine("Pilihan jadwal tidak valid. Silakan coba lagi.");        }    }    public void ChooseSeat(Ticket selectedTicket, User user)    {        List<string> availableSeats = ticketService.GenerateAvailableSeats(ticketService.GetNumberOfSeatsByTicketType(selectedTicket.Type));        if (availableSeats == null || availableSeats.Count == 0)        {            Console.WriteLine("Tidak ada tempat duduk yang tersedia.");            return;        }        Console.WriteLine("Tempat duduk yang tersedia:");        for (int i = 0; i < availableSeats.Count; i++)        {            Console.WriteLine((i + 1) + ". " + availableSeats[i]);        }        Console.Write("Pilih tempat duduk: ");        if (int.TryParse(Console.ReadLine(), out int seatChoice) && seatChoice >= 1 && seatChoice <= availableSeats.Count)        {            string selectedSeat = availableSeats[seatChoice - 1];            DisplaySelectedTicket(selectedTicket, selectedSeat, user);        }        else        {            Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");        }    }    public void DisplaySelectedTicket(Ticket selectedTicket, string selectedSeat, User user)    {        Console.WriteLine("Anda telah memilih jadwal keberangkatan:");        Console.WriteLine("ID Tiket: " + selectedTicket.TicketId + " | " + selectedTicket.From + " - " + selectedTicket.To + ", Tanggal: " + selectedTicket.DepartureDate + ", Tempat Duduk: " + selectedSeat + ", Harga: " + selectedTicket.Price);        var ticket = new Ticket(selectedTicket.TicketId, selectedTicket.Type, selectedSeat, selectedTicket.From, selectedTicket.To, selectedTicket.DepartureDate, selectedTicket.Price);        ticket.User = user;        selectedTickets.Add(ticket);        Console.WriteLine("Tiket berhasil ditambahkan ke dalam keranjang Anda.");        Console.Write("Ingin membeli tiket lagi? (y/n): ");        var continueChoice = Console.ReadLine();        if (continueChoice.ToLower() == "y")        {            ShowTicketOptions(user);        }        else        {            CompleteTicketBooking(user);        }    }    public void CompleteTicketBooking(User user)    {        if (selectedTickets.Count == 0)        {            Console.WriteLine("Tidak ada tiket yang dipilih. Silakan pilih tiket terlebih dahulu.");            return;        }        Console.Write("Lanjutkan dengan pembelian tiket ini? (y/n): ");        var continueChoice = Console.ReadLine();        if (continueChoice.ToLower() == "y")        {            while (true)            {                Console.WriteLine("1. Checkout");                Console.WriteLine("2. Batalkan");                Console.Write("Pilih opsi: ");                var checkoutChoice = Console.ReadLine();                switch (checkoutChoice)                {                    case "1":                        Console.Write("Apakah Anda memiliki kode voucher? (y/n): ");                        var hasVoucher = Console.ReadLine();                        if (hasVoucher.ToLower() == "y")                        {                            Console.Write("Masukkan kode voucher: ");                            voucherCode = Console.ReadLine();                            if (voucherCode == "DISC10")                            {                                discount = 0.1; // 10% diskon
                            }                            else if (voucherCode == "DISC20")                            {                                discount = 0.2; // 20% diskon
                            }                            else if (voucherCode == "DISC30")                            {                                discount = 0.3; // 30% diskon
                            }                            else                            {                                discount = 0; // Tidak ada diskon
                            }                        }                        double totalHarga = selectedTickets.Sum(ticket => ticket.Price);                        double totalDiskon = totalHarga * discount;                        double hargaSetelahDiskon = totalHarga - totalDiskon;                        Console.WriteLine("Total Harga sebelum Diskon: " + totalHarga);                        Console.WriteLine("Diskon yang Anda dapatkan: " + totalDiskon);                        Console.WriteLine("Harga Setelah Diskon: " + hargaSetelahDiskon);

                        // Lanjutkan dengan mengisi data diri
                        Console.Write("Masukkan Nama Anda: ");                        string name = Console.ReadLine();                        Console.Write("Masukkan Nomor KTP Anda: ");                        string ktpNumber = Console.ReadLine();                        Console.Write("Masukkan Nomor Telepon Anda: ");                        string phoneNumber = Console.ReadLine();                        user.Name = name;                        user.PhoneNumber = phoneNumber;                        user.KtpNumber = ktpNumber;                        foreach (var ticket in selectedTickets)                        {                            ticket.User = user;                            ticketService.PurchaseTicket(user, ticket);                            Console.WriteLine("Tiket berhasil dibeli: " + ticket.TicketId);                        }                        DisplayInvoice(user);                        return;                    case "2":                        return;                    default:                        Console.WriteLine("Opsi tidak valid. Silakan pilih lagi.");                        break;                }            }        }        else        {            Console.WriteLine("Pembelian tiket dibatalkan.");        }    }    public void DisplayInvoice(User user)    {        Console.WriteLine("\n---INVOICE---");        Console.WriteLine("Nama: " + user.Name);        Console.WriteLine("Nomor KTP: " + user.KtpNumber);        Console.WriteLine("Nomor Telepon: " + user.PhoneNumber);        Console.WriteLine();        if (selectedTickets.Count == 0)        {            Console.WriteLine("Tidak ada tiket yang dibeli.");        }        else        {            Console.WriteLine("Tiket yang Anda beli:");            foreach (var ticket in selectedTickets)            {                Console.WriteLine("ID Tiket: " + ticket.TicketId);                Console.WriteLine("Jenis Tiket: " + ticket.Type);                Console.WriteLine("Dari: " + ticket.From);                Console.WriteLine("Menuju: " + ticket.To);                Console.WriteLine("Tanggal Keberangkatan: " + ticket.DepartureDate.ToString("dd-MM-yyyy"));                Console.WriteLine("Nomor Tempat Duduk: " + ticket.SeatNumber);                Console.WriteLine("Harga: " + ticket.Price);                Console.WriteLine();            }        }        Console.WriteLine("\n1. Kembali ke Menu Utama");        Console.WriteLine("2. Keluar dari Aplikasi");        Console.Write("Pilih menu: ");        string option = Console.ReadLine();        Console.WriteLine();        switch (option)        {            case "1":                Program.Main();                break;            case "2":                Environment.Exit(0);                break;            default:                Console.WriteLine("Pilihan tidak valid.");                break;        }    }}