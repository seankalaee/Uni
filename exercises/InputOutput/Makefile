CC = mcs
RM = rm -f

all: commandline stdin filestream

commandline: commandline.cs
	$(CC) -out:commandline.exe commandline.cs

stdin: stdin.cs
	$(CC) -out:stdin.exe stdin.cs

filestream: filestream.cs
	$(CC) -out:filestream.exe filestream.cs

run-commandline:
	mono commandline.exe -numbers:1,2,3,4,5 > out.txt

run-stdin:
	echo "1 2 3 4 5" | mono stdin.exe 2> out.txt

run-filestream:
	echo -e "1\n2\n3\n4\n5" > input.txt
	mono filestream.exe -input:input.txt -output:out.txt

clean:
	$(RM) *.exe *.txt
