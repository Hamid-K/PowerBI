using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003F1 RID: 1009
	[Serializable]
	internal class CreateSearchPropertyListStatement : TSqlStatement, IAuthorization
	{
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x0016DBBB File Offset: 0x0016BDBB
		// (set) Token: 0x06002FE2 RID: 12258 RVA: 0x0016DBC3 File Offset: 0x0016BDC3
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

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x0016DBD3 File Offset: 0x0016BDD3
		// (set) Token: 0x06002FE4 RID: 12260 RVA: 0x0016DBDB File Offset: 0x0016BDDB
		public MultiPartIdentifier SourceSearchPropertyList
		{
			get
			{
				return this._sourceSearchPropertyList;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._sourceSearchPropertyList = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x0016DBEB File Offset: 0x0016BDEB
		// (set) Token: 0x06002FE6 RID: 12262 RVA: 0x0016DBF3 File Offset: 0x0016BDF3
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

		// Token: 0x06002FE7 RID: 12263 RVA: 0x0016DC03 File Offset: 0x0016BE03
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x0016DC10 File Offset: 0x0016BE10
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.SourceSearchPropertyList != null)
			{
				this.SourceSearchPropertyList.Accept(visitor);
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E02 RID: 7682
		private Identifier _name;

		// Token: 0x04001E03 RID: 7683
		private MultiPartIdentifier _sourceSearchPropertyList;

		// Token: 0x04001E04 RID: 7684
		private Identifier _owner;
	}
}
