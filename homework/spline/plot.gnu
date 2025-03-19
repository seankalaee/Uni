set terminal pngcairo enhanced
set output 'plot.png'
set title 'Linear Spline and its Integral'
set xlabel 'x'
set ylabel 'y'
set grid
set key outside
set pointsize 1.8
set style line 1 lt 1 lw 2 lc rgb 'blue'
set style line 2 lt 1 lw 2 lc rgb 'black'
set style line 3 pt 7 ps 2 lc rgb 'red'
plot 'output.txt' using 1:2 with lines ls 1 title 'Linear Spline', \
     'output.txt' using 1:3 with lines ls 2 title 'Integral', \
     'cos_data.txt' using 1:2 with points ls 3 title 'Cos Data Points'
