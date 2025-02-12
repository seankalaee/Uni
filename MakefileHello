Out.txt : hello.exe              # Out.txt depends on hello.exe
	mono hello.exe > Out.txt # run hello.exe, send output to Out.txt
hello.exe : hello.cs             # hello.exe depends on hello.cs
	mcs -target:exe -out:hello.exe hello.cs # compile hello.cs, save bytecode in hello.exe
clean:                           # a phoney target, no dependencies
	rm -f Out.txt hello.exe  # remove secondary files
