using System;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D10 RID: 7440
	internal sealed class SessionContainerFactory : IContainerFactory, IDisposable
	{
		// Token: 0x0600B98D RID: 47501 RVA: 0x00259890 File Offset: 0x00257A90
		public SessionContainerFactory(IPooledContainerFactory factory, EvaluatorSession session)
		{
			this.factory = factory;
			this.session = session;
		}

		// Token: 0x0600B98E RID: 47502 RVA: 0x002598A6 File Offset: 0x00257AA6
		public IContainer CreateContainer()
		{
			return this.session.UsingFamiliarContainers<IContainer>(new Func<ConcurrentSet<int>, IContainer>(this.factory.CreateContainer));
		}

		// Token: 0x0600B98F RID: 47503 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x04005E79 RID: 24185
		private readonly EvaluatorSession session;

		// Token: 0x04005E7A RID: 24186
		private readonly IPooledContainerFactory factory;
	}
}
