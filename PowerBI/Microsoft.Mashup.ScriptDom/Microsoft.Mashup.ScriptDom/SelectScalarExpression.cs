using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003C9 RID: 969
	[Serializable]
	internal class SelectScalarExpression : SelectElement
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06002F03 RID: 12035 RVA: 0x0016CF2E File Offset: 0x0016B12E
		// (set) Token: 0x06002F04 RID: 12036 RVA: 0x0016CF36 File Offset: 0x0016B136
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

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06002F05 RID: 12037 RVA: 0x0016CF46 File Offset: 0x0016B146
		// (set) Token: 0x06002F06 RID: 12038 RVA: 0x0016CF4E File Offset: 0x0016B14E
		public IdentifierOrValueExpression ColumnName
		{
			get
			{
				return this._columnName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._columnName = value;
			}
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x0016CF5E File Offset: 0x0016B15E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x0016CF6A File Offset: 0x0016B16A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			if (this.ColumnName != null)
			{
				this.ColumnName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DCC RID: 7628
		private ScalarExpression _expression;

		// Token: 0x04001DCD RID: 7629
		private IdentifierOrValueExpression _columnName;
	}
}
