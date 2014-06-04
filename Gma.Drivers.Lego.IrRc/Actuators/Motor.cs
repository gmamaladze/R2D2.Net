using System;

namespace Gma.Netmf.Hardware.Lego.IrRc.Motors
{
    public class Motor : Actuator
    {
        public Motor(Connector connector) 
            : base(connector)
        {
        }

        public void SetSpeed(int percent)
        {
            if (percent<-100 || percent>100) throw new ArgumentOutOfRangeException("percent");
            var speed = percent.FromPercent();
            RemoteControl.Execute(Output, speed);
        }

        public void IncSpeed()
        {
            RemoteControl.Execute(Output, IncDec.IncrementPwm);   
        }

        public void DecSpeed()
        {
            RemoteControl.Execute(Output, IncDec.DecrementPwm); 
        }

        public void Brake()
        {
            RemoteControl.Execute(Output, PwmSpeed.BreakThenFloat); 
        }
    }
}