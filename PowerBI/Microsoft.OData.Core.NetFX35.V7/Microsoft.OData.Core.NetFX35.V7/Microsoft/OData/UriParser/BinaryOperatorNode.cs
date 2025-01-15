using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012A RID: 298
	public sealed class BinaryOperatorNode : SingleValueNode
	{
		// Token: 0x06000D98 RID: 3480 RVA: 0x00028AA6 File Offset: 0x00026CA6
		public BinaryOperatorNode(BinaryOperatorKind operatorKind, SingleValueNode left, SingleValueNode right)
			: this(operatorKind, left, right, null)
		{
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00028AB4 File Offset: 0x00026CB4
		internal BinaryOperatorNode(BinaryOperatorKind operatorKind, SingleValueNode left, SingleValueNode right, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(left, "left");
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(right, "right");
			this.operatorKind = operatorKind;
			this.left = left;
			this.right = right;
			if (typeReference != null)
			{
				this.typeReference = typeReference;
				return;
			}
			if (this.Left == null || this.Right == null || this.Left.TypeReference == null || this.Right.TypeReference == null)
			{
				this.typeReference = null;
				return;
			}
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = this.Left.TypeReference.AsPrimitive();
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference2 = this.Right.TypeReference.AsPrimitive();
			this.typeReference = QueryNodeUtils.GetBinaryOperatorResultType(edmPrimitiveTypeReference, edmPrimitiveTypeReference2, this.OperatorKind);
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00028B68 File Offset: 0x00026D68
		public BinaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00028B70 File Offset: 0x00026D70
		public SingleValueNode Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00028B78 File Offset: 0x00026D78
		public SingleValueNode Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00028B80 File Offset: 0x00026D80
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00028B88 File Offset: 0x00026D88
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.BinaryOperator;
			}
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00028B8B File Offset: 0x00026D8B
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000730 RID: 1840
		private readonly BinaryOperatorKind operatorKind;

		// Token: 0x04000731 RID: 1841
		private readonly SingleValueNode left;

		// Token: 0x04000732 RID: 1842
		private readonly SingleValueNode right;

		// Token: 0x04000733 RID: 1843
		private IEdmTypeReference typeReference;
	}
}
