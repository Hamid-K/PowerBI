using System;
using System.Collections;
using System.Threading;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000858 RID: 2136
	public class ProducerQueue
	{
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06004414 RID: 17428 RVA: 0x000E521C File Offset: 0x000E341C
		// (remove) Token: 0x06004415 RID: 17429 RVA: 0x000E5254 File Offset: 0x000E3454
		public event ServiceEventHandler EventAvailable;

		// Token: 0x06004416 RID: 17430 RVA: 0x000E5289 File Offset: 0x000E3489
		private ProducerQueue()
		{
			this.workerThread = new Thread(new ThreadStart(this.Process));
			this.workerThread.IsBackground = true;
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x000E52BF File Offset: 0x000E34BF
		public static ProducerQueue Instance
		{
			get
			{
				return ProducerQueue.scheduler;
			}
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x000E52C6 File Offset: 0x000E34C6
		public void Start()
		{
			if (!this.running)
			{
				this.running = true;
				this.workerThread.Start();
			}
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x000E52E6 File Offset: 0x000E34E6
		public void Stop()
		{
			if (this.running)
			{
				this.running = false;
				this.workerThread.Interrupt();
			}
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x000E5306 File Offset: 0x000E3506
		public void Add(object data)
		{
			this.workQueue.Enqueue(data);
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x000E5314 File Offset: 0x000E3514
		public bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x000E5320 File Offset: 0x000E3520
		private void Process()
		{
			this.isActive = true;
			while (this.running)
			{
				object obj = this.workQueue.Dequeue();
				try
				{
					if (obj != null)
					{
						if (this.EventAvailable != null)
						{
							this.EventAvailable(obj);
						}
					}
				}
				catch (ThreadInterruptedException)
				{
				}
				catch
				{
				}
			}
			this.isActive = false;
		}

		// Token: 0x04002FC9 RID: 12233
		private static readonly ProducerQueue scheduler = new ProducerQueue();

		// Token: 0x04002FCA RID: 12234
		private ProducerQueue.InternalQueue workQueue = new ProducerQueue.InternalQueue();

		// Token: 0x04002FCB RID: 12235
		private Thread workerThread;

		// Token: 0x04002FCC RID: 12236
		private volatile bool isActive;

		// Token: 0x04002FCD RID: 12237
		private volatile bool running;

		// Token: 0x02000859 RID: 2137
		private class InternalQueue
		{
			// Token: 0x0600441E RID: 17438 RVA: 0x000E53A0 File Offset: 0x000E35A0
			public void Enqueue(object obj)
			{
				Queue queue = this.internalQueue;
				lock (queue)
				{
					this.internalQueue.Enqueue(obj);
					Monitor.PulseAll(this.internalQueue);
				}
			}

			// Token: 0x0600441F RID: 17439 RVA: 0x000E53F4 File Offset: 0x000E35F4
			public object Dequeue()
			{
				Queue queue = this.internalQueue;
				object obj;
				lock (queue)
				{
					while (this.internalQueue.Count == 0)
					{
						if (this.WaitUntilPulsed())
						{
							return null;
						}
					}
					obj = this.internalQueue.Dequeue();
				}
				return obj;
			}

			// Token: 0x17001028 RID: 4136
			// (get) Token: 0x06004420 RID: 17440 RVA: 0x000E5458 File Offset: 0x000E3658
			public int Count
			{
				get
				{
					return this.internalQueue.Count;
				}
			}

			// Token: 0x06004421 RID: 17441 RVA: 0x000E5468 File Offset: 0x000E3668
			private bool WaitUntilPulsed()
			{
				try
				{
					Monitor.Wait(this.internalQueue);
				}
				catch (ThreadInterruptedException)
				{
					return true;
				}
				return false;
			}

			// Token: 0x04002FCF RID: 12239
			private Queue internalQueue = new Queue();
		}
	}
}
