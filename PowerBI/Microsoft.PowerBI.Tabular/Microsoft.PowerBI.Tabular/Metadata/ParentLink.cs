using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x02000201 RID: 513
	internal class ParentLink<TOwner, TObject> : ObjectLinkBase<TOwner, TObject> where TOwner : MetadataObject where TObject : MetadataObject
	{
		// Token: 0x06001D5F RID: 7519 RVA: 0x000C877A File Offset: 0x000C697A
		public ParentLink(TOwner owner, string propertyName)
			: base(owner, propertyName)
		{
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x000C8784 File Offset: 0x000C6984
		public override LinkType LinkType
		{
			get
			{
				return LinkType.ParentLink;
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x000C8788 File Offset: 0x000C6988
		public override void CopyFrom(ObjectLinkBase<TOwner, TObject> other, CopyContext context)
		{
			if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
			{
				base.CopyFrom(other, context);
				return;
			}
			if ((context.Flags & CopyFlags.ShallowCopy) == CopyFlags.ShallowCopy)
			{
				base.CopyFrom(other, context);
				return;
			}
			if ((context.Flags & CopyFlags.IncludeObjectIds) == CopyFlags.IncludeObjectIds && base.ObjectID != other.ObjectID)
			{
				base.UpdateObjectId(other.ObjectID);
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000C87E9 File Offset: 0x000C69E9
		public override bool IsEqualTo(ObjectLinkBase<TOwner, TObject> other, CopyContext context)
		{
			return (context.Flags & CopyFlags.IncludeObjectIds) != CopyFlags.IncludeObjectIds || !(base.ObjectID != other.ObjectID);
		}
	}
}
