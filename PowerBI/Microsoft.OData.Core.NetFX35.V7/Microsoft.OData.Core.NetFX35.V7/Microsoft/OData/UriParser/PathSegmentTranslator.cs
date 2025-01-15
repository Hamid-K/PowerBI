using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000195 RID: 405
	public abstract class PathSegmentTranslator<T>
	{
		// Token: 0x06001054 RID: 4180 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(TypeSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(NavigationPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(EntitySetSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(SingletonSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(KeySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(PropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(OperationImportSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(OperationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(DynamicPathSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(CountSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(NavigationPropertyLinkSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(ValueSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(BatchSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(BatchReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(MetadataSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(PathTemplateSegment segment)
		{
			throw new NotImplementedException();
		}
	}
}
