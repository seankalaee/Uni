using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class Data {
    public int a, b;
    public double sum;
}

public class HarmonicSum {
    public static void Harmonic(object obj) {
        var arg = (Data)obj;
        arg.sum = 0;
        for (int i = arg.a; i < arg.b; i++) {
            arg.sum += 1.0 / i;
        }
    }

    public static double ParallelHarmonicWrong(int N) {
        double localSum = 0;
        System.Threading.Tasks.Parallel.For(1, N + 1, (int i) => {
            lock (typeof(HarmonicSum)) { // Synchronize access to avoid race conditions
                localSum += 1.0 / i;
            }
        });
        return localSum;
    }

    public static double ParallelHarmonicCorrect(int N) {
        var sum = new System.Threading.ThreadLocal<double>(() => 0, trackAllValues: true);
        System.Threading.Tasks.Parallel.For(1, N + 1, (int i) => sum.Value += 1.0 / i);
        return sum.Values.Sum();
    }

    public static void Main(string[] args) {
        int nthreads = 1, nterms = (int)1e8; // Default values

        foreach (var arg in args) {
            var words = arg.Split(':');
            if (words[0] == "-threads") nthreads = int.Parse(words[1]);
            if (words[0] == "-terms") nterms = (int)float.Parse(words[1]);
        }

        Data[] paramsArray = new Data[nthreads];
        for (int i = 0; i < nthreads; i++) {
            paramsArray[i] = new Data();
            paramsArray[i].a = 1 + nterms / nthreads * i;
            paramsArray[i].b = 1 + nterms / nthreads * (i + 1);
        }
        paramsArray[paramsArray.Length - 1].b = nterms + 1; // Adjust endpoint

        Thread[] threads = new Thread[nthreads];
        for (int i = 0; i < nthreads; i++) {
            threads[i] = new Thread(Harmonic);
            threads[i].Start(paramsArray[i]);
        }

        foreach (var thread in threads) thread.Join();

        double total = 0;
        foreach (var p in paramsArray) total += p.sum;
        Console.WriteLine($"Total Harmonic Sum (Threading): {total}");

        // Parallel.For Wrong Version (Fixed)
        double wrongSum = ParallelHarmonicWrong(nterms);
        Console.WriteLine($"Total Harmonic Sum (Parallel.For Wrong - Fixed with Lock): {wrongSum}");

        // Parallel.For Correct Version
        double correctSum = ParallelHarmonicCorrect(nterms);
        Console.WriteLine($"Total Harmonic Sum (Parallel.For Correct): {correctSum}");
    }
}
