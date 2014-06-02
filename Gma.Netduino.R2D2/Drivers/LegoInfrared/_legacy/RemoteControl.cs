// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

#endregion

namespace Gma.Netduino.R2D2.Drivers.LegoInfrared.Internal
{
    
    public class RemoteControl
    {
        private const ushort High = 0xFE00;
        private const ushort Low = 0x0000;

        private readonly SPI spi;
        private readonly byte[] toggle = {0, 0, 0, 0};

        public RemoteControl(Cpu.Pin enablePin)
        {
            //Frequency is 38KHz in the protocol
            const float t_carrier = 1/38.0f;
            //Reality is that there is milliseconds 2us difference in the output as there is always milliseconds 2us bit on on SPI using MOSI
            const float t_ushort = t_carrier - 2e-3f;
            //Calulate the outpout frenquency. Here = 16/(1/38 -2^-3) = 658KHz
            const uint freq = (uint) (16.0f/t_ushort);

            var config = new SPI.Configuration(
                enablePin, // SS-pin
                true, // SS-pin active state
                0, // The setup time for the SS port
                0, // The hold time for the SS port
                true, // The idle state of the clock
                true, // The sampling clock edge
                freq, // The SPI clock rate in KHz
                SPI.SPI_module.SPI1); // The used SPI bus (refers to milliseconds MOSI MISO and SCLK pinset)

            spi = new SPI(config);
        }

        internal void ComboMode(ComboDirectModeCommand comboDirectModeCommand, Channel channel)
        {
            //set nibs
            var nib1 = toggle[(uint) channel] | (uint) channel;
            //nib1 = (uint)channel;
            var nib2 = (uint) RemoteControlMode.ComboDirectMode;
            var nib3 = (uint) comboDirectModeCommand.BlueState | (uint) comboDirectModeCommand.RedState;
            var nib4 = 0xf ^ nib1 ^ nib2 ^ nib3;

            this.SpiSend((ushort) nib1, (ushort) nib2, (ushort) nib3, (ushort) nib4, (uint) channel);
        }

        private void SpiSend(ushort nib1, ushort nib2, ushort nib3, ushort nib4, uint channel)
        {
            var code = (ushort) ((nib1 << 12) | (nib2 << 8) | (nib3 << 4) | nib4);
            for (uint i = 0; i < 6; i++)
            {
                this.SpiSendPause(channel, i);
                this.SpiSendData(code);
            }
            this.toggle[channel] = (byte) ((this.toggle[channel] == 0) ? 8 : 0);
        }

        private void SpiSendPause(uint channel, uint count)
        {
            var milliseconds = 0;
            // delay for first message
            // (4 - Ch) * Tm
            switch (count)
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
            Pause(milliseconds);
        }

        private static void Pause(int milliseconds)
        {
            Thread.Sleep(milliseconds*16);
        }

        private void SpiSendData(ushort code)
        {
            try
            {
                var tosend = new ushort[522]; // 522 is the max size of the message to be send
                ushort x = 0x8000;
                var i = 0;

                //Start bit
                i = Fill(tosend, i, 6, 39);

                //encoding the 2 codes
                while (x != 0)
                {
                    if ((code & x) != 0)
                        i = Fill(tosend, i, 6, 21);
                    else
                        i = Fill(tosend, i, 6, 10);
                    x >>= 1; //next bit
                }
                //stop bit
                i = Fill(tosend, i, 6, 39);
                Send(tosend);
            }
            catch (Exception e)
            {
                Debug.Print("error spi send: " + e.Message);
            }
        }

        protected virtual void Send(ushort[] tosend)
        {
            this.spi.Write(tosend);
        }

        private static int Fill(ushort[] uBuff, int iStart, int highCount, int lowCount)
        {
            //Bit high = 6 x IR + 21 x ZE
            int inc;
            var i = iStart;
            //High bit
            for (inc = 0; inc < highCount; inc++)
            {
                uBuff[i] = High;
                i++;
            }
            for (inc = 0; inc < lowCount; inc++)
            {
                uBuff[i] = Low;
                i++;
            }
            return i;
        }
    }
}