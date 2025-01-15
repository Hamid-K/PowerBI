using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BAB RID: 7083
	public sealed class MetadataAddBinaryExpressionSyntaxNode : BinaryExpressionSyntaxNode
	{
		// Token: 0x0600B11D RID: 45341 RVA: 0x0024386F File Offset: 0x00241A6F
		public MetadataAddBinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(left, right, range)
		{
		}

		// Token: 0x17002C56 RID: 11350
		// (get) Token: 0x0600B11E RID: 45342 RVA: 0x001422C0 File Offset: 0x001404C0
		public override BinaryOperator2 Operator
		{
			get
			{
				return BinaryOperator2.MetadataAdd;
			}
		}
	}
}
