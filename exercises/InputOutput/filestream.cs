using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string infile = null, outfile = null;

        foreach (var arg in args)
        {
            var words = arg.Split(':');
            if (words[0] == "-input") infile = words[1];
            if (words[0] == "-output") outfile = words[1];
        }

        if (infile == null || outfile == null)
        {
            Console.Error.WriteLine("Error: Missing input or output file.");
            return;
        }

        using (var instream = new StreamReader(infile))
        using (var outstream = new StreamWriter(outfile, false))
        {
            string line;
            while ((line = instream.ReadLine()) != null)
            {
                double x = double.Parse(line);
                outstream.WriteLine($"{x}\t{Math.Sin(x)}\t{Math.Cos(x)}");
            }
        }
    }
}

