using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions
{
	// Token: 0x0200005B RID: 91
	internal sealed class LiteralExpressionNode : ExpressionNode
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00006558 File Offset: 0x00004758
		internal LiteralExpressionNode(ScalarValue value)
		{
			this._value = value;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00006567 File Offset: 0x00004767
		internal ScalarValue Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000656F File Offset: 0x0000476F
		internal override TResultType Accept<TResultType>(IExpressionNodeVisitor<TResultType> visitor)
		{
			return visitor.Accept(this);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006578 File Offset: 0x00004778
		public override string ToString()
		{
			return StringUtil.FormatInvariant("Literal[{0}]", new object[] { this._value });
		}

		// Token: 0x04000158 RID: 344
		private readonly ScalarValue _value;
	}
}
