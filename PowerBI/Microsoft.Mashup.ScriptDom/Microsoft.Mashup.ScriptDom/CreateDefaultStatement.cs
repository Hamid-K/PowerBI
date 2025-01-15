using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200022C RID: 556
	[Serializable]
	internal class CreateDefaultStatement : TSqlStatement
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x001627C1 File Offset: 0x001609C1
		// (set) Token: 0x06002517 RID: 9495 RVA: 0x001627C9 File Offset: 0x001609C9
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

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x001627D9 File Offset: 0x001609D9
		// (set) Token: 0x06002519 RID: 9497 RVA: 0x001627E1 File Offset: 0x001609E1
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

		// Token: 0x0600251A RID: 9498 RVA: 0x001627F1 File Offset: 0x001609F1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x001627FD File Offset: 0x001609FD
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

		// Token: 0x04001AEE RID: 6894
		private SchemaObjectName _name;

		// Token: 0x04001AEF RID: 6895
		private ScalarExpression _expression;
	}
}
