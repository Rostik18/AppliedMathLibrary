# AppliedMathLibrary
Math tools and structures library based on my C# and math knowledge

v 2.0.0
 - Result struct for wrapping methods calculation results (could be failed or success).
 - CancellationToken support for Gauss method
 - New Matrix methods (Divide, DivideBy, operator /, CreateIdentityMatrix, CalculateDeterminant, CalculateInverse)
 - Polynomial object (methods: Multiply, Sum, Subtract, Divide, HighestPower)
	! Coefficients of polynomial is in reverse order from lowest to highest (a0, a1, a2, ..., an)
 - LagrangianInterpolation
 - Integrals calculation by RectanglesMethod and TrapeziumMethod
 - NewtonMethod for solving nonlinear algebraic equation
 - SecantMethod for solving nonlinear algebraic equation
 - Namespaces breaking changes
 - Methods signatures breaking changes
 - Documentation improvements
 - Minor bug fixes and optimizations
 
v 1.0.0
 - Matrix (methods: Transpose, ToVectors, Subtract, Multiply)
 - Vector (methods: Comparable, Subtract, Sum, Divide, Multiply, Norm, DistanceBetween)
 - Point (similar to Vector)
 - GaussMethod
 - ParetoMethods
 - SlaterMethods
 - Statistics methods (Mean, Median, Mode, MathExpectation, Var, Std, GenerateRandomArray)