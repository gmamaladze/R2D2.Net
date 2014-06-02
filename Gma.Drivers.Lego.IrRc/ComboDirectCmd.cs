namespace Gma.Drivers.Lego.IrRc
{
    public class ComboDirectCmd : Command
    {
        private readonly ComboDirectState m_BlueState;
        private readonly ComboDirectState m_RedState;

        public ComboDirectCmd(ComboDirectState blueState, ComboDirectState redState)
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

        public override CommandType CommandType
        {
            get { return CommandType.ComboDirect; }
        }
    }
}