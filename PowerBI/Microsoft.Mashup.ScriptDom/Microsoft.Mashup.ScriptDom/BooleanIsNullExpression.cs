using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003B2 RID: 946
	[Serializable]
	internal class BooleanIsNullExpression : BooleanExpression
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x0016C591 File Offset: 0x0016A791
		// (set) Token: 0x06002E81 RID: 11905 RVA: 0x0016C599 File Offset: 0x0016A799
		public bool IsNot
		{
			get
			{
				return this._isNot;
			}
			set
			{
				this._isNot = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x0016C5A2 File Offset: 0x0016A7A2
		// (set) Token: 0x06002E83 RID: 11907 RVA: 0x0016C5AA File Offset: 0x0016A7AA
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

		// Token: 0x06002E84 RID: 11908 RVA: 0x0016C5BA File Offset: 0x0016A7BA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x0016C5C6 File Offset: 0x0016A7C6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DA6 RID: 7590
		private bool _isNot;

		// Token: 0x04001DA7 RID: 7591
		private ScalarExpression _expression;
	}
}
