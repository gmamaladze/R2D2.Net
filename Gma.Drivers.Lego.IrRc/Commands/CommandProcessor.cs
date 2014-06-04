// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using Gma.Netmf.Hardware.Lego.IrRc.Internal;

#endregion

namespace Gma.Netmf.Hardware.Lego.IrRc.Commands
{
    public class CommandProcessor : IDisposable
    {
        private readonly Channel m_Channel;
        private readonly bool m_DisposeTransmitter;
        private readonly Transmitter m_Transmitter;
        private Toggle m_Toggle;

        public CommandProcessor(Channel channel)
            : this(new Transmitter(), channel, true)
        {
        }

        internal CommandProcessor(Transmitter transmitter, Channel channel, bool disposeTransmitter)
        {
            m_Transmitter = transmitter;
            m_Channel = channel;
            m_DisposeTransmitter = disposeTransmitter;
            m_Toggle = Toggle.Even;
        }

        public void Dispose()
        {
            if (m_DisposeTransmitter)
            {
                m_Transmitter.Dispose();
            }
        }

        public void Execute(Command command)
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