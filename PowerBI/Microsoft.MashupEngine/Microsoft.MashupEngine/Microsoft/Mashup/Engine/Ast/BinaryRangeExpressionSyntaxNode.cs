using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BC0 RID: 7104
	public sealed class BinaryRangeExpressionSyntaxNode : RangeSyntaxNode, IRangeExpression, ISyntaxNode
	{
		// Token: 0x0600B16C RID: 45420 RVA: 0x00243B67 File Offset: 0x00241D67
		public BinaryRangeExpressionSyntaxNode(IExpression start, IExpression end, TokenRange range)
			: base(range)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x17002C7F RID: 11391
		// (get) Token: 0x0600B16D RID: 45421 RVA: 0x00243B7E File Offset: 0x00241D7E
		public IExpression Lower
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17002C80 RID: 11392
		// (get) Token: 0x0600B16E RID: 45422 RVA: 0x00243B86 File Offset: 0x00241D86
		public IExpression Upper
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x04005AF5 RID: 23285
		private IExpression start;

		// Token: 0x04005AF6 RID: 23286
		private IExpression end;
	}
}
