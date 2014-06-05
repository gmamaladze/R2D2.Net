using Gma.Netmf.Hardware.Lego.PowerFunctions.Rc;

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Commands
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
