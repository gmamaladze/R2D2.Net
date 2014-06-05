// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Threading;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

#endregion

namespace Gma.Netduino.R2D2
{
    public class Sonar : IDisposable
    {
        private const double SpeedOfSound = 343.21; //m / s

        private readonly AutoResetEvent m_AutoEvent = new AutoResetEvent(false);
        private readonly bool m_DisposePort;
        private readonly TristatePort m_Port;

        private int m_Distance;

        public Sonar(Cpu.Pin pin)
            : this(new TristatePort(pin, false, false, ResistorModes.Disabled), true)
        {
        }

        private Sonar(TristatePort port, bool disposePort)
        {
            m_Port = port;
            m_DisposePort = disposePort;
        }

        public void Dispose()
        {
            if (m_DisposePort)
            {
                m_Port.Dispose();
            }
        }

        public bool TryGetDistance(out int distance)
        {
            var thread = new Thread(MesureDistance);
            thread.Start();
            m_AutoEvent.Reset();
            m_AutoEvent.WaitOne(100, true);
            if (thread.IsAlive) thread.Abort();
            distance = m_Distance;
            return distance != int.MaxValue;
        }

        private void MesureDistance()
        {
            m_Distance = int.MaxValue;
            m_Port.Active = true;
            m_Port.Write(true);
            m_Port.Write(false);
            m_Port.Active = false;
            bool lineState = false;
            while (!lineState) lineState = m_Port.Read();
            var startOfPulseAt = DateTime.Now;

            while (lineState) lineState = m_Port.Read();
            var endOfPulse = DateTime.Now;

            double intervalInSeconds = (endOfPulse - startOfPulseAt).Ticks/(double) TimeSpan.TicksPerSecond;
            m_Distance = (int) Math.Round(intervalInSeconds*SpeedOfSound/2*100);
            m_AutoEvent.Set();
        }
    }
}