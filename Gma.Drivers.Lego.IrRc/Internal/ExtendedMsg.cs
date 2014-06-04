using Gma.Drivers.Lego.IrRc.Advanced;

namespace Gma.Drivers.Lego.IrRc.Internal
{
    internal class ExtendedMsg : Message
    {
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
            return 0;
        }

        protected override int GetNiblle3()
        {
            return (int)m_Command.ExtFunction;
        }
    }
}