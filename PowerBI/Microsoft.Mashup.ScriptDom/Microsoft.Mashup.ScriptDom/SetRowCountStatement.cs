using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000307 RID: 775
	[Serializable]
	internal class SetRowCountStatement : TSqlStatement
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06002A0F RID: 10767 RVA: 0x00167BC0 File Offset: 0x00165DC0
		// (set) Token: 0x06002A10 RID: 10768 RVA: 0x00167BC8 File Offset: 0x00165DC8
		public ValueExpression NumberRows
		{
			get
			{
				return this._numberRows;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._numberRows = value;
			}
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x00167BD8 File Offset: 0x00165DD8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x00167BE4 File Offset: 0x00165DE4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.NumberRows != null)
			{
				this.NumberRows.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C4B RID: 7243
		private ValueExpression _numberRows;
	}
}
