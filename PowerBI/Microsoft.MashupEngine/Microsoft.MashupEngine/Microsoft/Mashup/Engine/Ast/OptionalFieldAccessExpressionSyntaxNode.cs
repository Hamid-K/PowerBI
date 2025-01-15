using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B96 RID: 7062
	public class OptionalFieldAccessExpressionSyntaxNode : FieldAccessExpressionSyntaxNode
	{
		// Token: 0x0600B0D1 RID: 45265 RVA: 0x00243692 File Offset: 0x00241892
		public OptionalFieldAccessExpressionSyntaxNode(IExpression expression, Identifier identifier)
			: this(expression, identifier, TokenRange.Null)
		{
		}

		// Token: 0x0600B0D2 RID: 45266 RVA: 0x002436A1 File Offset: 0x002418A1
		public OptionalFieldAccessExpressionSyntaxNode(IExpression expression, Identifier identifier, TokenRange range)
			: base(expression, identifier, range)
		{
		}

		// Token: 0x17002C31 RID: 11313
		// (get) Token: 0x0600B0D3 RID: 45267 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsOptional
		{
			get
			{
				return true;
			}
		}
	}
}
