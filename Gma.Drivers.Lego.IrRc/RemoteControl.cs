// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using Gma.Drivers.Lego.IrRc.Advanced;
using Gma.Drivers.Lego.IrRc.Internal;

#endregion

namespace Gma.Drivers.Lego.IrRc
{
    //TODO Make disposable
    public class RemoteControl
    {
        private readonly CommandProcessor m_CommandProcessor;

        public RemoteControl(Channel channel)
            : this(new CommandProcessor(channel))
        {
            
        }

        public RemoteControl(CommandProcessor commandProcessor)
        {
            m_CommandProcessor = commandProcessor;
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