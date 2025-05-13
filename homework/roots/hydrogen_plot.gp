set terminal svg background "white"
set output "wavefunction.svg"

set title "Hydrogen Ground State Wavefunction"
set xlabel "r"
set ylabel "f(r)"
set xrange [0:8]
set yrange [-0.05:0.4]
set key top right
set grid

plot \
 "wavefunction.dat" using 1:2 with lines lw 3 lc rgb "blue" title "Numerical", \
 "wavefunction.dat" using 1:3 with lines lw 2 lc rgb "red" title "Exact (re^{-r})"
