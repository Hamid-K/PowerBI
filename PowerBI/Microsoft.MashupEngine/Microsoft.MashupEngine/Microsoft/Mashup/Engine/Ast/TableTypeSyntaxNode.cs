using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001BC8 RID: 7112
	public sealed class TableTypeSyntaxNode : RangeSyntaxNode, ITableTypeExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B186 RID: 45446 RVA: 0x00243C5F File Offset: 0x00241E5F
		public TableTypeSyntaxNode(IExpression rowType, TokenRange range)
			: base(range)
		{
			this.rowType = rowType;
		}

		// Token: 0x17002C90 RID: 11408
		// (get) Token: 0x0600B187 RID: 45447 RVA: 0x000E78B2 File Offset: 0x000E5AB2
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.TableType;
			}
		}

		// Token: 0x17002C91 RID: 11409
		// (get) Token: 0x0600B188 RID: 45448 RVA: 0x00243C6F File Offset: 0x00241E6F
		public IExpression RowType
		{
			get
			{
				return this.rowType;
			}
		}

		// Token: 0x04005B00 RID: 23296
		private IExpression rowType;
	}
}
