CC = mcs
OUTPUT = annA.exe
SRC = mainA.cs ann.cs vector.cs gradient.cs hessian.cs QR.cs newton.cs matrix.cs

all: $(OUTPUT) run plot

$(OUTPUT): $(SRC)
	$(CC) -out:$(OUTPUT) $(SRC)


runA:
	mcs -out:annA.exe mainA.cs ann.cs vector.cs gradient.cs hessian.cs QR.cs newton.cs matrix.cs
	mono annA.exe
	gnuplot plotA.gp

runB:
	mcs -out:annB.exe mainB.cs ann.cs vector.cs gradient.cs hessian.cs QR.cs newton.cs matrix.cs
	mono annB.exe
	gnuplot plotB.gp

clean:
	rm -f *.exe *.txt *.png

plot:
	@echo "Generating plot..."
	@gnuplot plotA.gp

