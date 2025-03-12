CC = mcs
EXE_H = HydrogenSolver.exe
EXE_E = EigenTest.exe
SRC_H = HydrogenSolver.cs Hamiltonian.cs Jacobi.cs Matrix.cs Vector.cs
SRC_E = EigenTest.cs Jacobi.cs Matrix.cs Vector.cs
OUTFILE_H = Hydrogen.txt
OUTFILE_E = EigenTest.txt
GNUPLOT_SCRIPT = plot_hydrogen.gp

all: $(EXE_H) $(EXE_E)
	@echo "Compilation complete."

$(EXE_H): $(SRC_H)
	$(CC) -out:$(EXE_H) $(SRC_H)

$(EXE_E): $(SRC_E)
	$(CC) -out:$(EXE_E) $(SRC_E)

run-hydrogen: $(EXE_H)
	@mono $(EXE_H)

run-eigen: $(EXE_E)
	@mono $(EXE_E) | tee $(OUTFILE_E)

clean:
	rm -f $(EXE_H) $(EXE_E) $(OUTFILE_H) $(OUTFILE_E) $(GNUPLOT_SCRIPT) convergence_dr.png
