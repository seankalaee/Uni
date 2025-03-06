EXEC = erf_plot.exe
SRC = erf_plot.cs

all: build run plot

build:
	mcs $(SRC) -out:$(EXEC)

run:
	mono $(EXEC)

plot:
	gnuplot erf_plot.gnuplot

clean:
	rm -f $(EXEC) erf_data.txt erf_plot.gnuplot erf_plot.png
