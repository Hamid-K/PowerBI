using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000CD RID: 205
	[Serializable]
	internal sealed class RecordContextBuilder
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00022E4B File Offset: 0x0002104B
		public RecordContextBuilderPool ObjectPool
		{
			get
			{
				if (this.m_objectPool == null)
				{
					this.m_objectPool = new RecordContextBuilderPool(this);
				}
				return this.m_objectPool;
			}
		}

		// Token: 0x04000338 RID: 824
		public DomainManager DomainManager;

		// Token: 0x04000339 RID: 825
		public RecordBinding RecordBinding;

		// Token: 0x0400033A RID: 826
		public JoinSide JoinSide;

		// Token: 0x0400033B RID: 827
		[NonSerialized]
		private RecordContextBuilderPool m_objectPool;
	}
}
