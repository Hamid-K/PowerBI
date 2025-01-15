using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003A0 RID: 928
	[Serializable]
	internal class AlterFullTextCatalogStatement : FullTextCatalogStatement
	{
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06002E03 RID: 11779 RVA: 0x0016BC63 File Offset: 0x00169E63
		// (set) Token: 0x06002E04 RID: 11780 RVA: 0x0016BC6B File Offset: 0x00169E6B
		public AlterFullTextCatalogAction Action
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

		// Token: 0x06002E05 RID: 11781 RVA: 0x0016BC74 File Offset: 0x00169E74
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x0016BC80 File Offset: 0x00169E80
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			int i = 0;
			int count = base.Options.Count;
			while (i < count)
			{
				base.Options[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001D7E RID: 7550
		private AlterFullTextCatalogAction _action;
	}
}
