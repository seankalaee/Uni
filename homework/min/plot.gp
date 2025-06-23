set terminal png size 800,600
set output "higgs_fit.png"

set title "Breit-Wigner Fit to Higgs Data"
set xlabel "Energy [GeV]"
set ylabel "Signal"
set grid
set key left top

plot \
    "higgs.data.txt" using 1:2:3 with yerrorbars pt 7 ps 1 lc rgb "black" title "Data", \
    "higgs_fit.txt" using 1:2 with lines lw 2 lc rgb "blue" title "Fit"
