CC = mcs
OUT = output.txt
SRC = Program.cs Jacobi.cs Hamiltonian.cs
EXE = program.exe

all: compile run plot

compile:
	$(CC) -out:$(EXE) $(SRC)

run:
	mono $(EXE) 10 0.3 > $(OUT)
	mono $(EXE) 15 0.2 > output_fine.txt  # Extra test case

plot:
	gnuplot plot_wavefunction.gp

clean:
	rm -f $(EXE) $(OUT) wavefunction.txt plot.png
