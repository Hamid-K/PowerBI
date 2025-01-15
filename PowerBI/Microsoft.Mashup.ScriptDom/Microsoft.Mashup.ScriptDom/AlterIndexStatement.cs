using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002A8 RID: 680
	[Serializable]
	internal class AlterIndexStatement : IndexStatement
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x001659CB File Offset: 0x00163BCB
		// (set) Token: 0x060027F4 RID: 10228 RVA: 0x001659D3 File Offset: 0x00163BD3
		public bool All
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x001659DC File Offset: 0x00163BDC
		// (set) Token: 0x060027F6 RID: 10230 RVA: 0x001659E4 File Offset: 0x00163BE4
		public AlterIndexType AlterIndexType
		{
			get
			{
				return this._alterIndexType;
			}
			set
			{
				this._alterIndexType = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x001659ED File Offset: 0x00163BED
		// (set) Token: 0x060027F8 RID: 10232 RVA: 0x001659F5 File Offset: 0x00163BF5
		public PartitionSpecifier Partition
		{
			get
			{
				return this._partition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._partition = value;
			}
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x00165A05 File Offset: 0x00163C05
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x00165A14 File Offset: 0x00163C14
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
			int count = base.IndexOptions.Count;
			while (i < count)
			{
				base.IndexOptions[i].Accept(visitor);
				i++;
			}
			if (this.Partition != null)
			{
				this.Partition.Accept(visitor);
			}
		}

		// Token: 0x04001BBB RID: 7099
		private bool _all;

		// Token: 0x04001BBC RID: 7100
		private AlterIndexType _alterIndexType;

		// Token: 0x04001BBD RID: 7101
		private PartitionSpecifier _partition;
	}
}
