set terminal pngcairo enhanced size 800,600
set output 'erf_plot.png'
set title 'Error Function Approximation vs Tabulated Values'
set xlabel 'x'
set ylabel 'erf(x)'
set grid
plot \
    'erf_data.txt' using 1:2 with lines lw 2 title 'Approximation', \
    'erf_data.txt' using 1:3 with points pt 7 lc 'red' title 'Tabulated Values'
