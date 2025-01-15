using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B9A RID: 7066
	public sealed class OptionalMultiFieldRecordProjectionExpressionSyntaxNode : MultiFieldRecordProjectionExpressionSyntaxNode
	{
		// Token: 0x0600B0E1 RID: 45281 RVA: 0x00243703 File Offset: 0x00241903
		public OptionalMultiFieldRecordProjectionExpressionSyntaxNode(IExpression expression, IList<Identifier> memberNames)
			: this(expression, memberNames, TokenRange.Null)
		{
		}

		// Token: 0x0600B0E2 RID: 45282 RVA: 0x00243712 File Offset: 0x00241912
		public OptionalMultiFieldRecordProjectionExpressionSyntaxNode(IExpression expression, IList<Identifier> memberNames, TokenRange range)
			: base(expression, memberNames, range)
		{
		}

		// Token: 0x17002C3A RID: 11322
		// (get) Token: 0x0600B0E3 RID: 45283 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsOptional
		{
			get
			{
				return true;
			}
		}
	}
}
