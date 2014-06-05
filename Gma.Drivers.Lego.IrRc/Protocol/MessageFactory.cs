// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Commands;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Rc;

#endregion

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Protocol
{
    internal static class MessageFactory
    {
        public static Message GetMessage(Command command, Channel channel, Toggle toggle)
        {
            var commandType = command.CommandType;
            switch (commandType)
            {
                case CommandType.Extended:
                    return new ExtendedMsg(channel, toggle, (ExtendedCmd)command);
                case CommandType.ComboDirect:
                    return new ComboDirectMsg(channel, toggle, (ComboDirectCmd)command);
                case CommandType.SingleOutput:
                    return new SingleOutputMsg(channel, toggle, (SingleOutputCmd)command);
                case CommandType.CompboPwm:
                    return new ComboPwmMsg(channel, toggle, (ComboPwmCmd)command);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}