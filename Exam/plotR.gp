set terminal png size 1000,800
set output 'plotR.png'
set title "Rosenbrock optimization"
set xlabel "x"
set ylabel "y"
set key top right

plot \
    'trace_rosenbrock.txt' using 1:2 with lines lc rgb 'black' title 'Trace', \
    'best_rosenbrock.txt' using 1:2 with points pt 7 ps 1.5 lc rgb 'blue' title 'Best sample', \
    'final_rosenbrock.txt' using 1:2 with points pt 7 ps 1.5 lc rgb 'red' title 'Final result'
