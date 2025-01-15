using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016F RID: 367
	public sealed class BinaryOperatorNode : SingleValueNode
	{
		// Token: 0x06001270 RID: 4720 RVA: 0x000383DA File Offset: 0x000365DA
		public BinaryOperatorNode(BinaryOperatorKind operatorKind, SingleValueNode left, SingleValueNode right)
			: this(operatorKind, left, right, null)
		{
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000383E8 File Offset: 0x000365E8
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

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0003849C File Offset: 0x0003669C
		public BinaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x000384A4 File Offset: 0x000366A4
		public SingleValueNode Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x000384AC File Offset: 0x000366AC
		public SingleValueNode Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x000384B4 File Offset: 0x000366B4
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x000384BC File Offset: 0x000366BC
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.BinaryOperator;
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x000384BF File Offset: 0x000366BF
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000851 RID: 2129
		private readonly BinaryOperatorKind operatorKind;

		// Token: 0x04000852 RID: 2130
		private readonly SingleValueNode left;

		// Token: 0x04000853 RID: 2131
		private readonly SingleValueNode right;

		// Token: 0x04000854 RID: 2132
		private IEdmTypeReference typeReference;
	}
}
