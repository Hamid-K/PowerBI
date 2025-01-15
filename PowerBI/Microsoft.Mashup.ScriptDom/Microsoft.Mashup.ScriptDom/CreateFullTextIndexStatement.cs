using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002B0 RID: 688
	[Serializable]
	internal class CreateFullTextIndexStatement : TSqlStatement
	{
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x00165F14 File Offset: 0x00164114
		// (set) Token: 0x06002838 RID: 10296 RVA: 0x00165F1C File Offset: 0x0016411C
		public SchemaObjectName OnName
		{
			get
			{
				return this._onName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._onName = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x00165F2C File Offset: 0x0016412C
		public IList<FullTextIndexColumn> FullTextIndexColumns
		{
			get
			{
				return this._fullTextIndexColumns;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x00165F34 File Offset: 0x00164134
		// (set) Token: 0x0600283B RID: 10299 RVA: 0x00165F3C File Offset: 0x0016413C
		public Identifier KeyIndexName
		{
			get
			{
				return this._keyIndexName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._keyIndexName = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x00165F4C File Offset: 0x0016414C
		// (set) Token: 0x0600283D RID: 10301 RVA: 0x00165F54 File Offset: 0x00164154
		public FullTextCatalogAndFileGroup CatalogAndFileGroup
		{
			get
			{
				return this._catalogAndFileGroup;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._catalogAndFileGroup = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600283E RID: 10302 RVA: 0x00165F64 File Offset: 0x00164164
		public IList<FullTextIndexOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00165F6C File Offset: 0x0016416C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x00165F78 File Offset: 0x00164178
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.OnName != null)
			{
				this.OnName.Accept(visitor);
			}
			int i = 0;
			int count = this.FullTextIndexColumns.Count;
			while (i < count)
			{
				this.FullTextIndexColumns[i].Accept(visitor);
				i++;
			}
			if (this.KeyIndexName != null)
			{
				this.KeyIndexName.Accept(visitor);
			}
			if (this.CatalogAndFileGroup != null)
			{
				this.CatalogAndFileGroup.Accept(visitor);
			}
			int j = 0;
			int count2 = this.Options.Count;
			while (j < count2)
			{
				this.Options[j].Accept(visitor);
				j++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BD3 RID: 7123
		private SchemaObjectName _onName;

		// Token: 0x04001BD4 RID: 7124
		private List<FullTextIndexColumn> _fullTextIndexColumns = new List<FullTextIndexColumn>();

		// Token: 0x04001BD5 RID: 7125
		private Identifier _keyIndexName;

		// Token: 0x04001BD6 RID: 7126
		private FullTextCatalogAndFileGroup _catalogAndFileGroup;

		// Token: 0x04001BD7 RID: 7127
		private List<FullTextIndexOption> _options = new List<FullTextIndexOption>();
	}
}
