set terminal pngcairo size 1000,800
set output 'plotB.png'

set title "Beale Function Heatmap"
set xlabel "x"
set ylabel "y"
set xrange [-4.5:4.5]
set yrange [-4.5:4.5]

# Heatmap configuration
set pm3d map
set palette defined (0 "navy", 50 "blue", 100 "green", 150 "yellow", 200 "red", 250 "white")
set cbrange [0:200]
set logscale cb
set colorbox
set size ratio -1
set key top left

# Label near final minimum
set label "Minimum" at graph 0.72, 0.12 tc rgb "red"

splot 'beale_surface.txt' using 1:2:3 with pm3d notitle, \
      'best_beale.txt' using 1:2:(0) with points pt 7 ps 3 lc rgb 'black' title "Initial guess", \
      'final_beale.txt' using 1:2:(0) with points pt 7 ps 3 lc rgb 'red' title "Minimum"
