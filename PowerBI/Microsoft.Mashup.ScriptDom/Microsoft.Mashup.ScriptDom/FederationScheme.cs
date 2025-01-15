using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000341 RID: 833
	[Serializable]
	internal class FederationScheme : TSqlFragment
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x001692A5 File Offset: 0x001674A5
		// (set) Token: 0x06002B95 RID: 11157 RVA: 0x001692AD File Offset: 0x001674AD
		public Identifier DistributionName
		{
			get
			{
				return this._distributionName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._distributionName = value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x001692BD File Offset: 0x001674BD
		// (set) Token: 0x06002B97 RID: 11159 RVA: 0x001692C5 File Offset: 0x001674C5
		public Identifier ColumnName
		{
			get
			{
				return this._columnName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._columnName = value;
			}
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x001692D5 File Offset: 0x001674D5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x001692E1 File Offset: 0x001674E1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.DistributionName != null)
			{
				this.DistributionName.Accept(visitor);
			}
			if (this.ColumnName != null)
			{
				this.ColumnName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CBE RID: 7358
		private Identifier _distributionName;

		// Token: 0x04001CBF RID: 7359
		private Identifier _columnName;
	}
}
