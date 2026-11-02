using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    public class Pojazd
    {
        private string _numer_seryjny;

        public string Marka { get; set; }
        public uint Rok_Produkcji { get; set; }

        public string Numer_Seryjny
        {
            get { return _numer_seryjny; }
            protected set { _numer_seryjny = value; }
        }

        public Pojazd(string marka, uint rok)
        {
            Marka = marka;
            Rok_Produkcji = rok;
            Numer_Seryjny = "BRAK";
        }

        public void Wyswietl_Podstawowe_Informacje()
        {
            Console.WriteLine($"Marka: {Marka}, rok produkcji: {Rok_Produkcji}, numer seryjny: {Numer_Seryjny}");
        }
    }

    public class Samochod_Osobowy : Pojazd
    {
        public int Liczba_Drzwi { get; set; }
        public double Pojemnosc_Silnika { get; set; }

        public Samochod_Osobowy(string marka, uint rok, int drzwi, double pojemnosc, string numer)
            : base(marka, rok)
        {
            Liczba_Drzwi = drzwi;
            Pojemnosc_Silnika = pojemnosc;
            Numer_Seryjny = numer;
        }

        public void Wyswietl_Szczegoly()
        {
            Console.WriteLine($"Liczba drzwi: {Liczba_Drzwi}, pojemność silnika: {Pojemnosc_Silnika} l");
        }
    }

    public class Wyswietlacz_Tekstu
    {
        public static int Licznik_Wyswietlen = 0;

        public void Wyswietl(string tekst)
        {
            Console.WriteLine(tekst);
            Licznik_Wyswietlen++;
        }

        public void Wyswietl(int liczba)
        {
            Console.WriteLine(liczba);
            Licznik_Wyswietlen++;
        }

        public void Wyswietl(decimal kwota)
        {
            Console.WriteLine($"{kwota:F2} zł");
            Licznik_Wyswietlen++;
        }

        public void Wyswietl_Licznik()
        {
            Console.WriteLine($"Liczba wyświetleń: {Licznik_Wyswietlen}");
        }
    }

    public class Pracownik
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public virtual decimal Oblicz_Wynagrodzenie()
        {
            return 5000M;
        }
    }

    public class Pracownik_Etatowy : Pracownik
    {
        public uint Liczba_Godzin_Pracy_W_Miesiacu { get; set; }
        public decimal Stawka_Godzinowa { get; set; }
        public decimal Premia { get; set; }

        public override decimal Oblicz_Wynagrodzenie()
        {
            return Stawka_Godzinowa * Liczba_Godzin_Pracy_W_Miesiacu + Premia;
        }
    }

    public class Pracownik_Godzinowy : Pracownik
    {
        public uint Liczba_Godzin_Pracy_W_Miesiacu { get; set; }
        public decimal Stawka_Godzinowa { get; set; }

        public override decimal Oblicz_Wynagrodzenie()
        {
            return Liczba_Godzin_Pracy_W_Miesiacu * Stawka_Godzinowa;
        }
    }

    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Zadanie 1");
            Samochod_Osobowy samochod = new Samochod_Osobowy("Toyota", 2020, 5, 1.8, "ABC123456");
            samochod.Wyswietl_Podstawowe_Informacje();
            samochod.Wyswietl_Szczegoly();

            Console.WriteLine("\nZadanie 2");
            Wyswietlacz_Tekstu wyswietlacz = new Wyswietlacz_Tekstu();
            wyswietlacz.Wyswietl("Przykładowy tekst");
            wyswietlacz.Wyswietl(25);
            wyswietlacz.Wyswietl(99.99M);
            wyswietlacz.Wyswietl_Licznik();

            Console.WriteLine("\nZadanie 3");
            List<Pracownik> pracownicy = new List<Pracownik>();
            pracownicy.Add(new Pracownik_Etatowy
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Liczba_Godzin_Pracy_W_Miesiacu = 160,
                Stawka_Godzinowa = 30M,
                Premia = 500M
            });
            pracownicy.Add(new Pracownik_Godzinowy
            {
                Imie = "Anna",
                Nazwisko = "Nowak",
                Liczba_Godzin_Pracy_W_Miesiacu = 120,
                Stawka_Godzinowa = 35M
            });

            decimal suma = 0M;
            foreach (Pracownik pracownik in pracownicy)
            {
                decimal wynagrodzenie = pracownik.Oblicz_Wynagrodzenie();
                Console.WriteLine($"{pracownik.Imie} {pracownik.Nazwisko}: {wynagrodzenie:F2} zł");
                suma += wynagrodzenie;
            }

            Console.WriteLine($"Suma wynagrodzeń: {suma:F2} zł");
        }
    }
}
