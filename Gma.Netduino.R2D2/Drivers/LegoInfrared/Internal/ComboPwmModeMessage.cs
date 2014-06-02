using Gma.Netduino.R2D2.Drivers.LegoInfrared.Internal;

namespace Gma.Netduino.R2D2.Drivers.LegoInfrared
{
    internal class ComboPwmModeMessage : Message
    {
        private readonly ComboPwmModeCommand m_Command;

        public ComboPwmModeMessage(Channel channel, Toggle toggle, ComboPwmModeCommand command)
            : base(channel, toggle)
        {
            m_Command = command;
        }

        public override Escape Escape
        {
            get { return Escape.ComboPwmMode; }
        }

        public byte BlueValue
        {
            get { return m_Command.BlueValue; }
        }

        public byte RedValue
        {
            get { return m_Command.RedValue; }
        }

        protected override int GetNiblle2()
        {
            return BlueValue;
        }

        protected override int GetNiblle3()
        {
            return RedValue;
        }
    }
}