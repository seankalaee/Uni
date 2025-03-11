CC = mcs
OUT = output.txt
SRC = main.cs QR.cs matrix.cs vector.cs
EXE = program.exe

all: compile run

compile:
	$(CC) -out:$(EXE) $(SRC)

run:
	mono $(EXE) > $(OUT)

clean:
	rm -f $(EXE) $(OUT)
