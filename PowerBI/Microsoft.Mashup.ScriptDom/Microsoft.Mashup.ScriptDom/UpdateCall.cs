using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000262 RID: 610
	[Serializable]
	internal class UpdateCall : BooleanExpression
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x00164026 File Offset: 0x00162226
		// (set) Token: 0x0600266A RID: 9834 RVA: 0x0016402E File Offset: 0x0016222E
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

		// Token: 0x0600266B RID: 9835 RVA: 0x0016403E File Offset: 0x0016223E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x0016404A File Offset: 0x0016224A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Identifier != null)
			{
				this.Identifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B52 RID: 6994
		private Identifier _identifier;
	}
}
