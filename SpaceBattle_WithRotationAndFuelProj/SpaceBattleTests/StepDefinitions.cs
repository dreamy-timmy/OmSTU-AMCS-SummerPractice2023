using System;
using TechTalk.SpecFlow;
using SpaceBattleLib;

namespace SpaceBattleTests
{
    [Binding, Scope(Feature = "Равномерное движение корабля")]
    public class StraightlineUniformMotion
    {
        private Exception _actualExc;
        private (double,double) _actualStartingCoord;
        private (double,double) _actualResultingCoord;
        private (double,double) _actualStartingSpeed;
        private (double,double) _actualResultingSpeed;
        private bool _actualPossobilityToMove = true;

        [Given(@"космический корабль, положение в пространстве которого невозможно определить")]
         public void GivenSpaceshipPositionIsIndefinite()
         {
            _actualStartingCoord = (Double.PositiveInfinity,Double.PositiveInfinity);
         }

        [Given(@"имеет мгновенную скорость \((.*), (.*)\)")]
        public void GivenHasInstantSpeed(double x, double y)
        {
            _actualStartingSpeed = (x,y);
        } 

        [When(@"происходит прямолинейное равномерное движение без деформации")]
        public void WhenStraightlineUniformMotionWithoutDeformation()
        {
            try
            {
                _actualResultingCoord = SpaceBattle.FindingSpaceshipCoordinates(_actualStartingCoord,_actualStartingSpeed, _actualPossobilityToMove);
            }
            catch(Exception e)
            {
                _actualExc = e;
            }
        }

        [Then(@"возникает ошибка Exception")]
        public void ThenExceptionOccurs()
        {
            if (_actualExc != null) Assert.ThrowsAsync<ArgumentException>(() => throw _actualExc);
        }

        [Given(@"космический корабль находится в точке пространства с координатами \((.*), (.*)\)")]
        public void GivenSpaceshipPositionIs(int p0, int p1)
        {
            _actualStartingCoord = (p0,p1);
        }

        [Given(@"скорость корабля определить невозможно")]
         public void GivenSpaceshipSpeedIsIndefinite()
         {
            _actualStartingSpeed = (Double.PositiveInfinity, Double.PositiveInfinity);    
         }
        
        [Given(@"изменить положение в пространстве космического корабля невозможно")]
         public void GivenSpaceshipPositionCantBeChanged()
         {
            _actualPossobilityToMove = false;
         }
        
        [Then(@"космический корабль перемещается в точку пространства с координатами \((.*), (.*)\)")]
        public void ThenSpaceshipMovesToThePosition(int p0, int p1)
        {
            (double, double) expectedResultingCoord = (p0, p1); 
            Assert.Equal(_actualResultingCoord, expectedResultingCoord);
        }
        
    }

    [Binding, Scope(Feature = "Передвижение с помощью топлива")]
    public class SpaceshipRotation
    {
        private Exception _actualExc;
        private double _actualOldSpaceshipFuel;
        private double _actualNewSpaceshipFuel;
        private double _actualFuelConsumptionRateOnMotion;


        [Given(@"космический корабль имеет топливо в объеме (.*) ед")]
        public void GivenSpaceshipHasFuelInVolumeOf(double OldFuel)
        {
            _actualOldSpaceshipFuel = OldFuel;
        }
        
        [Given(@"имеет скорость расхода топлива при движении (.*) ед")]
        public void GivenHasFuelConsumptionRateOnMotion(double FuelConsumptionRate)
        {
            _actualFuelConsumptionRateOnMotion = FuelConsumptionRate;
        }

        [When(@"происходит прямолинейное равномерное движение без деформации")]
        public void WhenStraightlineUniformMotionWithoutDeformation()
        {
            try
            {
                _actualNewSpaceshipFuel = SpaceBattle.FuelMovement(_actualOldSpaceshipFuel, _actualFuelConsumptionRateOnMotion);
            }
            catch(Exception ex)
            {
                _actualExc = ex;
            }
        }

        [Then(@"новый объем топлива космического корабля равен (.*) ед")]
         public void ThenNewSpaceshipFuelVolumeIs(double NewFuel)
         {
            double expected = NewFuel;
            Assert.Equal(_actualNewSpaceshipFuel, expected);
         }

         [Then(@"возникает ошибка Exception")]
         public void ThenExceptionOccurs()
         {
            if (_actualExc != null) Assert.ThrowsAsync<ArgumentException>(() => throw _actualExc);
         }
        
    }

    [Binding, Scope(Feature = "Поворот корабля")]
    public class MotionWithFuel
    {
        private Exception _actualExc;
        private double _actualStartingSpaceshipAngle;
        private double _actualResultingSpaceshipAngle;
        private double _actualInstantSpeedAngle;

        [Given(@"космический корабль имеет угол наклона (.*) град к оси OX")]
         public void GivenSpaceshipInclinationAngleIs(double StartingSpaceshipAngle)
         {
            _actualStartingSpaceshipAngle = StartingSpaceshipAngle;
         }

        [Given(@"космический корабль, угол наклона которого невозможно определить")]
        public void GivenSpaceshipInclinationAngleIsIndefinite()
        {
            _actualStartingSpaceshipAngle = Double.PositiveInfinity;
        } 

        [Given(@"имеет мгновенную угловую скорость (.*) град")]
        public void GivenSpaceshipHasAngularInstantVelocity(double InstantSpeedAngle)
        {
            _actualInstantSpeedAngle = InstantSpeedAngle;
        }

        [Given(@"мгновенную угловую скорость невозможно определить")]
        public void GivenAngularInstantVelocityIsIndefinite()
        {
            _actualInstantSpeedAngle = Double.PositiveInfinity;
        }

        [When(@"происходит вращение вокруг собственной оси")]
        public void WhenRotationAroundItsOwnAxis()
        {
            try
            {
                _actualResultingSpaceshipAngle = SpaceBattle.SpaceshipRotation(_actualStartingSpaceshipAngle,_actualInstantSpeedAngle);
            }
            catch(Exception e)
            {
                _actualExc = e;
            }
        }

        [Then(@"возникает ошибка Exception")]
        public void ThenExceptionOccurs()
        {
            if (_actualExc != null) Assert.ThrowsAsync<ArgumentException>(() => throw _actualExc);
        }

        [Then(@"угол наклона космического корабля к оси OX составляет (.*) град")]
        public void ThenSpaceshipInclinationAngleIs(double InclinationAngle)
        {
            double expectedInclinationAngle = InclinationAngle;
            Assert.Equal(expectedInclinationAngle, _actualResultingSpaceshipAngle);
        }
        
    }
}