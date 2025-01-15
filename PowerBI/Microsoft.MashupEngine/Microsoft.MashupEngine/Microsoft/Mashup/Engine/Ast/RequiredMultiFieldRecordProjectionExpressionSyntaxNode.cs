using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B9B RID: 7067
	public sealed class RequiredMultiFieldRecordProjectionExpressionSyntaxNode : MultiFieldRecordProjectionExpressionSyntaxNode
	{
		// Token: 0x0600B0E4 RID: 45284 RVA: 0x0024371D File Offset: 0x0024191D
		public RequiredMultiFieldRecordProjectionExpressionSyntaxNode(IExpression expression, IList<Identifier> memberNames)
			: this(expression, memberNames, TokenRange.Null)
		{
		}

		// Token: 0x0600B0E5 RID: 45285 RVA: 0x00243712 File Offset: 0x00241912
		public RequiredMultiFieldRecordProjectionExpressionSyntaxNode(IExpression expression, IList<Identifier> memberNames, TokenRange range)
			: base(expression, memberNames, range)
		{
		}

		// Token: 0x17002C3B RID: 11323
		// (get) Token: 0x0600B0E6 RID: 45286 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsOptional
		{
			get
			{
				return false;
			}
		}
	}
}
