# Compiler
CC = mcs

# Executable name
EXE = program.exe

# Source files
SRC = GenList.cs Program.cs

# Default rule
all: $(EXE)

# Compile
$(EXE): $(SRC)
	$(CC) -out:$(EXE) $(SRC)

# Run the program
run: $(EXE)
	mono $(EXE)

# Clean
clean:
	rm -f $(EXE)
