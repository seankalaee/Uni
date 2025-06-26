This is the solution to Examination project #13 "yagopt" AKA. "Yet Another Stochastic Global Optimizer".


The goal of the project is to minimize a function.

First using a global minimum optimizer, this was done using a Quasi Monte Carlo algorithm (Halton Sampling), to find with broad strokes a global minimum estimate,
within a specific number of seconds or samples.

Then we wish to refine this by using a minimizing algorithm around the best estimated minimum (Using Newton minimizer) with the previous method to find the local minimum.

This was compared to multiple different interesting functions to showcase how well the project is at doing this, aswell as plotting this.
