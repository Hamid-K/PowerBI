using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x020000AC RID: 172
	public sealed class ConvertNode : SingleValueNode
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		public ConvertNode(SingleValueNode source, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			this.source = source;
			this.typeReference = typeReference;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000D304 File Offset: 0x0000B504
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000D30C File Offset: 0x0000B50C
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000D314 File Offset: 0x0000B514
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Convert;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000D317 File Offset: 0x0000B517
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000152 RID: 338
		private readonly SingleValueNode source;

		// Token: 0x04000153 RID: 339
		private readonly IEdmTypeReference typeReference;
	}
}
