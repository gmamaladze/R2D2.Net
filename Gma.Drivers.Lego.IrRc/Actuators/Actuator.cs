namespace Gma.Netmf.Hardware.Lego.IrRc.Motors
{
    public abstract class Actuator
    {
        private readonly RemoteControl m_RemoteControl;
        private readonly Output m_Output;

        protected Actuator(Connector connector)
        {
            m_RemoteControl = connector.RemoteControl;
            m_Output = connector.Output;
        }

        protected RemoteControl RemoteControl
        {
            get { return m_RemoteControl; }
        }

        protected Output Output
        {
            get { return m_Output; }
        }
    }
}