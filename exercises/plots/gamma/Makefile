EXEC = gamma_plot.exe
SRC = gamma_plot.cs

all: build run plot

build:
	mcs $(SRC) -out:$(EXEC)

run:
	mono $(EXEC)

plot:
	gnuplot gamma_plot.gnuplot

clean:
	rm -f $(EXEC) gamma_data.txt gamma_plot.gnuplot gamma_plot.png
