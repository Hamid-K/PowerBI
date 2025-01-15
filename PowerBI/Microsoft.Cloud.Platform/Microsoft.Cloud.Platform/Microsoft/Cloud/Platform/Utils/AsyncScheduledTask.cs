using System;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200015B RID: 347
	public class AsyncScheduledTask : IScheduledTask, IIdentifiable
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x0001F53B File Offset: 0x0001D73B
		public AsyncScheduledTask(string name, Func<Task<ScheduledTaskResult>> taskFunction)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<Task<ScheduledTaskResult>>>(taskFunction, "taskFunction");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(name, "name");
			this.m_taskFunction = taskFunction;
			this.Name = name;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001F567 File Offset: 0x0001D767
		public IAsyncResult BeginExecute(ScheduledTaskInformation info, AsyncCallback callback, object state)
		{
			return this.ExecuteTask().ToApm(callback, state);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001F576 File Offset: 0x0001D776
		public ScheduledTaskResult EndExecute(IAsyncResult asyncResult)
		{
			return ((Task<ScheduledTaskResult>)asyncResult).ExtendedResult<ScheduledTaskResult>();
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001F583 File Offset: 0x0001D783
		private Task<ScheduledTaskResult> ExecuteTask()
		{
			return this.m_taskFunction();
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Abort()
		{
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0001F590 File Offset: 0x0001D790
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0001F598 File Offset: 0x0001D798
		public string Name { get; private set; }

		// Token: 0x0400036A RID: 874
		private readonly Func<Task<ScheduledTaskResult>> m_taskFunction;
	}
}
