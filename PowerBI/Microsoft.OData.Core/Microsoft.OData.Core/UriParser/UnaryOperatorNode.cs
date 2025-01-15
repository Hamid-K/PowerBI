using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B1 RID: 433
	public sealed class UnaryOperatorNode : SingleValueNode
	{
		// Token: 0x0600146C RID: 5228 RVA: 0x0003BAD0 File Offset: 0x00039CD0
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

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0003BB1C File Offset: 0x00039D1C
		public UnaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0003BB24 File Offset: 0x00039D24
		public SingleValueNode Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x0003BB2C File Offset: 0x00039D2C
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0003BB34 File Offset: 0x00039D34
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.UnaryOperator;
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0003BB37 File Offset: 0x00039D37
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008F7 RID: 2295
		private readonly SingleValueNode operand;

		// Token: 0x040008F8 RID: 2296
		private readonly UnaryOperatorKind operatorKind;

		// Token: 0x040008F9 RID: 2297
		private IEdmTypeReference typeReference;
	}
}
