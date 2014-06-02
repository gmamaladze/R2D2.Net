namespace Gma.Netduino.R2D2.Drivers.LegoInfrared.Internal
{
    internal class ComboDirectModeMessage : Message
    {
        private readonly ComboDirectModeCommand m_Command;

        public ComboDirectModeMessage(Channel channel, Toggle toggle, ComboDirectModeCommand command) 
            : base(channel, toggle)
        {
            m_Command = command;
        }

        public override Escape Escape
        {
            get { return Escape.ComboPwmMode; }
        }

        protected override int GetNiblle2()
        {
            return 0x1; //0001
        }

        protected override int GetNiblle3()
        {
            return (int)m_Command.BlueState | (int)m_Command.RedState;
        }
    }
}