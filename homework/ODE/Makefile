CC = mcs
PEND_SRC = main.cs rkstep.cs driver.cs pendulum.cs
RELA_SRC = relativity_main.cs rkstep.cs driver.cs relativity.cs

all: pendulum.exe relativity.exe run-pendulum run-relativity plot-all

pendulum.exe: $(PEND_SRC)
	$(CC) -out:$@ $^

relativity.exe: $(RELA_SRC)
	$(CC) -out:$@ $^

run-pendulum:
	@echo "Running pendulum simulation..."
	@mono pendulum.exe > Aoutput.txt

run-relativity:
	@echo "Running relativity simulation..."
	@mono relativity.exe > Boutput.txt

plot-pendulum:
	@echo "Generating pendulum plot..."
	@gnuplot plot.gp

plot-orbit:
	@echo "Generating polar orbit plot..."
	@gnuplot relativity_orbit.gp

plot-r-vs-phi:
	@echo "Generating radial vs angle plot..."
	@gnuplot relativity_r_vs_phi.gp

plot-all: plot-pendulum plot-orbit plot-r-vs-phi

clean:
	@rm -f pendulum.exe relativity.exe *.txt *.png *.gp~ *.exe~ *.cs~
