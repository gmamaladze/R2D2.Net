// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using Gma.Drivers.Lego.IrRc.Advanced;

#endregion

namespace Gma.Drivers.Lego.IrRc.Queue
{

    public class CommandQueue
    {
        private readonly CommandProcessor m_CommandProcessor;
        private readonly System.Collections.Queue m_Queue;

        public CommandQueue(Channel channel) :
            this(new CommandProcessor(channel))
        {
        }

        public CommandQueue(CommandProcessor commandProcessor)
        {
            m_CommandProcessor = commandProcessor;
            m_Queue = new System.Collections.Queue();
        }

        public static CommandQueue Create(Channel channel)
        {
            return new CommandQueue(channel);
        }

        public CommandQueue Enqueue(Command command)
        {
            m_Queue.Enqueue(command);
            return this;
        }


        public CommandQueue Enqueue(ExtFunction extFunction)
        {
            var command = CommandFactory.Create(extFunction);
            return Enqueue(command);
        }

        public CommandQueue Enqueue(DirectState blueState, DirectState redState)
        {
            var command = CommandFactory.Create(blueState, redState);
            return Enqueue(command);
        }

        public CommandQueue Enqueue(PwmSpeed redSpeed, PwmSpeed blueSpeed)
        {
            var command = CommandFactory.Create(redSpeed, blueSpeed);
            return Enqueue(command);
        }

        public CommandQueue Enqueue(Output output, IncDec incDec)
        {
            var command = CommandFactory.Create(output, incDec);
            return Enqueue(command);
        }

        public CommandQueue Enqueue(Output output, PwmSpeed speed)
        {
            var command = CommandFactory.Create(output, speed);
            return Enqueue(command);
        }

        public CommandQueue Pause(int milliseconds)
        {
            var command = CommandFactory.Create(milliseconds);
            return Enqueue(command);
        }

        public void Execute()
        {
            while (m_Queue.Count > 0)
            {
                var command = (Command) m_Queue.Dequeue();
                m_CommandProcessor.Execute(command);
            }
        }

        public Task ExecuteAsyc()
        {
            return new Task(this);
        }
    }
}