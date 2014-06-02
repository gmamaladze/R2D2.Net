// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;

#endregion

namespace Gma.Drivers.Lego.IrRc.Internal
{
    internal class SingleOutputMsg : Message
    {
        public SingleOutputMsg(Channel channel, Toggle toggle) : base(channel, toggle)
        {
        }

        public override Escape Escape
        {
            get { throw new NotImplementedException(); }
        }

        protected override int GetNiblle2()
        {
            throw new NotImplementedException();
        }

        protected override int GetNiblle3()
        {
            throw new NotImplementedException();
        }
    }
}