using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000131 RID: 305
	public sealed class ConvertNode : SingleValueNode
	{
		// Token: 0x06000DD4 RID: 3540 RVA: 0x00028FCE File Offset: 0x000271CE
		public ConvertNode(SingleValueNode source, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			this.source = source;
			this.typeReference = typeReference;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00028FFC File Offset: 0x000271FC
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00029004 File Offset: 0x00027204
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0002900C File Offset: 0x0002720C
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Convert;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0002900F File Offset: 0x0002720F
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400074A RID: 1866
		private readonly SingleValueNode source;

		// Token: 0x0400074B RID: 1867
		private readonly IEdmTypeReference typeReference;
	}
}
