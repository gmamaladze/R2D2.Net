// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings



#endregion

namespace Gma.Drivers.Lego.IrRc.Internal
{
    /// <summary>
    ///     |   Nibble 1   |   Nibble 2   |   Nibble 3   |   LRC       |
    ///     |  T  E  C  C  |  a  M  M  M  |  D  D  D  D  | L  L  L  L  |
    /// </summary>
    internal abstract class Message
    {
        private const byte Address = 0;
        private readonly Channel m_Channel;
        private readonly Toggle m_Toggle;

        protected Message(Channel channel, Toggle toggle)
        {
            m_Channel = channel;
            m_Toggle = toggle;
        }


        public Channel Channel
        {
            get { return m_Channel; }
        }

        public Toggle Toggle
        {
            get { return m_Toggle; }
        }

        public abstract Escape Escape { get; }

        public ushort GetData()
        {
            int nibble1 = GetNiblle1();
            int nibble2 = GetNiblle2();
            int nibble3 = GetNiblle3();
            int lrc = CalculateLrc(nibble1, nibble2, nibble3);
            var data = (ushort) ((nibble1 << 12) | (nibble2 << 8) | (nibble3 << 4) | lrc);
            return data;
        }

        private int GetNiblle1()
        {
            return (Address << 3) | ((byte) Toggle << 2) | ((byte) Escape << 1) | (byte) Channel;
        }

        protected abstract int GetNiblle2();

        protected abstract int GetNiblle3();

        private int CalculateLrc(int nibble1, int nibble2, int nibble3)
        {
            //Longitudinal Redundancy Check
            return 0xf ^ nibble1 ^ nibble2 ^ nibble3;
        }
    }
}