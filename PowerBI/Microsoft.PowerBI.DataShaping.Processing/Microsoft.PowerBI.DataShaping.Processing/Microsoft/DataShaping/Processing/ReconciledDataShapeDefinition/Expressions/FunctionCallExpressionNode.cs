using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions
{
	// Token: 0x02000057 RID: 87
	internal sealed class FunctionCallExpressionNode : ExpressionNode
	{
		// Token: 0x06000229 RID: 553 RVA: 0x000064F6 File Offset: 0x000046F6
		internal FunctionCallExpressionNode(FunctionKind kind, IList<ExpressionNode> arguments)
		{
			this._kind = kind;
			this._arguments = arguments;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000650C File Offset: 0x0000470C
		internal FunctionKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00006514 File Offset: 0x00004714
		internal IList<ExpressionNode> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000651C File Offset: 0x0000471C
		internal override TResultType Accept<TResultType>(IExpressionNodeVisitor<TResultType> visitor)
		{
			return visitor.Accept(this);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006525 File Offset: 0x00004725
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}({1})", new object[]
			{
				this._kind,
				string.Join<ExpressionNode>(", ", this._arguments)
			});
		}

		// Token: 0x04000151 RID: 337
		private readonly FunctionKind _kind;

		// Token: 0x04000152 RID: 338
		private readonly IList<ExpressionNode> _arguments;
	}
}
