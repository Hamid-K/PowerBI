using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200027D RID: 637
	public sealed class TryingRetriableCommand<T> : ITryingRetriableCommand, IRetriableCommand where T : class, ITryingRetriableFlow
	{
		// Token: 0x06001103 RID: 4355 RVA: 0x0003A927 File Offset: 0x00038B27
		public TryingRetriableCommand(Func<T> createFlow)
		{
			ExtendedDiagnostics.EnsureNotNull<Func<T>>(createFlow, "createFlow");
			this.m_createFlow = createFlow;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x0003A941 File Offset: 0x00038B41
		public T Flow
		{
			get
			{
				return this.m_flow;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x00038D53 File Offset: 0x00036F53
		private ITraceSource Tracer
		{
			get
			{
				return TraceSourceBase<UtilsTrace>.Tracer;
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0003A94C File Offset: 0x00038B4C
		public bool IsRetryRequired()
		{
			this.Tracer.TraceInformation("IsRetryRequired invoked - flow retry on known exception {0}, flow succeeded {1}", new object[]
			{
				this.m_flow.ShouldRetryOnKnownExceptions,
				this.m_flow.Succeeded
			});
			return this.m_flow.ShouldRetryOnKnownExceptions && !this.m_flow.Succeeded;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0003A9C8 File Offset: 0x00038BC8
		public IAsyncResult BeginExecute(RetrierContext retrierContext, AsyncCallback callback, object userState)
		{
			bool flag = retrierContext.CurrentAttemptNumber < retrierContext.MaxAttemptsNumber;
			this.m_flow = this.m_createFlow();
			ExtendedDiagnostics.EnsureNotNull<T>(this.m_flow, "m_flow");
			this.m_flow.ShouldRetryOnKnownExceptions = flag;
			return this.m_flow.BeginExecute(callback, userState);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0003AA28 File Offset: 0x00038C28
		public void EndExecute(IAsyncResult ar)
		{
			this.m_flow.EndExecute(ar);
		}

		// Token: 0x0400063F RID: 1599
		private readonly Func<T> m_createFlow;

		// Token: 0x04000640 RID: 1600
		private T m_flow;
	}
}
