using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Lucia.Core.TermIndex;
using Microsoft.PowerBI.ExploreHost.Utils;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200004D RID: 77
	internal sealed class DataIndexDisposer : IDataIndexDisposer
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00007BAF File Offset: 0x00005DAF
		internal DataIndexDisposer()
		{
			this.m_disposingTasks = new List<Task>();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007BD0 File Offset: 0x00005DD0
		public void Dispose(DataIndex index)
		{
			Task task = Task.Run(delegate
			{
				try
				{
					index.Dispose();
				}
				catch (Exception ex)
				{
					ExploreHostUtils.TraceLuciaSessionDisposalTelemetry(ex);
				}
			});
			object disposingTaskLock = this.m_disposingTaskLock;
			lock (disposingTaskLock)
			{
				this.m_disposingTasks.RemoveAll((Task t) => t.IsCompleted);
				this.m_disposingTasks.Add(task);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007C60 File Offset: 0x00005E60
		public void WaitForCompletion()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			object disposingTaskLock = this.m_disposingTaskLock;
			lock (disposingTaskLock)
			{
				this.m_disposingTasks.ToArray();
			}
			Task.WaitAll(this.m_disposingTasks.ToArray());
			ExploreHostUtils.TraceLuciaSessionDisposalTelemetry(this.m_disposingTasks.Count, stopwatch.ElapsedMilliseconds);
		}

		// Token: 0x040000F0 RID: 240
		private readonly object m_disposingTaskLock = new object();

		// Token: 0x040000F1 RID: 241
		private readonly List<Task> m_disposingTasks;
	}
}
