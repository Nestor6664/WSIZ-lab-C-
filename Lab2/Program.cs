using System;
using System.Text;

namespace Lab2
{
    public enum Typ_Paliwa
    {
        Benzyna,
        Elektryczny,
        LPG
    }

    public class Samochod
    {
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Rok_Produkcji { get; set; }
        public Typ_Paliwa Paliwo { get; set; }

        public Samochod() : this("Toyota", "Corolla", 2015, Typ_Paliwa.Benzyna)
        {
        }

        public Samochod(string marka, string model, int rok_Produkcji, Typ_Paliwa paliwo)
        {
            Marka = marka;
            Model = model;
            Rok_Produkcji = rok_Produkcji;
            Paliwo = paliwo;
        }

        public Samochod(string marka, string model, int rok_Produkcji, string paliwo)
        {
            Marka = marka;
            Model = model;
            Rok_Produkcji = rok_Produkcji;

            switch (paliwo)
            {
                case "Benzyna":
                    Paliwo = Typ_Paliwa.Benzyna;
                    break;
                case "Elektryczny":
                    Paliwo = Typ_Paliwa.Elektryczny;
                    break;
                case "LPG":
                    Paliwo = Typ_Paliwa.LPG;
                    break;
                default:
                    throw new ArgumentException("Nieprawidłowy typ paliwa.");
            }
        }

        public void Wyswietl_Informacje()
        {
            Console.WriteLine($"Marka: {Marka}, model: {Model}, rok produkcji: {Rok_Produkcji}, paliwo: {Paliwo}");
        }
    }

    public class Konto_Bankowe
    {
        private decimal _saldo;
        private int _liczba_operacji;

        public string Wlasciciel { get; set; }

        public decimal Saldo
        {
            get { return _saldo; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Saldo nie może być ujemne.");

                _saldo = value;
            }
        }

        public int Liczba_Operacji
        {
            get { return _liczba_operacji; }
        }

        public Konto_Bankowe() : this("N/A", 0M)
        {
        }

        public Konto_Bankowe(string wlasciciel, decimal saldo)
        {
            Wlasciciel = wlasciciel;
            Saldo = saldo;
            _liczba_operacji = 0;
        }

        public void Wplac(decimal Kwota)
        {
            if (Kwota <= 0)
            {
                Console.WriteLine("Kwota wpłaty musi być dodatnia.");
                return;
            }

            Saldo += Kwota;
            _liczba_operacji++;
        }

        public void Wyplac(decimal Kwota)
        {
            if (Kwota <= 0)
            {
                Console.WriteLine("Kwota wypłaty musi być dodatnia.");
                return;
            }

            if (Kwota > Saldo)
            {
                Console.WriteLine("Brak wystarczających środków na koncie.");
                return;
            }

            Saldo -= Kwota;
            _liczba_operacji++;
        }

        public void Wyswietl_Informacje()
        {
            Console.WriteLine($"Właściciel: {Wlasciciel}, saldo: {Saldo:F2} zł, liczba operacji: {Liczba_Operacji}");
        }
    }

    public class Bilet_Do_Kina
    {
        public static int Licznik_Sprzedanych_Biletow = 0;

        public string Nazwa_Filmu { get; set; }
        public int Rzad { get; set; }
        public int Miejsce { get; set; }
        public decimal Cena { get; set; }

        public Bilet_Do_Kina() : this("Avatar", 1, 1, 25M)
        {
        }

        public Bilet_Do_Kina(string nazwa_Filmu, int rzad, int miejsce, decimal cena)
        {
            Nazwa_Filmu = nazwa_Filmu;
            Rzad = rzad;
            Miejsce = miejsce;
            Cena = cena;
            Licznik_Sprzedanych_Biletow++;
        }

        public static void Podsumowanie_Sprzedazy()
        {
            Console.WriteLine($"Liczba sprzedanych biletów: {Licznik_Sprzedanych_Biletow}");
        }
    }

    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Zadanie 1");
            Samochod samochod1 = new Samochod();
            Samochod samochod2 = new Samochod("Tesla", "Model 3", 2023, Typ_Paliwa.Elektryczny);
            Samochod samochod3 = new Samochod("Opel", "Astra", 2010, "LPG");

            samochod1.Wyswietl_Informacje();
            samochod2.Wyswietl_Informacje();
            samochod3.Wyswietl_Informacje();

            Console.WriteLine("Samochód przed zmianą:");
            samochod1.Wyswietl_Informacje();
            samochod1.Paliwo = Typ_Paliwa.LPG;
            Console.WriteLine("Samochód po zmianie:");
            samochod1.Wyswietl_Informacje();

            Console.WriteLine("\nZadanie 2");
            Konto_Bankowe konto = new Konto_Bankowe("Jan Kowalski", 1000M);
            konto.Wyswietl_Informacje();

            try
            {
                Konto_Bankowe kontoUjemne = new Konto_Bankowe("Adam Nowak", -100M);
                kontoUjemne.Wyswietl_Informacje();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            konto.Wplac(500M);
            konto.Wyplac(200M);
            konto.Wplac(100M);
            konto.Wyplac(50M);
            konto.Wplac(-20M);
            konto.Wyplac(0M);
            konto.Wyplac(2000M);
            konto.Wyswietl_Informacje();

            Console.WriteLine("\nZadanie 3");
            Bilet_Do_Kina.Podsumowanie_Sprzedazy();

            Bilet_Do_Kina bilet1 = new Bilet_Do_Kina();
            Bilet_Do_Kina bilet2 = new Bilet_Do_Kina("Interstellar", 3, 5, 30M);
            Bilet_Do_Kina bilet3 = new Bilet_Do_Kina("Incepcja", 4, 8, 28M);

            Console.WriteLine($"{bilet1.Nazwa_Filmu}, rząd: {bilet1.Rzad}, miejsce: {bilet1.Miejsce}, cena: {bilet1.Cena:F2} zł");
            Console.WriteLine($"{bilet2.Nazwa_Filmu}, rząd: {bilet2.Rzad}, miejsce: {bilet2.Miejsce}, cena: {bilet2.Cena:F2} zł");
            Console.WriteLine($"{bilet3.Nazwa_Filmu}, rząd: {bilet3.Rzad}, miejsce: {bilet3.Miejsce}, cena: {bilet3.Cena:F2} zł");

            Bilet_Do_Kina.Podsumowanie_Sprzedazy();
        }
    }
}
