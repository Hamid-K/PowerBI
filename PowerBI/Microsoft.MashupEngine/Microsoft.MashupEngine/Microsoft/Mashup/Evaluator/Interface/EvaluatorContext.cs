using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DD0 RID: 7632
	public sealed class EvaluatorContext : IDisposable
	{
		// Token: 0x0600BD2B RID: 48427 RVA: 0x002661E0 File Offset: 0x002643E0
		public EvaluatorContext(EvaluatorConfiguration configuration)
		{
			EvaluatorThreadPool.Initialize(Math.Max((int)((double)configuration.ContainerCount * 0.75), 15));
			this.softCancelTimeout = configuration.SoftCancelTimeout;
			if (configuration.ContainerCount > 0)
			{
				this.concurrentEvaluatorFactory = new ConcurrentDocumentEvaluatorFactory(configuration.Identity, configuration.ContainerCount);
			}
			if (configuration.IsRemote)
			{
				this.remoteEvaluatorFactory = EvaluatorContext.CreateRemoteDocumentEvaluatorFactory(configuration);
			}
		}

		// Token: 0x17002E8D RID: 11917
		// (get) Token: 0x0600BD2C RID: 48428 RVA: 0x00266251 File Offset: 0x00264451
		public int ActiveContainerCount
		{
			get
			{
				if (this.remoteEvaluatorFactory != null)
				{
					return this.remoteEvaluatorFactory.ActiveContainerCount;
				}
				return 0;
			}
		}

		// Token: 0x0600BD2D RID: 48429 RVA: 0x00266268 File Offset: 0x00264468
		public IDisposable BeginSession(string session)
		{
			return this.remoteEvaluatorFactory.BeginSession(session);
		}

		// Token: 0x0600BD2E RID: 48430 RVA: 0x00266276 File Offset: 0x00264476
		public IDocumentEvaluator DocumentEvaluator(IEngineHost engineHost, IEngine engine)
		{
			return new DocumentEvaluator(engineHost, engine, new Func<IEngineHost, IEngine, bool, IDocumentEvaluator>(this.CreateDocumentEvaluator));
		}

		// Token: 0x0600BD2F RID: 48431 RVA: 0x0026628B File Offset: 0x0026448B
		public void Dispose()
		{
			if (this.remoteEvaluatorFactory != null)
			{
				this.remoteEvaluatorFactory.Dispose();
				this.remoteEvaluatorFactory = null;
			}
			if (this.concurrentEvaluatorFactory != null)
			{
				this.concurrentEvaluatorFactory.Dispose();
				this.concurrentEvaluatorFactory = null;
			}
		}

		// Token: 0x0600BD30 RID: 48432 RVA: 0x002662C4 File Offset: 0x002644C4
		private static RemoteDocumentEvaluatorFactory CreateRemoteDocumentEvaluatorFactory(EvaluatorConfiguration configuration)
		{
			IContainerFactory containerFactory = new ProcessContainerFactory(configuration);
			if (configuration.ContainerMaxWorkingSetInMB >= 0)
			{
				containerFactory = new WorkingSetProcessContainerFactory((IProcessContainerFactory)containerFactory, configuration.ContainerMaxWorkingSetInMB);
			}
			if (configuration.SharedMaxWorkingSetInMB >= 0)
			{
				containerFactory = new SharedWorkingSetContainerFactory((IProcessContainerFactory)containerFactory, configuration.SharedMaxWorkingSetInMB);
			}
			return new RemoteDocumentEvaluatorFactory(containerFactory, configuration.Identity, configuration.ContainerMinCount, configuration.ContainerTimeToLive, configuration.SessionTimeToLive);
		}

		// Token: 0x0600BD31 RID: 48433 RVA: 0x0026632C File Offset: 0x0026452C
		private IDocumentEvaluator CreateDocumentEvaluator(IEngineHost engineHost, IEngine engine, bool enableFirewall)
		{
			Func<IEngineHost, IEngine, IDocumentEvaluator> func = (IEngineHost h, IEngine e) => new SimpleDocumentEvaluator(h, e);
			if (enableFirewall)
			{
				Func<IEngineHost, IEngine, IDocumentEvaluator> evaluatorCtorCopy2 = func;
				func = (IEngineHost h, IEngine e) => new FirewallDocumentEvaluator(h, e, evaluatorCtorCopy2);
			}
			if (this.remoteEvaluatorFactory != null)
			{
				func = (IEngineHost h, IEngine e) => this.remoteEvaluatorFactory.CreateDocumentEvaluator(h, e, enableFirewall);
			}
			if (this.concurrentEvaluatorFactory != null)
			{
				Func<IEngineHost, IEngine, IDocumentEvaluator> evaluatorCtorCopy3 = func;
				func = (IEngineHost h, IEngine e) => this.concurrentEvaluatorFactory.CreateDocumentEvaluator(h, e, evaluatorCtorCopy3);
			}
			if (this.softCancelTimeout != TimeSpan.Zero)
			{
				Func<IEngineHost, IEngine, IDocumentEvaluator> evaluatorCtorCopy = func;
				func = (IEngineHost h, IEngine e) => new SoftCancellingDocumentEvaluator(h, e, evaluatorCtorCopy, this.softCancelTimeout);
			}
			return func(engineHost, engine);
		}

		// Token: 0x04006082 RID: 24706
		private readonly TimeSpan softCancelTimeout;

		// Token: 0x04006083 RID: 24707
		private ConcurrentDocumentEvaluatorFactory concurrentEvaluatorFactory;

		// Token: 0x04006084 RID: 24708
		private RemoteDocumentEvaluatorFactory remoteEvaluatorFactory;
	}
}
