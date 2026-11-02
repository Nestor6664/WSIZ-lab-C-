# WSIZ — laboratoria C#

Każde laboratorium jest osobnym projektem konsolowym dla .NET 8.
W Visual Studio otwórz plik `.csproj` z wybranego folderu i uruchom projekt przez Ctrl+F5.
Można też skopiować cały `Program.cs` do własnego projektu konsolowego C#.

| Folder | Temat | Uruchomienie w terminalu |
| --- | --- | --- |
| Lab2 | Klasy, konstruktory i pola statyczne | `dotnet run --project Lab2/Lab2.csproj` |
| Lab3 | Właściwości, konstruktory i indeksatory | `dotnet run --project Lab3/Lab3.csproj` |
| Lab4 | Dziedziczenie i polimorfizm | `dotnet run --project Lab4/Lab4.csproj` |
| Lab5 | Klasy abstrakcyjne i interfejsy | `dotnet run --project Lab5/Lab5.csproj` |

W Lab3 licznik pojazdów wynosi 3, bufor zawiera 10, 20, 30, 40, a odczyt spoza zakresu zwraca -1.
Indekser katalogu ustawia stan produktu, a `Dodaj_Dostawe` zwiększa go o podaną ilość.

Aby sprawdzić właściwość `init` w Lab3, dodaj w `Main`, po utworzeniu konta, linię `konto.Identyfikator_Konta = "K002";`.
Kompilator zgłosi błąd CS8852. Usuń tę linię przed ponownym uruchomieniem programu.

W Lab4 licznik wyświetleń wynosi 3, a suma wynagrodzeń wynosi 9500 zł.

W Lab5 zasięg jest liczony dla pełnego zbiornika lub baterii. Spalanie podano w l/100 km,
a zużycie energii w kWh/100 km. Tankowanie, ładowanie i uruchomienie silnika są symulowane
przez komunikaty w konsoli; program nie przechowuje aktualnego poziomu paliwa ani energii.
