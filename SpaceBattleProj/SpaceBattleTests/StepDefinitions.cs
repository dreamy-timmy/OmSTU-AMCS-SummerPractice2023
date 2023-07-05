using System;
using TechTalk.SpecFlow;
using SpaceBattleLib;

namespace SpaceBattleTests
{
    [Binding]
    public class StepDefinitions
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
}