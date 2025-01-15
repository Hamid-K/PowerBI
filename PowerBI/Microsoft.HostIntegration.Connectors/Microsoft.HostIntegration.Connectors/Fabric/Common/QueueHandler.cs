using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000410 RID: 1040
	internal class QueueHandler<T>
	{
		// Token: 0x0600241C RID: 9244 RVA: 0x0006ECE4 File Offset: 0x0006CEE4
		public QueueHandler(ReaderQueue<T> queue, IQueueHandler<T> handler)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.m_queue = queue;
			this.m_handler = handler;
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x0006ED16 File Offset: 0x0006CF16
		public ReaderQueue<T> Queue
		{
			get
			{
				return this.m_queue;
			}
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x0006ED20 File Offset: 0x0006CF20
		public void StartProcessing()
		{
			IAsyncResult asyncResult = this.m_queue.BeginDequeue(QueueHandler<T>.ItemAvailableCallback, this);
			if (asyncResult.CompletedSynchronously)
			{
				this.CompleteProcessItem(asyncResult);
			}
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x0006ED50 File Offset: 0x0006CF50
		private static void StaticItemAvailableCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			QueueHandler<T> queueHandler = (QueueHandler<T>)result.AsyncState;
			queueHandler.CompleteProcessItem(result);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x0006ED7C File Offset: 0x0006CF7C
		private void DispatchItem(object state)
		{
			T t = (T)((object)state);
			this.m_handler.ProcessItem(t);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x0006ED9C File Offset: 0x0006CF9C
		private void CompleteProcessItem(IAsyncResult result)
		{
			T t = this.m_queue.EndDequeue(result);
			if (t != null)
			{
				for (;;)
				{
					result = this.m_queue.BeginDequeue(QueueHandler<T>.ItemAvailableCallback, this);
					if (result.CompletedSynchronously)
					{
						Priority priority = Priority.NormalPriority;
						IAsyncOperation asyncOperation = result as IAsyncOperation;
						if (asyncOperation != null)
						{
							priority = asyncOperation.OperationPriority;
						}
						try
						{
							Thread currentThread = Thread.CurrentThread;
							ThreadPriority priority2 = currentThread.Priority;
							T t2;
							try
							{
								Priority.SetPriority(currentThread, priority, priority2);
								t2 = this.m_queue.EndDequeue(result);
							}
							finally
							{
								Priority.RevertPriority(currentThread, priority2);
							}
							if (t2 != null)
							{
								IOCompletionPortWorkQueue iocompletionPortWorkQueue;
								if (priority == Priority.HighPriority)
								{
									iocompletionPortWorkQueue = IOCompletionPortWorkQueue.HighPriorityWorkQueue;
								}
								else
								{
									iocompletionPortWorkQueue = IOCompletionPortWorkQueue.NormalPriorityWorkQueue;
								}
								iocompletionPortWorkQueue.QueueWorkItem(new WaitCallback(this.DispatchItem), t2);
								continue;
							}
						}
						catch (OperationCompletedException ex)
						{
							if (!(ex.InnerException is ObjectDisposedException))
							{
								throw;
							}
						}
						break;
					}
					break;
				}
			}
			this.m_handler.ProcessItem(t);
		}

		// Token: 0x04001654 RID: 5716
		private ReaderQueue<T> m_queue;

		// Token: 0x04001655 RID: 5717
		private IQueueHandler<T> m_handler;

		// Token: 0x04001656 RID: 5718
		private static AsyncCallback ItemAvailableCallback = new AsyncCallback(QueueHandler<T>.StaticItemAvailableCallback);
	}
}
