using System;

public static class Hamiltonian
{
    public static matrix BuildMatrix(int npoints, vector r, double dr)
    {
        matrix H = new matrix(npoints, npoints);

        // Constructing Kinetic Energy Matrix (K)
        for (int i = 0; i < npoints - 1; i++)
        {
            H[i, i] = -2 * (-0.5 / (dr * dr));
            H[i, i + 1] = 1 * (-0.5 / (dr * dr));
            H[i + 1, i] = 1 * (-0.5 / (dr * dr));
        }
        H[npoints - 1, npoints - 1] = -2 * (-0.5 / (dr * dr));

        // Adding Potential Energy Matrix (W)
        for (int i = 0; i < npoints; i++)
            H[i, i] += -1 / r[i];

        return H;
    }
}
