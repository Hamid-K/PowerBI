using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000163 RID: 355
	public sealed class UnaryOperatorNode : SingleValueNode
	{
		// Token: 0x06000F34 RID: 3892 RVA: 0x0002BA14 File Offset: 0x00029C14
		public UnaryOperatorNode(UnaryOperatorKind operatorKind, SingleValueNode operand)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(operand, "operand");
			this.operand = operand;
			this.operatorKind = operatorKind;
			if (operand == null || operand.TypeReference == null)
			{
				this.typeReference = null;
				return;
			}
			this.typeReference = operand.TypeReference;
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0002BA60 File Offset: 0x00029C60
		public UnaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0002BA68 File Offset: 0x00029C68
		public SingleValueNode Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0002BA70 File Offset: 0x00029C70
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0002BA78 File Offset: 0x00029C78
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.UnaryOperator;
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0002BA7B File Offset: 0x00029C7B
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040007A9 RID: 1961
		private readonly SingleValueNode operand;

		// Token: 0x040007AA RID: 1962
		private readonly UnaryOperatorKind operatorKind;

		// Token: 0x040007AB RID: 1963
		private IEdmTypeReference typeReference;
	}
}
