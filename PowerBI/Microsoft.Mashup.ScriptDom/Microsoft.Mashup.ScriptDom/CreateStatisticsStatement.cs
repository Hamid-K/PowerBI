using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002D3 RID: 723
	[Serializable]
	internal class CreateStatisticsStatement : TSqlStatement
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x001669A7 File Offset: 0x00164BA7
		// (set) Token: 0x060028EC RID: 10476 RVA: 0x001669AF File Offset: 0x00164BAF
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

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x001669BF File Offset: 0x00164BBF
		// (set) Token: 0x060028EE RID: 10478 RVA: 0x001669C7 File Offset: 0x00164BC7
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

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060028EF RID: 10479 RVA: 0x001669D7 File Offset: 0x00164BD7
		public IList<ColumnReferenceExpression> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x001669DF File Offset: 0x00164BDF
		public IList<StatisticsOption> StatisticsOptions
		{
			get
			{
				return this._statisticsOptions;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x001669E7 File Offset: 0x00164BE7
		// (set) Token: 0x060028F2 RID: 10482 RVA: 0x001669EF File Offset: 0x00164BEF
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

		// Token: 0x060028F3 RID: 10483 RVA: 0x001669FF File Offset: 0x00164BFF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x00166A0C File Offset: 0x00164C0C
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
			int count2 = this.StatisticsOptions.Count;
			while (j < count2)
			{
				this.StatisticsOptions[j].Accept(visitor);
				j++;
			}
			if (this.FilterPredicate != null)
			{
				this.FilterPredicate.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BFF RID: 7167
		private Identifier _name;

		// Token: 0x04001C00 RID: 7168
		private SchemaObjectName _onName;

		// Token: 0x04001C01 RID: 7169
		private List<ColumnReferenceExpression> _columns = new List<ColumnReferenceExpression>();

		// Token: 0x04001C02 RID: 7170
		private List<StatisticsOption> _statisticsOptions = new List<StatisticsOption>();

		// Token: 0x04001C03 RID: 7171
		private BooleanExpression _filterPredicate;
	}
}
