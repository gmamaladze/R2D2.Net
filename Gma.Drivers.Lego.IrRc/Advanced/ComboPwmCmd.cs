// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings



#endregion

namespace Gma.Drivers.Lego.IrRc.Advanced
{
    public class ComboPwmCmd : Command
    {
        private readonly PwmSpeed m_BlueSpeed;
        private readonly PwmSpeed m_RedSpeed;

        public ComboPwmCmd(PwmSpeed redSpeed, PwmSpeed blueSpeed)
        {
            m_RedSpeed = redSpeed;
            m_BlueSpeed = blueSpeed;
        }

        public PwmSpeed RedSpeed
        {
            get { return m_RedSpeed; }
        }

        public PwmSpeed BlueSpeed
        {
            get { return m_BlueSpeed; }
        }


        public override CommandType CommandType
        {
            get { return CommandType.CompboPwm; }
        }
    }
}