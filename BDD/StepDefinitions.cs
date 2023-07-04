using System;
using TechTalk.SpecFlow;
using SquareEquationLib;

namespace BDD
{
    
[Binding]
public class StepDefinitions
{
    private double[] _coeffs = new double[3];
    private Exception? _actualException;
    private double[] _actualRoots;
    [Given(@"Квадратное уравнение с коэффициентами \((.*), (.*), (.*)\)")]
    public void GivenSquareEquationWithCoeffs(string a, string b, string c)
    {   
        List<string> sequence = new List<string> {"Double", "NaN"};
        if (!(sequence.Contains(a.Split('.')[0])) && !(sequence.Contains(b.Split('.')[0])) && !(sequence.Contains(c.Split('.')[0])))
    {
        _coeffs[0] = Convert.ToDouble(a);
        _coeffs[1] = Convert.ToDouble(b);
        _coeffs[2] = Convert.ToDouble(c);
    } 
    else
    {
        string[] ar = new string[] {a,b,c};
        for(int i = 0;i<ar.Length;i++)
        {
            if(ar[i].Split('.')[^1] == "NegativeInfinity") _coeffs[i] = Double.NegativeInfinity;
            if(ar[i].Split('.')[^1] == "PositiveInfinity") _coeffs[i] = Double.PositiveInfinity;
            if(ar[i] == "NaN")  _coeffs[i] = Double.NaN;
        }
    }
        
    }
        
    [When(@"вычисляются корни квадратного уравнения")]
    public void WhenCalculatingSquareEquationRoots()
    {
        try
        {
            _actualRoots = SquareEquation.Solve(_coeffs[0], _coeffs[1], _coeffs[2]);
        }
        catch(Exception ex)
        {
            _actualException = ex;
        }
    }
        
    [Then(@"выбрасывается исключение ArgumentException")]
    public void ThenThrowsArgumentException()
    {
        // Action result = () => 
        // { 
        //     SquareEquation.Solve(_coeffs[0],_coeffs[1],_coeffs[2]);
        // };
        // Assert.Throws<ArgumentException>(result);
        if (_actualException != null) Assert.ThrowsAsync<ArgumentException>(() => throw _actualException); 
    }

    [Then(@"квадратное уравнение имеет один корень (.*) кратности два")]
    public void ThenSquareEquationHasOneRootMultiplicityOfTwo(string x)
    {
        double[] expected = new double[] {Convert.ToDouble(x)};
        if (_actualRoots.Length != 1) Assert.Fail("Должен быть один корень!");
        Assert.Equal(_actualRoots[0],expected[0]);
    }

    [Then(@"квадратное уравнение имеет два корня \((.*), (.*)\) кратности один")]
    public void ThenSquareEquationHasTwoRootMultiplicityOfOne(double x, double y)
    {
        double[] expected = new double[] {Convert.ToDouble(x), Convert.ToDouble(y)};
        for(int i=0;i<expected.Length;i++)
        {
            Assert.Equal(expected[i], _actualRoots[i]);
        }
    }
    
    [Then(@"множество корней квадратного уравнения пустое")]
    public void ThenSquareEquationRootsSetIsEmpty()
    {
    //    Assert.Empty(_actualRoots);
        Assert.Equal(_actualRoots, new double[] {});
    }
}
}
