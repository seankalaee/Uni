set terminal png
set output 'convergence_dr.png'
set xlabel 'Δr'
set ylabel 'ε0'
set title 'Ground state energy vs. Δr'
set xrange [0:*]
set grid
plot 'Hydrogen.txt' using 1:2 with linespoints title 'Energy'
