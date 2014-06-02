// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings



#endregion

namespace Gma.Drivers.Lego.IrRc.Internal
{
    internal class ComboPwmMsg : Message
    {
        private readonly ComboPwmCmd m_Command;

        public ComboPwmMsg(Channel channel, Toggle toggle, Command command)
            : base(channel, toggle)
        {
            m_Command = (ComboPwmCmd) command;
        }

        public override Escape Escape
        {
            get { return Escape.ComboPwmMode; }
        }

        public byte BlueValue
        {
            get { return m_Command.BlueValue; }
        }

        public byte RedValue
        {
            get { return m_Command.RedValue; }
        }

        protected override int GetNiblle2()
        {
            return BlueValue;
        }

        protected override int GetNiblle3()
        {
            return RedValue;
        }
    }
}