using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs
{
	// Token: 0x02000191 RID: 401
	internal sealed class StaTaskScheduler : TaskScheduler, IDisposable
	{
		// Token: 0x060012E3 RID: 4835 RVA: 0x0003FF98 File Offset: 0x0003E198
		public StaTaskScheduler(int numberOfThreads)
		{
			if (numberOfThreads < 1)
			{
				throw new ArgumentOutOfRangeException("numberOfThreads");
			}
			this._tasks = new BlockingCollection<Task>();
			this._threads = Enumerable.Range(0, numberOfThreads).Select(delegate(int _)
			{
				Thread thread = new Thread(delegate
				{
					foreach (Task task in this._tasks.GetConsumingEnumerable())
					{
						base.TryExecuteTask(task);
					}
				});
				thread.IsBackground = true;
				thread.SetApartmentState(ApartmentState.STA);
				return thread;
			}).ToList<Thread>();
			this._threads.ForEach(delegate(Thread t)
			{
				t.Start();
			});
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00040012 File Offset: 0x0003E212
		public override int MaximumConcurrencyLevel
		{
			get
			{
				return this._threads.Count;
			}
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00040020 File Offset: 0x0003E220
		public void Dispose()
		{
			if (this._tasks != null)
			{
				this._tasks.CompleteAdding();
				foreach (Thread thread in this._threads)
				{
					thread.Join();
				}
				this._tasks.Dispose();
				this._tasks = null;
			}
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00040098 File Offset: 0x0003E298
		protected override void QueueTask(Task task)
		{
			this._tasks.Add(task);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000400A6 File Offset: 0x0003E2A6
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return this._tasks.ToArray();
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000400B3 File Offset: 0x0003E2B3
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return Thread.CurrentThread.GetApartmentState() == ApartmentState.STA && base.TryExecuteTask(task);
		}

		// Token: 0x0400072F RID: 1839
		private readonly List<Thread> _threads;

		// Token: 0x04000730 RID: 1840
		private BlockingCollection<Task> _tasks;
	}
}
