using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003A8 RID: 936
	[Serializable]
	internal class ComputeFunction : TSqlFragment
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x0016C05A File Offset: 0x0016A25A
		// (set) Token: 0x06002E33 RID: 11827 RVA: 0x0016C062 File Offset: 0x0016A262
		public ComputeFunctionType ComputeFunctionType
		{
			get
			{
				return this._computeFunctionType;
			}
			set
			{
				this._computeFunctionType = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x0016C06B File Offset: 0x0016A26B
		// (set) Token: 0x06002E35 RID: 11829 RVA: 0x0016C073 File Offset: 0x0016A273
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

		// Token: 0x06002E36 RID: 11830 RVA: 0x0016C083 File Offset: 0x0016A283
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x0016C08F File Offset: 0x0016A28F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D8C RID: 7564
		private ComputeFunctionType _computeFunctionType;

		// Token: 0x04001D8D RID: 7565
		private ScalarExpression _expression;
	}
}
