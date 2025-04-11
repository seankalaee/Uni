set terminal pngcairo size 1000,700
set output 'all_errors_comparison.png'

set logscale xy
set xlabel "N (log scale)"
set ylabel "Error (log scale)"
set title "Monte Carlo vs Quasi-Monte Carlo Error Scaling"
set key top right

plot \
    'circle_data.txt' using 1:3 with linespoints title "Pseudo Circle", \
    'gaussian_data.txt' using 1:3 with linespoints title "Pseudo Gaussian", \
    'quasi_data.txt' using 1:2 with linespoints title "Quasi Circle", \
    'quasi_data.txt' using 1:3 with linespoints title "Quasi Gaussian"
