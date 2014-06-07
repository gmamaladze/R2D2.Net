// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Rc;

#endregion

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Actuators
{
    public static class PwmSpeedExtensions
    {
        private static readonly PwmSpeed[] AscendingSpeeds =
        {
            PwmSpeed.BackwardStep7,
            PwmSpeed.BackwardStep6,
            PwmSpeed.BackwardStep5,
            PwmSpeed.BackwardStep4,
            PwmSpeed.BackwardStep3,
            PwmSpeed.BackwardStep2,
            PwmSpeed.BackwardStep1,
            PwmSpeed.BreakThenFloat,
            PwmSpeed.ForwardStep1,
            PwmSpeed.ForwardStep2,
            PwmSpeed.ForwardStep3,
            PwmSpeed.ForwardStep4,
            PwmSpeed.ForwardStep5,
            PwmSpeed.ForwardStep6,
            PwmSpeed.ForwardStep7
        };

        public static PwmSpeed FromPercent(this int percent)
        {
            var index = percent;// (int)Math.Truncate((75.0 * (100 + percent)) / 1500);
            return AscendingSpeeds[index];
        }

        public static PwmSpeed FromAngle(this int angle)
        {
            var scaledSpeed = (AscendingSpeeds.Length-1)/180.0*angle;
            var index = (int) Math.Ceiling(scaledSpeed);
            return AscendingSpeeds[index];
        }

        public static int ToPercent(this PwmSpeed speed)
        {
            var index = Array.IndexOf(AscendingSpeeds, speed);
            var percent = (index - AscendingSpeeds.Length/2)*100;
            return percent;
        }
    }
}