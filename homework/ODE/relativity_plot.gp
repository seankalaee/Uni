set terminal png size 800,600
set output 'relativity_orbit.png'
set title "Relativistic Planetary Orbits"
set xlabel "x"
set ylabel "y"
set size ratio -1
plot \
    "relativity_circular.txt" using (1/$2)*cos($1):(1/$2)*sin($1) with lines title "Circular (ε=0)", \
    "relativity_elliptical.txt" using (1/$2)*cos($1):(1/$2)*sin($1) with lines title "Elliptical (ε=0)", \
    "relativity_precessing.txt" using (1/$2)*cos($1):(1/$2)*sin($1) with lines title "Precessing (ε=0.01)"
