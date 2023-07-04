namespace SquareEquationLib;

public class SquareEquation
{
   public static double[] Solve(double a, double b, double c)
    {
        double eps = 1e-8;
        if (Math.Abs(a) < eps) throw new System.ArgumentException();
        foreach(var x in new double[] {a,b,c})
        if (System.Double.IsNaN(x) || System.Double.IsInfinity(x)) throw new System.ArgumentException(); 
        
        double D = Math.Pow(b,2) - 4*a*c;
        if (Math.Sign(D) < 0 && !(Math.Abs(D) < eps)) return new double[0];
       double x1 = -(b + Math.Sign(b)*Math.Sqrt(D))/2;
        if (Math.Abs(b) < eps)
        {
            x1 = Math.Sqrt(D)/2;
        }
        double x2 = c/x1;

        if (D < eps) return new double[] {x1};
        
        return new double[] {x1, x2};
    }
}
