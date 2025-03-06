CC = mcs

# Output files
DLL = cmath.dll 
EXE = main.exe
OUT = output.txt

# Source files
SRC = cmath.cs complex.cs
MAIN = main.cs

# Default rule
all: $(DLL) $(EXE)

# Compile cmath.dll
$(DLL): $(SRC)
	$(CC) -target:library -out:$(DLL) $(SRC)

# Compile main program
$(EXE): $(MAIN) $(DLL)
	$(CC) -reference:$(DLL) -target:exe -out:$(EXE) $(MAIN)

# Run the program and save output to a file
run: $(EXE)
	mono $(EXE) > $(OUT)

# Clean compiled files
clean:
	rm -f $(EXE) $(DLL) $(OUT)

