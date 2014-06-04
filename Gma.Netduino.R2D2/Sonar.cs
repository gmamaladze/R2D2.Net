using System;
using System.Threading;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Gma.Netduino.R2D2
{
    public class Sonar : IDisposable
    {
        private readonly TristatePort m_Port;
        private readonly bool m_DisposePort;

        private const double SpeedOfSound = 343.21; //m / s

        public Sonar(Cpu.Pin pin)
            : this(new TristatePort(pin, false, false, ResistorModes.Disabled), true)
        {
            
        }

        readonly AutoResetEvent m_AutoEvent = new AutoResetEvent(false);

        public double GetDistance()
        {
            var thread = new Thread(GetDistance1);
            thread.Start();
            m_AutoEvent.Reset();
            m_AutoEvent.WaitOne(100, true);
            if (thread.IsAlive) thread.Abort();
            return m_Distance;
        }

        private double m_Distance;

        private void GetDistance1()
        {
            m_Port.Active = true;
            m_Port.Write(true);
            m_Port.Write(false);
            m_Port.Active = false;
            bool lineState = false;
            while (!lineState) lineState = m_Port.Read();
            var startOfPulseAt = DateTime.Now;

            while (lineState) lineState = m_Port.Read();
            var endOfPulse = DateTime.Now;

            double intervalSeconds = (double)(endOfPulse - startOfPulseAt).Ticks / TimeSpan.TicksPerSecond;
            m_Distance = intervalSeconds / 2 * SpeedOfSound;
            m_AutoEvent.Set();
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
    }
}