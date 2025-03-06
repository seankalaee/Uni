set terminal pngcairo enhanced size 800,600
set output 'gamma_plot.png'
set title 'Gamma Function Approximation vs Factorial'
set xlabel 'x'
set ylabel 'Gamma(x)'
set grid
plot \
    'gamma_data.txt' using 1:2 with lines lw 2 title 'Computed Gamma(x)', \
    'gamma_data.txt' using 1:3 with points pt 7 lc 'red' title 'Factorial(x-1)'
