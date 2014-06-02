// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings



#endregion

namespace Gma.Drivers.Lego.IrRc.Internal
{
    internal class ComboDirectMsg : Message
    {
        private readonly ComboDirectCmd m_Command;

        public ComboDirectMsg(Channel channel, Toggle toggle, Command command)
            : base(channel, toggle)
        {
            m_Command = (ComboDirectCmd) command;
        }

        public override Escape Escape
        {
            get { return Escape.ComboPwmMode; }
        }

        protected override int GetNiblle2()
        {
            return 0x1; //0001
        }

        protected override int GetNiblle3()
        {
            return (int) m_Command.BlueState | (int) m_Command.RedState;
        }
    }
}