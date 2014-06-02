using System;

namespace Gma.Netduino.R2D2.Drivers.LegoInfrared.Internal
{
    internal class SingleOutputModeMessage : Message
    {
        public SingleOutputModeMessage(Channel channel, Toggle toggle) : base(channel, toggle)
        {
        }

        public override Escape Escape
        {
            get { throw new NotImplementedException(); }
        }

        protected override int GetNiblle2()
        {
            throw new NotImplementedException();
        }

        protected override int GetNiblle3()
        {
            throw new NotImplementedException();
        }
    }
}