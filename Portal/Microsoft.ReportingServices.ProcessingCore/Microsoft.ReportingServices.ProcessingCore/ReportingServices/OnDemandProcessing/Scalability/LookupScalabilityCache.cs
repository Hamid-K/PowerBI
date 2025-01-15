using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200084B RID: 2123
	internal sealed class LookupScalabilityCache : PartitionedTreeScalabilityCache
	{
		// Token: 0x0600767A RID: 30330 RVA: 0x001EB142 File Offset: 0x001E9342
		internal LookupScalabilityCache(TreePartitionManager partitionManager, IStorage storage)
			: base(partitionManager, storage, 2000L, 0.3, 2097152L)
		{
		}

		// Token: 0x170027B1 RID: 10161
		// (get) Token: 0x0600767B RID: 30331 RVA: 0x001EB161 File Offset: 0x001E9361
		public override ScalabilityCacheType CacheType
		{
			get
			{
				return ScalabilityCacheType.Lookup;
			}
		}

		// Token: 0x0600767C RID: 30332 RVA: 0x001EB164 File Offset: 0x001E9364
		internal override BaseReference TransferTo(BaseReference reference)
		{
			IStorable storable = reference.InternalValue();
			BaseReference baseReference = base.AllocateAndPin(storable, ItemSizes.SizeOf(storable));
			ITransferable transferable = storable as ITransferable;
			if (transferable != null)
			{
				transferable.TransferTo(this);
			}
			baseReference.UnPinValue();
			reference.ScalabilityCache.Free(reference);
			return baseReference;
		}

		// Token: 0x04003C01 RID: 15361
		private const long CacheExpansionIntervalMs = 2000L;

		// Token: 0x04003C02 RID: 15362
		private const double CacheExpansionRatio = 0.3;

		// Token: 0x04003C03 RID: 15363
		private const long MinReservedMemoryBytes = 2097152L;
	}
}
