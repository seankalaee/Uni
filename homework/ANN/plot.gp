set terminal pngcairo size 800,600
set output 'fit.png'
set title "Neural Network Approximation"
set xlabel "x"
set ylabel "y"
plot 'output.txt' with lines title 'NN Fit'
