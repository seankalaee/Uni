EXEC = lngamma_plot.exe
SRC = lngamma_plot.cs

all: build run plot

build:
	mcs $(SRC) -out:$(EXEC)

run:
	mono $(EXEC)

plot:
	gnuplot lngamma_plot.gnuplot

clean:
	rm -f $(EXEC) lngamma_data.txt lngamma_plot.gnuplot lngamma_plot.png
