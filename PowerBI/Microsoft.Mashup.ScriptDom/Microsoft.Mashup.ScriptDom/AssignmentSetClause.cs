using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200025A RID: 602
	[Serializable]
	internal class AssignmentSetClause : SetClause
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x00163D57 File Offset: 0x00161F57
		// (set) Token: 0x0600263F RID: 9791 RVA: 0x00163D5F File Offset: 0x00161F5F
		public VariableReference Variable
		{
			get
			{
				return this._variable;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._variable = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x00163D6F File Offset: 0x00161F6F
		// (set) Token: 0x06002641 RID: 9793 RVA: 0x00163D77 File Offset: 0x00161F77
		public ColumnReferenceExpression Column
		{
			get
			{
				return this._column;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._column = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x00163D87 File Offset: 0x00161F87
		// (set) Token: 0x06002643 RID: 9795 RVA: 0x00163D8F File Offset: 0x00161F8F
		public ScalarExpression NewValue
		{
			get
			{
				return this._newValue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._newValue = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x00163D9F File Offset: 0x00161F9F
		// (set) Token: 0x06002645 RID: 9797 RVA: 0x00163DA7 File Offset: 0x00161FA7
		public AssignmentKind AssignmentKind
		{
			get
			{
				return this._assignmentKind;
			}
			set
			{
				this._assignmentKind = value;
			}
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00163DB0 File Offset: 0x00161FB0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00163DBC File Offset: 0x00161FBC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Variable != null)
			{
				this.Variable.Accept(visitor);
			}
			if (this.Column != null)
			{
				this.Column.Accept(visitor);
			}
			if (this.NewValue != null)
			{
				this.NewValue.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B47 RID: 6983
		private VariableReference _variable;

		// Token: 0x04001B48 RID: 6984
		private ColumnReferenceExpression _column;

		// Token: 0x04001B49 RID: 6985
		private ScalarExpression _newValue;

		// Token: 0x04001B4A RID: 6986
		private AssignmentKind _assignmentKind;
	}
}
