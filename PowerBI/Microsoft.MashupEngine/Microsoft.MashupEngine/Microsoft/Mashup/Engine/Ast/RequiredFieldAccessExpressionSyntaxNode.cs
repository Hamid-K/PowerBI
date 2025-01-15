using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B97 RID: 7063
	public class RequiredFieldAccessExpressionSyntaxNode : FieldAccessExpressionSyntaxNode
	{
		// Token: 0x0600B0D4 RID: 45268 RVA: 0x002436AC File Offset: 0x002418AC
		public RequiredFieldAccessExpressionSyntaxNode(IExpression expression, Identifier identifier)
			: this(expression, identifier, TokenRange.Null)
		{
		}

		// Token: 0x0600B0D5 RID: 45269 RVA: 0x002436A1 File Offset: 0x002418A1
		public RequiredFieldAccessExpressionSyntaxNode(IExpression expression, Identifier identifier, TokenRange range)
			: base(expression, identifier, range)
		{
		}

		// Token: 0x17002C32 RID: 11314
		// (get) Token: 0x0600B0D6 RID: 45270 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsOptional
		{
			get
			{
				return false;
			}
		}
	}
}
