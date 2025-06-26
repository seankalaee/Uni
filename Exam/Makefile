CC = mcs
OUTPUT = globalopt.exe
SRC = main.cs functions.cs QuasiMonteCarlo.cs vector.cs gradient.cs hessian.cs QR.cs newton.cs matrix.cs

all: $(OUTPUT) run plot

$(OUTPUT): $(SRC)
	$(CC) -out:$(OUTPUT) $(SRC)

run:
	mono $(OUTPUT)

plot:
	@echo "Generating plots..."
	@gnuplot plotH.gp
	@gnuplot plotR.gp

clean:
	rm -f *.exe *.txt *.png
