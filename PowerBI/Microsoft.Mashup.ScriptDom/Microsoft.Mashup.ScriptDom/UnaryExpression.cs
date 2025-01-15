using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003D2 RID: 978
	[Serializable]
	internal class UnaryExpression : ScalarExpression
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x0016D39C File Offset: 0x0016B59C
		// (set) Token: 0x06002F47 RID: 12103 RVA: 0x0016D3A4 File Offset: 0x0016B5A4
		public UnaryExpressionType UnaryExpressionType
		{
			get
			{
				return this._unaryExpressionType;
			}
			set
			{
				this._unaryExpressionType = value;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x0016D3AD File Offset: 0x0016B5AD
		// (set) Token: 0x06002F49 RID: 12105 RVA: 0x0016D3B5 File Offset: 0x0016B5B5
		public ScalarExpression Expression
		{
			get
			{
				return this._expression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._expression = value;
			}
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x0016D3C5 File Offset: 0x0016B5C5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x0016D3D1 File Offset: 0x0016B5D1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DE1 RID: 7649
		private UnaryExpressionType _unaryExpressionType;

		// Token: 0x04001DE2 RID: 7650
		private ScalarExpression _expression;
	}
}
