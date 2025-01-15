using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003CB RID: 971
	[Serializable]
	internal class SelectSetVariable : SelectElement
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x0016CFEC File Offset: 0x0016B1EC
		// (set) Token: 0x06002F10 RID: 12048 RVA: 0x0016CFF4 File Offset: 0x0016B1F4
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

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x0016D004 File Offset: 0x0016B204
		// (set) Token: 0x06002F12 RID: 12050 RVA: 0x0016D00C File Offset: 0x0016B20C
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

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x0016D01C File Offset: 0x0016B21C
		// (set) Token: 0x06002F14 RID: 12052 RVA: 0x0016D024 File Offset: 0x0016B224
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

		// Token: 0x06002F15 RID: 12053 RVA: 0x0016D02D File Offset: 0x0016B22D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x0016D039 File Offset: 0x0016B239
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Variable != null)
			{
				this.Variable.Accept(visitor);
			}
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DCF RID: 7631
		private VariableReference _variable;

		// Token: 0x04001DD0 RID: 7632
		private ScalarExpression _expression;

		// Token: 0x04001DD1 RID: 7633
		private AssignmentKind _assignmentKind;
	}
}
