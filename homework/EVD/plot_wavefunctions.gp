set terminal png
set output 'wavefunctions.png'
set xlabel 'r'
set ylabel 'ψ(r)'
set title 'Lowest three wavefunctions'
set grid
plot 'Wavefunctions.txt' using 2:3 with lines title 'ψ1', \
     'Wavefunctions.txt' using 2:4 with lines title 'ψ2', \
     'Wavefunctions.txt' using 2:5 with lines title 'ψ3'
