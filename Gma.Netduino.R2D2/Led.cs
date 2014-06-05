// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using Microsoft.SPOT.Hardware;

#endregion

namespace Gma.Netduino.R2D2
{
    public class Led
    {
        private readonly Cpu.Pin m_Pin;

        public Led(Cpu.Pin pin)
        {
            m_Pin = pin;
        }

        public void TurnOn()
        {
        }

        public void TurnOff()
        {
        }
    }
}