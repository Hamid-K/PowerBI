using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200027A RID: 634
	[Serializable]
	internal class CreateXmlSchemaCollectionStatement : TSqlStatement
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x00164879 File Offset: 0x00162A79
		// (set) Token: 0x060026EB RID: 9963 RVA: 0x00164881 File Offset: 0x00162A81
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

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x00164891 File Offset: 0x00162A91
		// (set) Token: 0x060026ED RID: 9965 RVA: 0x00164899 File Offset: 0x00162A99
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

		// Token: 0x060026EE RID: 9966 RVA: 0x001648A9 File Offset: 0x00162AA9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x001648B5 File Offset: 0x00162AB5
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

		// Token: 0x04001B73 RID: 7027
		private SchemaObjectName _name;

		// Token: 0x04001B74 RID: 7028
		private ScalarExpression _expression;
	}
}
