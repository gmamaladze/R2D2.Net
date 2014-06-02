using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.Netduino.R2D2.Drivers.LegoInfrared;
using Gma.Netduino.R2D2.Drivers.LegoInfrared.Internal;
using NUnit.Framework;

namespace Gma.Netduino.R2D2.Drivers.Tests
{
    [TestFixture]
    public class Class1
    {
        //public class RemoteControlSpy : RemoteControl
        //{
        //    //public RemoteControlSpy(Pin enablePin)
        //    //    : base(enablePin)
        //    //{
        //    //}
        //}

        [TestCase]
        public void TestX()
        {


            var speeds = Enum.GetValues(typeof(ComboDirectState));
            var channels = Enum.GetValues(typeof(Channel));
            foreach (ComboDirectState speedBlue in speeds)
                foreach (ComboDirectState speedRed in speeds)
                    foreach (Channel channel in channels)
            {
                
            }
        }

        public void TestX(ComboDirectState stateRed, ComboDirectState stateBlue, Channel channel)
        {
            
        }

    }
}
