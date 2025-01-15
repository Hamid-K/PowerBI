using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200027B RID: 635
	[Serializable]
	internal class AlterXmlSchemaCollectionStatement : TSqlStatement
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x001648EE File Offset: 0x00162AEE
		// (set) Token: 0x060026F2 RID: 9970 RVA: 0x001648F6 File Offset: 0x00162AF6
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

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x00164906 File Offset: 0x00162B06
		// (set) Token: 0x060026F4 RID: 9972 RVA: 0x0016490E File Offset: 0x00162B0E
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

		// Token: 0x060026F5 RID: 9973 RVA: 0x0016491E File Offset: 0x00162B1E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0016492A File Offset: 0x00162B2A
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

		// Token: 0x04001B75 RID: 7029
		private SchemaObjectName _name;

		// Token: 0x04001B76 RID: 7030
		private ScalarExpression _expression;
	}
}
