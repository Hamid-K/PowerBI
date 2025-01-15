using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B9D RID: 7069
	public sealed class OptionalElementAccessExpressionSyntaxNode : ElementAccessExpressionSyntaxNode
	{
		// Token: 0x0600B0EC RID: 45292 RVA: 0x00243753 File Offset: 0x00241953
		public OptionalElementAccessExpressionSyntaxNode(IExpression collection, IExpression key, TokenRange range)
			: base(collection, key, range)
		{
		}

		// Token: 0x17002C40 RID: 11328
		// (get) Token: 0x0600B0ED RID: 45293 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsOptional
		{
			get
			{
				return true;
			}
		}
	}
}
