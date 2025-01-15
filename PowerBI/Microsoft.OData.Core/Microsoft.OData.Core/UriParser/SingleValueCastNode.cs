using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000110 RID: 272
	public sealed class SingleValueCastNode : SingleValueNode
	{
		// Token: 0x06000F66 RID: 3942 RVA: 0x000264BD File Offset: 0x000246BD
		public SingleValueCastNode(SingleValueNode source, IEdmPrimitiveType primitiveType)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmPrimitiveType>(primitiveType, "primitiveType");
			this.source = source;
			this.primitiveTypeReference = new EdmPrimitiveTypeReference(primitiveType, true);
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x000264F1 File Offset: 0x000246F1
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x000264F9 File Offset: 0x000246F9
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.primitiveTypeReference;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x00026501 File Offset: 0x00024701
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueCast;
			}
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00026505 File Offset: 0x00024705
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000787 RID: 1927
		private readonly SingleValueNode source;

		// Token: 0x04000788 RID: 1928
		private readonly IEdmPrimitiveTypeReference primitiveTypeReference;
	}
}
