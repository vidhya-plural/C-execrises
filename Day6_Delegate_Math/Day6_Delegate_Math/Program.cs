using System;

namespace Delegate_Math
{
    // Define the delegate type
    public delegate double MathOperation(double a, double b);

    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the BasicMath class
            BasicMath math = new BasicMath();

            // Create instances of the delegate for each operation
            MathOperation addOp = new MathOperation(math.Add);
            MathOperation subtractOp = new MathOperation(math.Subtract);
            MathOperation multiplyOp = new MathOperation(math.Multiply);
            MathOperation divideOp = new MathOperation(math.Divide);

            // Test the Add method
            double addResult = addOp(10.5, 5.2);
            Console.WriteLine($"Addition: 10.5 + 5.2 = {addResult}");

            // Test the Subtract method
            double subtractResult = subtractOp(10.5, 5.2);
            Console.WriteLine($"Subtraction: 10.5 - 5.2 = {subtractResult}");

            // Test the Multiply method
            double multiplyResult = multiplyOp(10.5, 5.2);
            Console.WriteLine($"Multiplication: 10.5 * 5.2 = {multiplyResult}");

            // Test the Divide method
            double divideResult;
            try
            {
                divideResult = divideOp(10.5, 5.2);
                Console.WriteLine($"Division: 10.5 / 5.2 = {divideResult}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
