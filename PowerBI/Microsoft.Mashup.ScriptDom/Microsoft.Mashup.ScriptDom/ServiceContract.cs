using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003A4 RID: 932
	[Serializable]
	internal class ServiceContract : TSqlFragment
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x0016BE44 File Offset: 0x0016A044
		// (set) Token: 0x06002E18 RID: 11800 RVA: 0x0016BE4C File Offset: 0x0016A04C
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

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x0016BE5C File Offset: 0x0016A05C
		// (set) Token: 0x06002E1A RID: 11802 RVA: 0x0016BE64 File Offset: 0x0016A064
		public AlterAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				this._action = value;
			}
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x0016BE6D File Offset: 0x0016A06D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x0016BE79 File Offset: 0x0016A079
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D83 RID: 7555
		private Identifier _name;

		// Token: 0x04001D84 RID: 7556
		private AlterAction _action;
	}
}
