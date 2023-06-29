using System;
using Xunit;
using SquareEquationLib;

namespace  XUnit.Coverlet.MSBuild;

public class Sqeq_Solver
{
    [Theory]
    [InlineData(0+1e-20,2,1)]
    [InlineData(System.Double.NaN,2,1)]
    [InlineData(1,System.Double.PositiveInfinity,1)]
    [InlineData(1,2,System.Double.NegativeInfinity)]    
    public void AEqualsZeroOrAnyOfInputIsNanOrInf_ThrowsArgumentException(double a,double b,double c)
    {
        
        Action result = () => 
        { 
            SquareEquation.Solve(a,b,c);
        };
        Assert.Throws<ArgumentException>(result);
    }

    // D < 0 
    [Theory]
    [InlineData(1,1,1)]
    [InlineData(1,4.5/4,1)]
    public void DLessThanZero_ReturnEmptyArray(double a, double b, double c)
    {
        var result = SquareEquation.Solve(a,b,c);
        var expected = new double[0];
        Assert.Equal(result,expected);
    }

    // D = 0
    [Theory]
    [InlineData(1,2,1,-1)]
    [InlineData(1,4,4,-2)]
    [InlineData(1,1,(1-(1e-10))/4,-0.5)]
    public void DEqualsZero_ReturnArrayWithOneElement(double a, double b, double c, double exp)
    {
        var result = SquareEquation.Solve(a,b,c);
        var expected = new double[] {exp};
        Assert.Equal(result, expected);
    }

    // D > 0
    [Theory]
    [InlineData(1,-5,6,3,2)]
    public void DMoreThanZero_ReturnFalse(double a, double b, double c,  double exp1, double exp2)
    {
        var result = SquareEquation.Solve(a,b,c);
        var expected = new double[] {exp1, exp2};
        Assert.Equal(result, expected);
    }


}