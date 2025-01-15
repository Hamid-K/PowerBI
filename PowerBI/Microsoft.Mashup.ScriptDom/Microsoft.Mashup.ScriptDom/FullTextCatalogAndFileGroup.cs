using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002B5 RID: 693
	[Serializable]
	internal class FullTextCatalogAndFileGroup : TSqlFragment
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x0016613E File Offset: 0x0016433E
		// (set) Token: 0x0600285A RID: 10330 RVA: 0x00166146 File Offset: 0x00164346
		public Identifier CatalogName
		{
			get
			{
				return this._catalogName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._catalogName = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x00166156 File Offset: 0x00164356
		// (set) Token: 0x0600285C RID: 10332 RVA: 0x0016615E File Offset: 0x0016435E
		public Identifier FileGroupName
		{
			get
			{
				return this._fileGroupName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fileGroupName = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x0016616E File Offset: 0x0016436E
		// (set) Token: 0x0600285E RID: 10334 RVA: 0x00166176 File Offset: 0x00164376
		public bool FileGroupIsFirst
		{
			get
			{
				return this._fileGroupIsFirst;
			}
			set
			{
				this._fileGroupIsFirst = value;
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x0016617F File Offset: 0x0016437F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x0016618B File Offset: 0x0016438B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.CatalogName != null)
			{
				this.CatalogName.Accept(visitor);
			}
			if (this.FileGroupName != null)
			{
				this.FileGroupName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BDE RID: 7134
		private Identifier _catalogName;

		// Token: 0x04001BDF RID: 7135
		private Identifier _fileGroupName;

		// Token: 0x04001BE0 RID: 7136
		private bool _fileGroupIsFirst;
	}
}
