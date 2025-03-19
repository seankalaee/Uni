all: linear quad plot_quad

compile:
	mcs -out:program.exe Program.cs LinearSpline.cs vector.cs
	mcs -out:program_qspline.exe Program_QSpline.cs QSpline.cs vector.cs

linear: compile
	mono program.exe

quad: compile
	mono program_qspline.exe

plot_quad:
	gnuplot plot_qspline.gnu
