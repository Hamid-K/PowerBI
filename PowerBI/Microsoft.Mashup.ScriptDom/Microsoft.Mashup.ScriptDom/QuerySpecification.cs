using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003C6 RID: 966
	[Serializable]
	internal class QuerySpecification : QueryExpression
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x0016CD61 File Offset: 0x0016AF61
		// (set) Token: 0x06002EEE RID: 12014 RVA: 0x0016CD69 File Offset: 0x0016AF69
		public UniqueRowFilter UniqueRowFilter
		{
			get
			{
				return this._uniqueRowFilter;
			}
			set
			{
				this._uniqueRowFilter = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x0016CD72 File Offset: 0x0016AF72
		// (set) Token: 0x06002EF0 RID: 12016 RVA: 0x0016CD7A File Offset: 0x0016AF7A
		public TopRowFilter TopRowFilter
		{
			get
			{
				return this._topRowFilter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._topRowFilter = value;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x0016CD8A File Offset: 0x0016AF8A
		public IList<SelectElement> SelectElements
		{
			get
			{
				return this._selectElements;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x0016CD92 File Offset: 0x0016AF92
		// (set) Token: 0x06002EF3 RID: 12019 RVA: 0x0016CD9A File Offset: 0x0016AF9A
		public FromClause FromClause
		{
			get
			{
				return this._fromClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fromClause = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x0016CDAA File Offset: 0x0016AFAA
		// (set) Token: 0x06002EF5 RID: 12021 RVA: 0x0016CDB2 File Offset: 0x0016AFB2
		public WhereClause WhereClause
		{
			get
			{
				return this._whereClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._whereClause = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x0016CDC2 File Offset: 0x0016AFC2
		// (set) Token: 0x06002EF7 RID: 12023 RVA: 0x0016CDCA File Offset: 0x0016AFCA
		public GroupByClause GroupByClause
		{
			get
			{
				return this._groupByClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._groupByClause = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x0016CDDA File Offset: 0x0016AFDA
		// (set) Token: 0x06002EF9 RID: 12025 RVA: 0x0016CDE2 File Offset: 0x0016AFE2
		public HavingClause HavingClause
		{
			get
			{
				return this._havingClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._havingClause = value;
			}
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x0016CDF2 File Offset: 0x0016AFF2
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x0016CE00 File Offset: 0x0016B000
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.TopRowFilter != null)
			{
				this.TopRowFilter.Accept(visitor);
			}
			int i = 0;
			int count = this.SelectElements.Count;
			while (i < count)
			{
				this.SelectElements[i].Accept(visitor);
				i++;
			}
			if (this.FromClause != null)
			{
				this.FromClause.Accept(visitor);
			}
			if (this.WhereClause != null)
			{
				this.WhereClause.Accept(visitor);
			}
			if (this.GroupByClause != null)
			{
				this.GroupByClause.Accept(visitor);
			}
			if (this.HavingClause != null)
			{
				this.HavingClause.Accept(visitor);
			}
		}

		// Token: 0x04001DC4 RID: 7620
		private UniqueRowFilter _uniqueRowFilter;

		// Token: 0x04001DC5 RID: 7621
		private TopRowFilter _topRowFilter;

		// Token: 0x04001DC6 RID: 7622
		private List<SelectElement> _selectElements = new List<SelectElement>();

		// Token: 0x04001DC7 RID: 7623
		private FromClause _fromClause;

		// Token: 0x04001DC8 RID: 7624
		private WhereClause _whereClause;

		// Token: 0x04001DC9 RID: 7625
		private GroupByClause _groupByClause;

		// Token: 0x04001DCA RID: 7626
		private HavingClause _havingClause;
	}
}
