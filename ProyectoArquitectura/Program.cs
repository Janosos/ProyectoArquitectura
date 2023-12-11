using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        var watch = Stopwatch.StartNew();
        int conteoSecuencial = ContarNumerosParesSecuencial(numeros);
        watch.Stop();
        
        Console.WriteLine($"Cantidad de números pares (foreach): {conteoSecuencial} | Tiempo de ejecución : {watch.ElapsedMilliseconds} ms");
        
        // Contar números pares usando Parallel.ForEach
        var watchForParallel = Stopwatch.StartNew();
        int conteoParalelo = ContarNumerosParesParalelo(numeros);
        watchForParallel.Stop();

        Console.WriteLine($"Cantidad de números pares (Parallel.ForEach): {conteoParalelo} | Tiempo de ejecución : {watchForParallel.ElapsedMilliseconds} ms");

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