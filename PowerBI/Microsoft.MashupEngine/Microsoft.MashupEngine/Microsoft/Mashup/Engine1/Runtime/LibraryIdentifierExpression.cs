using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200155A RID: 5466
	internal sealed class LibraryIdentifierExpression : NullRangeSyntaxNode, IIdentifierExpression, IExpression, ISyntaxNode
	{
		// Token: 0x060087F8 RID: 34808 RVA: 0x001CD56C File Offset: 0x001CB76C
		public LibraryIdentifierExpression(string name)
		{
			this.name = name;
		}

		// Token: 0x170023C2 RID: 9154
		// (get) Token: 0x060087F9 RID: 34809 RVA: 0x00002461 File Offset: 0x00000661
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Identifier;
			}
		}

		// Token: 0x170023C3 RID: 9155
		// (get) Token: 0x060087FA RID: 34810 RVA: 0x001CD57B File Offset: 0x001CB77B
		public Identifier Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170023C4 RID: 9156
		// (get) Token: 0x060087FB RID: 34811 RVA: 0x00002105 File Offset: 0x00000305
		public bool IsInclusive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023C5 RID: 9157
		// (get) Token: 0x060087FC RID: 34812 RVA: 0x00002E8B File Offset: 0x0000108B
		public TokenRange Range
		{
			get
			{
				return TokenRange.Null;
			}
		}

		// Token: 0x04004B5C RID: 19292
		private readonly string name;
	}
}
