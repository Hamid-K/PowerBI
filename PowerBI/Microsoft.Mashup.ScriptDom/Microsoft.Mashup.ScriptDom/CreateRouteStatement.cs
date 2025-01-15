using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002A2 RID: 674
	[Serializable]
	internal class CreateRouteStatement : RouteStatement, IAuthorization
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x00165702 File Offset: 0x00163902
		// (set) Token: 0x060027D4 RID: 10196 RVA: 0x0016570A File Offset: 0x0016390A
		public Identifier Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._owner = value;
			}
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x0016571A File Offset: 0x0016391A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x00165728 File Offset: 0x00163928
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			int i = 0;
			int count = base.RouteOptions.Count;
			while (i < count)
			{
				base.RouteOptions[i].Accept(visitor);
				i++;
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
		}

		// Token: 0x04001BB2 RID: 7090
		private Identifier _owner;
	}
}
