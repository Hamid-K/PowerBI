using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BA5 RID: 7077
	public sealed class SubtractBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B10B RID: 45323 RVA: 0x00243889 File Offset: 0x00241A89
		public SubtractBinaryExpressionSyntaxNode(IExpression left, IExpression right)
			: this(left, right, TokenRange.Null)
		{
		}

		// Token: 0x0600B10C RID: 45324 RVA: 0x0024386F File Offset: 0x00241A6F
		public SubtractBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C50 RID: 11344
		// (get) Token: 0x0600B10D RID: 45325 RVA: 0x00002139 File Offset: 0x00000339
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.Subtract;
			}
		}
	}
}
