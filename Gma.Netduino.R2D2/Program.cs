// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Threading;
using Gma.Netduino.R2D2.Drivers.LegoInfrared;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

#endregion

namespace Gma.Netduino.R2D2
{
    public class Program
    {
        private const double SpeedOfSound = 343.21; //m / s

        public static void Main()
        {
            var random = new Random(DateTime.Now.Millisecond);
            using (var port = new TristatePort(Pins.GPIO_PIN_D0, false, false, ResistorModes.Disabled))
            using (var transmitter = new Transmitter())
                while (true)
                {
                    var distance = GetDistance(port);
                    var vector = distance - 0.5;
                    var sign = Math.Sign(vector);
                    var value = Math.Abs(vector);
                    var wheel = random.Next(14)+1;


                    byte speed;
                    if (distance < 1)
                    {
                        speed = 2;
                    }
                    else
                    {
                        speed = 3;
                    }

                    if (distance < 0.50)
                    {
                        wheel = (random.Next(1) == 0) ? 7 : 9;
                        transmitter.Execute(new ComboPwmModeCommand(9, (byte)wheel), Channel.Ch1);
                        Thread.Sleep(1000);
                     
                    }
                    else
                    {
                        var command = new ComboPwmModeCommand(speed, (byte)wheel);
                        transmitter.Execute(command, Channel.Ch1);
                        Thread.Sleep(5);
                    }
                }
        }

        private static double GetDistance(TristatePort _port)
        {
            _port.Active = true;
            _port.Write(true);
            _port.Write(false);
            _port.Active = false;
            bool lineState = false;
            while (!lineState) lineState = _port.Read();
            var startOfPulseAt = DateTime.Now;

            while (lineState) lineState = _port.Read();
            var endOfPulse = DateTime.Now;

            double intervalSeconds = (double) (endOfPulse - startOfPulseAt).Ticks/TimeSpan.TicksPerSecond;
            return intervalSeconds/2*SpeedOfSound;
        }
    }
}