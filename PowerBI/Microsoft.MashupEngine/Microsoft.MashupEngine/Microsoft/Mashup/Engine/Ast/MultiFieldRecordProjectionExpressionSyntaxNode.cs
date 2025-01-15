using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B99 RID: 7065
	public abstract class MultiFieldRecordProjectionExpressionSyntaxNode : RangeSyntaxNode, IMultiFieldRecordProjectionExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0DC RID: 45276 RVA: 0x002436DC File Offset: 0x002418DC
		protected MultiFieldRecordProjectionExpressionSyntaxNode(IExpression expression, IList<Identifier> memberNames, TokenRange range)
			: base(range)
		{
			this.expression = expression;
			this.memberNames = memberNames;
		}

		// Token: 0x17002C36 RID: 11318
		// (get) Token: 0x0600B0DD RID: 45277 RVA: 0x0014213C File Offset: 0x0014033C
		public virtual ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.MultiFieldRecordProjection;
			}
		}

		// Token: 0x17002C37 RID: 11319
		// (get) Token: 0x0600B0DE RID: 45278 RVA: 0x002436F3 File Offset: 0x002418F3
		public IExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17002C38 RID: 11320
		// (get) Token: 0x0600B0DF RID: 45279 RVA: 0x002436FB File Offset: 0x002418FB
		public IList<Identifier> MemberNames
		{
			get
			{
				return this.memberNames;
			}
		}

		// Token: 0x17002C39 RID: 11321
		// (get) Token: 0x0600B0E0 RID: 45280
		public abstract bool IsOptional { get; }

		// Token: 0x04005ADB RID: 23259
		private IExpression expression;

		// Token: 0x04005ADC RID: 23260
		private IList<Identifier> memberNames;
	}
}
