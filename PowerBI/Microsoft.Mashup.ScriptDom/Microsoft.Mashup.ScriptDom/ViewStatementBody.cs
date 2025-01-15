using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001B6 RID: 438
	[Serializable]
	internal abstract class ViewStatementBody : TSqlStatement
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x0015F3A4 File Offset: 0x0015D5A4
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x0015F3AC File Offset: 0x0015D5AC
		public SchemaObjectName SchemaObjectName
		{
			get
			{
				return this._schemaObjectName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._schemaObjectName = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x0015F3BC File Offset: 0x0015D5BC
		public IList<Identifier> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x0015F3C4 File Offset: 0x0015D5C4
		public IList<ViewOption> ViewOptions
		{
			get
			{
				return this._viewOptions;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x0015F3CC File Offset: 0x0015D5CC
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x0015F3D4 File Offset: 0x0015D5D4
		public SelectStatement SelectStatement
		{
			get
			{
				return this._selectStatement;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._selectStatement = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x0015F3E4 File Offset: 0x0015D5E4
		// (set) Token: 0x06002253 RID: 8787 RVA: 0x0015F3EC File Offset: 0x0015D5EC
		public bool WithCheckOption
		{
			get
			{
				return this._withCheckOption;
			}
			set
			{
				this._withCheckOption = value;
			}
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x0015F3F8 File Offset: 0x0015D5F8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SchemaObjectName != null)
			{
				this.SchemaObjectName.Accept(visitor);
			}
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.ViewOptions.Count;
			while (j < count2)
			{
				this.ViewOptions[j].Accept(visitor);
				j++;
			}
			if (this.SelectStatement != null)
			{
				this.SelectStatement.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A1C RID: 6684
		private SchemaObjectName _schemaObjectName;

		// Token: 0x04001A1D RID: 6685
		private List<Identifier> _columns = new List<Identifier>();

		// Token: 0x04001A1E RID: 6686
		private List<ViewOption> _viewOptions = new List<ViewOption>();

		// Token: 0x04001A1F RID: 6687
		private SelectStatement _selectStatement;

		// Token: 0x04001A20 RID: 6688
		private bool _withCheckOption;
	}
}
