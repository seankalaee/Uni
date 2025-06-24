set terminal pngcairo size 1000,600
set output 'fitA.png'
set title 'Neural Network vs. Target Function'
set xlabel 'x'
set ylabel 'y'
set key top right
plot 'fitA.txt' using 1:3 with lines lt rgb 'blue' title 'g(x) = cos(5x - 1)·e^{-x²}',\
     'fitA.txt' using 1:2 with points pt 7 ps 0.7 lc rgb 'red' title 'ANN output'
