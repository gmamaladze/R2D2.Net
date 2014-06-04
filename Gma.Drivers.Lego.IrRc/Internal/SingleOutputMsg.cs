// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using Gma.Drivers.Lego.IrRc.Advanced;

#endregion



namespace Gma.Drivers.Lego.IrRc.Internal
{
    internal class SingleOutputMsg : Message
    {
        private readonly SingleOutputCmd m_Command;

        public SingleOutputMsg(Channel channel, Toggle toggle, SingleOutputCmd command) : base(channel, toggle)
        {
            m_Command = command;
        }

        public override Escape Escape
        {
            get { return Escape.UseMode; }
        }

        protected override int GetNiblle2()
        {
            return 1 << 2 | (int) m_Command.SingleOutputMode << 1 | (int) m_Command.Output;
        }

        protected override int GetNiblle3()
        {
            return m_Command.Data;
        }
    }
}