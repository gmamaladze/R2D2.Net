using Gma.Netmf.Hardware.Lego.PowerFunctions.Commands;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Rc;

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Protocol
{
    internal class ExtendedMsg : Message
    {
        //  Extended mode message format
        //     +--------------+--------------+--------------+-------------+
        //     |   Nibble 1   |   Nibble 2   |   Nibble 3   |   LRC       |
        //     +--------------+--------------+--------------+-------------+
        //     |  T  E  C  C  |  a  0  0  0  |  F  F  F  F  | L  L  L  L  |
        //     +--------------+--------------+--------------+-------------+

        private readonly ExtendedCmd m_Command;

        public ExtendedMsg(Channel channel, Toggle toggle, ExtendedCmd command) 
            : base(channel, toggle)
        {
            m_Command = command;
        }

        public override Escape Escape
        {
            get { return 0; }
        }

        protected override int GetNiblle2()
        {
            return Address << 3 | 0x0;
        }

        protected override int GetNiblle3()
        {
            return (int)m_Command.ExtFunction;
        }
    }
}