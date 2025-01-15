using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D0F RID: 7439
	internal sealed class RemoteDocumentEvaluatorFactory : IDisposable
	{
		// Token: 0x0600B987 RID: 47495 RVA: 0x002597B0 File Offset: 0x002579B0
		public RemoteDocumentEvaluatorFactory(IContainerFactory containerFactory, string identity, int minContainerPoolSize, TimeSpan? containerTimeToLive, TimeSpan sessionTTL)
		{
			this.identity = identity;
			this.sessions = new EvaluatorSessionManager(sessionTTL);
			this.containerFactory = new ContainerPoolContainerFactory(containerFactory, identity, minContainerPoolSize, containerTimeToLive);
		}

		// Token: 0x17002DEA RID: 11754
		// (get) Token: 0x0600B988 RID: 47496 RVA: 0x002597DC File Offset: 0x002579DC
		public int ActiveContainerCount
		{
			get
			{
				if (this.containerFactory != null)
				{
					return this.containerFactory.ActiveContainerCount;
				}
				return 0;
			}
		}

		// Token: 0x0600B989 RID: 47497 RVA: 0x002597F3 File Offset: 0x002579F3
		public IDisposable BeginSession(string sessionId)
		{
			return this.sessions.Reserve(sessionId);
		}

		// Token: 0x0600B98A RID: 47498 RVA: 0x00259804 File Offset: 0x00257A04
		public IDocumentEvaluator CreateDocumentEvaluator(IEngineHost engineHost, IEngine engine, bool enableFirewall)
		{
			IContainerFactory containerFactory = this.GetContainerFactory(engineHost);
			return new RemoteDocumentEvaluator(this.identity, new RemoteEvaluationContainerFactory(containerFactory, engineHost), engine, engineHost, enableFirewall);
		}

		// Token: 0x0600B98B RID: 47499 RVA: 0x00259830 File Offset: 0x00257A30
		private IContainerFactory GetContainerFactory(IEngineHost engineHost)
		{
			ISessionService sessionService = engineHost.QueryService<ISessionService>();
			if (sessionService != null)
			{
				string session = sessionService.Session;
				EvaluatorSession evaluatorSession;
				if (session != null && this.sessions.TryGetSession(session, out evaluatorSession))
				{
					return new SessionContainerFactory(this.containerFactory, evaluatorSession);
				}
			}
			return this.containerFactory;
		}

		// Token: 0x0600B98C RID: 47500 RVA: 0x00259874 File Offset: 0x00257A74
		public void Dispose()
		{
			if (this.containerFactory != null)
			{
				this.containerFactory.Dispose();
				this.containerFactory = null;
			}
		}

		// Token: 0x04005E76 RID: 24182
		private readonly string identity;

		// Token: 0x04005E77 RID: 24183
		private readonly EvaluatorSessionManager sessions;

		// Token: 0x04005E78 RID: 24184
		private IPooledContainerFactory containerFactory;
	}
}
