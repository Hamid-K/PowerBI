using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200049F RID: 1183
	[Serializable]
	internal class UseFederationStatement : TSqlStatement
	{
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x001716AD File Offset: 0x0016F8AD
		// (set) Token: 0x060033B3 RID: 13235 RVA: 0x001716B5 File Offset: 0x0016F8B5
		public Identifier FederationName
		{
			get
			{
				return this._federationName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._federationName = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x001716C5 File Offset: 0x0016F8C5
		// (set) Token: 0x060033B5 RID: 13237 RVA: 0x001716CD File Offset: 0x0016F8CD
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

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060033B6 RID: 13238 RVA: 0x001716DD File Offset: 0x0016F8DD
		// (set) Token: 0x060033B7 RID: 13239 RVA: 0x001716E5 File Offset: 0x0016F8E5
		public ScalarExpression Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060033B8 RID: 13240 RVA: 0x001716F5 File Offset: 0x0016F8F5
		// (set) Token: 0x060033B9 RID: 13241 RVA: 0x001716FD File Offset: 0x0016F8FD
		public bool Filtering
		{
			get
			{
				return this._filtering;
			}
			set
			{
				this._filtering = value;
			}
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x00171706 File Offset: 0x0016F906
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x00171714 File Offset: 0x0016F914
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.FederationName != null)
			{
				this.FederationName.Accept(visitor);
			}
			if (this.DistributionName != null)
			{
				this.DistributionName.Accept(visitor);
			}
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001F04 RID: 7940
		private Identifier _federationName;

		// Token: 0x04001F05 RID: 7941
		private Identifier _distributionName;

		// Token: 0x04001F06 RID: 7942
		private ScalarExpression _value;

		// Token: 0x04001F07 RID: 7943
		private bool _filtering;
	}
}
