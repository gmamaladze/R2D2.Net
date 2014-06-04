using System;

namespace Gma.Drivers.Lego.IrRc.Advanced
{
    public class PauseCmd : Command
    {
        private readonly TimeSpan m_Interval;

        public PauseCmd(int milliseconds) 
            : this(new TimeSpan(0, 0, 0, 0, milliseconds))
        {
            
        }

        public PauseCmd(TimeSpan interval)
        {
            m_Interval = interval;
        }

        public override CommandType CommandType
        {
            get { return CommandType.Pause; }
        }

        public TimeSpan Interval
        {
            get { return m_Interval; }
        }
    }
}
