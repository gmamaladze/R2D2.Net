// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System.Threading;
using Gma.Drivers.Lego.IrRc.Internal;

#endregion

namespace Gma.Drivers.Lego.IrRc.Advanced
{
    public class CommandProcessor
    {
        private Toggle m_Toggle;
        private readonly Transmitter m_Transmitter;
        private readonly Channel m_Channel;

        public CommandProcessor(Channel channel)
            : this(new Transmitter(), channel)
        {
        }

        internal CommandProcessor(Transmitter transmitter, Channel channel)
        {
            m_Transmitter = transmitter;
            m_Channel = channel;
            m_Toggle = Toggle.Even;
        }

        public void Execute(Command command)
        {
            if (command.CommandType == CommandType.Pause)
                ExecuteFlowControl(command);
            else 
                ExecuteMessage(command);
        }

        private void ExecuteFlowControl(Command command)
        {
            var pause = (PauseCmd)command;
            Thread.Sleep(pause.Interval.Milliseconds);
        }

        private void ExecuteMessage(Command command)
        {
            var message = MessageFactory.GetMessage(command, m_Channel, m_Toggle);
            m_Transmitter.Send(message);
            TriggerToggle();
        }

        private void TriggerToggle()
        {
            m_Toggle = (m_Toggle == Toggle.Even) ? Toggle.Odd : Toggle.Even;
        }
    }
}