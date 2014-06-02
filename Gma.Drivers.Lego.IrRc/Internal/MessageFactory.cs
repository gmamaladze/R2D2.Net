// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;

#endregion

namespace Gma.Drivers.Lego.IrRc.Internal
{
    internal static class MessageFactory
    {
        public static Message GetMessage(Command command, Channel channel, Toggle toggle)
        {
            var commandType = command.CommandType;
            switch (commandType)
            {
                case CommandType.Extended:
                    throw new NotImplementedException();
                case CommandType.ComboDirect:
                    return new ComboDirectMsg(channel, toggle, command);
                case CommandType.SingleOutput:
                    throw new NotImplementedException();
                case CommandType.CompboPwm:
                    return new ComboPwmMsg(channel, toggle, command);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}