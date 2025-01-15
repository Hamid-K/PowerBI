using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000268 RID: 616
	public sealed class UnaryOperatorNode : SingleValueNode
	{
		// Token: 0x060015AA RID: 5546 RVA: 0x0004BE4A File Offset: 0x0004A04A
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

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0004BE8A File Offset: 0x0004A08A
		public UnaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x0004BE92 File Offset: 0x0004A092
		public SingleValueNode Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0004BE9A File Offset: 0x0004A09A
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0004BEA2 File Offset: 0x0004A0A2
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.UnaryOperator;
			}
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0004BEA5 File Offset: 0x0004A0A5
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000902 RID: 2306
		private readonly SingleValueNode operand;

		// Token: 0x04000903 RID: 2307
		private readonly UnaryOperatorKind operatorKind;

		// Token: 0x04000904 RID: 2308
		private IEdmTypeReference typeReference;
	}
}
