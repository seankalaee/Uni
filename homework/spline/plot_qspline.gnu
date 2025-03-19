set terminal pngcairo enhanced
set output 'qspline_plot.png'
set title 'Quadratic Spline, Derivative, and Integral'
set xlabel 'x'
set ylabel 'y'
set grid
set key outside
set pointsize 1.5

# Define styles
set style line 1 lt 1 lw 2 lc rgb 'blue'  # Quadratic Spline
set style line 2 lt 1 lw 2 lc rgb 'orange' # Derivative
set style line 3 lt 1 lw 2 lc rgb 'black'  # Integral
set style line 4 pt 7 ps 2 lc rgb 'red'    # Data Points

# Plot the data
plot 'qspline_results.txt' using 1:2 with lines ls 1 title 'Quadratic Spline', \
     'qspline_results.txt' using 1:3 with lines ls 2 title 'Derivative', \
     'qspline_results.txt' using 1:4 with lines ls 3 title 'Integral', \
     'test_data.txt' using 1:2 with points ls 4 title 'Data Points'
