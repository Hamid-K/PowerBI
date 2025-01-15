using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000085 RID: 133
	[Serializable]
	internal class IdentifierOrValueExpression : TSqlFragment
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000B648 File Offset: 0x00009848
		public string Value
		{
			get
			{
				if (this.Identifier != null)
				{
					return this.Identifier.Value;
				}
				if (this.ValueExpression == null)
				{
					return null;
				}
				Literal literal = this.ValueExpression as Literal;
				if (literal != null)
				{
					return literal.Value;
				}
				VariableReference variableReference = this.ValueExpression as VariableReference;
				if (variableReference != null)
				{
					return variableReference.Name;
				}
				GlobalVariableExpression globalVariableExpression = this.ValueExpression as GlobalVariableExpression;
				if (globalVariableExpression != null)
				{
					return globalVariableExpression.Name;
				}
				return null;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000B6B6 File Offset: 0x000098B6
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000B6BE File Offset: 0x000098BE
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000B6CE File Offset: 0x000098CE
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000B6D6 File Offset: 0x000098D6
		public ValueExpression ValueExpression
		{
			get
			{
				return this._valueExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._valueExpression = value;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B6E6 File Offset: 0x000098E6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B6F2 File Offset: 0x000098F2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Identifier != null)
			{
				this.Identifier.Accept(visitor);
			}
			if (this.ValueExpression != null)
			{
				this.ValueExpression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04000338 RID: 824
		private Identifier _identifier;

		// Token: 0x04000339 RID: 825
		private ValueExpression _valueExpression;
	}
}
