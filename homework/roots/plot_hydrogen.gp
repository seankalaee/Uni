set terminal pngcairo size 800,600
set output 'hydrogen_plot.png'
set title 'Hydrogen Ground State Wavefunction'
set xlabel 'r'
set ylabel 'f(r)'
plot 'hydrogen_data.txt' using 1:2 with lines title 'Numerical', \
     'hydrogen_data.txt' using 1:3 with lines title 'Exact r*exp(-r)'
