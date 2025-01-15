using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003F2 RID: 1010
	[Serializable]
	internal class AlterSearchPropertyListStatement : TSqlStatement
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x0016DC68 File Offset: 0x0016BE68
		// (set) Token: 0x06002FEB RID: 12267 RVA: 0x0016DC70 File Offset: 0x0016BE70
		public Identifier Name
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

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x0016DC80 File Offset: 0x0016BE80
		// (set) Token: 0x06002FED RID: 12269 RVA: 0x0016DC88 File Offset: 0x0016BE88
		public SearchPropertyListAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._action = value;
			}
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x0016DC98 File Offset: 0x0016BE98
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x0016DCA4 File Offset: 0x0016BEA4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.Action != null)
			{
				this.Action.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E05 RID: 7685
		private Identifier _name;

		// Token: 0x04001E06 RID: 7686
		private SearchPropertyListAction _action;
	}
}
