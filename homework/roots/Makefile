# Files
EXEC = program.exe
SRC = Main.cs vector.cs matrix.cs QR.cs newton.cs jacobian.cs RK.cs Driver.cs Hydrogen.cs ODE.cs
OUT_TXT = Output.txt
WAVE_TXT = wavefunction.dat
PLOT = wavefunction.svg

# Default rule
all: $(PLOT) $(OUT_TXT)

# Compile with mcs (Mono C# compiler)
$(EXEC): $(SRC)
	@echo "Compiling..."
	@mcs -target:exe -out:$@ $(SRC)

# Run the program and fix commas
$(OUT_TXT) $(WAVE_TXT): $(EXEC)
	@echo "Running program..."
	@mono $(EXEC)
	@echo "Fixing decimal formatting..."
	@sed -i '' 's/,/\./g' $(OUT_TXT) 2>/dev/null || sed -i 's/,/\./g' $(OUT_TXT)
	@sed -i '' 's/,/\./g' $(WAVE_TXT) 2>/dev/null || sed -i 's/,/\./g' $(WAVE_TXT)

# Plot with gnuplot
$(PLOT): $(WAVE_TXT) Makefile
	@echo "Plotting wavefunction..."
	@echo '\
	set term svg background "white";\
	set out "$@";\
	set title "Hydrogen Ground State Wavefunction";\
	set xlabel "r";\
	set ylabel "f(r)";\
	plot \
	 "$(WAVE_TXT)" using 1:2 with lines lw 3 lc rgb "blue" title "Numerical",\
	 "$(WAVE_TXT)" using 1:3 with lines lw 2 lc rgb "red" title "Exact (re^{-r})"\
	' | gnuplot

# Clean up generated files
clean:
	@echo "Cleaning up..."
	@rm -f $(EXEC) $(OUT_TXT) $(WAVE_TXT) $(PLOT)
