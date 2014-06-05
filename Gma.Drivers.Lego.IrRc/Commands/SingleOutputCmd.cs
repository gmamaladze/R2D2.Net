// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using Gma.Netmf.Hardware.Lego.PowerFunctions.Protocol;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Rc;

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Commands
{
    public class SingleOutputCmd : Command
    {
        private readonly int m_Data;
        private readonly Output m_Output;
        private readonly SingleOutputMode m_SingleOutputMode;

        public SingleOutputCmd(Output output, PwmSpeed speed)
        {
            m_Output = output;
            m_SingleOutputMode = SingleOutputMode.Pwm;
            m_Data = (int) speed;
        }

        public SingleOutputCmd(Output output, IncDec incDec)
        {
            m_Output = output;
            m_SingleOutputMode = SingleOutputMode.IncDec;
            m_Data = (int) incDec;
        }

        public override CommandType CommandType
        {
            get { return CommandType.SingleOutput; }
        }

        internal SingleOutputMode SingleOutputMode
        {
            get { return m_SingleOutputMode; }
        }

        public Output Output
        {
            get { return m_Output; }
        }

        public int Data
        {
            get { return m_Data; }
        }
    }
}