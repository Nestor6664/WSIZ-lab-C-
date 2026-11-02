using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class Konto_Klienta
    {
        private readonly string _numer_pesel;
        private decimal _saldo;
        private decimal _limit_debetowy;

        public string Pesel
        {
            get { return _numer_pesel; }
        }

        public string Identyfikator_Konta { get; init; }

        public decimal Limit_Debetowy
        {
            get { return _limit_debetowy; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Limit debetowy nie może być ujemny.");

                _limit_debetowy = value;
            }
        }

        public decimal Dostepne_Srodki
        {
            get { return _saldo + Limit_Debetowy; }
        }

        public Konto_Klienta(string pesel, decimal saldo, string identyfikator)
        {
            _numer_pesel = pesel;
            _saldo = saldo;
            Identyfikator_Konta = identyfikator;
        }
    }

    public class Pojazd_Magazynowy
    {
        private static int _licznik_pojazdow = 0;
        private readonly string _numer_rejestracyjny;

        public string Numer_Rejestracyjny
        {
            get { return _numer_rejestracyjny; }
        }

        public double Maksymalny_Udzwig_Kg { get; set; }
        public bool Czy_Sprawny { get; set; }

        public Pojazd_Magazynowy(string numer, double udzwig, bool sprawny)
        {
            _numer_rejestracyjny = numer;
            Maksymalny_Udzwig_Kg = udzwig;
            Czy_Sprawny = sprawny;
            _licznik_pojazdow++;
        }

        public Pojazd_Magazynowy(string numer, double udzwig) : this(numer, udzwig, true)
        {
        }

        public Pojazd_Magazynowy() : this("BRAK", 500.0)
        {
        }

        public static int Pobierz_Liczbe_Pojazdow()
        {
            return _licznik_pojazdow;
        }
    }

    public class Bufor_Liczbowy
    {
        private int[] _liczby;

        public int Pojemnosc
        {
            get { return _liczby.Length; }
        }

        public Bufor_Liczbowy(int rozmiar)
        {
            if (rozmiar < 0)
                throw new ArgumentException("Rozmiar nie może być ujemny.");

            _liczby = new int[rozmiar];
        }

        public int this[int indeks]
        {
            get
            {
                if (indeks >= 0 && indeks < Pojemnosc)
                    return _liczby[indeks];

                return -1;
            }
            set
            {
                if (indeks >= 0 && indeks < Pojemnosc && value >= 0)
                    _liczby[indeks] = value;
            }
        }
    }

    public class Katalog_Produktow
    {
        private Dictionary<string, int> _magazyn = new Dictionary<string, int>();

        public int this[string nazwa]
        {
            get
            {
                if (_magazyn.ContainsKey(nazwa))
                    return _magazyn[nazwa];

                return 0;
            }
            set
            {
                if (value >= 0)
                    _magazyn[nazwa] = value;
            }
        }

        public void Dodaj_Dostawe(string nazwa_produktu, int ilosc)
        {
            if (ilosc > 0)
                this[nazwa_produktu] = checked(this[nazwa_produktu] + ilosc);
        }

        public void Dodaj_Dostawe(string nazwa_produktu)
        {
            Dodaj_Dostawe(nazwa_produktu, 1);
        }
    }

    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Zadanie 1");
            Konto_Klienta konto = new Konto_Klienta("00000000000", 1000M, "K001");
            Console.WriteLine($"PESEL: {konto.Pesel}, konto: {konto.Identyfikator_Konta}");
            Console.WriteLine($"Dostępne środki: {konto.Dostepne_Srodki:F2} zł");
            konto.Limit_Debetowy = 500M;
            Console.WriteLine($"Po ustawieniu limitu: {konto.Dostepne_Srodki:F2} zł");

            try
            {
                konto.Limit_Debetowy = -100M;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine($"Limit po błędnej próbie: {konto.Limit_Debetowy:F2} zł");

            Console.WriteLine("\nZadanie 2");
            Pojazd_Magazynowy pojazd1 = new Pojazd_Magazynowy("RZ123", 1500, false);
            Pojazd_Magazynowy pojazd2 = new Pojazd_Magazynowy("RZ456", 1000);
            Pojazd_Magazynowy pojazd3 = new Pojazd_Magazynowy();

            Pojazd_Magazynowy[] pojazdy = { pojazd1, pojazd2, pojazd3 };
            foreach (Pojazd_Magazynowy pojazd in pojazdy)
                Console.WriteLine($"{pojazd.Numer_Rejestracyjny}, udźwig: {pojazd.Maksymalny_Udzwig_Kg} kg, sprawny: {pojazd.Czy_Sprawny}");

            Console.WriteLine($"Liczba pojazdów: {Pojazd_Magazynowy.Pobierz_Liczbe_Pojazdow()}");

            Console.WriteLine("\nZadanie 3");
            Bufor_Liczbowy bufor = new Bufor_Liczbowy(4);
            bufor[0] = 10;
            bufor[1] = 20;
            bufor[2] = 30;
            bufor[3] = 40;
            bufor[1] = -5;
            bufor[4] = 50;
            bufor[-1] = 60;

            for (int i = 0; i < bufor.Pojemnosc; i++)
                Console.WriteLine($"bufor[{i}] = {bufor[i]}");

            Console.WriteLine($"Odczyt spoza zakresu: {bufor[4]}");

            Console.WriteLine("\nZadanie 4");
            Katalog_Produktow katalog = new Katalog_Produktow();
            katalog["Chleb"] = 10;
            katalog["Mleko"] = 5;
            katalog["Jajka"] = 12;
            katalog["Mleko"] = 8;
            katalog["Chleb"] = -3;
            katalog.Dodaj_Dostawe("Chleb", 5);
            katalog.Dodaj_Dostawe("Mleko");
            katalog.Dodaj_Dostawe("Masło", 4);

            string[] produkty = { "Chleb", "Mleko", "Jajka", "Masło", "Ser" };
            foreach (string produkt in produkty)
                Console.WriteLine($"{produkt}: {katalog[produkt]} szt.");
        }
    }
}
