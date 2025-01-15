using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000016 RID: 22
	internal sealed class BatchScalarDeclarationReferenceExpressionNode : ExpressionNode
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x0000447F File Offset: 0x0000267F
		internal BatchScalarDeclarationReferenceExpressionNode(string declarationName)
		{
			this.m_declarationName = declarationName;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000448E File Offset: 0x0000268E
		public string DeclarationName
		{
			get
			{
				return this.m_declarationName;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004496 File Offset: 0x00002696
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.BatchScalarDeclarationReference;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000449C File Offset: 0x0000269C
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			BatchScalarDeclarationReferenceExpressionNode batchScalarDeclarationReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<BatchScalarDeclarationReferenceExpressionNode>(this, other, out flag, out batchScalarDeclarationReferenceExpressionNode))
			{
				return flag;
			}
			return this.m_declarationName.Equals(batchScalarDeclarationReferenceExpressionNode.m_declarationName);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000044C9 File Offset: 0x000026C9
		protected override int GetHashCodeImpl()
		{
			return this.m_declarationName.GetHashCode();
		}

		// Token: 0x04000045 RID: 69
		private readonly string m_declarationName;
	}
}
