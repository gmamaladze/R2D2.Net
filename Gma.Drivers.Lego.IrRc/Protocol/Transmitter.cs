// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Threading;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Rc;
using Microsoft.SPOT.Hardware;

#endregion

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Protocol
{
    public class Transmitter : IDisposable
    {
        private const int MessageResendCount = 5;
        private readonly bool m_DisposeSpi;
        private readonly SPI m_Spi;

        public Transmitter() : this(Cpu.Pin.GPIO_NONE)
        {
        }

        public Transmitter(Cpu.Pin enablePin)
            : this(CreateSpi(enablePin), true)
        {
        }

        internal Transmitter(SPI spi, bool disposeSpi)
        {
            m_Spi = spi;
            m_DisposeSpi = disposeSpi;
        }

        public void Dispose()
        {
            if (m_DisposeSpi && m_Spi != null)
            {
                m_Spi.Dispose();
            }
        }

        private static SPI CreateSpi(Cpu.Pin enablePin)
        {
            //Frequency is 38KHz in the protocol
            const float tCarrier = 1/38.0f;
            //Reality is that there is milliseconds 2us difference in the output as there is always milliseconds 2us bit on on SPI using MOSI
            const float tUshort = tCarrier - 2e-3f;
            //Calulate the outpout frenquency. Here = 16/(1/38 -2^-3) = 658KHz
            const uint freq = (uint) (16.0f/tUshort);

            var config = new SPI.Configuration(
                enablePin, // SS-pin
                true, // SS-pin active state
                0, // The setup time for the SS port
                0, // The hold time for the SS port
                true, // The idle state of the clock
                true, // The sampling clock edge
                freq, // The SPI clock rate in KHz
                SPI.SPI_module.SPI1); // The used SPI bus (refers to milliseconds MOSI MISO and SCLK pinset)

            return new SPI(config);
        }

        internal void Send(Message message)
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
            // delay for first message (4 - Ch) * Tm
            switch (resendIndex)
            {
                case 0:
                    milliseconds = 4 - (int) channel + 1;
                    break;
                case 2:
                case 1:
                    milliseconds = 5;
                    break;
                case 4:
                case 3:
                    milliseconds = 5 + ((int) channel + 1)*2;
                    break;
            }

            // Tm = 16 ms (in theory 13.7 ms)
            Thread.Sleep(milliseconds*16);
        }
    }
}