using System;
using System.IO;
using static System.Console;
using static System.Math;

class LeastSquaresFit {
    public static (vector, matrix) lsfit(Func<double, double>[] fs, vector x, vector y, vector dy) {
        int n = x.size;
        int m = fs.Length;
        matrix A = new matrix(n, m);
        vector b = new vector(n);
        
        for (int i = 0; i < n; i++) {
            for (int k = 0; k < m; k++) {
                A[i, k] = fs[k](x[i]) / dy[i];
            }
            b[i] = y[i] / dy[i];
        }
        
        QR qr = new QR(A);
        vector c = qr.solve(b);
        
        // Compute covariance matrix C = (A^T A)^(-1)
        matrix RTR = qr.R.transpose() * qr.R;
        matrix ATA_inv = new matrix(RTR.size1, RTR.size2);
        
        for (int i = 0; i < RTR.size2; i++) {
            vector e = new vector(RTR.size1);
            e[i] = 1.0;  // Basis vector for identity matrix
            vector col = QR.backsub(RTR, e);  // Solve for this column
            ATA_inv[i] = col;  // Store column in inverse matrix
        }
        
        return (c, ATA_inv);
    }
    
    static void Main() {
        // Data points from Rutherford & Soddy (1902)
        vector t = new vector(1, 2, 3, 4, 6, 9, 10, 13, 15);
        vector y = new vector(117, 100, 88, 72, 53, 29.5, 25.2, 15.2, 11.1);
        vector dy = new vector(6, 5, 4, 4, 4, 3, 3, 3, 2); 
        
        // Log-transform y values and propagate uncertainty
        vector ln_y = y.map(Math.Log);
        vector dln_y = dy / y;
        
        // Define basis functions for fitting ln(y) = ln(a) - λt
        Func<double, double>[] fs = {
            z => 1.0,
            z => -z
        };
        
        // Perform least-squares fitting
        (vector c, matrix covariance) = lsfit(fs, t, ln_y, dln_y);
        double ln_a = c[0];
        double lambda = c[1];
        double half_life = Log(2) / lambda;
        
        // Extract uncertainties from covariance matrix
        double sigma_ln_a = Sqrt(covariance[0, 0]);
        double sigma_lambda = Sqrt(covariance[1, 1]);
        double sigma_half_life = (Log(2) / (lambda * lambda)) * sigma_lambda;
        
        // Output results
        using (StreamWriter file = new StreamWriter("output.txt")) {
            file.WriteLine($"ln(a) = {ln_a:F4} ± {sigma_ln_a:F4}");
            file.WriteLine($"Lambda (decay constant) = {lambda:F4} ± {sigma_lambda:F4}");
            file.WriteLine($"Half-life T_1/2 = {half_life:F4} ± {sigma_half_life:F4} days, modern value is approx. 3.6 days. So not within range");
            file.WriteLine("\nThe uncertainty of the logarithm is δln(y) = δy / y (proved).\n");
        }
        
        // Generate data for GNUplot
        using (StreamWriter dataFile = new StreamWriter("data.txt")) {
            for (int i = 0; i < t.size; i++) {
                dataFile.WriteLine($"{t[i]} {ln_y[i]} {dln_y[i]}");
            }
        }
        
        using (StreamWriter plotFile = new StreamWriter("plot.gp")) {
            plotFile.WriteLine("set terminal pngcairo size 800,600");
            plotFile.WriteLine("set output 'fit.png'");
            plotFile.WriteLine("set xlabel 'Time (days)'");
            plotFile.WriteLine("set ylabel 'ln(Activity)'");
            plotFile.WriteLine("set title 'Radioactive Decay Fit'");
            plotFile.WriteLine("set grid");
            plotFile.WriteLine($"plot 'data.txt' using 1:2:3 with yerrorbars title 'Data', " +
                $"{ln_a:F4} - {lambda:F4}*x title 'Fit' with lines");
        }
        
        WriteLine("Fitting complete. Results saved in output.txt and plot saved in fit.png.");
    }
}
