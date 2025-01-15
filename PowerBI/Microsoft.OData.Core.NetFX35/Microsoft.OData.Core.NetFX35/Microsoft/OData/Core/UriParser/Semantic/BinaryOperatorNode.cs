using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000226 RID: 550
	public sealed class BinaryOperatorNode : SingleValueNode
	{
		// Token: 0x060013E3 RID: 5091 RVA: 0x00048D62 File Offset: 0x00046F62
		public BinaryOperatorNode(BinaryOperatorKind operatorKind, SingleValueNode left, SingleValueNode right)
			: this(operatorKind, left, right, null)
		{
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00048D70 File Offset: 0x00046F70
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

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x00048E22 File Offset: 0x00047022
		public BinaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00048E2A File Offset: 0x0004702A
		public SingleValueNode Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00048E32 File Offset: 0x00047032
		public SingleValueNode Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00048E3A File Offset: 0x0004703A
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00048E42 File Offset: 0x00047042
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.BinaryOperator;
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00048E45 File Offset: 0x00047045
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000865 RID: 2149
		private readonly BinaryOperatorKind operatorKind;

		// Token: 0x04000866 RID: 2150
		private readonly SingleValueNode left;

		// Token: 0x04000867 RID: 2151
		private readonly SingleValueNode right;

		// Token: 0x04000868 RID: 2152
		private IEdmTypeReference typeReference;
	}
}
