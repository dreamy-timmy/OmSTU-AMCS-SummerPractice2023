namespace SpaceBattleLib;
public class SpaceBattle
{
    public static (double, double) FindingSpaceshipCoordinates((double, double) startingCoordinates, (double, double) speed, bool PossibilityToMove) 
    {
        foreach(double coordinate in new double[] {startingCoordinates.Item1, startingCoordinates.Item2}) 
        {
            if (System.Double.IsInfinity(coordinate) || System.Double.IsNaN(coordinate)) throw new Exception(); 
        }
        foreach(double coordinate in new double[] {speed.Item1, speed.Item2}) 
        {
            if (System.Double.IsInfinity(coordinate) || System.Double.IsNaN(coordinate)) throw new Exception(); 
        }
        if (!PossibilityToMove) throw new Exception();
        (double, double) resultingCoordinates = (startingCoordinates.Item1+speed.Item1,startingCoordinates.Item2+speed.Item2);
        return resultingCoordinates;
    }
    public static double SpaceshipRotation(double SpaceshipAngleToOx, double AngularInstantVelocity)
    {
        foreach(double x in new double[] {SpaceshipAngleToOx, AngularInstantVelocity}) 
        {
            if (System.Double.IsInfinity(x) || System.Double.IsNaN(x)) throw new Exception(); 
        }
        return SpaceshipAngleToOx+AngularInstantVelocity;
    }
    public static double FuelMovement(double SpaceshipFuel, double FuelConsumptionRateOnMotion)
    {
        if (SpaceshipFuel < FuelConsumptionRateOnMotion) throw new Exception();
        return SpaceshipFuel - FuelConsumptionRateOnMotion;
    }

}
