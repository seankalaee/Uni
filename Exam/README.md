# Yet Another Stochastic Global Optimizer (yagopt) - Project #13

Implement a stochastic global optimizer using the following algorithm,

1. Use the given number of seconds (or the number of samples) to search for the global minimum of the given cost-function in the given volume by sampling the function using a low-discrepancy sequence.
2. From the best point found at the previous step run your favourite local minimizer.

---

## Overview

1. **Global search:**  
   Using Halton sampling, the algorithm explores the search space broadly within a fixed number of samples, to find a global minimum region.

2. **Local refinement:**  
   The best candidate from the global search is refined by using Newton's method to locate the local minimum.

3. **Evaluation:**  
   This is tested on multiple functions (Himmelblau, Rosenbrock, Beale, Booth) to demonstrate its effectiveness both with plots and data.

---

## Files

- `main.cs`: The main program implementing the optimization and outputs.
- `functions.cs`: Defines the functions used in the project.
- `QR.cs`, `vector.cs`, `gradient.cs`, `hessian.cs`, `matrix.cs`: Different C# files that implement different numerical methods, these have been developed from previous assignments/homeworks.
- `QuasiMonteCarlo.cs`: Implements low discrepancy quasi-random sampling, which is important for the first part of the project.
- `newton.cs`: Implements newtons method for local minimization for the best sampled result from the output from the Global search.
- `RESULTS.txt`: Summary of results for each function used.
- `plotX.gp`, `plotX.png`: Gnuplot scripts and output images for visualization.
- `Makefile`: Runs and compiles the project

---


## Scoring

This project achieves a score of **8/10**.
