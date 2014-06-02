// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using Gma.Netduino.R2D2.Drivers.LegoInfrared.Internal;

#endregion

namespace Gma.Netduino.R2D2.Drivers.LegoInfrared
{
    internal class ComboPwmModeCommand : Command
    {
        private readonly byte m_BlueValue;
        private readonly byte m_RedValue;

        public ComboPwmModeCommand(byte redValue, byte blueValue)
        {
            if (redValue >= 0xF0) throw new ArgumentOutOfRangeException("redValue");
            if (blueValue >= 0xF0) throw new ArgumentOutOfRangeException("blueValue");
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


        public override Message GetMessage(Channel channel, Toggle toggle)
        {
            return new ComboPwmModeMessage(channel, toggle, this);
        }
    }
}