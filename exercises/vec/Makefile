all: vec.dll main.exe run

vec.dll: vec.cs
	mcs -target:library -out:vec.dll vec.cs

main.exe: main.cs vec.dll
	mcs -target:exe -out:main.exe -reference:vec.dll main.cs

run:
	mono main.exe

