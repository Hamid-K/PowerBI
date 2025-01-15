using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200015A RID: 346
	public class GenericScheduledTask<T> : IScheduledTask, IIdentifiable where T : IScheduledTaskSequencer
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x0001F454 File Offset: 0x0001D654
		public GenericScheduledTask([NotNull] string name, [NotNull] GenericScheduledTask<T>.TryCreateWorkTicket tryCreateWorkTicket, [NotNull] GenericScheduledTask<T>.CreateScheduledTaskSequencer createScheduledTaskSequencer)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgumentNotNull<GenericScheduledTask<T>.TryCreateWorkTicket>(tryCreateWorkTicket, "tryCreateWorkTicket");
			ExtendedDiagnostics.EnsureArgumentNotNull<GenericScheduledTask<T>.CreateScheduledTaskSequencer>(createScheduledTaskSequencer, "createScheduledTaskSequencer");
			this.Name = name;
			this.m_tryCreateWorkTicket = tryCreateWorkTicket;
			this.m_createScheduledTaskSequencer = createScheduledTaskSequencer;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0001F492 File Offset: 0x0001D692
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x0001F49A File Offset: 0x0001D69A
		public string Name { get; private set; }

		// Token: 0x060008EE RID: 2286 RVA: 0x0001F4A4 File Offset: 0x0001D6A4
		public virtual IAsyncResult BeginExecute(ScheduledTaskInformation info, AsyncCallback callback, object state)
		{
			WorkTicket workTicket = this.m_tryCreateWorkTicket(this);
			if (workTicket == null)
			{
				return new CompletedAsyncResult(callback, state);
			}
			IAsyncResult asyncResult2;
			using (DisposeController disposeController = new DisposeController(workTicket))
			{
				IAsyncResult asyncResult = SequencerInvoker<T>.BeginExecute(this.m_createScheduledTaskSequencer(), workTicket, callback, state);
				disposeController.PreventDispose();
				asyncResult2 = asyncResult;
			}
			return asyncResult2;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001F508 File Offset: 0x0001D708
		public virtual ScheduledTaskResult EndExecute(IAsyncResult asyncResult)
		{
			CompletedAsyncResult completedAsyncResult = asyncResult as CompletedAsyncResult;
			if (completedAsyncResult != null)
			{
				completedAsyncResult.End();
				return ScheduledTaskResult.Skipped;
			}
			T t = SequencerInvoker<T>.EndExecute(asyncResult);
			return t.Result;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00009B3B File Offset: 0x00007D3B
		public virtual void Abort()
		{
		}

		// Token: 0x04000367 RID: 871
		private readonly GenericScheduledTask<T>.TryCreateWorkTicket m_tryCreateWorkTicket;

		// Token: 0x04000368 RID: 872
		private readonly GenericScheduledTask<T>.CreateScheduledTaskSequencer m_createScheduledTaskSequencer;

		// Token: 0x0200062D RID: 1581
		// (Invoke) Token: 0x06002CB2 RID: 11442
		public delegate WorkTicket TryCreateWorkTicket(IScheduledTask task);

		// Token: 0x0200062E RID: 1582
		// (Invoke) Token: 0x06002CB6 RID: 11446
		public delegate T CreateScheduledTaskSequencer();
	}
}
