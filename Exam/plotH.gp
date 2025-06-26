set terminal png size 1000,800
set output 'plotH.png'
set title "Himmelblau Optimization (Contour Lines)"
set xlabel "x"
set ylabel "y"
set xrange [-5:5]
set yrange [-5:5]
unset key
unset colorbox
unset pm3d
unset surface
set view map
set dgrid3d 100,100
set contour base
set cntrparam levels incremental 0,10,200
unset clabel
set style data lines

set multiplot
set origin 0,0
set size 1,1

splot 'himmelblau_surface.txt' using 1:2:3 notitle with lines lc rgb "black"

plot 'final_himmelblau.txt' using 1:2 with points pt 7 ps 2 lc rgb 'red' notitle

unset multiplot
