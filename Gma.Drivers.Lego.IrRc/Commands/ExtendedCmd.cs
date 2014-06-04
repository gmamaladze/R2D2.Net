namespace Gma.Netmf.Hardware.Lego.IrRc.Commands
{
    public class ExtendedCmd : Command
    {
        private readonly ExtFunction m_ExtFunction;

        public ExtendedCmd(ExtFunction extFunction)
        {
            m_ExtFunction = extFunction;
        }

        public override CommandType CommandType
        {
            get { return CommandType.Extended; }
        }

        public ExtFunction ExtFunction
        {
            get { return m_ExtFunction; }
        }
    }
}
