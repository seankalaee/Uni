set terminal pngcairo size 1000,800
set output 'plotR.png'

set title "Rosenbrock Function Heatmap"
set xlabel "x"
set ylabel "y"
set xrange [-3:3]
set yrange [-3:3]

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

splot 'rosenbrock_surface.txt' using 1:2:3 with pm3d notitle, \
      'best_rosenbrock.txt' using 1:2:(0) with points pt 7 ps 3 lc rgb 'black' title "Initial guess", \
      'final_rosenbrock.txt' using 1:2:(0) with points pt 7 ps 3 lc rgb 'red' title "Minimum"
