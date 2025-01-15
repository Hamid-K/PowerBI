using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.DataRefresh;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x02000203 RID: 515
	internal abstract class RefreshableMetadataObjectBody<TOwner> : NamedMetadataObjectBody<TOwner>, IRefreshableMetadataObjectBody, IMetadataObjectBody, ITxObjectBody where TOwner : MetadataObject
	{
		// Token: 0x06001D63 RID: 7523 RVA: 0x000C880C File Offset: 0x000C6A0C
		public RefreshableMetadataObjectBody(TOwner owner)
			: base(owner)
		{
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x000C8815 File Offset: 0x000C6A15
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x000C881D File Offset: 0x000C6A1D
		public bool RefreshRequested { get; set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x000C8826 File Offset: 0x000C6A26
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x000C882E File Offset: 0x000C6A2E
		public RefreshTypeMask RequestedRefreshMask { get; set; }

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x000C8837 File Offset: 0x000C6A37
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x000C883F File Offset: 0x000C6A3F
		public ICollection<OverrideCollection> Overrides { get; set; }

		// Token: 0x06001D6A RID: 7530 RVA: 0x000C8848 File Offset: 0x000C6A48
		public void MarkForRefresh(RefreshType type, ICollection<OverrideCollection> overrides)
		{
			if (overrides != null)
			{
				List<OverrideCollection> list = new List<OverrideCollection>();
				foreach (OverrideCollection overrideCollection in overrides)
				{
					if (overrideCollection.Scope == null)
					{
						OverrideCollection overrideCollection2 = overrideCollection.Clone();
						overrideCollection2.Scope = base.Owner;
						list.Add(overrideCollection2);
					}
					else
					{
						list.Add(overrideCollection);
					}
				}
				overrides = list;
			}
			this.RefreshRequested = true;
			this.RequestedRefreshMask = (this.RequestedRefreshMask |= Utils.ConvertRefreshTypeToMask(type));
			this.Overrides = overrides;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x000C88F4 File Offset: 0x000C6AF4
		public override void CopyFrom(MetadataObjectBody<TOwner> other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeOperationalFlags) == CopyFlags.IncludeOperationalFlags)
			{
				this.CopyOperationalFlags((RefreshableMetadataObjectBody<TOwner>)other);
			}
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x000C891D File Offset: 0x000C6B1D
		public override void ClearOperationFlags()
		{
			base.ClearOperationFlags();
			this.RefreshRequested = false;
			this.RequestedRefreshMask = RefreshTypeMask.None;
			this.Overrides = null;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x000C893A File Offset: 0x000C6B3A
		private void CopyOperationalFlags(RefreshableMetadataObjectBody<TOwner> other)
		{
			this.RefreshRequested = other.RefreshRequested;
			this.RequestedRefreshMask = other.RequestedRefreshMask;
			this.Overrides = other.Overrides;
		}
	}
}
