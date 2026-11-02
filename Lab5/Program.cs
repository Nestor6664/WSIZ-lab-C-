using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    public abstract class Figura_Geometryczna
    {
        public string Nazwa { get; protected set; }

        protected Figura_Geometryczna(string nazwa)
        {
            Nazwa = nazwa;
        }

        public abstract void Wyswietl_Informacje();
    }

    public abstract class Figura_2D : Figura_Geometryczna
    {
        protected Figura_2D(string nazwa) : base(nazwa)
        {
        }

        public abstract decimal Pole();
        public abstract decimal Obwod();
    }

    public abstract class Figura_3D : Figura_Geometryczna
    {
        protected Figura_3D(string nazwa) : base(nazwa)
        {
        }

        public abstract decimal Objetosc();
        public abstract decimal Pole_Powierzchni();
    }

    public class Kwadrat : Figura_2D
    {
        public decimal Dlugosc_Boku { get; private set; }

        public Kwadrat(decimal bok) : base("Kwadrat")
        {
            if (bok <= 0)
                throw new ArgumentException("Bok musi być dodatni.");

            Dlugosc_Boku = bok;
        }

        public override decimal Pole()
        {
            return Dlugosc_Boku * Dlugosc_Boku;
        }

        public override decimal Obwod()
        {
            return 4 * Dlugosc_Boku;
        }

        public override void Wyswietl_Informacje()
        {
            Console.WriteLine($"{Nazwa}, bok: {Dlugosc_Boku}, pole: {Pole():F2}, obwód: {Obwod():F2}");
        }
    }

    public class Kolo : Figura_2D
    {
        public decimal Promien { get; private set; }

        public Kolo(decimal promien) : base("Koło")
        {
            if (promien <= 0)
                throw new ArgumentException("Promień musi być dodatni.");

            Promien = promien;
        }

        public override decimal Pole()
        {
            return (decimal)Math.PI * Promien * Promien;
        }

        public override decimal Obwod()
        {
            return 2 * (decimal)Math.PI * Promien;
        }

        public override void Wyswietl_Informacje()
        {
            Console.WriteLine($"{Nazwa}, promień: {Promien}, pole: {Pole():F2}, obwód: {Obwod():F2}");
        }
    }

    public class Szescian : Figura_3D
    {
        public decimal Dlugosc_Krawedzi { get; private set; }

        public Szescian(decimal krawedz) : base("Sześcian")
        {
            if (krawedz <= 0)
                throw new ArgumentException("Krawędź musi być dodatnia.");

            Dlugosc_Krawedzi = krawedz;
        }

        public override decimal Objetosc()
        {
            return Dlugosc_Krawedzi * Dlugosc_Krawedzi * Dlugosc_Krawedzi;
        }

        public override decimal Pole_Powierzchni()
        {
            return 6 * Dlugosc_Krawedzi * Dlugosc_Krawedzi;
        }

        public override void Wyswietl_Informacje()
        {
            Console.WriteLine($"{Nazwa}, krawędź: {Dlugosc_Krawedzi}, objętość: {Objetosc():F2}, pole powierzchni: {Pole_Powierzchni():F2}");
        }
    }

    public interface I_Tankowalny
    {
        void Zatankuj(decimal ilosc_litrow);
    }

    public interface I_Elektryczny
    {
        void Naladuj(decimal ilosc_energii);
    }

    public abstract class Pojazd
    {
        public string Marka { get; protected set; }
        public uint Rok_Produkcji { get; protected set; }

        protected Pojazd(string marka, uint rok)
        {
            Marka = marka;
            Rok_Produkcji = rok;
        }

        public abstract void Wyswietl_Informacje();
    }

    public abstract class Pojazd_Ladowy : Pojazd
    {
        protected Pojazd_Ladowy(string marka, uint rok) : base(marka, rok)
        {
        }

        public abstract decimal Oblicz_Zasieg();
        public abstract void Uruchom_Silnik();
    }

    public abstract class Pojazd_Powietrzny : Pojazd
    {
        protected Pojazd_Powietrzny(string marka, uint rok) : base(marka, rok)
        {
        }

        public abstract decimal Maksymalny_Pulap();
    }

    public class Samochod : Pojazd_Ladowy, I_Tankowalny
    {
        public decimal Pojemnosc_Zbiornika { get; private set; }
        public decimal Srednie_Spalanie { get; private set; }

        public Samochod(string marka, uint rok, decimal pojemnosc, decimal spalanie) : base(marka, rok)
        {
            if (pojemnosc <= 0 || spalanie <= 0)
                throw new ArgumentException("Pojemność zbiornika i spalanie muszą być dodatnie.");

            Pojemnosc_Zbiornika = pojemnosc;
            Srednie_Spalanie = spalanie;
        }

        public override decimal Oblicz_Zasieg()
        {
            return Pojemnosc_Zbiornika / Srednie_Spalanie * 100;
        }

        public override void Uruchom_Silnik()
        {
            Console.WriteLine($"{Marka}: uruchomiono silnik spalinowy.");
        }

        public override void Wyswietl_Informacje()
        {
            Console.WriteLine($"Samochód {Marka}, rok: {Rok_Produkcji}, zbiornik: {Pojemnosc_Zbiornika} l, spalanie: {Srednie_Spalanie} l/100 km, zasięg na pełnym baku: {Oblicz_Zasieg():F2} km");
        }

        public void Zatankuj(decimal ilosc_litrow)
        {
            if (ilosc_litrow <= 0 || ilosc_litrow > Pojemnosc_Zbiornika)
            {
                Console.WriteLine("Nieprawidłowa ilość paliwa.");
                return;
            }

            Console.WriteLine($"{Marka}: zatankowano {ilosc_litrow} l paliwa.");
        }
    }

    public class Motocykl : Pojazd_Ladowy, I_Elektryczny
    {
        public decimal Pojemnosc_Baterii { get; private set; }
        public decimal Zuzycie_Energii { get; private set; }

        public Motocykl(string marka, uint rok, decimal pojemnosc, decimal zuzycie) : base(marka, rok)
        {
            if (pojemnosc <= 0 || zuzycie <= 0)
                throw new ArgumentException("Pojemność baterii i zużycie energii muszą być dodatnie.");

            Pojemnosc_Baterii = pojemnosc;
            Zuzycie_Energii = zuzycie;
        }

        public override decimal Oblicz_Zasieg()
        {
            return Pojemnosc_Baterii / Zuzycie_Energii * 100;
        }

        public override void Uruchom_Silnik()
        {
            Console.WriteLine($"{Marka}: uruchomiono silnik elektryczny.");
        }

        public override void Wyswietl_Informacje()
        {
            Console.WriteLine($"Motocykl {Marka}, rok: {Rok_Produkcji}, bateria: {Pojemnosc_Baterii} kWh, zużycie: {Zuzycie_Energii} kWh/100 km, zasięg na pełnej baterii: {Oblicz_Zasieg():F2} km");
        }

        public void Naladuj(decimal ilosc_energii)
        {
            if (ilosc_energii <= 0 || ilosc_energii > Pojemnosc_Baterii)
            {
                Console.WriteLine("Nieprawidłowa ilość energii.");
                return;
            }

            Console.WriteLine($"{Marka}: naładowano {ilosc_energii} kWh energii.");
        }
    }

    public class Samolot : Pojazd_Powietrzny, I_Tankowalny
    {
        public uint Liczba_Silnikow { get; private set; }
        public decimal Pulap_Lotu { get; private set; }

        public Samolot(string marka, uint rok, uint silniki, decimal pulap) : base(marka, rok)
        {
            if (silniki == 0 || pulap <= 0)
                throw new ArgumentException("Liczba silników i pułap muszą być dodatnie.");

            Liczba_Silnikow = silniki;
            Pulap_Lotu = pulap;
        }

        public override decimal Maksymalny_Pulap()
        {
            return Pulap_Lotu;
        }

        public override void Wyswietl_Informacje()
        {
            Console.WriteLine($"Samolot {Marka}, rok: {Rok_Produkcji}, liczba silników: {Liczba_Silnikow}, maksymalny pułap: {Maksymalny_Pulap()} m");
        }

        public void Zatankuj(decimal ilosc_litrow)
        {
            if (ilosc_litrow <= 0)
            {
                Console.WriteLine("Ilość paliwa musi być dodatnia.");
                return;
            }

            Console.WriteLine($"{Marka}: zatankowano {ilosc_litrow} l paliwa.");
        }
    }

    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Zadanie 1");
            List<Figura_Geometryczna> figury = new List<Figura_Geometryczna>();
            figury.Add(new Kwadrat(4M));
            figury.Add(new Kolo(3M));
            figury.Add(new Szescian(2M));

            foreach (Figura_Geometryczna figura in figury)
                figura.Wyswietl_Informacje();

            Console.WriteLine("\nZadanie 2");
            Samochod samochod = new Samochod("Toyota", 2020, 50M, 5M);
            Motocykl motocykl = new Motocykl("Zero", 2023, 15M, 5M);
            Samolot samolot = new Samolot("Boeing", 2018, 2, 12000M);

            List<Pojazd> pojazdy = new List<Pojazd> { samochod, motocykl, samolot };
            foreach (Pojazd pojazd in pojazdy)
                pojazd.Wyswietl_Informacje();

            samochod.Uruchom_Silnik();
            motocykl.Uruchom_Silnik();

            List<I_Tankowalny> tankowalne = new List<I_Tankowalny> { samochod, samolot };
            foreach (I_Tankowalny pojazd in tankowalne)
                pojazd.Zatankuj(20M);

            I_Elektryczny elektryczny = motocykl;
            elektryczny.Naladuj(10M);
        }
    }
}
