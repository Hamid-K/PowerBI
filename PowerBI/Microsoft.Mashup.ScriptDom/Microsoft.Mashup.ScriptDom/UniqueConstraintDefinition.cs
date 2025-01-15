using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000349 RID: 841
	[Serializable]
	internal class UniqueConstraintDefinition : ConstraintDefinition, IFileStreamSpecifier
	{
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x00169660 File Offset: 0x00167860
		// (set) Token: 0x06002BD0 RID: 11216 RVA: 0x00169668 File Offset: 0x00167868
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

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x00169671 File Offset: 0x00167871
		// (set) Token: 0x06002BD2 RID: 11218 RVA: 0x00169679 File Offset: 0x00167879
		public bool IsPrimaryKey
		{
			get
			{
				return this._isPrimaryKey;
			}
			set
			{
				this._isPrimaryKey = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x00169682 File Offset: 0x00167882
		public IList<ColumnWithSortOrder> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x0016968A File Offset: 0x0016788A
		public IList<IndexOption> IndexOptions
		{
			get
			{
				return this._indexOptions;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x00169692 File Offset: 0x00167892
		// (set) Token: 0x06002BD6 RID: 11222 RVA: 0x0016969A File Offset: 0x0016789A
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

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x001696AA File Offset: 0x001678AA
		// (set) Token: 0x06002BD8 RID: 11224 RVA: 0x001696B2 File Offset: 0x001678B2
		public IdentifierOrValueExpression FileStreamOn
		{
			get
			{
				return this._fileStreamOn;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fileStreamOn = value;
			}
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x001696C2 File Offset: 0x001678C2
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x001696D0 File Offset: 0x001678D0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
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
			if (this.FileStreamOn != null)
			{
				this.FileStreamOn.Accept(visitor);
			}
		}

		// Token: 0x04001CD1 RID: 7377
		private bool? _clustered;

		// Token: 0x04001CD2 RID: 7378
		private bool _isPrimaryKey;

		// Token: 0x04001CD3 RID: 7379
		private List<ColumnWithSortOrder> _columns = new List<ColumnWithSortOrder>();

		// Token: 0x04001CD4 RID: 7380
		private List<IndexOption> _indexOptions = new List<IndexOption>();

		// Token: 0x04001CD5 RID: 7381
		private FileGroupOrPartitionScheme _onFileGroupOrPartitionScheme;

		// Token: 0x04001CD6 RID: 7382
		private IdentifierOrValueExpression _fileStreamOn;
	}
}
