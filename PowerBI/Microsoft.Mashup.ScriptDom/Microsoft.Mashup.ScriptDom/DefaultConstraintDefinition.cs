using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000346 RID: 838
	[Serializable]
	internal class DefaultConstraintDefinition : ConstraintDefinition
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x001694A8 File Offset: 0x001676A8
		// (set) Token: 0x06002BB5 RID: 11189 RVA: 0x001694B0 File Offset: 0x001676B0
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

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x001694C0 File Offset: 0x001676C0
		// (set) Token: 0x06002BB7 RID: 11191 RVA: 0x001694C8 File Offset: 0x001676C8
		public bool WithValues
		{
			get
			{
				return this._withValues;
			}
			set
			{
				this._withValues = value;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x001694D1 File Offset: 0x001676D1
		// (set) Token: 0x06002BB9 RID: 11193 RVA: 0x001694D9 File Offset: 0x001676D9
		public Identifier Column
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

		// Token: 0x06002BBA RID: 11194 RVA: 0x001694E9 File Offset: 0x001676E9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x001694F5 File Offset: 0x001676F5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			if (this.Column != null)
			{
				this.Column.Accept(visitor);
			}
		}

		// Token: 0x04001CC7 RID: 7367
		private ScalarExpression _expression;

		// Token: 0x04001CC8 RID: 7368
		private bool _withValues;

		// Token: 0x04001CC9 RID: 7369
		private Identifier _column;
	}
}
