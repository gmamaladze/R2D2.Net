// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using Gma.Netmf.Hardware.Lego.PowerFunctions.Actuators;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Rc;
using SecretLabs.NETMF.Hardware.Netduino;

#endregion

namespace Gma.Netduino.R2D2
{
    public class Program
    {
        public static void Main()
        {
            var receiver = new Receiver(Channel.Ch1);
            var motor = new Motor(receiver.RedConnector);
            var servo = new Servo(receiver.BlueConnector);
            var sonar = new Sonar(Pins.GPIO_PIN_D0);
            var led = new Led(Pins.GPIO_PIN_D1);

            var robot = new Robot(motor, servo, sonar, led);

            while (!robot.IsBlocked())
            {
                robot.Reset();
                if (robot.HasBarrier())
                {
                    robot.StepForward();
                }
                else
                {
                    robot.TurnRight();
                    robot.StepBackward();                   
                }
            }
        }
    }
}