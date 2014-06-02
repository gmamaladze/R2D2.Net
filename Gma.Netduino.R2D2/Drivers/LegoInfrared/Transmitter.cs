// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Threading;
using Gma.Netduino.R2D2.Drivers.LegoInfrared.Internal;
using Microsoft.SPOT.Hardware;

#endregion

namespace Gma.Netduino.R2D2.Drivers.LegoInfrared
{
    internal class Transmitter : IDisposable
    {
        private readonly SPI m_Spi;
        private const int MessageResendCount = 5;
        private readonly Toggle[] m_Toggle;

        public Transmitter(SPI spi)
        {
            m_Spi = spi;
            m_Toggle = new[]
            {
                Toggle.Even,
                Toggle.Even,
                Toggle.Even,
                Toggle.Even
            };
        }

        public void Execute(Command command, Channel channel)
        {
            var toggle = GetToggle(channel);
            var message = command.GetMessage(channel, toggle);
            Send(message);
            TriggerToggle(channel);
        }

        private Toggle GetToggle(Channel channel)
        {
            var channelIndex = (byte) channel;
            return m_Toggle[channelIndex];
        }

        private void TriggerToggle(Channel channel)
        {
            var channelIndex = (byte)channel;
            var current = m_Toggle[channelIndex];
            m_Toggle[channelIndex] = (current == Toggle.Even) ? Toggle.Odd : Toggle.Even ;
        }

        public void Send(Message message)
        {
            var rawData = message.GetData();
            var channel = message.Channel;

            var data = IrPulseEncoder.Encode(rawData);

            for (byte resendIndex = 0; resendIndex <= MessageResendCount; resendIndex++)
            {
                Pause(channel, resendIndex);
                SendData(data);
            }
        }

        protected virtual void SendData(ushort[] data)
        {
            m_Spi.Write(data);
        }

        protected virtual void Pause(Channel channel, byte resendIndex)
        {
            var milliseconds = 0;
            // delay for first message
            // (4 - Ch) * Tm
            switch (resendIndex)
            {
                case 0:
                    milliseconds = 4 - (int)channel + 1;
                    break;
                case 2:
                case 1:
                    milliseconds = 5;
                    break;
                case 4:
                case 3:
                    milliseconds = 5 + ((int)channel + 1) * 2;
                    break;
            }

            // Tm = 16 ms (in theory 13.7 ms)
            Thread.Sleep(milliseconds * 16);
        }

        public void Dispose()
        {
            m_Spi.Dispose();   
        }
    }
}