using Gma.Netduino.R2D2.Drivers.LegoInfrared.Internal;

namespace Gma.Netduino.R2D2.Drivers.LegoInfrared
{
    internal abstract class Command
    {
        public abstract Message GetMessage(Channel channel, Toggle toggle);
    }
}