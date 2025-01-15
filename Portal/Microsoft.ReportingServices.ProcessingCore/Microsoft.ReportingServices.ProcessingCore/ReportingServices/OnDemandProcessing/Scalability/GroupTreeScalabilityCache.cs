using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000841 RID: 2113
	internal sealed class GroupTreeScalabilityCache : PartitionedTreeScalabilityCache
	{
		// Token: 0x06007622 RID: 30242 RVA: 0x001E9A1B File Offset: 0x001E7C1B
		internal GroupTreeScalabilityCache(TreePartitionManager partitionManager, IStorage storage)
			: base(partitionManager, storage, 2000L, 0.3, 2097152L)
		{
		}

		// Token: 0x170027A9 RID: 10153
		// (get) Token: 0x06007623 RID: 30243 RVA: 0x001E9A3A File Offset: 0x001E7C3A
		public override ScalabilityCacheType CacheType
		{
			get
			{
				return ScalabilityCacheType.GroupTree;
			}
		}

		// Token: 0x04003BB1 RID: 15281
		private const long CacheExpansionIntervalMs = 2000L;

		// Token: 0x04003BB2 RID: 15282
		private const double CacheExpansionRatio = 0.3;

		// Token: 0x04003BB3 RID: 15283
		private const long MinReservedMemoryBytes = 2097152L;
	}
}
