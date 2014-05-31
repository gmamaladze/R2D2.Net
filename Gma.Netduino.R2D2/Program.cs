using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

using LegoRemoteControl;

namespace Gma.Netduino.R2D2
{
    public class Program
    {
        public static void Main()
        {
         
            LegoInfrared myLego = new LegoInfrared(Pins.GPIO_PIN_D13);
            var port = new TristatePort(Pins.GPIO_PIN_D0, false, false, ResistorModes.Disabled);

            while (true)
            {

                var distance = GetDistance(port);
                if (distance > 60)
                {
                      myLego.ComboMode(LegoInfrared.LegoSpeed.BLUE_BRK, LegoInfrared.LegoSpeed.RED_FWD, LegoInfrared.LegoChannel.CH1);
                }
                else
                {

                    if (distance < 40)
                    {
                        myLego.ComboMode(LegoInfrared.LegoSpeed.BLUE_BRK, LegoInfrared.LegoSpeed.RED_REV, LegoInfrared.LegoChannel.CH1);
                    }
                    else
                    {
                        myLego.ComboMode(LegoInfrared.LegoSpeed.BLUE_BRK, LegoInfrared.LegoSpeed.RED_BRK, LegoInfrared.LegoChannel.CH1);

                    }
                }
                //Debug.Print("Range: " + distance + " cm");
                Thread.Sleep(5);
            }
           
        }

        private static int GetDistance(TristatePort _port)
        {
            _port.Active = true; // Put port in write mode
            _port.Write(true);   // Pulse pin
            _port.Write(false);
            _port.Active = false;// Put port in read mode;    
            bool lineState = false;
            while (!lineState) lineState = _port.Read();
            long startOfPulseAt = DateTime.Now.Ticks;

            while (lineState) lineState = _port.Read();
            long endOfPulse = DateTime.Now.Ticks;
            int ticks = (int)(endOfPulse - startOfPulseAt);

            return ticks / 580;
        }


        static void ping_RangeEvent(object sender, PingEventArgs e)
        {
            Debug.Print("Range: " + e.Distance + " cm");
        }
    }
}
