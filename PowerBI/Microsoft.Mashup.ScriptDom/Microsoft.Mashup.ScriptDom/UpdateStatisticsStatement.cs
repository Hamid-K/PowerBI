using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002D4 RID: 724
	[Serializable]
	internal class UpdateStatisticsStatement : TSqlStatement
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x00166ACE File Offset: 0x00164CCE
		// (set) Token: 0x060028F7 RID: 10487 RVA: 0x00166AD6 File Offset: 0x00164CD6
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

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x00166AE6 File Offset: 0x00164CE6
		public IList<Identifier> SubElements
		{
			get
			{
				return this._subElements;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x00166AEE File Offset: 0x00164CEE
		public IList<StatisticsOption> StatisticsOptions
		{
			get
			{
				return this._statisticsOptions;
			}
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x00166AF6 File Offset: 0x00164CF6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x00166B04 File Offset: 0x00164D04
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SchemaObjectName != null)
			{
				this.SchemaObjectName.Accept(visitor);
			}
			int i = 0;
			int count = this.SubElements.Count;
			while (i < count)
			{
				this.SubElements[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.StatisticsOptions.Count;
			while (j < count2)
			{
				this.StatisticsOptions[j].Accept(visitor);
				j++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C04 RID: 7172
		private SchemaObjectName _schemaObjectName;

		// Token: 0x04001C05 RID: 7173
		private List<Identifier> _subElements = new List<Identifier>();

		// Token: 0x04001C06 RID: 7174
		private List<StatisticsOption> _statisticsOptions = new List<StatisticsOption>();
	}
}
