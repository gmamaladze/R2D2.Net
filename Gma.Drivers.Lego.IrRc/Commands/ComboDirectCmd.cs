// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using Gma.Netmf.Hardware.Lego.PowerFunctions.Rc;

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Commands
{
    public class ComboDirectCmd : Command
    {
        private readonly DirectState m_BlueState;
        private readonly DirectState m_RedState;

        public ComboDirectCmd(DirectState blueState, DirectState redState)
        {
            m_BlueState = blueState;
            m_RedState = redState;
        }

        public DirectState BlueState
        {
            get { return m_BlueState; }
        }

        public DirectState RedState
        {
            get { return m_RedState; }
        }

        public override CommandType CommandType
        {
            get { return CommandType.ComboDirect; }
        }
    }
}