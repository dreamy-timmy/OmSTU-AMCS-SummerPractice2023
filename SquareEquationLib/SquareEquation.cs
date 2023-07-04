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
        if (D <= -eps) return new double[0];
        double x1, x2;
        if (Math.Abs(b) >= eps)
        {   
            if (Math.Abs(D) < eps) return new double[] {Math.Round(-(b + Math.Sign(b)*Math.Sqrt(D))/(2*a),4)}; 
            x1 = Math.Round(-(b + Math.Sign(b)*Math.Sqrt(D))/(2*a),4);
            x2 = c/x1; 
            return new double[] {x1,x2};

        }
        if (Math.Abs(D) < eps) return new double[] {Math.Round(-(b +Math.Sqrt(D))/(2*a),4)}; 
        x1 = Math.Round(-(b -Math.Sqrt(D))/(2*a),4);
        x2 = Math.Round(-(b +Math.Sqrt(D))/(2*a),4);
        return new double[] {x1,x2};

    }
}
