CC = mcs
OUTPUT = fit_program.exe
SRC = vector.cs matrix.cs QR.cs least_squares_fit.cs

all: $(OUTPUT) run plot

$(OUTPUT): $(SRC)
	$(CC) -out:$(OUTPUT) $(SRC)

run:
	@echo "Running the program..."
	@mono $(OUTPUT)

plot:
	@echo "Generating plot..."
	@gnuplot plot.gp

clean:
	@rm -f $(OUTPUT) output.txt data.txt fit.png plot.gp
