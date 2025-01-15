using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x02000204 RID: 516
	internal abstract class RefreshablePartitionBody<TOwner> : RefreshableMetadataObjectBody<TOwner>, IRefreshablePartitionBody, IRefreshableMetadataObjectBody, IMetadataObjectBody, ITxObjectBody where TOwner : MetadataObject
	{
		// Token: 0x06001D6E RID: 7534 RVA: 0x000C8960 File Offset: 0x000C6B60
		public RefreshablePartitionBody(TOwner owner)
			: base(owner)
		{
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x000C8969 File Offset: 0x000C6B69
		public bool MergePartitionsRequested
		{
			get
			{
				return this.MergePartitionSources != null && this.MergePartitionSources.Any<Partition>();
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x000C8980 File Offset: 0x000C6B80
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x000C8988 File Offset: 0x000C6B88
		public IEnumerable<Partition> MergePartitionSources { get; set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x000C8991 File Offset: 0x000C6B91
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x000C8999 File Offset: 0x000C6B99
		public bool AnalyzeRefreshPolicyImpactRequested { get; set; }

		// Token: 0x06001D74 RID: 7540 RVA: 0x000C89A2 File Offset: 0x000C6BA2
		public override void CopyFrom(MetadataObjectBody<TOwner> other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeOperationalFlags) == CopyFlags.IncludeOperationalFlags)
			{
				this.CopyOperationalFlags((RefreshablePartitionBody<TOwner>)other);
			}
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x000C89CB File Offset: 0x000C6BCB
		public override void ClearOperationFlags()
		{
			base.ClearOperationFlags();
			this.MergePartitionSources = null;
			this.AnalyzeRefreshPolicyImpactRequested = false;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x000C89E1 File Offset: 0x000C6BE1
		private void CopyOperationalFlags(RefreshablePartitionBody<TOwner> other)
		{
			this.MergePartitionSources = other.MergePartitionSources;
			this.AnalyzeRefreshPolicyImpactRequested = other.AnalyzeRefreshPolicyImpactRequested;
		}
	}
}
