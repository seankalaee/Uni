all: run

sfuns.dll: sfuns.cs
	mcs -target:library -out:sfuns.dll sfuns.cs

main.exe: main.cs sfuns.dll
	mcs -target:exe -out:main.exe -reference:sfuns.dll main.cs

run: main.exe
	mono main.exe > output.txt

clean:
	rm -f *.exe *.dll *.txt
