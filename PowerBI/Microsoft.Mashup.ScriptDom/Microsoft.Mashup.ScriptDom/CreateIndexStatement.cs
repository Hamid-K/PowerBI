using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002AB RID: 683
	[Serializable]
	internal class CreateIndexStatement : IndexStatement, IFileStreamSpecifier
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x00165C11 File Offset: 0x00163E11
		// (set) Token: 0x0600280E RID: 10254 RVA: 0x00165C19 File Offset: 0x00163E19
		public bool Translated80SyntaxTo90
		{
			get
			{
				return this._translated80SyntaxTo90;
			}
			set
			{
				this._translated80SyntaxTo90 = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x00165C22 File Offset: 0x00163E22
		// (set) Token: 0x06002810 RID: 10256 RVA: 0x00165C2A File Offset: 0x00163E2A
		public bool Unique
		{
			get
			{
				return this._unique;
			}
			set
			{
				this._unique = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x00165C33 File Offset: 0x00163E33
		// (set) Token: 0x06002812 RID: 10258 RVA: 0x00165C3B File Offset: 0x00163E3B
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

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x00165C44 File Offset: 0x00163E44
		public IList<ColumnWithSortOrder> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x00165C4C File Offset: 0x00163E4C
		public IList<ColumnReferenceExpression> IncludeColumns
		{
			get
			{
				return this._includeColumns;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x00165C54 File Offset: 0x00163E54
		// (set) Token: 0x06002816 RID: 10262 RVA: 0x00165C5C File Offset: 0x00163E5C
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

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x00165C6C File Offset: 0x00163E6C
		// (set) Token: 0x06002818 RID: 10264 RVA: 0x00165C74 File Offset: 0x00163E74
		public BooleanExpression FilterPredicate
		{
			get
			{
				return this._filterPredicate;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._filterPredicate = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x00165C84 File Offset: 0x00163E84
		// (set) Token: 0x0600281A RID: 10266 RVA: 0x00165C8C File Offset: 0x00163E8C
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

		// Token: 0x0600281B RID: 10267 RVA: 0x00165C9C File Offset: 0x00163E9C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00165CA8 File Offset: 0x00163EA8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			if (base.OnName != null)
			{
				base.OnName.Accept(visitor);
			}
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.IncludeColumns.Count;
			while (j < count2)
			{
				this.IncludeColumns[j].Accept(visitor);
				j++;
			}
			int k = 0;
			int count3 = base.IndexOptions.Count;
			while (k < count3)
			{
				base.IndexOptions[k].Accept(visitor);
				k++;
			}
			if (this.OnFileGroupOrPartitionScheme != null)
			{
				this.OnFileGroupOrPartitionScheme.Accept(visitor);
			}
			if (this.FilterPredicate != null)
			{
				this.FilterPredicate.Accept(visitor);
			}
			if (this.FileStreamOn != null)
			{
				this.FileStreamOn.Accept(visitor);
			}
		}

		// Token: 0x04001BC4 RID: 7108
		private bool _translated80SyntaxTo90;

		// Token: 0x04001BC5 RID: 7109
		private bool _unique;

		// Token: 0x04001BC6 RID: 7110
		private bool? _clustered;

		// Token: 0x04001BC7 RID: 7111
		private List<ColumnWithSortOrder> _columns = new List<ColumnWithSortOrder>();

		// Token: 0x04001BC8 RID: 7112
		private List<ColumnReferenceExpression> _includeColumns = new List<ColumnReferenceExpression>();

		// Token: 0x04001BC9 RID: 7113
		private FileGroupOrPartitionScheme _onFileGroupOrPartitionScheme;

		// Token: 0x04001BCA RID: 7114
		private BooleanExpression _filterPredicate;

		// Token: 0x04001BCB RID: 7115
		private IdentifierOrValueExpression _fileStreamOn;
	}
}
