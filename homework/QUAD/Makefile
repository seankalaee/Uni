CC = mcs
OUTPUT = Main.exe
SRC = Main.cs Integrator.cs

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
	@rm -f $(OUTPUT) Aoutput.txt Checks.txt error_data.txt erf.png
