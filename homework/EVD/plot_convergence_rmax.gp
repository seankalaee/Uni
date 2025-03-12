set terminal png
set output 'convergence_rmax.png'
set xlabel 'rmax'
set ylabel 'ε0'
set title 'Ground state energy vs. rmax'
set grid
plot 'Convergence_Rmax.txt' using 1:2 with linespoints title 'Energy'
