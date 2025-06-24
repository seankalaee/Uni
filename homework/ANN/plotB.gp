set terminal pngcairo size 1000,700
set output "fitB.png"
set title "ANN Function, Derivative, and Integral"
set xlabel "x"
set ylabel "y"
set grid
plot \
    'fitB.txt' using 1:2 with lines title "ANN response f(x)" lt rgb "red" lw 2, \
    'fitB.txt' using 1:3 with lines title "First derivative f'(x)" lt rgb "blue" lw 2, \
    'fitB.txt' using 1:4 with lines title "Second derivative f''(x)" lt rgb "green" lw 2, \
    'fitB.txt' using 1:5 with lines title "Integral ∫f(x)dx" lt rgb "purple" lw 2
