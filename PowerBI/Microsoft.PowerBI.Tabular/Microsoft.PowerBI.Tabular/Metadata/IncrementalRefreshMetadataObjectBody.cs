using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.DataRefresh;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001F0 RID: 496
	internal abstract class IncrementalRefreshMetadataObjectBody<TOwner> : RefreshableMetadataObjectBody<TOwner>, IIncrementalRefreshMetadataObjectBody, IRefreshableMetadataObjectBody, IMetadataObjectBody, ITxObjectBody where TOwner : MetadataObject
	{
		// Token: 0x06001C95 RID: 7317 RVA: 0x000C61D4 File Offset: 0x000C43D4
		public IncrementalRefreshMetadataObjectBody(TOwner owner)
			: base(owner)
		{
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x000C61DD File Offset: 0x000C43DD
		// (set) Token: 0x06001C97 RID: 7319 RVA: 0x000C61E5 File Offset: 0x000C43E5
		public bool ApplyRefreshPolicyRequested { get; set; }

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x000C61EE File Offset: 0x000C43EE
		// (set) Token: 0x06001C99 RID: 7321 RVA: 0x000C61F6 File Offset: 0x000C43F6
		public DateTime? ApplyRefreshPolicyEffectiveDate { get; set; }

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x000C61FF File Offset: 0x000C43FF
		// (set) Token: 0x06001C9B RID: 7323 RVA: 0x000C6207 File Offset: 0x000C4407
		public bool RefreshAfterApplyRefreshPolicyRequested { get; set; }

		// Token: 0x06001C9C RID: 7324 RVA: 0x000C6210 File Offset: 0x000C4410
		public void MarkForRefresh(RefreshType type, ICollection<OverrideCollection> overrides, bool applyRefreshPolicy, DateTime? effectiveDate, bool refreshPartitions)
		{
			base.MarkForRefresh(type, overrides);
			this.ApplyRefreshPolicyRequested = applyRefreshPolicy;
			this.ApplyRefreshPolicyEffectiveDate = effectiveDate;
			this.RefreshAfterApplyRefreshPolicyRequested = applyRefreshPolicy && refreshPartitions;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x000C6233 File Offset: 0x000C4433
		public override void CopyFrom(MetadataObjectBody<TOwner> other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeOperationalFlags) == CopyFlags.IncludeOperationalFlags)
			{
				this.CopyOperationalFlags((IncrementalRefreshMetadataObjectBody<TOwner>)other);
			}
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000C625C File Offset: 0x000C445C
		public override void ClearOperationFlags()
		{
			base.ClearOperationFlags();
			this.ApplyRefreshPolicyRequested = false;
			this.ApplyRefreshPolicyEffectiveDate = null;
			this.RefreshAfterApplyRefreshPolicyRequested = false;
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000C628C File Offset: 0x000C448C
		private void CopyOperationalFlags(IncrementalRefreshMetadataObjectBody<TOwner> other)
		{
			this.ApplyRefreshPolicyRequested = other.ApplyRefreshPolicyRequested;
			this.RefreshAfterApplyRefreshPolicyRequested = other.RefreshAfterApplyRefreshPolicyRequested;
			this.ApplyRefreshPolicyEffectiveDate = other.ApplyRefreshPolicyEffectiveDate;
		}
	}
}
