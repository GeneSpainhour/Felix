using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


/*
 * Original source: C:\Users\Gene\Documents\Visual Studio 2015\Projects\Essays\ApexEssay\RetrieverConsole
 */
namespace Felix.Tools
{
	/// <summary>
	/// Adding MarshallByRefObject so we can marshal this across AppDomain boundaries.
	/// After adding, the console, test app started working as if the clients were in
	/// the same AppDomain.
	/// </summary>
	[Serializable]
	public class SynchronizedQueue : MarshalByRefObject
	{
		Queue _que;

		public QueueEvents Events { get; private set; }

		Mutex _mux;

		public SynchronizedQueue()
		{
			_que = new Queue();

			Events = new QueueEvents();

			_mux = new Mutex(false);
		}

		public void Push(object obj)
		{
			_mux.WaitOne();

			_que.Enqueue(obj);

			if (_que.Count > 0)
			{
				SetContent();
			}

			_mux.ReleaseMutex();
		}

        public bool WaitForContent()
        {
           return Events.ContentEvent.WaitOne(-1);
        }

		public void SetContent()
		{
			if (Events != null)
			{
				Events.SetContent();
			}
		}

		public void ResetContent()
		{
			if (Events != null)
			{
				Events.ResetContent();
			}
		}

		public bool HasContent
		{
			get { return Events.HasContent; }
		}

		public bool IsRunning
		{
			get { return Events.IsRunning; }
		}


		public object Pop()
		{
			_mux.WaitOne();

			object oRet = null;

			int nCount = _que.Count;

			if (nCount > 0)
			{
				oRet = _que.Dequeue();

				nCount = _que.Count;

				if (nCount <= 0)
				{
					ResetContent();
				}
			}

			_mux.ReleaseMutex();

			return oRet;
		}

		public int Count
		{
			get { return _que.Count; }
		}
	}

}
