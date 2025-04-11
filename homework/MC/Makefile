CC = mcs
OUTPUT = montecarlo.exe
SRC = vector.cs MonteCarlo.cs QuasiMonteCarlo.cs Main.cs

all: $(OUTPUT) run

$(OUTPUT): $(SRC)
	$(CC) -out:$(OUTPUT) $(SRC)

run:
	@echo "Running the program..."
	@mono $(OUTPUT)

plotcombined:
	@echo "Generating combined plot for circle and Gaussian..."
	@gnuplot combined_plot.gp

plotall:
	@echo "Generating full comparison plot..."
	@gnuplot plot_all_errors.gp

clean:
	@rm -f $(OUTPUT) *.txt *.png *.gp
