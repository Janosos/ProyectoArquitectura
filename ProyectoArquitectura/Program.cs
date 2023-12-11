using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Crear una lista de números del 1 al 1,000,000
        List<int> numeros = new List<int>(1000000);
        for (int i = 1; i <= 1000000; i++)
        {
            numeros.Add(i);
        }

        // Contar números pares usando foreach
        int conteoSecuencial = ContarNumerosParesSecuencial(numeros);
        Console.WriteLine($"Cantidad de números pares (foreach): {conteoSecuencial}");

        // Contar números pares usando Parallel.ForEach
        int conteoParalelo = ContarNumerosParesParalelo(numeros);
        Console.WriteLine($"Cantidad de números pares (Parallel.ForEach): {conteoParalelo}");

        Console.ReadLine();
    }

    static int ContarNumerosParesSecuencial(List<int> numeros)
    {
        int conteo = 0;
        foreach (var numero in numeros)
        {
            if (EsPar(numero))
            {
                conteo++;
            }
        }
        return conteo;
    }

    static int ContarNumerosParesParalelo(List<int> numeros)
    {
        object conteoLock = new object();
        int conteo = 0;

        Parallel.ForEach(numeros, (numero) =>
        {
            if (EsPar(numero))
            {
                lock (conteoLock)
                {
                    conteo++;
                }
            }
        });

        return conteo;
    }

    static bool EsPar(int numero)
    {
        return numero % 2 == 0;
    }
}