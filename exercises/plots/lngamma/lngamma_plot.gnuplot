set terminal pngcairo enhanced size 800,600
set output 'lngamma_plot.png'
set title 'Log Gamma Function Approximation vs ln(Factorial)'
set xlabel 'x'
set ylabel 'ln(Gamma(x))'
set grid
plot \
    'lngamma_data.txt' using 1:2 with lines lw 2 title 'Computed ln(Gamma(x))', \
    'lngamma_data.txt' using 1:3 with points pt 7 lc 'red' title 'ln(Factorial(x-1))'
