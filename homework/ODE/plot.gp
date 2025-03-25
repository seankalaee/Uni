set terminal png size 800,600
set output 'plot.png'
set title "Pendulum Motion"
set xlabel "t"
set ylabel "Values"
plot "data.txt" using 1:2 with lines title "theta(t)" lw 2 lc rgb "blue", \
     "data.txt" using 1:3 with lines title "omega(t)" lw 2 lc rgb "green"
