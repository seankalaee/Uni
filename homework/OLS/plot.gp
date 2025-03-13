set terminal pngcairo size 800,600
set output 'fit.png'
set xlabel 'Time (days)'
set ylabel 'ln(Activity)'
set title 'Radioactive Decay Fit'
set grid
plot 'data.txt' using 1:2:3 with yerrorbars title 'Data', 4.9577 - 0.1703*x title 'Fit' with lines
