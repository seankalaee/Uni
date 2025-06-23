CC = mcs
SRC = vector.cs matrix.cs QR.cs gradient.cs hessian.cs newton.cs functions.cs

all: partA partB partC

partA: $(SRC) main.cs
	$(CC) -out:minimizeA.exe $(SRC) main.cs

partB: $(SRC) mainB.cs
	$(CC) -out:minimizeB.exe $(SRC) mainB.cs

partC: $(SRC) mainC.cs
	$(CC) -out:minimizeC.exe $(SRC) mainC.cs

runA: partA
	@echo "Running Part A (minimization)..."
	@mono minimizeA.exe

runB: partB
	@echo "Running Part B (Higgs fit)..."
	@mono minimizeB.exe < higgs.data.txt > out.txt

runC: partC
	@echo "Running Part C (central difference test)..."
	@mono minimizeC.exe

plot:
	@echo "Generating plot..."
	@gnuplot plot.gp

clean:
	rm -f *.exe out.txt higgs_fit.txt higgs_fit.png centraldiff.txt
