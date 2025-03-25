set terminal pngcairo size 1600,1000 enhanced font 'Arial,16'
set output 'relativity_r_vs_phi.png'

set title "Planetary Orbits" font ",20"
set xlabel "Angle (phi)" font ",16"
set ylabel "Radial Distance (r)" font ",16"
set grid lw 1 lc rgb "#aaaaaa"
set key top right box

plot \
    "relativity_circular.txt" using 1:(1/$2) with lines lw 3 lc rgb "red" title "Circular Orbit", \
    "relativity_elliptical.txt" using 1:(1/$2) with lines lw 3 lc rgb "blue" title "Elliptical Orbit", \
    "relativity_precessing.txt" using 1:(1/$2) with lines lw 3 lc rgb "orange" title "Relativistic Precession"
