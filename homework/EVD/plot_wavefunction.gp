set terminal pngcairo enhanced font 'Arial,12' size 800,600
set output "plot.png"

set title "Radial Wavefunction for Hydrogen Atom"
set xlabel "r (Bohr radii)"
set ylabel "Wavefunction f(r)"
set grid
set key top right

plot "wavefunction.txt" using 1:2 with lines title "First Wavefunction" lw 2 lc rgb "blue"
