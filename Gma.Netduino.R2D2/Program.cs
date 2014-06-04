﻿// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Threading;
using Gma.Netduino.R2D2.Drivers.LegoInfrared;
using Microsoft.SPOT;
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
            var random = new Random();
            int bestIndex = 0;
            var sonarPositions = new byte[] {7, 6, 5, 4, 3, 2, 1, 8, 15, 14, 13, 12, 11, 10, 9};
            var wheelPositions = new byte[] {9, 10, 11, 12, 13, 14, 15, 8, 1, 2, 3, 4, 5, 6, 7};

            using (var port = new TristatePort(Pins.GPIO_PIN_D0, false, false, ResistorModes.Disabled))
            using (var transmitter = new Transmitter())
                while (true)
                {
                    double maxDistance = double.MinValue;
                    for (int index = 0; index < sonarPositions.Length; index++)
                    {
                        var pos = sonarPositions[index];
                        transmitter.Execute(new ComboPwmModeCommand(8, pos), Channel.Ch1);
                        var distance = GetDistance(port);
                        Debug.Print(pos + " - " + distance);
                        if (distance > maxDistance)
                        {
                            maxDistance = distance;
                            bestIndex = index;
                        }
                    }

                    if (maxDistance < 0.25)
                    {
                        var randomDirection = (byte) (random.Next(14) + 1);
                        transmitter.Execute(new ComboPwmModeCommand(13, randomDirection), Channel.Ch1);
                        Thread.Sleep(500);
                        continue;
                    }

                    var wheelPos = wheelPositions[bestIndex];

                    transmitter.Execute(new ComboPwmModeCommand(3, wheelPos), Channel.Ch1);
                    Thread.Sleep(200);
                    transmitter.Execute(new ComboPwmModeCommand(3, 8), Channel.Ch1);
                    Thread.Sleep(500);
                }
        }

        private static double GetDistance(TristatePort port)
        {
            port.Active = true;
            port.Write(true);
            port.Write(false);
            port.Active = false;
            bool lineState = false;
            while (!lineState) lineState = port.Read();
            var startOfPulseAt = DateTime.Now;

            while (lineState) lineState = port.Read();
            var endOfPulse = DateTime.Now;

            double intervalSeconds = (double) (endOfPulse - startOfPulseAt).Ticks/TimeSpan.TicksPerSecond;
            return intervalSeconds/2*SpeedOfSound;
        }
    }
}