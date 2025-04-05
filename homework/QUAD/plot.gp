set terminal pngcairo size 800,600
set output 'erf.png'
set logscale xy
set grid
set title 'Error in erf(1) vs Accuracy (log-log)'
set xlabel 'Requested Accuracy (acc)'
set ylabel 'Absolute Error'
set format x "10^{%T}"
set format y "10^{%T}"
plot 'error_data.txt' using 1:2 with linespoints title 'error vs acc'
