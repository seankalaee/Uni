set terminal png
set output 'convergence_dr.png'
set xlabel 'Δr'
set ylabel 'ε0'
set title 'Ground state energy vs. Δr'
set grid
plot 'Convergence_DeltaR.txt' using 1:2 with linespoints title 'Energy'
