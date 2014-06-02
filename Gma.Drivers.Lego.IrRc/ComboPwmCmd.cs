// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;

#endregion

namespace Gma.Drivers.Lego.IrRc
{
    public class ComboPwmCmd : Command
    {
        private readonly byte m_BlueValue;
        private readonly byte m_RedValue;

        public ComboPwmCmd(byte redValue, byte blueValue)
        {
            if (redValue > 0x0F) throw new ArgumentOutOfRangeException("redValue");
            if (blueValue > 0x0F) throw new ArgumentOutOfRangeException("blueValue");
            m_RedValue = redValue;
            m_BlueValue = blueValue;
        }

        public byte RedValue
        {
            get { return m_RedValue; }
        }

        public byte BlueValue
        {
            get { return m_BlueValue; }
        }


        public override CommandType CommandType
        {
            get { return CommandType.CompboPwm; }
        }
    }
}