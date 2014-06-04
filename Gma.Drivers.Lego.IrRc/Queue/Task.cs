using System.Threading;

namespace Gma.Drivers.Lego.IrRc.Queue
{
    public class Task
    {
        private readonly CommandQueue m_CommandQueue;
        private Thread m_Thread;

        public Task(CommandQueue commandQueue)
        {
            m_CommandQueue = commandQueue;
            Start();
        }

        private void Start()
        {
            var start = new ThreadStart(Execute);
            m_Thread = new Thread(start);
            m_Thread.Start();
        }

        private void Execute()
        {
            m_CommandQueue.Execute();
            InvokeOnFinished(new OnFinishedEventArgs());
        }

        public bool IsFinished
        {
            get { return m_Thread.IsAlive; }
        }

        public void Cancel()
        {
            m_Thread.Abort();
        }

        public event OnFinishedEventHandler OnFinished;

        protected virtual void InvokeOnFinished(OnFinishedEventArgs args)
        {
            var handler = OnFinished;
            if (handler != null) handler(this, args);
        }
    }
}