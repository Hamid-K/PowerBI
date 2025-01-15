using System;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001FB RID: 507
	internal abstract class NamedMetadataObjectBody<TOwner> : MetadataObjectBody<TOwner>, INamedMetadataObjectBody, IMetadataObjectBody, ITxObjectBody where TOwner : MetadataObject
	{
		// Token: 0x06001CDD RID: 7389 RVA: 0x000C6677 File Offset: 0x000C4877
		public NamedMetadataObjectBody(TOwner owner)
			: base(owner)
		{
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x000C6680 File Offset: 0x000C4880
		// (set) Token: 0x06001CDF RID: 7391 RVA: 0x000C6688 File Offset: 0x000C4888
		public bool RenameRequestedThroughAPI { get; set; }

		// Token: 0x06001CE0 RID: 7392
		public abstract string GetObjectName();

		// Token: 0x06001CE1 RID: 7393 RVA: 0x000C6691 File Offset: 0x000C4891
		public override void CopyFrom(MetadataObjectBody<TOwner> other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeOperationalFlags) == CopyFlags.IncludeOperationalFlags)
			{
				this.CopyOperationalFlags((NamedMetadataObjectBody<TOwner>)other);
			}
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000C66BC File Offset: 0x000C48BC
		private void CompareWith(NamedMetadataObjectBody<TOwner> other, CompareContext context)
		{
			if (!PropertyHelper.AreValuesIdentical(base.Id, other.Id))
			{
				context.RegisterPropertyChange(base.Owner, "Id", typeof(ObjectId), PropertyFlags.DdlAndUser, other.Id, base.Id);
			}
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x000C6713 File Offset: 0x000C4913
		public override void CompareWith(IMetadataObjectBody other, CompareContext context)
		{
			this.CompareWith((NamedMetadataObjectBody<TOwner>)other, context);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000C6722 File Offset: 0x000C4922
		public override void ClearOperationFlags()
		{
			base.ClearOperationFlags();
			this.RenameRequestedThroughAPI = false;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x000C6731 File Offset: 0x000C4931
		private void CopyOperationalFlags(NamedMetadataObjectBody<TOwner> other)
		{
			this.RenameRequestedThroughAPI = other.RenameRequestedThroughAPI;
		}
	}
}
