CC = mcs
RM = rm -f

all: HarmonicSum

HarmonicSum: HarmonicSum.cs
	$(CC) -out:HarmonicSum.exe HarmonicSum.cs

run:
	(time mono HarmonicSum.exe -terms:100000000 -threads:1) > output.txt
	(time mono HarmonicSum.exe -terms:100000000 -threads:2) >> output.txt
	(time mono HarmonicSum.exe -terms:100000000 -threads:3) >> output.txt
	(time mono HarmonicSum.exe -terms:100000000 -threads:4) >> output.txt

clean:
	$(RM) HarmonicSum.exe output.txt

