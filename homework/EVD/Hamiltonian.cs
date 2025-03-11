using System;

public class Hamiltonian
{
    public static double[,] BuildHamiltonian(int npoints, double rmax, double dr)
    {
        double[,] H = new double[npoints, npoints];

        // Create r values
        double[] r = new double[npoints];
        for (int i = 0; i < npoints; i++)
            r[i] = dr * (i + 1);

        // Fill kinetic energy matrix K
        double factor = -1.0 / (2 * dr * dr);
        for (int i = 0; i < npoints - 1; i++)
        {
            H[i, i] = -2 * factor;
            H[i, i + 1] = factor;
            H[i + 1, i] = factor;
        }
        H[npoints - 1, npoints - 1] = -2 * factor;

        // Add potential energy matrix W
        for (int i = 0; i < npoints; i++)
        {
            H[i, i] += -1.0 / r[i]; // Coulomb potential
        }

        return H;
    }
}
