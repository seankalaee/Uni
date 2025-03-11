CC = mcs
OUT = output.txt
SRC = Program.cs Jacobi.cs Matrix.cs Vector.cs  # Make sure these are capitalized
EXE = program.exe

all: compile run

compile:
	$(CC) -out:$(EXE) $(SRC)

run:
	mono $(EXE) > $(OUT)

clean:
	rm -f $(EXE) $(OUT)

