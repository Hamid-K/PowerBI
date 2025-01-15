using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B7 RID: 439
	public sealed class SingleValueCastNode : SingleValueNode
	{
		// Token: 0x0600116F RID: 4463 RVA: 0x000309ED File Offset: 0x0002EBED
		public SingleValueCastNode(SingleValueNode source, IEdmPrimitiveType primitiveType)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmPrimitiveType>(primitiveType, "primitiveType");
			this.source = source;
			this.primitiveTypeReference = new EdmPrimitiveTypeReference(primitiveType, true);
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x00030A21 File Offset: 0x0002EC21
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x00030A29 File Offset: 0x0002EC29
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.primitiveTypeReference;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00030A31 File Offset: 0x0002EC31
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueCast;
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00030A35 File Offset: 0x0002EC35
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008E8 RID: 2280
		private readonly SingleValueNode source;

		// Token: 0x040008E9 RID: 2281
		private readonly IEdmPrimitiveTypeReference primitiveTypeReference;
	}
}
