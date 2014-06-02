using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Gma.Netduino.R2D2.Drivers.LegoInfrared;
using Gma.Netduino.R2D2.Drivers.LegoInfrared.Internal;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Gma.Netduino.R2D2
{
    public class Program
    {
        public static void Main()
        {
         
            RemoteControl myLego = new RemoteControl(Pins.GPIO_PIN_D13);
            var port = new TristatePort(Pins.GPIO_PIN_D0, false, false, ResistorModes.Disabled);

            while (true)
            {

                var distance = GetDistance(port);
                if (distance > 60)
                {
                      myLego.ComboMode(new ComboDirectModeCommand(ComboDirectState.BlueBrk, ComboDirectState.RedFwd), Channel.Ch1);
                }
                else
                {

                    if (distance < 40)
                    {
                        myLego.ComboMode(new ComboDirectModeCommand(ComboDirectState.BlueBrk, ComboDirectState.RedRev), Channel.Ch1);
                    }
                    else
                    {
                        myLego.ComboMode(new ComboDirectModeCommand(ComboDirectState.BlueBrk, ComboDirectState.RedBrk), Channel.Ch1);

                    }
                }
                //Debug.Print("Range: " + distance + " cm");
                Thread.Sleep(5);
            }
           
        }

        private static float GetDistance(TristatePort _port)
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
            var timeInterval = (int)(endOfPulse - startOfPulseAt);

            return timeInterval / 580;
        }
    }
}
