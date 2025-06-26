set terminal png size 1000,800
set output 'plotH.png'
set title "Himmelblau optimization"
set xlabel "x"
set ylabel "y"
set xrange [-5:5]
set yrange [-5:5]
set key top right
set palette rgb 33,13,10
set colorbox
unset surface
unset hidden3d
unset pm3d

# fallback heatmap
set dgrid3d 100,100
set contour base
set cntrparam levels 50
set style data lines

splot 'himmelblau_surface.txt' using 1:2:3 notitle, \
      'trace_himmelblau.txt' using 1:2:(0) with lines lc rgb 'black' title 'Trace', \
      'best_himmelblau.txt' using 1:2:(0) with points pt 7 ps 2 lc rgb 'blue' title 'Best sample', \
      'final_himmelblau.txt' using 1:2:(0) with points pt 7 ps 2 lc rgb 'red' title 'Final result'
