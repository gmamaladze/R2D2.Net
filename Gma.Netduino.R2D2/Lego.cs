using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace LegoRemoteControl
{
    public class LegoInfrared
    {
        //mode
        public enum LegoMode
        {
            COMBO_DIRECT_MODE = 0x1,
            SINGLE_PIN_CONTINUOUS = 0x2,
            SINGLE_PIN_TIMEOUT = 0x3,
            SINGLE_OUTPUT = 0x4
        };

        //speed
        public enum LegoSpeed
        {
            RED_FLT = 0x0,
            RED_FWD = 0x1,
            RED_REV = 0x2,
            RED_BRK = 0x3,
            BLUE_FLT = 0x0,
            BLUE_FWD = 0x4,
            BLUE_REV = 0x8,
            BLUE_BRK = 0xC
        };

        //channel
        public enum LegoChannel
        {
            CH1 = 0x0,
            CH2 = 0x1,
            CH3 = 0x2,
            CH4 = 0x3
        };
        #region Public

        public LegoInfrared(Cpu.Pin enablePin)
        {
            try
            {
                //Frequency is 38KHz in the protocol
                var t_carrier = 1 / 38.0f;
                //Reality is that there is a 2us difference in the output as there is always a 2us bit on on SPI using MOSI
                var t_ushort = t_carrier - 2e-3f;
                //Calulate the outpout frenquency. Here = 16/(1/38 -2^-3) = 658KHz
                var freq = (uint)(16.0f / t_ushort);

                var config = new SPI.Configuration(
                enablePin,  // SS-pin
                true,             // SS-pin active state
                0,                 // The setup time for the SS port
                0,                 // The hold time for the SS port
                true,              // The idle state of the clock
                true,             // The sampling clock edge
                freq,              // The SPI clock rate in KHz
                SPI.SPI_module.SPI1);   // The used SPI bus (refers to a MOSI MISO and SCLK pinset)

                this.spi = new SPI(config);

            }
            catch (Exception e)
            {
                Debug.Print("Error: " + e.Message);
            }
        }

        public void ComboMode(LegoSpeed blue_speed, LegoSpeed red_speed, LegoChannel channel)
        {
            //set nibs
            var nib1 = toggle[(uint)channel] | (uint)channel;
            //nib1 = (uint)channel;
            var nib2 = (uint)LegoMode.COMBO_DIRECT_MODE;
            var nib3 = (uint)blue_speed | (uint)red_speed;
            var nib4 = 0xf ^ nib1 ^ nib2 ^ nib3;

            this.SpiSend((ushort)nib1, (ushort)nib2, (ushort)nib3, (ushort)nib4, (uint)channel);
        }

        #endregion

        #region Private

        #region Members

        private const ushort High = 0xFE00;
        private const ushort Low = 0x0000;

        private readonly byte[] toggle = new byte[] { 0, 0, 0, 0 };
        private readonly SPI spi;

        #endregion

        private void SpiSend(ushort nib1, ushort nib2, ushort nib3, ushort nib4, uint channel)
        {
            var code = (ushort)((nib1 << 12) | (nib2 << 8) | (nib3 << 4) | nib4);
            for (uint i = 0; i < 6; i++)
            {
                this.SpiSendPause(channel, i);
                this.SpiSendData(code);
            }
            this.toggle[channel] = (byte)((this.toggle[channel] == 0) ? 8 : 0);
        }

        private void SpiSendPause(uint channel, uint count)
        {

            var a = 0;
            // delay for first message
            // (4 - Ch) * Tm
            switch (count)
            {
                case 0:
                    a = 4 - (int)channel + 1;
                    break;
                case 2:
                case 1:
                    a = 5;
                    break;
                case 4:
                case 3:
                    a = 5 + ((int)channel + 1) * 2;
                    break;
            }

            // Tm = 16 ms (in theory 13.7 ms)
            System.Threading.Thread.Sleep(a * 16);
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
                    x >>= 1;  //next bit
                }
                //stop bit
                i = Fill(tosend, i, 6, 39);
                this.spi.Write(tosend);
            }
            catch (Exception e)
            {
                Debug.Print("error spi send: " + e.Message);
            }

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

        #endregion

        //private static int FillStartStop(ushort[] uBuff, int iStart)
        //{
        //    //Bit Start/stop = 6 x IR + 39 x ZE
        //    int inc;
        //    var i = iStart;
        //    //startstop bit
        //    for (inc = 0; inc < 6; inc++)
        //    {
        //        uBuff[i] = _high;
        //        i++;
        //    }
        //    for (inc = 0; inc < 39; inc++)
        //    {
        //        uBuff[i] = _low;
        //        i++;
        //    }
        //    return i;
        //}

        //private static int FillHigh(ushort[] uBuff, int iStart)
        //{
        //    //Bit high = 6 x IR + 21 x ZE
        //    int inc;
        //    var i = iStart;
        //    //High bit
        //    for (inc = 0; inc < 6; inc++)
        //    {
        //        uBuff[i] = _high;
        //        i++;
        //    }
        //    for (inc = 0; inc < 21; inc++)
        //    {
        //        uBuff[i] = _low;
        //        i++;
        //    }
        //    return i;
        //}

        //private static int FillLow(ushort[] uBuff, int iStart)
        //{
        //    //Bit low = 6 x IR + 10 x ZE
        //    int inc;
        //    var i = iStart;
        //    //Low bit
        //    for (inc = 0; inc < 6; inc++)
        //    {
        //        uBuff[i] = _high;
        //        i++;
        //    }
        //    for (inc = 0; inc < 10; inc++)
        //    {
        //        uBuff[i] = _low;
        //        i++;
        //    }
        //    return i;
        //}

    }
}