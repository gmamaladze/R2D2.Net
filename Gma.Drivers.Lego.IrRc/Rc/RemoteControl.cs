// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Commands;

#endregion

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Rc
{
    public class RemoteControl : IDisposable
    {
        private readonly CommandProcessor m_CommandProcessor;
        private readonly bool m_DisposeCommandProcessor;

        public RemoteControl(Channel channel)
            : this(new CommandProcessor(channel), true)
        {
        }

        public RemoteControl(CommandProcessor commandProcessor, bool disposeCommandProcessor)
        {
            m_CommandProcessor = commandProcessor;
            m_DisposeCommandProcessor = disposeCommandProcessor;
        }

        public void Dispose()
        {
            if (m_DisposeCommandProcessor)
            {
                m_CommandProcessor.Dispose();
            }
        }

        public void Execute(ExtFunction extFunction)
        {
            var command = CommandFactory.Create(extFunction);
            m_CommandProcessor.Execute(command);
        }

        public void Execute(DirectState blueState, DirectState redState)
        {
            var command = CommandFactory.Create(blueState, redState);
            m_CommandProcessor.Execute(command);
        }

        public void Execute(PwmSpeed redSpeed, PwmSpeed blueSpeed)
        {
            var command = CommandFactory.Create(redSpeed, blueSpeed);
            m_CommandProcessor.Execute(command);
        }

        public void Execute(Output output, IncDec incDec)
        {
            var command = CommandFactory.Create(output, incDec);
            m_CommandProcessor.Execute(command);
        }

        public void Execute(Output output, PwmSpeed speed)
        {
            var command = CommandFactory.Create(output, speed);
            m_CommandProcessor.Execute(command);
        }
    }
}