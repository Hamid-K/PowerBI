using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002D9 RID: 729
	[Serializable]
	internal class SetVariableStatement : TSqlStatement
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06002914 RID: 10516 RVA: 0x00166D1B File Offset: 0x00164F1B
		// (set) Token: 0x06002915 RID: 10517 RVA: 0x00166D23 File Offset: 0x00164F23
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

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x00166D33 File Offset: 0x00164F33
		// (set) Token: 0x06002917 RID: 10519 RVA: 0x00166D3B File Offset: 0x00164F3B
		public SeparatorType SeparatorType
		{
			get
			{
				return this._separatorType;
			}
			set
			{
				this._separatorType = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x00166D44 File Offset: 0x00164F44
		// (set) Token: 0x06002919 RID: 10521 RVA: 0x00166D4C File Offset: 0x00164F4C
		public Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._identifier = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600291A RID: 10522 RVA: 0x00166D5C File Offset: 0x00164F5C
		// (set) Token: 0x0600291B RID: 10523 RVA: 0x00166D64 File Offset: 0x00164F64
		public bool FunctionCallExists
		{
			get
			{
				return this._functionCallExists;
			}
			set
			{
				this._functionCallExists = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x00166D6D File Offset: 0x00164F6D
		public IList<ScalarExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x00166D75 File Offset: 0x00164F75
		// (set) Token: 0x0600291E RID: 10526 RVA: 0x00166D7D File Offset: 0x00164F7D
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

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x00166D8D File Offset: 0x00164F8D
		// (set) Token: 0x06002920 RID: 10528 RVA: 0x00166D95 File Offset: 0x00164F95
		public CursorDefinition CursorDefinition
		{
			get
			{
				return this._cursorDefinition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._cursorDefinition = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x00166DA5 File Offset: 0x00164FA5
		// (set) Token: 0x06002922 RID: 10530 RVA: 0x00166DAD File Offset: 0x00164FAD
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

		// Token: 0x06002923 RID: 10531 RVA: 0x00166DB6 File Offset: 0x00164FB6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x00166DC4 File Offset: 0x00164FC4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Variable != null)
			{
				this.Variable.Accept(visitor);
			}
			if (this.Identifier != null)
			{
				this.Identifier.Accept(visitor);
			}
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			if (this.CursorDefinition != null)
			{
				this.CursorDefinition.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C0D RID: 7181
		private VariableReference _variable;

		// Token: 0x04001C0E RID: 7182
		private SeparatorType _separatorType;

		// Token: 0x04001C0F RID: 7183
		private Identifier _identifier;

		// Token: 0x04001C10 RID: 7184
		private bool _functionCallExists;

		// Token: 0x04001C11 RID: 7185
		private List<ScalarExpression> _parameters = new List<ScalarExpression>();

		// Token: 0x04001C12 RID: 7186
		private ScalarExpression _expression;

		// Token: 0x04001C13 RID: 7187
		private CursorDefinition _cursorDefinition;

		// Token: 0x04001C14 RID: 7188
		private AssignmentKind _assignmentKind;
	}
}
