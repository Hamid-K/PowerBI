using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000CE RID: 206
	internal class RecordContextBuilderPool
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x00022E6F File Offset: 0x0002106F
		public RecordContextBuilderPool(RecordContextBuilder builder)
		{
			this.m_builder = builder;
			this.m_objectPool = new TemporalObjectPool(new TemporalObjectPool.CreateObjectDelegate(this.CreateInstance))
			{
				MaxCount = SqlClr.ObjectManager.RecordContextBuilderPoolSize
			};
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00022EA5 File Offset: 0x000210A5
		public void ReturnInstance(TemporalHandle context)
		{
			this.m_objectPool.ReturnObject(context);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00022EB3 File Offset: 0x000210B3
		private void ResetInstance(RecordContextBuilderInstance instance)
		{
			instance.Reset();
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00022EBC File Offset: 0x000210BC
		public TemporalHandle GetInstance(IObjectManager objectManager)
		{
			TemporalHandle temporalHandle = this.m_objectPool.TryGetObject(objectManager);
			RecordContextBuilderInstance recordContextBuilderInstance = temporalHandle.TryGetObject() as RecordContextBuilderInstance;
			if (recordContextBuilderInstance == null)
			{
				throw new Exception("Object returned from object pool was unexpectedly null.");
			}
			this.ResetInstance(recordContextBuilderInstance);
			return temporalHandle;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00022EF8 File Offset: 0x000210F8
		private TemporalHandle CreateInstance(IObjectManager objectManager)
		{
			TransientTokenIdProvider transientTokenIdProvider = new TransientTokenIdProvider(this.m_builder.DomainManager.TokenIdProvider);
			RecordContextBuilderInstance recordContextBuilderInstance = new RecordContextBuilderInstance
			{
				TransientTokenIdProvider = transientTokenIdProvider,
				LookupUpdateContext = new LookupUpdateContext(this.m_builder.DomainManager, transientTokenIdProvider, this.m_builder.RecordBinding, this.m_builder.RecordBinding.GetBoundDomainNames(), null, this.m_builder.JoinSide)
			};
			return objectManager.GetObjectHandle(objectManager.CreateReference(recordContextBuilderInstance));
		}

		// Token: 0x0400033C RID: 828
		private RecordContextBuilder m_builder;

		// Token: 0x0400033D RID: 829
		private TemporalObjectPool m_objectPool;
	}
}
