CC = mcs
OUTPUT = program.exe
SRC = Main.cs vector.cs matrix.cs QR.cs jacobian.cs newton.cs Driver.cs

all: $(OUTPUT) run plot

$(OUTPUT): $(SRC)
	$(CC) -out:$(OUTPUT) $(SRC)

run:
	@echo "Running the program..."
	@mono $(OUTPUT)
	@mv output.txt RosenbrockHimmelblau.txt

plot:
	@echo "Generating hydrogen wavefunction plot..."
	@gnuplot plot_hydrogen.gp

clean:
	@rm -f *.exe *.txt *.png
