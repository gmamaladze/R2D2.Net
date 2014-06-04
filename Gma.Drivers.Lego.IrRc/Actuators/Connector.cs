namespace Gma.Netmf.Hardware.Lego.IrRc.Motors
{
    public class Connector
    {
        private readonly RemoteControl m_RemoteControl;
        private readonly Output m_Output;

        internal Connector(RemoteControl remoteControl, Output output)
        {
            m_RemoteControl = remoteControl;
            m_Output = output;
        }

        internal RemoteControl RemoteControl
        {
            get { return m_RemoteControl; }
        }

        public Output Output
        {
            get { return m_Output; }
        }
    }
}