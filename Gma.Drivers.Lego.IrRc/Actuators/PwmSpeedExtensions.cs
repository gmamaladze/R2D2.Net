using System;

namespace Gma.Netmf.Hardware.Lego.IrRc.Motors
{
    public static class PwmSpeedExtensions
    {
        private static readonly PwmSpeed[] AscendingSpeeds = new PwmSpeed[]
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
            var scaledSpeed = AscendingSpeeds.Length / 200.0 * (percent +  100);
            var index = (int)Math.Ceiling(scaledSpeed);
            return AscendingSpeeds[index];
        }

        public static PwmSpeed FromAngle(this int angle)
        {
            var scaledSpeed = AscendingSpeeds.Length / 180.0 * angle;
            var index = (int)Math.Ceiling(scaledSpeed);
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