using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Felix.Tools
{
	[Serializable]
	public class QueueEvents
    {
        public enum Wh
        {
            Content=0,
            Running
        };
        public ManualResetEvent RunningEvent { get; set; }

        public ManualResetEvent ContentEvent { get; set; }
        public ManualResetEvent PauseEvent { get; set; }

        public ManualResetEvent ResumeEvent { get; set; }

        public WaitHandle[] WaitHandles
        {
            get { return new WaitHandle[] {ContentEvent, RunningEvent}; }
        }

        public QueueEvents()
        {
            RunningEvent = new ManualResetEvent(false);
            ContentEvent = new ManualResetEvent(false);
            PauseEvent = new ManualResetEvent(false);
            ResumeEvent = new ManualResetEvent(false);

        }
		/// <summary>
		/// Blocking method to determine Running and Content
		/// </summary>
		/// <param name="nPeriod"></param>
		/// <returns></returns>
        public bool Rdy(int nPeriod)
        {
			bool bRdy= WaitHandle.WaitAll(WaitHandles, nPeriod);

			//bool bRdy = RunningEvent.WaitOne(nPeriod) && ContentEvent.WaitOne(nPeriod);

			return bRdy;
        }

        public void Start()
        {
            RunningEvent.Set();
            PauseEvent.Reset();
        }
		/// <summary>
		/// Resets the Running Event
		/// </summary>
        public void Stop()
        {
            RunningEvent.Reset();
        }

        public void Pause()
        {
            ResumeEvent.Reset();

            PauseEvent.Set();
        }

        public void Resume()
        {
            PauseEvent.Reset();

            ResumeEvent.Set();
        }

        public void SetContent()
        {
            ContentEvent.Set();
        }

        public void ResetContent()
        {
            ContentEvent.Reset();
        }

        public bool WaitOnRun()
        {
            return RunningEvent.WaitOne(-1);
        }

        
		/// <summary>
		/// Non blocking Content Check
		/// </summary>
        public bool HasContent
        {
            get { return ContentEvent.WaitOne(0); }
        }

		/// <summary>
		/// Does non blocking Running check
		/// </summary>
        public bool IsRunning
        {
            get { return RunningEvent.WaitOne(0); }
        }
    }
}
