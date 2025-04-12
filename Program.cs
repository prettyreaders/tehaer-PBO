using System;

class Program
{
    static void Main()
    {
        Karyawan karyawan;

        while (true)
        {
            Console.WriteLine("=== Program Hitung Gaji Karyawan ===");
            Console.WriteLine("Pilih Tipe Karyawan (Masukkan Angka):");
            Console.WriteLine("1. Karyawan Tetap");
            Console.WriteLine("2. Karyawan Kontrak");
            Console.WriteLine("3. Karyawan Magang");
            Console.WriteLine("0. Keluar");
            Console.Write("Pilihan Anda: ");
            string pilihan = Console.ReadLine();

            switch (pilihan)
            {
                case "1":
                    karyawan = new KaryawanTetap();
                    break;
                case "2":
                    karyawan = new KaryawanKontrak();
                    break;
                case "3":
                    karyawan = new KaryawanMagang();
                    break;
                case "0":
                    Console.WriteLine("Terima kasih! Program dihentikan.");
                    return; 
                default:
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.\n");
                    continue; 
            }

            AmbilDataKaryawan(karyawan);

            TampilkanRingkasan(karyawan);
        }
    }

    static void AmbilDataKaryawan(Karyawan karyawan)
    {
        while (string.IsNullOrEmpty(karyawan.Nama))
        {
            Console.Write("Masukkan nama karyawan: ");
            karyawan.Nama = Console.ReadLine();
        }

        while (string.IsNullOrEmpty(karyawan.ID))
        {
            Console.Write("Masukkan ID karyawan: ");
            karyawan.ID = Console.ReadLine();
        }

        while (karyawan.GajiPokok <= 0)
        {
            Console.Write("Masukkan Gaji Pokok karyawan: ");
            string inputGaji = Console.ReadLine();
            if (double.TryParse(inputGaji, out double gaji) && gaji > 0)
            {
                karyawan.GajiPokok = gaji;
            }
            else
            {
                Console.WriteLine("Inputan harus berupa angka positif.");
            }
        }
    }

    static void TampilkanRingkasan(Karyawan karyawan)
    {
        Console.WriteLine($"\n=== RANGKUMAN ===");
        Console.WriteLine($"Nama Karyawan    : {karyawan.Nama}");
        Console.WriteLine($"Tipe Karyawan    : {karyawan.TipeKaryawan}");
        Console.WriteLine($"ID Karyawan      : {karyawan.ID}");
        Console.WriteLine($"Total Gaji       : Rp{(karyawan.HitungGaji()).ToString("N0").Replace(",", ".")}");
        Console.WriteLine("Tekan Enter untuk melanjutkan...");
        Console.ReadLine();
    }
}

class Karyawan
{
    private const double MinimumGaji = 1_500_000; 
    public string TipeKaryawan { get; private set; }
    private string _nama;
    private string _id;
    private double _gajiPokok;

    public Karyawan(string tipeKaryawan)
    {
        TipeKaryawan = tipeKaryawan;
    }

    public string Nama
    {
        get => _nama;
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && value.All(char.IsLetter))
            {
                _nama = CapitalizeWords(value);
            }
            else
            {
                Console.WriteLine("Nama hanya boleh mengandung huruf.");
            }
        }
    }

    public string ID
    {
        get => _id;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _id = value;
            }
            else
            {
                Console.WriteLine("ID tidak boleh kosong.");
            }
        }
    }

    public double GajiPokok
    {
        get => _gajiPokok;
        set
        {
            if (value >= MinimumGaji)
            {
                _gajiPokok = value;
            }
            else
            {
                Console.WriteLine($"Gaji Pokok harus lebih dari {MinimumGaji}.");
            }
        }
    }

    public virtual double HitungGaji()
    {
        return _gajiPokok;
    }

    private string CapitalizeWords(string input)
    {
        var words = input.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > 0)
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
            }
        }
        return string.Join(" ", words);
    }
}

class KaryawanTetap : Karyawan
{
    private const int BonusTetap = 500_000;

    public KaryawanTetap() : base("Karyawan Tetap") { }

    public override double HitungGaji()
    {
        return GajiPokok + BonusTetap;
    }
}

class KaryawanKontrak : Karyawan
{
    private const double PotonganKontrak = 200_000;

    public KaryawanKontrak() : base("Karyawan Kontrak") { }

    public override double HitungGaji()
    {
        Console.WriteLine($"Potongan Gaji    : {PotonganKontrak}");
        return GajiPokok - PotonganKontrak;
    }
}

class KaryawanMagang : Karyawan
{
    public KaryawanMagang() : base("Karyawan Magang") { }
}
