using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200022E RID: 558
	[Serializable]
	internal class CreateRuleStatement : TSqlStatement
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x00162911 File Offset: 0x00160B11
		// (set) Token: 0x06002521 RID: 9505 RVA: 0x00162919 File Offset: 0x00160B19
		public SchemaObjectName Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x00162929 File Offset: 0x00160B29
		// (set) Token: 0x06002523 RID: 9507 RVA: 0x00162931 File Offset: 0x00160B31
		public BooleanExpression Expression
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

		// Token: 0x06002524 RID: 9508 RVA: 0x00162941 File Offset: 0x00160B41
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x0016294D File Offset: 0x00160B4D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AF0 RID: 6896
		private SchemaObjectName _name;

		// Token: 0x04001AF1 RID: 6897
		private BooleanExpression _expression;
	}
}
