using Gma.Netduino.R2D2.Drivers.LegoInfrared.Internal;

namespace Gma.Netduino.R2D2.Drivers.LegoInfrared
{
    internal class ComboDirectModeCommand : Command
    {
        private readonly ComboDirectState m_BlueState;
        private readonly ComboDirectState m_RedState;

        public ComboDirectModeCommand(ComboDirectState blueState, ComboDirectState redState)
        {
            m_BlueState = blueState;
            m_RedState = redState;
        }

        public ComboDirectState BlueState
        {
            get { return m_BlueState; }
        }

        public ComboDirectState RedState
        {
            get { return m_RedState; }
        }

        public override Message GetMessage(Channel channel, Toggle toggle)
        {
            return new ComboDirectModeMessage(channel, toggle, this);
        }
    }
}