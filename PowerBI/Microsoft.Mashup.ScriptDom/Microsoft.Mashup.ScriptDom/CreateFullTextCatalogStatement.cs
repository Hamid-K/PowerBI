using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200039F RID: 927
	[Serializable]
	internal class CreateFullTextCatalogStatement : FullTextCatalogStatement, IAuthorization
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x0016BB6D File Offset: 0x00169D6D
		// (set) Token: 0x06002DF9 RID: 11769 RVA: 0x0016BB75 File Offset: 0x00169D75
		public Identifier FileGroup
		{
			get
			{
				return this._fileGroup;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fileGroup = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x0016BB85 File Offset: 0x00169D85
		// (set) Token: 0x06002DFB RID: 11771 RVA: 0x0016BB8D File Offset: 0x00169D8D
		public Literal Path
		{
			get
			{
				return this._path;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._path = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x0016BB9D File Offset: 0x00169D9D
		// (set) Token: 0x06002DFD RID: 11773 RVA: 0x0016BBA5 File Offset: 0x00169DA5
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
			set
			{
				this._isDefault = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x0016BBAE File Offset: 0x00169DAE
		// (set) Token: 0x06002DFF RID: 11775 RVA: 0x0016BBB6 File Offset: 0x00169DB6
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

		// Token: 0x06002E00 RID: 11776 RVA: 0x0016BBC6 File Offset: 0x00169DC6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x0016BBD4 File Offset: 0x00169DD4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			if (this.FileGroup != null)
			{
				this.FileGroup.Accept(visitor);
			}
			if (this.Path != null)
			{
				this.Path.Accept(visitor);
			}
			int i = 0;
			int count = base.Options.Count;
			while (i < count)
			{
				base.Options[i].Accept(visitor);
				i++;
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
		}

		// Token: 0x04001D7A RID: 7546
		private Identifier _fileGroup;

		// Token: 0x04001D7B RID: 7547
		private Literal _path;

		// Token: 0x04001D7C RID: 7548
		private bool _isDefault;

		// Token: 0x04001D7D RID: 7549
		private Identifier _owner;
	}
}
