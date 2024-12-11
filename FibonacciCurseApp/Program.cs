using System;
using System.Collections.Generic;

public class WitchSaga
{
    private static Dictionary<int, int> villagersKilled = new Dictionary<int, int>();

    // Metode untuk menghitung jumlah orang yang dibunuh setiap tahun
    private static void PrecomputeVillagersKilled(int maxYear)
    {
        int a = 1, b = 1;
        villagersKilled[1] = 1; // Tahun 1 membunuh 1 orang
        villagersKilled[2] = 2; // Tahun 2 membunuh 2 orang

        for (int i = 3; i <= maxYear; i++)
        {
            int c = a + b;
            villagersKilled[i] = villagersKilled[i - 1] + c;
            a = b;
            b = c;
        }
    }

    // Metode untuk menghitung tahun kelahiran berdasarkan usia kematian dan tahun kematian
    private static int CalculateYearOfBirth(int ageOfDeath, int yearOfDeath)
    {
        // Jika usia atau tahun kelahiran tidak valid, kembalikan -1
        int birthYear = yearOfDeath - ageOfDeath;
        if (ageOfDeath < 0 || birthYear < 1)
        {
            return -1;
        }
        return birthYear;
    }

    // Metode untuk mendapatkan jumlah orang yang dibunuh pada tahun tertentu
    private static int GetVillagersKilled(int year)
    {
        if (villagersKilled.ContainsKey(year))
        {
            return villagersKilled[year];
        }
        return -1; // Jika tahun tidak valid
    }

    // Metode untuk menghitung rata-rata jumlah orang yang dibunuh pada tahun kelahiran dua orang
    public static double CalculateAverageVillagersKilled(int ageOfDeathA, int yearOfDeathA, int ageOfDeathB, int yearOfDeathB)
    {
        // Precompute jumlah orang yang dibunuh hingga tahun maksimal yang diperlukan
        int maxYear = Math.Max(yearOfDeathA, yearOfDeathB);
        PrecomputeVillagersKilled(maxYear);

        // Menghitung tahun kelahiran
        int birthYearA = CalculateYearOfBirth(ageOfDeathA, yearOfDeathA);
        int birthYearB = CalculateYearOfBirth(ageOfDeathB, yearOfDeathB);

        // Jika ada tahun kelahiran yang tidak valid, kembalikan -1
        if (birthYearA == -1 || birthYearB == -1)
        {
            return -1;
        }

        // Mendapatkan jumlah orang yang dibunuh pada tahun tersebut
        int villagersKilledA = GetVillagersKilled(birthYearA);
        int villagersKilledB = GetVillagersKilled(birthYearB);

        // Jika tahun tidak valid, kembalikan -1
        if (villagersKilledA == -1 || villagersKilledB == -1)
        {
            return -1;
        }

        // Menghitung rata-rata
        return (villagersKilledA + villagersKilledB) / 2.0;
    }

    public static void Main()
    {
        Console.WriteLine("Enter data for 2 people.");

        // Input data for Person A
        Console.Write("Enter age of death for Person A: ");
        int ageOfDeathA = int.Parse(Console.ReadLine());

        Console.Write("Enter year of death for Person A: ");
        int yearOfDeathA = int.Parse(Console.ReadLine());

        // Input data for Person B
        Console.Write("Enter age of death for Person B: ");
        int ageOfDeathB = int.Parse(Console.ReadLine());

        Console.Write("Enter year of death for Person B: ");
        int yearOfDeathB = int.Parse(Console.ReadLine());

        // Calculate the average number of people killed in their birth years
        double averageKilled = CalculateAverageVillagersKilled(ageOfDeathA, yearOfDeathA, ageOfDeathB, yearOfDeathB);

        if (averageKilled == -1)
        {
            Console.WriteLine("Invalid data.");
        }
        else
        {
            Console.WriteLine($"Average number of people killed in the birth year: {averageKilled}");
        }
    }
}
