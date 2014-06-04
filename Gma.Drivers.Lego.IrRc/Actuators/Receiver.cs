namespace Gma.Netmf.Hardware.Lego.IrRc.Motors
{

    internal class Receiver
    {
        private readonly Connector m_RedConnector;
        private readonly Connector m_BlueConnector;

        public Receiver(Channel channel)
            : this(new RemoteControl(channel))
        {
            
        }

        internal Receiver(RemoteControl remoteControl)
        {
            m_RedConnector = new Connector(remoteControl, Output.Red);
            m_BlueConnector = new Connector(remoteControl, Output.Blue);
        }

        public Connector RedConnector
        {
            get { return m_RedConnector; }
        }


        public Connector BlueConnector
        {
            get { return m_BlueConnector; }
        }
    }
}
