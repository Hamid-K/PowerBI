using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020004A2 RID: 1186
	[Serializable]
	internal class CreateColumnStoreIndexStatement : TSqlStatement
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x0017183F File Offset: 0x0016FA3F
		// (set) Token: 0x060033CB RID: 13259 RVA: 0x00171847 File Offset: 0x0016FA47
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

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x00171857 File Offset: 0x0016FA57
		// (set) Token: 0x060033CD RID: 13261 RVA: 0x0017185F File Offset: 0x0016FA5F
		public bool? Clustered
		{
			get
			{
				return this._clustered;
			}
			set
			{
				this._clustered = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x00171868 File Offset: 0x0016FA68
		// (set) Token: 0x060033CF RID: 13263 RVA: 0x00171870 File Offset: 0x0016FA70
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

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060033D0 RID: 13264 RVA: 0x00171880 File Offset: 0x0016FA80
		public IList<ColumnReferenceExpression> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x00171888 File Offset: 0x0016FA88
		public IList<IndexOption> IndexOptions
		{
			get
			{
				return this._indexOptions;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060033D2 RID: 13266 RVA: 0x00171890 File Offset: 0x0016FA90
		// (set) Token: 0x060033D3 RID: 13267 RVA: 0x00171898 File Offset: 0x0016FA98
		public FileGroupOrPartitionScheme OnFileGroupOrPartitionScheme
		{
			get
			{
				return this._onFileGroupOrPartitionScheme;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._onFileGroupOrPartitionScheme = value;
			}
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x001718A8 File Offset: 0x0016FAA8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x001718B4 File Offset: 0x0016FAB4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.OnName != null)
			{
				this.OnName.Accept(visitor);
			}
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.IndexOptions.Count;
			while (j < count2)
			{
				this.IndexOptions[j].Accept(visitor);
				j++;
			}
			if (this.OnFileGroupOrPartitionScheme != null)
			{
				this.OnFileGroupOrPartitionScheme.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001F0C RID: 7948
		private Identifier _name;

		// Token: 0x04001F0D RID: 7949
		private bool? _clustered;

		// Token: 0x04001F0E RID: 7950
		private SchemaObjectName _onName;

		// Token: 0x04001F0F RID: 7951
		private List<ColumnReferenceExpression> _columns = new List<ColumnReferenceExpression>();

		// Token: 0x04001F10 RID: 7952
		private List<IndexOption> _indexOptions = new List<IndexOption>();

		// Token: 0x04001F11 RID: 7953
		private FileGroupOrPartitionScheme _onFileGroupOrPartitionScheme;
	}
}
