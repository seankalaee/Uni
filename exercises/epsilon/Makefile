CC = mcs
EXECUTABLES = minmax machineepsilon tiny double
OUTPUTS = minmax_output.txt machineepsilon_output.txt tiny_output.txt double_output.txt

all: $(EXECUTABLES)

minmax: minmax.cs
	$(CC) minmax.cs -out:minmax.exe

machineepsilon: machineepsilon.cs
	$(CC) machineepsilon.cs -out:machineepsilon.exe

tiny: tiny.cs
	$(CC) tiny.cs -out:tiny.exe

double: double.cs
	$(CC) double.cs -out:double.exe

run: all
	@echo "Running minmax..."
	@mono minmax.exe
	@echo "Running machineepsilon..."
	@mono machineepsilon.exe
	@echo "Running tiny..."
	@mono tiny.exe
	@echo "Running double..."
	@mono double.exe

clean:
	rm -f $(EXECUTABLES:=.exe) $(OUTPUTS)
