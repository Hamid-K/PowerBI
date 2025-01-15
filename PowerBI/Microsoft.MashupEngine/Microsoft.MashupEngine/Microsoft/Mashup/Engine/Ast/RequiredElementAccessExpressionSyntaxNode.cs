using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B9E RID: 7070
	public sealed class RequiredElementAccessExpressionSyntaxNode : ElementAccessExpressionSyntaxNode
	{
		// Token: 0x0600B0EE RID: 45294 RVA: 0x0024375E File Offset: 0x0024195E
		public RequiredElementAccessExpressionSyntaxNode(IExpression collection, IExpression key)
			: this(collection, key, TokenRange.Null)
		{
		}

		// Token: 0x0600B0EF RID: 45295 RVA: 0x00243753 File Offset: 0x00241953
		public RequiredElementAccessExpressionSyntaxNode(IExpression collection, IExpression key, TokenRange range)
			: base(collection, key, range)
		{
		}

		// Token: 0x17002C41 RID: 11329
		// (get) Token: 0x0600B0F0 RID: 45296 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsOptional
		{
			get
			{
				return false;
			}
		}
	}
}
