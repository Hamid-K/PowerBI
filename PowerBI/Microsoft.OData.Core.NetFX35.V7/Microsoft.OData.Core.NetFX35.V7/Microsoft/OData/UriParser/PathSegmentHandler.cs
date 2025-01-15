using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200018F RID: 399
	public abstract class PathSegmentHandler
	{
		// Token: 0x0600101F RID: 4127 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(ODataPathSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(TypeSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(NavigationPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(EntitySetSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(SingletonSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(KeySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(PropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(OperationImportSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(OperationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(DynamicPathSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(CountSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(NavigationPropertyLinkSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(ValueSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(BatchSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(BatchReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(MetadataSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(PathTemplateSegment segment)
		{
			throw new NotImplementedException();
		}
	}
}
